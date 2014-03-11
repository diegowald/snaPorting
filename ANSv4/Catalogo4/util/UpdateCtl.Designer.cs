namespace Catalogo.util
{
    partial class UpdateCtl
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
            this.fraFRAME1 = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.lblSI = new System.Windows.Forms.Label();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.fraFRAME2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgressUpdate = new System.Windows.Forms.Label();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.fraFRAME1.SuspendLayout();
            this.fraFRAME2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fraFRAME1
            // 
            this.fraFRAME1.Controls.Add(this.lblVersion);
            this.fraFRAME1.Controls.Add(this.lblProgress);
            this.fraFRAME1.Controls.Add(this.lblNo);
            this.fraFRAME1.Controls.Add(this.lblSI);
            this.fraFRAME1.Controls.Add(this.lblStep2);
            this.fraFRAME1.Location = new System.Drawing.Point(13, 12);
            this.fraFRAME1.Name = "fraFRAME1";
            this.fraFRAME1.Size = new System.Drawing.Size(438, 140);
            this.fraFRAME1.TabIndex = 0;
            this.fraFRAME1.TabStop = false;
            this.fraFRAME1.Text = "Paso 1 de 2";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(137, 58);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(52, 13);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "lblVersion";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(16, 58);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(58, 13);
            this.lblProgress.TabIndex = 3;
            this.lblProgress.Text = "lblProgress";
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Location = new System.Drawing.Point(19, 114);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(306, 13);
            this.lblNo.TabIndex = 2;
            this.lblNo.Text = "NO hay actualizaciones dispoibles. click en Cancelar para Salir.";
            // 
            // lblSI
            // 
            this.lblSI.Location = new System.Drawing.Point(19, 81);
            this.lblSI.Name = "lblSI";
            this.lblSI.Size = new System.Drawing.Size(375, 33);
            this.lblSI.TabIndex = 1;
            this.lblSI.Text = "Hay na nueva version dispoible, click en Continuar para descargarla o click en Ca" +
    "ncelar para salir.";
            // 
            // lblStep2
            // 
            this.lblStep2.Location = new System.Drawing.Point(16, 27);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(416, 44);
            this.lblStep2.TabIndex = 0;
            this.lblStep2.Text = "Presione click en Continuar para conectar al servidor y verificar actualizaciones" +
    " disponibles...";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(295, 158);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancelar";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(376, 158);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(75, 23);
            this.cmdNext.TabIndex = 2;
            this.cmdNext.Text = "Continuar";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // fraFRAME2
            // 
            this.fraFRAME2.Controls.Add(this.progressBar1);
            this.fraFRAME2.Controls.Add(this.lblProgressUpdate);
            this.fraFRAME2.Controls.Add(this.lblUpdate);
            this.fraFRAME2.Location = new System.Drawing.Point(13, 12);
            this.fraFRAME2.Name = "fraFRAME2";
            this.fraFRAME2.Size = new System.Drawing.Size(438, 140);
            this.fraFRAME2.TabIndex = 3;
            this.fraFRAME2.TabStop = false;
            this.fraFRAME2.Text = "Paso 2 de 2";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 87);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(274, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // lblProgressUpdate
            // 
            this.lblProgressUpdate.AutoSize = true;
            this.lblProgressUpdate.Location = new System.Drawing.Point(10, 49);
            this.lblProgressUpdate.Name = "lblProgressUpdate";
            this.lblProgressUpdate.Size = new System.Drawing.Size(87, 13);
            this.lblProgressUpdate.TabIndex = 1;
            this.lblProgressUpdate.Text = "lblprogressupfate";
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Location = new System.Drawing.Point(7, 20);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(95, 13);
            this.lblUpdate.TabIndex = 0;
            this.lblUpdate.Text = "Descargando... $n";
            // 
            // UpdateCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fraFRAME2);
            this.Controls.Add(this.cmdNext);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.fraFRAME1);
            this.Name = "UpdateCtl";
            this.Size = new System.Drawing.Size(472, 197);
            this.fraFRAME1.ResumeLayout(false);
            this.fraFRAME1.PerformLayout();
            this.fraFRAME2.ResumeLayout(false);
            this.fraFRAME2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fraFRAME1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdNext;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label lblSI;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.GroupBox fraFRAME2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgressUpdate;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.Label lblVersion;
    }
}
