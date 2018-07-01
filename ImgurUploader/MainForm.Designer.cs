namespace ImgurUploader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.resultsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // resultsListView
            // 
            this.resultsListView.BackColor = System.Drawing.Color.Black;
            this.resultsListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.resultsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsListView.ForeColor = System.Drawing.Color.White;
            this.resultsListView.FullRowSelect = true;
            this.resultsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.resultsListView.Location = new System.Drawing.Point(0, 0);
            this.resultsListView.Margin = new System.Windows.Forms.Padding(6);
            this.resultsListView.Name = "resultsListView";
            this.resultsListView.Size = new System.Drawing.Size(284, 161);
            this.resultsListView.TabIndex = 0;
            this.resultsListView.UseCompatibleStateImageBehavior = false;
            this.resultsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            // 
            // progress
            // 
            this.progress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progress.Location = new System.Drawing.Point(0, 138);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(284, 23);
            this.progress.Step = 1;
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progress.TabIndex = 1;
            this.progress.Visible = false;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.resultsListView);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Imgur Upload";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView resultsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ProgressBar progress;
    }
}

