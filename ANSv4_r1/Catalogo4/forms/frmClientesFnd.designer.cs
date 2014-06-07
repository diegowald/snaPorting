namespace Catalogo.varios
{
    partial class frmClientesFnd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientesFnd));
            this.PnlFondoRojo = new System.Windows.Forms.Panel();
            this.MainPnl = new System.Windows.Forms.Panel();
            this.plistView = new System.Windows.Forms.ListView();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.errormessage = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.TopPnl = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.PnlFondoRojo.SuspendLayout();
            this.MainPnl.SuspendLayout();
            this.TopPnl.SuspendLayout();
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
            this.PnlFondoRojo.Size = new System.Drawing.Size(612, 427);
            this.PnlFondoRojo.TabIndex = 0;
            // 
            // MainPnl
            // 
            this.MainPnl.BackColor = System.Drawing.Color.White;
            this.MainPnl.Controls.Add(this.plistView);
            this.MainPnl.Controls.Add(this.txtBuscar);
            this.MainPnl.Controls.Add(this.errormessage);
            this.MainPnl.Controls.Add(this.btnBuscar);
            this.MainPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPnl.Location = new System.Drawing.Point(2, 38);
            this.MainPnl.Name = "MainPnl";
            this.MainPnl.Size = new System.Drawing.Size(608, 387);
            this.MainPnl.TabIndex = 12;
            // 
            // plistView
            // 
            this.plistView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.plistView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plistView.Enabled = false;
            this.plistView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plistView.FullRowSelect = true;
            this.plistView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.plistView.HideSelection = false;
            this.plistView.Location = new System.Drawing.Point(10, 72);
            this.plistView.MultiSelect = false;
            this.plistView.Name = "plistView";
            this.plistView.Size = new System.Drawing.Size(588, 293);
            this.plistView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.plistView.TabIndex = 3;
            this.plistView.Tag = "nada";
            this.plistView.UseCompatibleStateImageBehavior = false;
            this.plistView.View = System.Windows.Forms.View.Details;
            this.plistView.SelectedIndexChanged += new System.EventHandler(this.plistView_SelectedIndexChanged);
            this.plistView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.plistView_KeyPress);
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(10, 41);
            this.txtBuscar.MaxLength = 7;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(301, 22);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // errormessage
            // 
            this.errormessage.AutoSize = true;
            this.errormessage.BackColor = System.Drawing.Color.Transparent;
            this.errormessage.ForeColor = System.Drawing.Color.Red;
            this.errormessage.Location = new System.Drawing.Point(12, 367);
            this.errormessage.Name = "errormessage";
            this.errormessage.Size = new System.Drawing.Size(22, 13);
            this.errormessage.TabIndex = 17;
            this.errormessage.Text = ". . .";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(28)))), ((int)(((byte)(25)))));
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBuscar.FlatAppearance.BorderSize = 2;
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(322, 38);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 28);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // TopPnl
            // 
            this.TopPnl.BackColor = System.Drawing.Color.White;
            this.TopPnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TopPnl.Controls.Add(this.label1);
            this.TopPnl.Controls.Add(this.btnCerrar);
            this.TopPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPnl.Location = new System.Drawing.Point(2, 2);
            this.TopPnl.Name = "TopPnl";
            this.TopPnl.Size = new System.Drawing.Size(608, 36);
            this.TopPnl.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Buscar Cliente ...";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Webdings", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnCerrar.Location = new System.Drawing.Point(585, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(20, 25);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "r";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // frmClientesFnd
            // 
            this.AcceptButton = this.btnBuscar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(612, 427);
            this.Controls.Add(this.PnlFondoRojo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmClientesFnd";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario de Registro de la Aplicación - Catálogo Dígital de Productos - Auto Ná" +
    "utica Sur s.r.l. - v4.0";
            this.TopMost = true;
            this.PnlFondoRojo.ResumeLayout(false);
            this.MainPnl.ResumeLayout(false);
            this.MainPnl.PerformLayout();
            this.TopPnl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlFondoRojo;
        private System.Windows.Forms.Panel MainPnl;
        private System.Windows.Forms.Label errormessage;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel TopPnl;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView plistView;



    }
}