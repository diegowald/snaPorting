namespace Catalogo._novedades
{
    partial class ucNovedades
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitC1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.dgvNovedades = new System.Windows.Forms.DataGridView();
            this.splitC1.Panel1.SuspendLayout();
            this.splitC1.Panel2.SuspendLayout();
            this.splitC1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNovedades)).BeginInit();
            this.SuspendLayout();
            // 
            // splitC1
            // 
            this.splitC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitC1.Location = new System.Drawing.Point(0, 0);
            this.splitC1.Name = "splitC1";
            this.splitC1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitC1.Panel1
            // 
            this.splitC1.Panel1.Controls.Add(this.pictureBox1);
            this.splitC1.Panel1.Controls.Add(this.webBrowser2);
            this.splitC1.Panel1.Controls.Add(this.webBrowser1);
            // 
            // splitC1.Panel2
            // 
            this.splitC1.Panel2.Controls.Add(this.dgvNovedades);
            this.splitC1.Size = new System.Drawing.Size(749, 443);
            this.splitC1.SplitterDistance = 321;
            this.splitC1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::Catalogo.Properties.Resources.Nuevo_logo_Auto_nautica_horizontal_original;
            this.pictureBox1.Location = new System.Drawing.Point(249, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(233, 173);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // webBrowser2
            // 
            this.webBrowser2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.webBrowser2.Location = new System.Drawing.Point(503, 80);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.Size = new System.Drawing.Size(243, 219);
            this.webBrowser2.TabIndex = 3;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.webBrowser1.Location = new System.Drawing.Point(0, 88);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(243, 211);
            this.webBrowser1.TabIndex = 0;
            // 
            // dgvNovedades
            // 
            this.dgvNovedades.AllowUserToAddRows = false;
            this.dgvNovedades.AllowUserToDeleteRows = false;
            this.dgvNovedades.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightYellow;
            this.dgvNovedades.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNovedades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNovedades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNovedades.Location = new System.Drawing.Point(0, 0);
            this.dgvNovedades.Name = "dgvNovedades";
            this.dgvNovedades.ReadOnly = true;
            this.dgvNovedades.RowHeadersWidth = 4;
            this.dgvNovedades.Size = new System.Drawing.Size(749, 118);
            this.dgvNovedades.TabIndex = 3;
            this.dgvNovedades.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dgvNovedades.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNovedades_CellFormatting);
            // 
            // ucNovedades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitC1);
            this.Name = "ucNovedades";
            this.Size = new System.Drawing.Size(749, 443);
            this.Load += new System.EventHandler(this.ucNovedades_Load);
            this.splitC1.Panel1.ResumeLayout(false);
            this.splitC1.Panel2.ResumeLayout(false);
            this.splitC1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNovedades)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitC1;
        private System.Windows.Forms.DataGridView dgvNovedades;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.WebBrowser webBrowser2;

    }
}
