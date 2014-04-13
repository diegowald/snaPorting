﻿namespace Catalogo
{
    partial class fReporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fReporte));
            this.panel1 = new System.Windows.Forms.Panel();
            this.crViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.TopPnl = new System.Windows.Forms.Panel();
            this.btnEnviarMail = new System.Windows.Forms.Button();
            this.btnVerPDF = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.TopPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.crViewer1);
            this.panel1.Controls.Add(this.TopPnl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(1024, 600);
            this.panel1.TabIndex = 0;
            // 
            // crViewer1
            // 
            this.crViewer1.ActiveViewIndex = -1;
            this.crViewer1.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.crViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crViewer1.DisplayBackgroundEdge = false;
            this.crViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewer1.EnableRefresh = false;
            this.crViewer1.EnableToolTips = false;
            this.crViewer1.Location = new System.Drawing.Point(2, 66);
            this.crViewer1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.crViewer1.Name = "crViewer1";
            this.crViewer1.ShowCloseButton = false;
            this.crViewer1.ShowCopyButton = false;
            this.crViewer1.ShowExportButton = false;
            this.crViewer1.ShowGotoPageButton = false;
            this.crViewer1.ShowGroupTreeButton = false;
            this.crViewer1.ShowLogo = false;
            this.crViewer1.ShowPageNavigateButtons = false;
            this.crViewer1.ShowParameterPanelButton = false;
            this.crViewer1.ShowRefreshButton = false;
            this.crViewer1.ShowTextSearchButton = false;
            this.crViewer1.Size = new System.Drawing.Size(1020, 532);
            this.crViewer1.TabIndex = 14;
            this.crViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crViewer1.ToolPanelWidth = 0;
            // 
            // TopPnl
            // 
            this.TopPnl.BackColor = System.Drawing.Color.White;
            this.TopPnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TopPnl.Controls.Add(this.btnEnviarMail);
            this.TopPnl.Controls.Add(this.btnVerPDF);
            this.TopPnl.Controls.Add(this.btnCerrar);
            this.TopPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPnl.Location = new System.Drawing.Point(2, 2);
            this.TopPnl.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.TopPnl.Name = "TopPnl";
            this.TopPnl.Size = new System.Drawing.Size(1020, 64);
            this.TopPnl.TabIndex = 13;
            // 
            // btnEnviarMail
            // 
            this.btnEnviarMail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(28)))), ((int)(((byte)(25)))));
            this.btnEnviarMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEnviarMail.Enabled = false;
            this.btnEnviarMail.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEnviarMail.FlatAppearance.BorderSize = 2;
            this.btnEnviarMail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnEnviarMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnviarMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarMail.ForeColor = System.Drawing.Color.White;
            this.btnEnviarMail.Location = new System.Drawing.Point(135, 24);
            this.btnEnviarMail.Name = "btnEnviarMail";
            this.btnEnviarMail.Size = new System.Drawing.Size(113, 33);
            this.btnEnviarMail.TabIndex = 5;
            this.btnEnviarMail.Text = "Enviar por Email";
            this.btnEnviarMail.UseVisualStyleBackColor = false;
            // 
            // btnVerPDF
            // 
            this.btnVerPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(28)))), ((int)(((byte)(25)))));
            this.btnVerPDF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnVerPDF.Enabled = false;
            this.btnVerPDF.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnVerPDF.FlatAppearance.BorderSize = 2;
            this.btnVerPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnVerPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerPDF.ForeColor = System.Drawing.Color.White;
            this.btnVerPDF.Location = new System.Drawing.Point(16, 24);
            this.btnVerPDF.Name = "btnVerPDF";
            this.btnVerPDF.Size = new System.Drawing.Size(113, 33);
            this.btnVerPDF.TabIndex = 4;
            this.btnVerPDF.Text = "Obtener PDF";
            this.btnVerPDF.UseVisualStyleBackColor = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Webdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnCerrar.Location = new System.Drawing.Point(997, -2);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 25);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "r";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // fReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fReporte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ver . . .";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.fReporte_Load);
            this.panel1.ResumeLayout(false);
            this.TopPnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel TopPnl;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnEnviarMail;
        private System.Windows.Forms.Button btnVerPDF;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewer1;



    }
}