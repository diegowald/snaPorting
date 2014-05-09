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
            this.errormessage = new System.Windows.Forms.Label();
            this.TopPnl = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pb_a_procesar = new System.Windows.Forms.PictureBox();
            this.pb_en_espera = new System.Windows.Forms.PictureBox();
            this.pb_en_proceso = new System.Windows.Forms.PictureBox();
            this.pb_en_transporte = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PnlFondoRojo.SuspendLayout();
            this.MainPnl.SuspendLayout();
            this.TopPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_a_procesar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_espera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_proceso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_transporte)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlFondoRojo
            // 
            this.PnlFondoRojo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PnlFondoRojo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PnlFondoRojo.Controls.Add(this.MainPnl);
            this.PnlFondoRojo.Controls.Add(this.TopPnl);
            this.PnlFondoRojo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlFondoRojo.Location = new System.Drawing.Point(0, 0);
            this.PnlFondoRojo.Name = "PnlFondoRojo";
            this.PnlFondoRojo.Padding = new System.Windows.Forms.Padding(2);
            this.PnlFondoRojo.Size = new System.Drawing.Size(440, 227);
            this.PnlFondoRojo.TabIndex = 0;
            // 
            // MainPnl
            // 
            this.MainPnl.BackColor = System.Drawing.Color.White;
            this.MainPnl.Controls.Add(this.pb_en_transporte);
            this.MainPnl.Controls.Add(this.pb_en_proceso);
            this.MainPnl.Controls.Add(this.pb_en_espera);
            this.MainPnl.Controls.Add(this.pb_a_procesar);
            this.MainPnl.Controls.Add(this.errormessage);
            this.MainPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPnl.Location = new System.Drawing.Point(2, 38);
            this.MainPnl.Name = "MainPnl";
            this.MainPnl.Size = new System.Drawing.Size(436, 187);
            this.MainPnl.TabIndex = 12;
            // 
            // errormessage
            // 
            this.errormessage.AutoSize = true;
            this.errormessage.BackColor = System.Drawing.Color.Transparent;
            this.errormessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errormessage.ForeColor = System.Drawing.Color.Red;
            this.errormessage.Location = new System.Drawing.Point(10, 117);
            this.errormessage.Name = "errormessage";
            this.errormessage.Size = new System.Drawing.Size(23, 16);
            this.errormessage.TabIndex = 17;
            this.errormessage.Text = ". . .";
            // 
            // TopPnl
            // 
            this.TopPnl.BackColor = System.Drawing.Color.White;
            this.TopPnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TopPnl.Controls.Add(this.button1);
            this.TopPnl.Controls.Add(this.label1);
            this.TopPnl.Controls.Add(this.btnCerrar);
            this.TopPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPnl.Location = new System.Drawing.Point(2, 2);
            this.TopPnl.Name = "TopPnl";
            this.TopPnl.Size = new System.Drawing.Size(436, 36);
            this.TopPnl.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Estado del Pedido n°";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Webdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnCerrar.Location = new System.Drawing.Point(517, 3);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 25);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "r";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // pb_a_procesar
            // 
            this.pb_a_procesar.BackColor = System.Drawing.Color.Transparent;
            this.pb_a_procesar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_a_procesar.Enabled = false;
            this.pb_a_procesar.Image = global::Catalogo.Properties.Resources.a_procesar;
            this.pb_a_procesar.Location = new System.Drawing.Point(7, 23);
            this.pb_a_procesar.Name = "pb_a_procesar";
            this.pb_a_procesar.Size = new System.Drawing.Size(96, 75);
            this.pb_a_procesar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_a_procesar.TabIndex = 19;
            this.pb_a_procesar.TabStop = false;
            // 
            // pb_en_espera
            // 
            this.pb_en_espera.BackColor = System.Drawing.Color.Transparent;
            this.pb_en_espera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_en_espera.Enabled = false;
            this.pb_en_espera.Image = global::Catalogo.Properties.Resources.en_espera;
            this.pb_en_espera.Location = new System.Drawing.Point(109, 23);
            this.pb_en_espera.Name = "pb_en_espera";
            this.pb_en_espera.Size = new System.Drawing.Size(96, 75);
            this.pb_en_espera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_en_espera.TabIndex = 20;
            this.pb_en_espera.TabStop = false;
            // 
            // pb_en_proceso
            // 
            this.pb_en_proceso.BackColor = System.Drawing.Color.Transparent;
            this.pb_en_proceso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_en_proceso.Enabled = false;
            this.pb_en_proceso.Image = global::Catalogo.Properties.Resources.en_proceso;
            this.pb_en_proceso.Location = new System.Drawing.Point(211, 23);
            this.pb_en_proceso.Name = "pb_en_proceso";
            this.pb_en_proceso.Size = new System.Drawing.Size(96, 75);
            this.pb_en_proceso.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_en_proceso.TabIndex = 21;
            this.pb_en_proceso.TabStop = false;
            // 
            // pb_en_transporte
            // 
            this.pb_en_transporte.BackColor = System.Drawing.Color.Transparent;
            this.pb_en_transporte.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pb_en_transporte.Enabled = false;
            this.pb_en_transporte.Image = global::Catalogo.Properties.Resources.en_transporte;
            this.pb_en_transporte.Location = new System.Drawing.Point(313, 23);
            this.pb_en_transporte.Name = "pb_en_transporte";
            this.pb_en_transporte.Size = new System.Drawing.Size(96, 75);
            this.pb_en_transporte.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_en_transporte.TabIndex = 22;
            this.pb_en_transporte.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Webdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.button1.Location = new System.Drawing.Point(413, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 25);
            this.button1.TabIndex = 19;
            this.button1.Text = "r";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // EstadoPedidoMostrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(440, 227);
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
            this.PnlFondoRojo.ResumeLayout(false);
            this.MainPnl.ResumeLayout(false);
            this.MainPnl.PerformLayout();
            this.TopPnl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_a_procesar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_espera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_proceso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_en_transporte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlFondoRojo;
        private System.Windows.Forms.Panel MainPnl;
        private System.Windows.Forms.Label errormessage;
        private System.Windows.Forms.Panel TopPnl;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pb_en_transporte;
        private System.Windows.Forms.PictureBox pb_en_proceso;
        private System.Windows.Forms.PictureBox pb_en_espera;
        private System.Windows.Forms.PictureBox pb_a_procesar;
        private System.Windows.Forms.Button button1;



    }
}