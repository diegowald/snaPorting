namespace Catalogo._novedades
{
    partial class ucTips
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkVer = new System.Windows.Forms.CheckBox();
            this._Label3_1 = new System.Windows.Forms.Label();
            this._Label3_0 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Frame1 = new System.Windows.Forms.Panel();
            this.Frame2 = new System.Windows.Forms.Panel();
            this.lblTip = new System.Windows.Forms.Label();
            this.Frame1.SuspendLayout();
            this.Frame2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkVer
            // 
            this.chkVer.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.chkVer.Checked = true;
            this.chkVer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVer.Cursor = System.Windows.Forms.Cursors.Default;
            this.chkVer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkVer.Location = new System.Drawing.Point(151, 269);
            this.chkVer.Name = "chkVer";
            this.chkVer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkVer.Size = new System.Drawing.Size(127, 16);
            this.chkVer.TabIndex = 10;
            this.chkVer.Text = "Mostrar al inicio";
            this.chkVer.UseVisualStyleBackColor = false;
            this.chkVer.Visible = false;
            // 
            // _Label3_1
            // 
            this._Label3_1.AutoSize = true;
            this._Label3_1.BackColor = System.Drawing.Color.Transparent;
            this._Label3_1.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label3_1.ForeColor = System.Drawing.Color.Red;
            this._Label3_1.Location = new System.Drawing.Point(586, 274);
            this._Label3_1.Name = "_Label3_1";
            this._Label3_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label3_1.Size = new System.Drawing.Size(27, 13);
            this._Label3_1.TabIndex = 9;
            this._Label3_1.Text = "&Salir";
            // 
            // _Label3_0
            // 
            this._Label3_0.AutoSize = true;
            this._Label3_0.BackColor = System.Drawing.Color.Transparent;
            this._Label3_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._Label3_0.ForeColor = System.Drawing.Color.Red;
            this._Label3_0.Location = new System.Drawing.Point(490, 274);
            this._Label3_0.Name = "_Label3_0";
            this._Label3_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._Label3_0.Size = new System.Drawing.Size(51, 13);
            this._Label3_0.TabIndex = 8;
            this._Label3_0.Text = "Siguiente";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.ForeColor = System.Drawing.Color.Red;
            this.Label2.Location = new System.Drawing.Point(140, 90);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(73, 13);
            this.Label2.TabIndex = 7;
            this.Label2.Text = "Sugerencia ...";
            // 
            // Frame1
            // 
            this.Frame1.BackColor = System.Drawing.Color.White;
            this.Frame1.Controls.Add(this.Frame2);
            this.Frame1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Frame1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame1.Location = new System.Drawing.Point(145, 124);
            this.Frame1.Name = "Frame1";
            this.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame1.Size = new System.Drawing.Size(462, 129);
            this.Frame1.TabIndex = 11;
            // 
            // Frame2
            // 
            this.Frame2.BackColor = System.Drawing.Color.White;
            this.Frame2.Controls.Add(this.lblTip);
            this.Frame2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Frame2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Frame2.Location = new System.Drawing.Point(0, 3);
            this.Frame2.Name = "Frame2";
            this.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Frame2.Size = new System.Drawing.Size(462, 129);
            this.Frame2.TabIndex = 4;
            this.Frame2.Text = "Frame1";
            // 
            // lblTip
            // 
            this.lblTip.BackColor = System.Drawing.Color.Transparent;
            this.lblTip.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTip.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTip.ForeColor = System.Drawing.Color.Black;
            this.lblTip.Location = new System.Drawing.Point(12, 3);
            this.lblTip.Name = "lblTip";
            this.lblTip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTip.Size = new System.Drawing.Size(433, 109);
            this.lblTip.TabIndex = 5;
            // 
            // ucTips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.chkVer);
            this.Controls.Add(this._Label3_1);
            this.Controls.Add(this._Label3_0);
            this.Controls.Add(this.Label2);
            this.Name = "ucTips";
            this.Size = new System.Drawing.Size(752, 377);
            this.Frame1.ResumeLayout(false);
            this.Frame2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox chkVer;
        public System.Windows.Forms.Label _Label3_1;
        public System.Windows.Forms.Label _Label3_0;
        public System.Windows.Forms.Label Label2;
        public System.Windows.Forms.Panel Frame1;
        public System.Windows.Forms.Panel Frame2;
        public System.Windows.Forms.Label lblTip;

    }
}
