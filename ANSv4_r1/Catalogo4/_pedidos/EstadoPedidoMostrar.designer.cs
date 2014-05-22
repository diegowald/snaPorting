namespace Catalogo._pedidos
{
    partial class EstadoPedidoMostrar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EstadoPedidoMostrar));
            this.PnlFondoRojo = new System.Windows.Forms.Panel();
            this.MainPnl = new System.Windows.Forms.Panel();
            this.l_en_transporte = new System.Windows.Forms.Label();
            this.l_para_despacho = new System.Windows.Forms.Label();
            this.l_en_proceso = new System.Windows.Forms.Label();
            this.l_en_espera = new System.Windows.Forms.Label();
            this.gris_en_transporte = new System.Windows.Forms.Panel();
            this.l_a_procesar = new System.Windows.Forms.Label();
            this.gris_para_despacho = new System.Windows.Forms.Panel();
            this.gris_en_proceso = new System.Windows.Forms.Panel();
            this.gris_en_espera = new System.Windows.Forms.Panel();
            this.gris_a_procesar = new System.Windows.Forms.Panel();
            this.errormessage = new System.Windows.Forms.Label();
            this.pb_para_despacho = new System.Windows.Forms.PictureBox();
            this.pb_a_procesar = new System.Windows.Forms.PictureBox();
            this.pb_en_transporte = new System.Windows.Forms.PictureBox();
            this.pb_en_proceso = new System.Windows.Forms.PictureBox();
            this.pb_en_espera = new System.Windows.Forms.PictureBox();
            this.TopPnl = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.PnlFondoRojo.SuspendLayout();
            this.MainPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_para_despacho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_a_procesar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_transporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_proceso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_espera)).BeginInit();
            this.TopPnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlFondoRojo
            // 
            this.PnlFondoRojo.BackColor = System.Drawing.Color.DarkGray;
            this.PnlFondoRojo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PnlFondoRojo.Controls.Add(this.MainPnl);
            this.PnlFondoRojo.Controls.Add(this.TopPnl);
            this.PnlFondoRojo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlFondoRojo.Location = new System.Drawing.Point(0, 0);
            this.PnlFondoRojo.Name = "PnlFondoRojo";
            this.PnlFondoRojo.Padding = new System.Windows.Forms.Padding(2);
            this.PnlFondoRojo.Size = new System.Drawing.Size(535, 243);
            this.PnlFondoRojo.TabIndex = 0;
            // 
            // MainPnl
            // 
            this.MainPnl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MainPnl.Controls.Add(this.l_en_transporte);
            this.MainPnl.Controls.Add(this.l_para_despacho);
            this.MainPnl.Controls.Add(this.l_en_proceso);
            this.MainPnl.Controls.Add(this.l_en_espera);
            this.MainPnl.Controls.Add(this.gris_en_transporte);
            this.MainPnl.Controls.Add(this.l_a_procesar);
            this.MainPnl.Controls.Add(this.gris_para_despacho);
            this.MainPnl.Controls.Add(this.gris_en_proceso);
            this.MainPnl.Controls.Add(this.gris_en_espera);
            this.MainPnl.Controls.Add(this.gris_a_procesar);
            this.MainPnl.Controls.Add(this.errormessage);
            this.MainPnl.Controls.Add(this.pb_para_despacho);
            this.MainPnl.Controls.Add(this.pb_a_procesar);
            this.MainPnl.Controls.Add(this.pb_en_transporte);
            this.MainPnl.Controls.Add(this.pb_en_proceso);
            this.MainPnl.Controls.Add(this.pb_en_espera);
            this.MainPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPnl.Location = new System.Drawing.Point(2, 35);
            this.MainPnl.Name = "MainPnl";
            this.MainPnl.Size = new System.Drawing.Size(531, 206);
            this.MainPnl.TabIndex = 12;
            // 
            // l_en_transporte
            // 
            this.l_en_transporte.AutoSize = true;
            this.l_en_transporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_en_transporte.ForeColor = System.Drawing.Color.Red;
            this.l_en_transporte.Location = new System.Drawing.Point(429, 3);
            this.l_en_transporte.Name = "l_en_transporte";
            this.l_en_transporte.Size = new System.Drawing.Size(86, 16);
            this.l_en_transporte.TabIndex = 33;
            this.l_en_transporte.Text = "en transporte";
            this.l_en_transporte.Visible = false;
            // 
            // l_para_despacho
            // 
            this.l_para_despacho.AutoSize = true;
            this.l_para_despacho.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_para_despacho.ForeColor = System.Drawing.Color.Red;
            this.l_para_despacho.Location = new System.Drawing.Point(320, 3);
            this.l_para_despacho.Name = "l_para_despacho";
            this.l_para_despacho.Size = new System.Drawing.Size(100, 16);
            this.l_para_despacho.TabIndex = 32;
            this.l_para_despacho.Text = "para despacho";
            this.l_para_despacho.Visible = false;
            // 
            // l_en_proceso
            // 
            this.l_en_proceso.AutoSize = true;
            this.l_en_proceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_en_proceso.ForeColor = System.Drawing.Color.Red;
            this.l_en_proceso.Location = new System.Drawing.Point(231, 3);
            this.l_en_proceso.Name = "l_en_proceso";
            this.l_en_proceso.Size = new System.Drawing.Size(76, 16);
            this.l_en_proceso.TabIndex = 31;
            this.l_en_proceso.Text = "en proceso";
            this.l_en_proceso.Visible = false;
            // 
            // l_en_espera
            // 
            this.l_en_espera.AutoSize = true;
            this.l_en_espera.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_en_espera.ForeColor = System.Drawing.Color.Red;
            this.l_en_espera.Location = new System.Drawing.Point(133, 3);
            this.l_en_espera.Name = "l_en_espera";
            this.l_en_espera.Size = new System.Drawing.Size(69, 16);
            this.l_en_espera.TabIndex = 30;
            this.l_en_espera.Text = "en espera";
            this.l_en_espera.Visible = false;
            // 
            // gris_en_transporte
            // 
            this.gris_en_transporte.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gris_en_transporte.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gris_en_transporte.BackgroundImage")));
            this.gris_en_transporte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gris_en_transporte.Enabled = false;
            this.gris_en_transporte.Location = new System.Drawing.Point(425, 22);
            this.gris_en_transporte.Name = "gris_en_transporte";
            this.gris_en_transporte.Size = new System.Drawing.Size(95, 92);
            this.gris_en_transporte.TabIndex = 29;
            // 
            // l_a_procesar
            // 
            this.l_a_procesar.AutoSize = true;
            this.l_a_procesar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l_a_procesar.ForeColor = System.Drawing.Color.Red;
            this.l_a_procesar.Location = new System.Drawing.Point(29, 3);
            this.l_a_procesar.Name = "l_a_procesar";
            this.l_a_procesar.Size = new System.Drawing.Size(73, 16);
            this.l_a_procesar.TabIndex = 11;
            this.l_a_procesar.Text = "a procesar";
            this.l_a_procesar.Visible = false;
            // 
            // gris_para_despacho
            // 
            this.gris_para_despacho.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gris_para_despacho.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gris_para_despacho.BackgroundImage")));
            this.gris_para_despacho.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gris_para_despacho.Enabled = false;
            this.gris_para_despacho.Location = new System.Drawing.Point(323, 22);
            this.gris_para_despacho.Name = "gris_para_despacho";
            this.gris_para_despacho.Size = new System.Drawing.Size(95, 92);
            this.gris_para_despacho.TabIndex = 28;
            // 
            // gris_en_proceso
            // 
            this.gris_en_proceso.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gris_en_proceso.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gris_en_proceso.BackgroundImage")));
            this.gris_en_proceso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gris_en_proceso.Enabled = false;
            this.gris_en_proceso.Location = new System.Drawing.Point(222, 22);
            this.gris_en_proceso.Name = "gris_en_proceso";
            this.gris_en_proceso.Size = new System.Drawing.Size(95, 92);
            this.gris_en_proceso.TabIndex = 27;
            // 
            // gris_en_espera
            // 
            this.gris_en_espera.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gris_en_espera.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gris_en_espera.BackgroundImage")));
            this.gris_en_espera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gris_en_espera.Enabled = false;
            this.gris_en_espera.Location = new System.Drawing.Point(120, 22);
            this.gris_en_espera.Name = "gris_en_espera";
            this.gris_en_espera.Size = new System.Drawing.Size(95, 92);
            this.gris_en_espera.TabIndex = 26;
            // 
            // gris_a_procesar
            // 
            this.gris_a_procesar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gris_a_procesar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gris_a_procesar.BackgroundImage")));
            this.gris_a_procesar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gris_a_procesar.Enabled = false;
            this.gris_a_procesar.Location = new System.Drawing.Point(18, 22);
            this.gris_a_procesar.Name = "gris_a_procesar";
            this.gris_a_procesar.Size = new System.Drawing.Size(95, 92);
            this.gris_a_procesar.TabIndex = 25;
            // 
            // errormessage
            // 
            this.errormessage.AutoSize = true;
            this.errormessage.BackColor = System.Drawing.Color.Transparent;
            this.errormessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errormessage.ForeColor = System.Drawing.Color.Red;
            this.errormessage.Location = new System.Drawing.Point(15, 143);
            this.errormessage.Name = "errormessage";
            this.errormessage.Size = new System.Drawing.Size(23, 16);
            this.errormessage.TabIndex = 17;
            this.errormessage.Text = ". . .";
            // 
            // pb_para_despacho
            // 
            this.pb_para_despacho.BackColor = System.Drawing.Color.Transparent;
            this.pb_para_despacho.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_para_despacho.Enabled = false;
            this.pb_para_despacho.Image = ((System.Drawing.Image)(resources.GetObject("pb_para_despacho.Image")));
            this.pb_para_despacho.Location = new System.Drawing.Point(323, 22);
            this.pb_para_despacho.Name = "pb_para_despacho";
            this.pb_para_despacho.Size = new System.Drawing.Size(95, 92);
            this.pb_para_despacho.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_para_despacho.TabIndex = 24;
            this.pb_para_despacho.TabStop = false;
            // 
            // pb_a_procesar
            // 
            this.pb_a_procesar.BackColor = System.Drawing.Color.Transparent;
            this.pb_a_procesar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_a_procesar.Enabled = false;
            this.pb_a_procesar.Image = ((System.Drawing.Image)(resources.GetObject("pb_a_procesar.Image")));
            this.pb_a_procesar.Location = new System.Drawing.Point(17, 22);
            this.pb_a_procesar.Name = "pb_a_procesar";
            this.pb_a_procesar.Size = new System.Drawing.Size(96, 92);
            this.pb_a_procesar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_a_procesar.TabIndex = 23;
            this.pb_a_procesar.TabStop = false;
            // 
            // pb_en_transporte
            // 
            this.pb_en_transporte.BackColor = System.Drawing.Color.Transparent;
            this.pb_en_transporte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_en_transporte.Enabled = false;
            this.pb_en_transporte.Image = ((System.Drawing.Image)(resources.GetObject("pb_en_transporte.Image")));
            this.pb_en_transporte.Location = new System.Drawing.Point(425, 22);
            this.pb_en_transporte.Name = "pb_en_transporte";
            this.pb_en_transporte.Size = new System.Drawing.Size(95, 92);
            this.pb_en_transporte.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_en_transporte.TabIndex = 22;
            this.pb_en_transporte.TabStop = false;
            // 
            // pb_en_proceso
            // 
            this.pb_en_proceso.BackColor = System.Drawing.Color.Transparent;
            this.pb_en_proceso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_en_proceso.Enabled = false;
            this.pb_en_proceso.Image = ((System.Drawing.Image)(resources.GetObject("pb_en_proceso.Image")));
            this.pb_en_proceso.Location = new System.Drawing.Point(222, 22);
            this.pb_en_proceso.Name = "pb_en_proceso";
            this.pb_en_proceso.Size = new System.Drawing.Size(95, 92);
            this.pb_en_proceso.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_en_proceso.TabIndex = 21;
            this.pb_en_proceso.TabStop = false;
            // 
            // pb_en_espera
            // 
            this.pb_en_espera.BackColor = System.Drawing.Color.Transparent;
            this.pb_en_espera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_en_espera.Enabled = false;
            this.pb_en_espera.Image = ((System.Drawing.Image)(resources.GetObject("pb_en_espera.Image")));
            this.pb_en_espera.Location = new System.Drawing.Point(120, 22);
            this.pb_en_espera.Name = "pb_en_espera";
            this.pb_en_espera.Size = new System.Drawing.Size(95, 92);
            this.pb_en_espera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_en_espera.TabIndex = 20;
            this.pb_en_espera.TabStop = false;
            // 
            // TopPnl
            // 
            this.TopPnl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TopPnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TopPnl.Controls.Add(this.btnCerrar);
            this.TopPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPnl.Location = new System.Drawing.Point(2, 2);
            this.TopPnl.Name = "TopPnl";
            this.TopPnl.Size = new System.Drawing.Size(531, 33);
            this.TopPnl.TabIndex = 13;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Webdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnCerrar.Location = new System.Drawing.Point(508, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 25);
            this.btnCerrar.TabIndex = 19;
            this.btnCerrar.Text = "r";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // EstadoPedidoMostrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(535, 243);
            this.Controls.Add(this.PnlFondoRojo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "EstadoPedidoMostrar";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario de Registro de la Aplicación - Catálogo Dígital de Productos - Auto Ná" +
    "utica Sur s.r.l. - v4.0";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.EstadoPedidoMostrar_Load);
            this.PnlFondoRojo.ResumeLayout(false);
            this.MainPnl.ResumeLayout(false);
            this.MainPnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_para_despacho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_a_procesar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_transporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_proceso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_espera)).EndInit();
            this.TopPnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlFondoRojo;
        private System.Windows.Forms.Panel MainPnl;
        private System.Windows.Forms.Label errormessage;
        private System.Windows.Forms.Panel TopPnl;
        private System.Windows.Forms.Label l_a_procesar;
        private System.Windows.Forms.PictureBox pb_en_transporte;
        private System.Windows.Forms.PictureBox pb_en_proceso;
        private System.Windows.Forms.PictureBox pb_en_espera;
        private System.Windows.Forms.PictureBox pb_a_procesar;
        private System.Windows.Forms.Panel gris_en_transporte;
        private System.Windows.Forms.Panel gris_para_despacho;
        private System.Windows.Forms.Panel gris_en_proceso;
        private System.Windows.Forms.Panel gris_en_espera;
        private System.Windows.Forms.Panel gris_a_procesar;
        private System.Windows.Forms.PictureBox pb_para_despacho;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label l_en_transporte;
        private System.Windows.Forms.Label l_para_despacho;
        private System.Windows.Forms.Label l_en_proceso;
        private System.Windows.Forms.Label l_en_espera;



    }
}