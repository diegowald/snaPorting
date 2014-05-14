namespace Catalogo.varios
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
            this.lblStep2 = new System.Windows.Forms.Label();
            this.lblSI = new System.Windows.Forms.Label();
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
            this.fraFRAME1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.fraFRAME1.Controls.Add(this.lblVersion);
            this.fraFRAME1.Controls.Add(this.lblProgress);
            this.fraFRAME1.Controls.Add(this.lblNo);
            this.fraFRAME1.Controls.Add(this.lblStep2);
            this.fraFRAME1.Controls.Add(this.lblSI);
            this.fraFRAME1.Location = new System.Drawing.Point(10, 3);
            this.fraFRAME1.Name = "fraFRAME1";
            this.fraFRAME1.Size = new System.Drawing.Size(410, 140);
            this.fraFRAME1.TabIndex = 0;
            this.fraFRAME1.TabStop = false;
            this.fraFRAME1.Text = "Paso 1 de 2";
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(25, 71);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(360, 16);
            this.lblVersion.TabIndex = 4;
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(25, 71);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(360, 16);
            this.lblProgress.TabIndex = 3;
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Location = new System.Drawing.Point(19, 33);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(332, 13);
            this.lblNo.TabIndex = 2;
            this.lblNo.Text = "NO hay actualizaciones disponibles. click en Continuar para ingresar.";
            this.lblNo.Visible = false;
            // 
            // lblStep2
            // 
            this.lblStep2.Location = new System.Drawing.Point(10, 20);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(388, 44);
            this.lblStep2.TabIndex = 0;
            this.lblStep2.Text = "Haga click en Continuar para conectar al servidor y verificar actualizaciones dis" +
    "ponibles...";
            // 
            // lblSI
            // 
            this.lblSI.Location = new System.Drawing.Point(19, 94);
            this.lblSI.Name = "lblSI";
            this.lblSI.Size = new System.Drawing.Size(375, 33);
            this.lblSI.TabIndex = 1;
            this.lblSI.Text = "Hay una nueva versión disponible, click en Continuar para descargarla o click en " +
    "Cancelar para salir.";
            this.lblSI.Visible = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(28)))), ((int)(((byte)(25)))));
            this.cmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.cmdCancel.FlatAppearance.BorderSize = 2;
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(264, 149);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 27);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancelar";
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(28)))), ((int)(((byte)(25)))));
            this.cmdNext.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.cmdNext.FlatAppearance.BorderSize = 2;
            this.cmdNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.cmdNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNext.ForeColor = System.Drawing.Color.White;
            this.cmdNext.Location = new System.Drawing.Point(344, 149);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(75, 27);
            this.cmdNext.TabIndex = 2;
            this.cmdNext.Text = "Continuar";
            this.cmdNext.UseVisualStyleBackColor = false;
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // fraFRAME2
            // 
            this.fraFRAME2.Controls.Add(this.progressBar1);
            this.fraFRAME2.Controls.Add(this.lblProgressUpdate);
            this.fraFRAME2.Controls.Add(this.lblUpdate);
            this.fraFRAME2.Location = new System.Drawing.Point(10, 3);
            this.fraFRAME2.Name = "fraFRAME2";
            this.fraFRAME2.Size = new System.Drawing.Size(410, 140);
            this.fraFRAME2.TabIndex = 3;
            this.fraFRAME2.TabStop = false;
            this.fraFRAME2.Text = "Paso 2 de 2";
            this.fraFRAME2.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 104);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // lblProgressUpdate
            // 
            this.lblProgressUpdate.AutoSize = true;
            this.lblProgressUpdate.Location = new System.Drawing.Point(15, 81);
            this.lblProgressUpdate.Name = "lblProgressUpdate";
            this.lblProgressUpdate.Size = new System.Drawing.Size(87, 13);
            this.lblProgressUpdate.TabIndex = 1;
            this.lblProgressUpdate.Text = "lblprogressupfate";
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Location = new System.Drawing.Point(15, 20);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(95, 13);
            this.lblUpdate.TabIndex = 0;
            this.lblUpdate.Text = "Descargando... $n";
            // 
            // UpdateCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cmdNext);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.fraFRAME2);
            this.Controls.Add(this.fraFRAME1);
            this.Name = "UpdateCtl";
            this.Size = new System.Drawing.Size(430, 180);
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
