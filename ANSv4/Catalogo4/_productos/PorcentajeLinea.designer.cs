namespace Catalogo._productos
{
    partial class PorcentajeLinea
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PorcentajeLinea));
            this.PnlFondoRojo = new System.Windows.Forms.Panel();
            this.MainPnl = new System.Windows.Forms.Panel();
            this.idLbl = new System.Windows.Forms.Label();
            this.plistView = new System.Windows.Forms.ListView();
            this.pLineaLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pPorcentajeLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pIDLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pNadaLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cboLineas = new System.Windows.Forms.ComboBox();
            this.PorcentajeLineaTxt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.errormessage = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
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
            this.PnlFondoRojo.Size = new System.Drawing.Size(550, 427);
            this.PnlFondoRojo.TabIndex = 0;
            // 
            // MainPnl
            // 
            this.MainPnl.BackColor = System.Drawing.Color.White;
            this.MainPnl.Controls.Add(this.idLbl);
            this.MainPnl.Controls.Add(this.plistView);
            this.MainPnl.Controls.Add(this.cboLineas);
            this.MainPnl.Controls.Add(this.PorcentajeLineaTxt);
            this.MainPnl.Controls.Add(this.label12);
            this.MainPnl.Controls.Add(this.label7);
            this.MainPnl.Controls.Add(this.errormessage);
            this.MainPnl.Controls.Add(this.btnAgregar);
            this.MainPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPnl.Location = new System.Drawing.Point(2, 38);
            this.MainPnl.Name = "MainPnl";
            this.MainPnl.Size = new System.Drawing.Size(546, 387);
            this.MainPnl.TabIndex = 12;
            // 
            // idLbl
            // 
            this.idLbl.AutoSize = true;
            this.idLbl.Location = new System.Drawing.Point(337, 26);
            this.idLbl.Name = "idLbl";
            this.idLbl.Size = new System.Drawing.Size(13, 13);
            this.idLbl.TabIndex = 66;
            this.idLbl.Text = "0";
            this.idLbl.Visible = false;
            // 
            // plistView
            // 
            this.plistView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.plistView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pLineaLv,
            this.pPorcentajeLv,
            this.pIDLv,
            this.pNadaLv});
            this.plistView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plistView.FullRowSelect = true;
            this.plistView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.plistView.HideSelection = false;
            this.plistView.Location = new System.Drawing.Point(15, 72);
            this.plistView.MultiSelect = false;
            this.plistView.Name = "plistView";
            this.plistView.Size = new System.Drawing.Size(521, 293);
            this.plistView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.plistView.TabIndex = 3;
            this.plistView.Tag = "nada";
            this.plistView.UseCompatibleStateImageBehavior = false;
            this.plistView.View = System.Windows.Forms.View.Details;
            this.plistView.DoubleClick += new System.EventHandler(this.plistView_DoubleClick);
            this.plistView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.plistView_KeyDown);
            // 
            // pLineaLv
            // 
            this.pLineaLv.Text = "Línea";
            this.pLineaLv.Width = 260;
            // 
            // pPorcentajeLv
            // 
            this.pPorcentajeLv.Text = "Porcentaje";
            this.pPorcentajeLv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.pPorcentajeLv.Width = 90;
            // 
            // pIDLv
            // 
            this.pIDLv.Text = "ID";
            this.pIDLv.Width = 0;
            // 
            // cboLineas
            // 
            this.cboLineas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cboLineas.FormattingEnabled = true;
            this.cboLineas.Location = new System.Drawing.Point(15, 42);
            this.cboLineas.Name = "cboLineas";
            this.cboLineas.Size = new System.Drawing.Size(197, 21);
            this.cboLineas.TabIndex = 0;
            // 
            // PorcentajeLineaTxt
            // 
            this.PorcentajeLineaTxt.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PorcentajeLineaTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PorcentajeLineaTxt.Location = new System.Drawing.Point(245, 41);
            this.PorcentajeLineaTxt.MaxLength = 5;
            this.PorcentajeLineaTxt.Name = "PorcentajeLineaTxt";
            this.PorcentajeLineaTxt.Size = new System.Drawing.Size(71, 22);
            this.PorcentajeLineaTxt.TabIndex = 1;
            this.PorcentajeLineaTxt.Text = "0,00";
            this.PorcentajeLineaTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PorcentajeLineaTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PorcentajeLineaTxt_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "Línea";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Porcentaje";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(28)))), ((int)(((byte)(25)))));
            this.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAgregar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAgregar.FlatAppearance.BorderSize = 2;
            this.btnAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(461, 35);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 28);
            this.btnAgregar.TabIndex = 2;
            this.btnAgregar.Text = "Cambiar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
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
            this.TopPnl.Size = new System.Drawing.Size(546, 36);
            this.TopPnl.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(445, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Porcentaje aplicado a la Línea . . .";
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
            // 
            // PorcentajeLinea
            // 
            this.AcceptButton = this.btnAgregar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(550, 427);
            this.Controls.Add(this.PnlFondoRojo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "PorcentajeLinea";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulario de Registro de la Aplicación - Catálogo Dígital de Productos - Auto Ná" +
    "utica Sur s.r.l. - v4.0";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.fPorcentajeLinea_Load);
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
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Panel TopPnl;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox PorcentajeLineaTxt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboLineas;
        private System.Windows.Forms.ListView plistView;
        private System.Windows.Forms.ColumnHeader pLineaLv;
        private System.Windows.Forms.ColumnHeader pPorcentajeLv;
        private System.Windows.Forms.ColumnHeader pIDLv;
        private System.Windows.Forms.ColumnHeader pNadaLv;
        private System.Windows.Forms.Label idLbl;



    }
}