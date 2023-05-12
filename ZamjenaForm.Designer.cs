
namespace ZAVRSNI_ZADUZENJENASTAVNIKA
{
    partial class ZamjenaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZamjenaForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Toolbar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Panel();
            this.AddButtonText = new System.Windows.Forms.Label();
            this.NastavnikPickButton = new System.Windows.Forms.Panel();
            this.NastavnikPreviewerText = new System.Windows.Forms.Label();
            this.NastavnikPreviewer = new System.Windows.Forms.Panel();
            this.PredmetPreviewer = new System.Windows.Forms.Panel();
            this.PredmetPreviewerText = new System.Windows.Forms.Label();
            this.PredmetPickButton = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.DatumOdPicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DatumDoPicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Toolbar)).BeginInit();
            this.AddButton.SuspendLayout();
            this.NastavnikPreviewer.SuspendLayout();
            this.PredmetPreviewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(925, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Toolbar
            // 
            this.Toolbar.BackColor = System.Drawing.Color.Transparent;
            this.Toolbar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Toolbar.Cursor = System.Windows.Forms.Cursors.Default;
            this.Toolbar.Image = ((System.Drawing.Image)(resources.GetObject("Toolbar.Image")));
            this.Toolbar.Location = new System.Drawing.Point(-4, 0);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new System.Drawing.Size(966, 44);
            this.Toolbar.TabIndex = 2;
            this.Toolbar.TabStop = false;
            this.Toolbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseDown);
            this.Toolbar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseMove);
            this.Toolbar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Zamjena";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label5.Location = new System.Drawing.Point(47, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 22);
            this.label5.TabIndex = 15;
            this.label5.Text = "Nastavnik";
            // 
            // AddButton
            // 
            this.AddButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddButton.BackgroundImage")));
            this.AddButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AddButton.Controls.Add(this.AddButtonText);
            this.AddButton.Location = new System.Drawing.Point(383, 318);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(193, 61);
            this.AddButton.TabIndex = 20;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // AddButtonText
            // 
            this.AddButtonText.AutoSize = true;
            this.AddButtonText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AddButtonText.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.AddButtonText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.AddButtonText.Location = new System.Drawing.Point(59, 13);
            this.AddButtonText.Name = "AddButtonText";
            this.AddButtonText.Size = new System.Drawing.Size(80, 29);
            this.AddButtonText.TabIndex = 0;
            this.AddButtonText.Text = "Dodaj";
            this.AddButtonText.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // NastavnikPickButton
            // 
            this.NastavnikPickButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NastavnikPickButton.BackgroundImage")));
            this.NastavnikPickButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.NastavnikPickButton.Location = new System.Drawing.Point(422, 65);
            this.NastavnikPickButton.Name = "NastavnikPickButton";
            this.NastavnikPickButton.Size = new System.Drawing.Size(31, 29);
            this.NastavnikPickButton.TabIndex = 22;
            this.NastavnikPickButton.Click += new System.EventHandler(this.NastavnikPickButton_Click);
            // 
            // NastavnikPreviewerText
            // 
            this.NastavnikPreviewerText.AutoSize = true;
            this.NastavnikPreviewerText.ForeColor = System.Drawing.Color.DimGray;
            this.NastavnikPreviewerText.Location = new System.Drawing.Point(8, 14);
            this.NastavnikPreviewerText.Name = "NastavnikPreviewerText";
            this.NastavnikPreviewerText.Size = new System.Drawing.Size(102, 13);
            this.NastavnikPreviewerText.TabIndex = 0;
            this.NastavnikPreviewerText.Text = "Kliknite za pregled...";
            this.NastavnikPreviewerText.Click += new System.EventHandler(this.NastavnikPreviewer_Click);
            // 
            // NastavnikPreviewer
            // 
            this.NastavnikPreviewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.NastavnikPreviewer.Controls.Add(this.NastavnikPreviewerText);
            this.NastavnikPreviewer.Location = new System.Drawing.Point(168, 67);
            this.NastavnikPreviewer.Name = "NastavnikPreviewer";
            this.NastavnikPreviewer.Size = new System.Drawing.Size(248, 96);
            this.NastavnikPreviewer.TabIndex = 23;
            this.NastavnikPreviewer.Click += new System.EventHandler(this.NastavnikPreviewer_Click);
            // 
            // PredmetPreviewer
            // 
            this.PredmetPreviewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.PredmetPreviewer.Controls.Add(this.PredmetPreviewerText);
            this.PredmetPreviewer.Location = new System.Drawing.Point(598, 67);
            this.PredmetPreviewer.Name = "PredmetPreviewer";
            this.PredmetPreviewer.Size = new System.Drawing.Size(248, 96);
            this.PredmetPreviewer.TabIndex = 26;
            this.PredmetPreviewer.Click += new System.EventHandler(this.PredmetPreviewer_Click);
            // 
            // PredmetPreviewerText
            // 
            this.PredmetPreviewerText.AutoSize = true;
            this.PredmetPreviewerText.ForeColor = System.Drawing.Color.DimGray;
            this.PredmetPreviewerText.Location = new System.Drawing.Point(8, 14);
            this.PredmetPreviewerText.Name = "PredmetPreviewerText";
            this.PredmetPreviewerText.Size = new System.Drawing.Size(102, 13);
            this.PredmetPreviewerText.TabIndex = 0;
            this.PredmetPreviewerText.Text = "Kliknite za pregled...";
            this.PredmetPreviewerText.Click += new System.EventHandler(this.PredmetPreviewer_Click);
            // 
            // PredmetPickButton
            // 
            this.PredmetPickButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PredmetPickButton.BackgroundImage")));
            this.PredmetPickButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PredmetPickButton.Location = new System.Drawing.Point(852, 65);
            this.PredmetPickButton.Name = "PredmetPickButton";
            this.PredmetPickButton.Size = new System.Drawing.Size(31, 29);
            this.PredmetPickButton.TabIndex = 25;
            this.PredmetPickButton.Click += new System.EventHandler(this.PredmetPickButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label3.Location = new System.Drawing.Point(488, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 22);
            this.label3.TabIndex = 24;
            this.label3.Text = "Predmet";
            // 
            // DatumOdPicker
            // 
            this.DatumOdPicker.CalendarFont = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatumOdPicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.DatumOdPicker.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.DatumOdPicker.Font = new System.Drawing.Font("Arial", 14.25F);
            this.DatumOdPicker.Location = new System.Drawing.Point(168, 236);
            this.DatumOdPicker.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.DatumOdPicker.Name = "DatumOdPicker";
            this.DatumOdPicker.Size = new System.Drawing.Size(200, 29);
            this.DatumOdPicker.TabIndex = 27;
            this.DatumOdPicker.Value = new System.DateTime(2023, 5, 10, 23, 51, 14, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label4.Location = new System.Drawing.Point(104, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 22);
            this.label4.TabIndex = 28;
            this.label4.Text = "Od:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label6.Location = new System.Drawing.Point(532, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 22);
            this.label6.TabIndex = 29;
            this.label6.Text = "Do:";
            // 
            // DatumDoPicker
            // 
            this.DatumDoPicker.CalendarFont = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatumDoPicker.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.DatumDoPicker.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.DatumDoPicker.Font = new System.Drawing.Font("Arial", 14.25F);
            this.DatumDoPicker.Location = new System.Drawing.Point(598, 236);
            this.DatumDoPicker.MinDate = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.DatumDoPicker.Name = "DatumDoPicker";
            this.DatumDoPicker.Size = new System.Drawing.Size(200, 29);
            this.DatumDoPicker.TabIndex = 30;
            this.DatumDoPicker.Value = new System.DateTime(2023, 5, 10, 23, 51, 14, 0);
            // 
            // ZamjenaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(957, 447);
            this.Controls.Add(this.DatumDoPicker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DatumOdPicker);
            this.Controls.Add(this.PredmetPreviewer);
            this.Controls.Add(this.NastavnikPreviewer);
            this.Controls.Add(this.PredmetPickButton);
            this.Controls.Add(this.NastavnikPickButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Toolbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ZamjenaForm";
            this.Text = "NastavnikForm";
            this.Load += new System.EventHandler(this.ZamjenaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Toolbar)).EndInit();
            this.AddButton.ResumeLayout(false);
            this.AddButton.PerformLayout();
            this.NastavnikPreviewer.ResumeLayout(false);
            this.NastavnikPreviewer.PerformLayout();
            this.PredmetPreviewer.ResumeLayout(false);
            this.PredmetPreviewer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox Toolbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel AddButton;
        private System.Windows.Forms.Label AddButtonText;
        private System.Windows.Forms.Panel NastavnikPickButton;
        private System.Windows.Forms.Label NastavnikPreviewerText;
        private System.Windows.Forms.Panel NastavnikPreviewer;
        private System.Windows.Forms.Panel PredmetPreviewer;
        private System.Windows.Forms.Label PredmetPreviewerText;
        private System.Windows.Forms.Panel PredmetPickButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker DatumOdPicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker DatumDoPicker;
    }
}