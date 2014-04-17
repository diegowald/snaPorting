namespace Catalogo.varios
{
    partial class fLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fLogin));
            this.PnlFondoRojo = new System.Windows.Forms.Panel();
            this.MainPnl = new System.Windows.Forms.Panel();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.errormessage = new System.Windows.Forms.Label();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.chkActualizarClientes = new System.Windows.Forms.CheckBox();
            this.txtPIN = new System.Windows.Forms.TextBox();
            this.lblPIN = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPIN2 = new System.Windows.Forms.TextBox();
            this.TopPnl = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PnlFondoRojo.SuspendLayout();
            this.MainPnl.SuspendLayout();
            this.TopPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.PnlFondoRojo.Size = new System.Drawing.Size(362, 221);
            this.PnlFondoRojo.TabIndex = 0;
            // 
            // MainPnl
            // 
            this.MainPnl.BackColor = System.Drawing.Color.White;
            this.MainPnl.Controls.Add(this.btnNuevo);
            this.MainPnl.Controls.Add(this.errormessage);
            this.MainPnl.Controls.Add(this.pictureBox1);
            this.MainPnl.Controls.Add(this.btnIngresar);
            this.MainPnl.Controls.Add(this.chkActualizarClientes);
            this.MainPnl.Controls.Add(this.txtPIN);
            this.MainPnl.Controls.Add(this.lblPIN);
            this.MainPnl.Controls.Add(this.lblUsuario);
            this.MainPnl.Controls.Add(this.label1);
            this.MainPnl.Controls.Add(this.txtPIN2);
            this.MainPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPnl.Location = new System.Drawing.Point(2, 34);
            this.MainPnl.Name = "MainPnl";
            this.MainPnl.Size = new System.Drawing.Size(358, 185);
            this.MainPnl.TabIndex = 12;
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(28)))), ((int)(((byte)(25)))));
            this.btnNuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNuevo.FlatAppearance.BorderSize = 2;
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(202, 99);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 28);
            this.btnNuevo.TabIndex = 19;
            this.btnNuevo.Text = "Generar";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Visible = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // errormessage
            // 
            this.errormessage.Location = new System.Drawing.Point(15, 138);
            this.errormessage.Name = "errormessage";
            this.errormessage.Size = new System.Drawing.Size(217, 23);
            this.errormessage.TabIndex = 17;
            // 
            // btnIngresar
            // 
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(28)))), ((int)(((byte)(25)))));
            this.btnIngresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnIngresar.Enabled = false;
            this.btnIngresar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnIngresar.FlatAppearance.BorderSize = 2;
            this.btnIngresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresar.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.Location = new System.Drawing.Point(273, 154);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(75, 28);
            this.btnIngresar.TabIndex = 15;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // chkActualizarClientes
            // 
            this.chkActualizarClientes.AutoSize = true;
            this.chkActualizarClientes.Enabled = false;
            this.chkActualizarClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActualizarClientes.Location = new System.Drawing.Point(10, 164);
            this.chkActualizarClientes.Name = "chkActualizarClientes";
            this.chkActualizarClientes.Size = new System.Drawing.Size(124, 19);
            this.chkActualizarClientes.TabIndex = 14;
            this.chkActualizarClientes.Text = "Actualizar clientes";
            this.chkActualizarClientes.UseVisualStyleBackColor = true;
            this.chkActualizarClientes.CheckedChanged += new System.EventHandler(this.chkActualizarClientes_CheckedChanged);
            // 
            // txtPIN
            // 
            this.txtPIN.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPIN.Location = new System.Drawing.Point(41, 101);
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.PasswordChar = '*';
            this.txtPIN.Size = new System.Drawing.Size(152, 22);
            this.txtPIN.TabIndex = 13;
            // 
            // lblPIN
            // 
            this.lblPIN.AutoSize = true;
            this.lblPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPIN.Location = new System.Drawing.Point(38, 83);
            this.lblPIN.Name = "lblPIN";
            this.lblPIN.Size = new System.Drawing.Size(27, 15);
            this.lblPIN.TabIndex = 12;
            this.lblPIN.Text = "PIN";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.Silver;
            this.lblUsuario.Location = new System.Drawing.Point(15, 42);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(20, 16);
            this.lblUsuario.TabIndex = 11;
            this.lblUsuario.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Bienvenido . . .";
            // 
            // txtPIN2
            // 
            this.txtPIN2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtPIN2.Enabled = false;
            this.txtPIN2.Location = new System.Drawing.Point(41, 102);
            this.txtPIN2.Name = "txtPIN2";
            this.txtPIN2.PasswordChar = '*';
            this.txtPIN2.Size = new System.Drawing.Size(152, 20);
            this.txtPIN2.TabIndex = 18;
            this.txtPIN2.Visible = false;
            // 
            // TopPnl
            // 
            this.TopPnl.BackColor = System.Drawing.Color.White;
            this.TopPnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TopPnl.Controls.Add(this.btnCerrar);
            this.TopPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPnl.Location = new System.Drawing.Point(2, 2);
            this.TopPnl.Name = "TopPnl";
            this.TopPnl.Size = new System.Drawing.Size(358, 32);
            this.TopPnl.TabIndex = 13;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Webdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnCerrar.Location = new System.Drawing.Point(335, -2);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 25);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "r";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Catalogo.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(245, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(113, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // fLogin
            // 
            this.AcceptButton = this.btnIngresar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(362, 221);
            this.Controls.Add(this.PnlFondoRojo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fLogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingresar al Catálogo . . .";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.fLogin_Load);
            this.PnlFondoRojo.ResumeLayout(false);
            this.MainPnl.ResumeLayout(false);
            this.MainPnl.PerformLayout();
            this.TopPnl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlFondoRojo;
        private System.Windows.Forms.Panel MainPnl;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label errormessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.CheckBox chkActualizarClientes;
        private System.Windows.Forms.TextBox txtPIN;
        private System.Windows.Forms.Label lblPIN;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPIN2;
        private System.Windows.Forms.Panel TopPnl;
        private System.Windows.Forms.Button btnCerrar;



    }
}