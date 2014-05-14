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
            this.components = new System.ComponentModel.Container();
            this.lblSiguiente = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblSiguiente
            // 
            this.lblSiguiente.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSiguiente.BackColor = System.Drawing.Color.Transparent;
            this.lblSiguiente.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSiguiente.Location = new System.Drawing.Point(31, 20);
            this.lblSiguiente.Name = "lblSiguiente";
            this.lblSiguiente.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSiguiente.Size = new System.Drawing.Size(51, 13);
            this.lblSiguiente.TabIndex = 8;
            this.lblSiguiente.Text = "Siguiente";
            this.lblSiguiente.Visible = false;
            this.lblSiguiente.Click += new System.EventHandler(this.lblSiguiente_Click);
            // 
            // lblTip
            // 
            this.lblTip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTip.BackColor = System.Drawing.Color.Transparent;
            this.lblTip.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTip.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTip.ForeColor = System.Drawing.Color.Black;
            this.lblTip.Location = new System.Drawing.Point(34, 33);
            this.lblTip.Name = "lblTip";
            this.lblTip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTip.Size = new System.Drawing.Size(433, 86);
            this.lblTip.TabIndex = 5;
            this.lblTip.Click += new System.EventHandler(this.lblTip_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tag = "stop";
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucTips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.lblSiguiente);
            this.Name = "ucTips";
            this.Size = new System.Drawing.Size(470, 119);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblSiguiente;
        public System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Timer timer1;

    }
}
