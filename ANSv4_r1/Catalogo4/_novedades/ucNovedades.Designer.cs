﻿namespace Catalogo._novedades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucNovedades));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitC1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.dgvNovedades = new System.Windows.Forms.DataGridView();
            this.splitC1.Panel1.SuspendLayout();
            this.splitC1.Panel2.SuspendLayout();
            this.splitC1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNovedades)).BeginInit();
            this.SuspendLayout();
            // 
            // splitC1
            // 
            this.splitC1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitC1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitC1.Location = new System.Drawing.Point(0, 0);
            this.splitC1.Name = "splitC1";
            // 
            // splitC1.Panel1
            // 
            this.splitC1.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitC1.Panel1.Controls.Add(this.pictureBox);
            this.splitC1.Panel1.Controls.Add(this.webBrowser);
            // 
            // splitC1.Panel2
            // 
            this.splitC1.Panel2.Controls.Add(this.dgvNovedades);
            this.splitC1.Size = new System.Drawing.Size(749, 443);
            this.splitC1.SplitterDistance = 633;
            this.splitC1.TabIndex = 0;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(57, 68);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(509, 268);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.webBrowser.Location = new System.Drawing.Point(178, 89);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(243, 219);
            this.webBrowser.TabIndex = 3;
            // 
            // dgvNovedades
            // 
            this.dgvNovedades.AllowUserToAddRows = false;
            this.dgvNovedades.AllowUserToDeleteRows = false;
            this.dgvNovedades.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            this.dgvNovedades.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvNovedades.BackgroundColor = System.Drawing.Color.White;
            this.dgvNovedades.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNovedades.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvNovedades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNovedades.ColumnHeadersVisible = false;
            this.dgvNovedades.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNovedades.Location = new System.Drawing.Point(0, 0);
            this.dgvNovedades.MultiSelect = false;
            this.dgvNovedades.Name = "dgvNovedades";
            this.dgvNovedades.ReadOnly = true;
            this.dgvNovedades.RowHeadersVisible = false;
            this.dgvNovedades.RowHeadersWidth = 4;
            this.dgvNovedades.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvNovedades.Size = new System.Drawing.Size(112, 443);
            this.dgvNovedades.TabIndex = 4;
            this.dgvNovedades.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNovedades_CellFormatting);
            this.dgvNovedades.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNovedades_RowEnter);
            // 
            // ucNovedades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(209)))), ((int)(((byte)(210)))));
            this.Controls.Add(this.splitC1);
            this.Name = "ucNovedades";
            this.Size = new System.Drawing.Size(749, 443);
            this.Load += new System.EventHandler(this.ucNovedades_Load);
            this.splitC1.Panel1.ResumeLayout(false);
            this.splitC1.Panel2.ResumeLayout(false);
            this.splitC1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNovedades)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitC1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.DataGridView dgvNovedades;

    }
}
