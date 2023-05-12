
namespace ZAVRSNI_ZADUZENJENASTAVNIKA
{
    partial class NastavnikForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NastavnikForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Toolbar = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ImeUnos = new System.Windows.Forms.TextBox();
            this.PrezimeUnos = new System.Windows.Forms.TextBox();
            this.KategorijaUnos = new System.Windows.Forms.TextBox();
            this.OibUnos = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Panel();
            this.AddButtonText = new System.Windows.Forms.Label();
            this.MoreButton = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Toolbar)).BeginInit();
            this.AddButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(514, 0);
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
            this.label1.Size = new System.Drawing.Size(77, 23);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nastavnik";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Toolbar_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label2.Location = new System.Drawing.Point(136, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 22);
            this.label2.TabIndex = 12;
            this.label2.Text = "Ime";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label3.Location = new System.Drawing.Point(94, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 22);
            this.label3.TabIndex = 13;
            this.label3.Text = "Prezime";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label4.Location = new System.Drawing.Point(74, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 22);
            this.label4.TabIndex = 14;
            this.label4.Text = "Kategorija";
            this.label4.DoubleClick += new System.EventHandler(this.KategorijaUnos_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label5.Location = new System.Drawing.Point(135, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 22);
            this.label5.TabIndex = 15;
            this.label5.Text = "OIB";
            // 
            // ImeUnos
            // 
            this.ImeUnos.Font = new System.Drawing.Font("Arial", 14.25F);
            this.ImeUnos.ForeColor = System.Drawing.Color.Gray;
            this.ImeUnos.Location = new System.Drawing.Point(199, 78);
            this.ImeUnos.Name = "ImeUnos";
            this.ImeUnos.Size = new System.Drawing.Size(248, 29);
            this.ImeUnos.TabIndex = 16;
            // 
            // PrezimeUnos
            // 
            this.PrezimeUnos.Font = new System.Drawing.Font("Arial", 14.25F);
            this.PrezimeUnos.ForeColor = System.Drawing.Color.Gray;
            this.PrezimeUnos.Location = new System.Drawing.Point(199, 113);
            this.PrezimeUnos.Name = "PrezimeUnos";
            this.PrezimeUnos.Size = new System.Drawing.Size(248, 29);
            this.PrezimeUnos.TabIndex = 17;
            // 
            // KategorijaUnos
            // 
            this.KategorijaUnos.Font = new System.Drawing.Font("Arial", 14.25F);
            this.KategorijaUnos.ForeColor = System.Drawing.Color.Gray;
            this.KategorijaUnos.Location = new System.Drawing.Point(199, 148);
            this.KategorijaUnos.Name = "KategorijaUnos";
            this.KategorijaUnos.Size = new System.Drawing.Size(248, 29);
            this.KategorijaUnos.TabIndex = 18;
            this.KategorijaUnos.DoubleClick += new System.EventHandler(this.KategorijaUnos_DoubleClick);
            // 
            // OibUnos
            // 
            this.OibUnos.Font = new System.Drawing.Font("Arial", 14.25F);
            this.OibUnos.ForeColor = System.Drawing.Color.Gray;
            this.OibUnos.Location = new System.Drawing.Point(199, 183);
            this.OibUnos.Name = "OibUnos";
            this.OibUnos.Size = new System.Drawing.Size(248, 29);
            this.OibUnos.TabIndex = 19;
            // 
            // AddButton
            // 
            this.AddButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddButton.BackgroundImage")));
            this.AddButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AddButton.Controls.Add(this.AddButtonText);
            this.AddButton.Location = new System.Drawing.Point(170, 253);
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
            // MoreButton
            // 
            this.MoreButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MoreButton.BackgroundImage")));
            this.MoreButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MoreButton.Location = new System.Drawing.Point(452, 146);
            this.MoreButton.Name = "MoreButton";
            this.MoreButton.Size = new System.Drawing.Size(31, 29);
            this.MoreButton.TabIndex = 21;
            this.MoreButton.Click += new System.EventHandler(this.MoreButton_Click);
            // 
            // NastavnikForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(546, 382);
            this.Controls.Add(this.MoreButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.OibUnos);
            this.Controls.Add(this.KategorijaUnos);
            this.Controls.Add(this.PrezimeUnos);
            this.Controls.Add(this.ImeUnos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Toolbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NastavnikForm";
            this.Text = "NastavnikForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Toolbar)).EndInit();
            this.AddButton.ResumeLayout(false);
            this.AddButton.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox Toolbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox ImeUnos;
        public System.Windows.Forms.TextBox PrezimeUnos;
        public System.Windows.Forms.TextBox KategorijaUnos;
        public System.Windows.Forms.TextBox OibUnos;
        public System.Windows.Forms.Panel AddButton;
        private System.Windows.Forms.Label AddButtonText;
        public System.Windows.Forms.Panel MoreButton;
    }
}