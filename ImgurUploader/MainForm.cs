namespace ImgurUploader {
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();

            DragDrop += OnDrop;
            DragEnter += OnDragEventHandler;
            resultsListView.KeyDown += OnResultsListViewOnKeyDown;
        }

        private async void OnResultsListViewOnKeyDown(object s, KeyEventArgs e) {
            if (!e.Control) {
                // we're only gonna reply to ctrl keys
                return;
            }

            if (e.KeyCode == Keys.V) {
                if (!(Clipboard.GetData(DataFormats.Bitmap) is Image img)) {
                    // TODO: error handling
                    return;
                }

                using (var ms = new MemoryStream()) {
                    using (img) img.Save(ms, ImageFormat.Png);
                    ms.Position = 0; // reset it to the beginning for reading
                    await Add(ms, "clipboard");
                }
            }

            if (e.KeyCode == Keys.C) {
                var list = resultsListView.SelectedItems.Cast<ListViewItem>()
                    .Select(item => item.SubItems[1].Text).ToList();
                if (list.Count == 0) {
                    return;
                }

                Clipboard.Clear();
                Clipboard.SetText(string.Join(Environment.NewLine, list));
            }
        }

        private async void OnDrop(object s, DragEventArgs e) {
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files) {
                using (var fs = new FileStream(file, FileMode.Open)) {
                    await Add(fs, Path.GetFileName(file));
                }
            }
        }

        private static void OnDragEventHandler(object s, DragEventArgs e) {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        private async Task Add(Stream data, string name) {
            var indicator = new Progress<int>(i => progress.Value = i);
            progress.Value = 0;
            progress.Visible = true;

            var resp = await ImgurUploader.Upload(data, indicator);
            if (resp == null) {
                // TODO: error handling
                MessageBox.Show($"Cannot upload: '{name}'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                progress.Visible = false;
                return;
            }

            resultsListView.Items.Add(new ListViewItem(new[] {name, resp.Link}));
            resultsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            progress.Visible = false;
        }
    }

    public class StreamProgressContent : HttpContent {
        private readonly Stream input;
        private readonly IProgress<int> progress;

        public StreamProgressContent(Stream stream, IProgress<int> progress) {
            input = stream;
            this.progress = progress;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context) {
            var buf = new byte[4096];
            long total = 0;
            while (true) {
                var read = await input.ReadAsync(buf, 0, buf.Length);
                if (read == 0) break;
                await stream.WriteAsync(buf, 0, read);

                total += read;
                progress.Report((int) (100 * total / input.Length));
            }
        }

        protected override void Dispose(bool disposing) {
            if (disposing) input.Dispose();
            base.Dispose(disposing);
        }

        protected override bool TryComputeLength(out long length) {
            length = input.Length;
            return true;
        }
    }

    public static class ImgurUploader {
        private const string endpoint = "https://api.imgur.com/3/image";
        private static readonly HttpClient client = new HttpClient();

        static ImgurUploader() {
            const string key = "b3e46dc25aa969b";
            client.DefaultRequestHeaders.Add("Authorization", $"Client-ID {key}");
        }

        public static async Task<Result> Upload(Stream stream, IProgress<int> progress) {
            try {
                using (var content = new StreamProgressContent(stream, progress))
                using (var resp = await client.PostAsync(endpoint, content)) {
                    var body = await resp.Content.ReadAsStringAsync();
                    var imgur = body.FromJson<ImgurResponse>();
                    // TODO: error handling
                    return imgur.success ? new Result {Link = imgur.data.link} : null;
                }
            }
            catch (HttpRequestException) {
                // TODO: error handling
                return null;
            }
        }

        public class Result {
            public string Link { get; set; }
        }

        internal class Data {
            public string id { get; set; }
            public object description { get; set; }
            public int datetime { get; set; }
            public string deletehash { get; set; }
            public string name { get; set; }
            public string link { get; set; }
        }

        internal class ImgurResponse {
            public Data data { get; set; }
            public bool success { get; set; }
            public int status { get; set; }
        }
    }
}