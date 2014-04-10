namespace Catalogo
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
            this.crViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEnviarMail = new System.Windows.Forms.Button();
            this.btnVerPDF = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // crViewer1
            // 
            this.crViewer1.ActiveViewIndex = -1;
            this.crViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewer1.Location = new System.Drawing.Point(0, 45);
            this.crViewer1.Name = "crViewer1";
            this.crViewer1.ReuseParameterValuesOnRefresh = true;
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
            this.crViewer1.Size = new System.Drawing.Size(679, 396);
            this.crViewer1.TabIndex = 0;
            this.crViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnEnviarMail);
            this.panel1.Controls.Add(this.btnVerPDF);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(679, 45);
            this.panel1.TabIndex = 1;
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
            this.btnEnviarMail.ForeColor = System.Drawing.Color.White;
            this.btnEnviarMail.Location = new System.Drawing.Point(174, 11);
            this.btnEnviarMail.Name = "btnEnviarMail";
            this.btnEnviarMail.Size = new System.Drawing.Size(156, 23);
            this.btnEnviarMail.TabIndex = 3;
            this.btnEnviarMail.Text = "Enviar por Email";
            this.btnEnviarMail.UseVisualStyleBackColor = false;
            this.btnEnviarMail.Click += new System.EventHandler(this.btnEnviarMail_Click);
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
            this.btnVerPDF.ForeColor = System.Drawing.Color.White;
            this.btnVerPDF.Location = new System.Drawing.Point(12, 11);
            this.btnVerPDF.Name = "btnVerPDF";
            this.btnVerPDF.Size = new System.Drawing.Size(156, 23);
            this.btnVerPDF.TabIndex = 2;
            this.btnVerPDF.Text = "Obtener PDF";
            this.btnVerPDF.UseVisualStyleBackColor = false;
            this.btnVerPDF.Click += new System.EventHandler(this.btnVerPDF_Click);
            // 
            // fReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 441);
            this.Controls.Add(this.crViewer1);
            this.Controls.Add(this.panel1);
            this.Name = "fReporte";
            this.Text = "fReporte";
            this.Load += new System.EventHandler(this.fReporte_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEnviarMail;
        private System.Windows.Forms.Button btnVerPDF;
    }
}