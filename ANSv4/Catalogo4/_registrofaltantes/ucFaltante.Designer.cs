
namespace Catalogo._registrofaltantes
{

    partial class ucFaltante
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
            this.components = new System.ComponentModel.Container();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ucPnlTop = new System.Windows.Forms.Panel();
            this.btnVer = new System.Windows.Forms.Button();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.CliNPnlMain = new System.Windows.Forms.Panel();
            this.rflistView = new System.Windows.Forms.ListView();
            this.rfFechaLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rfDetalleLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rfIDLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rfCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rfTipoLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rfIdTipoLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rIdClienteLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rEnviadoLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CliNPnlTop = new System.Windows.Forms.Panel();
            this.rfTipoCbo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rfIDLbl = new System.Windows.Forms.Label();
            this.rfAgregarBtn = new System.Windows.Forms.Button();
            this.rfFechaDtp = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.rfNovedadTxt = new System.Windows.Forms.TextBox();
            this.rfDetalleLbl = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.ucPnlTop.SuspendLayout();
            this.CliNPnlMain.SuspendLayout();
            this.CliNPnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(1, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 302);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(775, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel1.Text = ". . .";
            // 
            // ucPnlTop
            // 
            this.ucPnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucPnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ucPnlTop.Controls.Add(this.btnVer);
            this.ucPnlTop.Controls.Add(this.cboCliente);
            this.ucPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucPnlTop.Location = new System.Drawing.Point(0, 0);
            this.ucPnlTop.Name = "ucPnlTop";
            this.ucPnlTop.Size = new System.Drawing.Size(775, 32);
            this.ucPnlTop.TabIndex = 5;
            // 
            // btnVer
            // 
            this.btnVer.BackColor = System.Drawing.Color.Transparent;
            this.btnVer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnVer.Enabled = false;
            this.btnVer.FlatAppearance.BorderSize = 0;
            this.btnVer.Location = new System.Drawing.Point(10, 5);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(75, 23);
            this.btnVer.TabIndex = 5;
            this.btnVer.Text = "Ver";
            this.btnVer.UseVisualStyleBackColor = false;
            this.btnVer.Visible = false;
            // 
            // cboCliente
            // 
            this.cboCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.Location = new System.Drawing.Point(385, 5);
            this.cboCliente.MaxDropDownItems = 16;
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(368, 23);
            this.cboCliente.TabIndex = 3;
            this.cboCliente.SelectedIndexChanged += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
            // 
            // CliNPnlMain
            // 
            this.CliNPnlMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CliNPnlMain.Controls.Add(this.rflistView);
            this.CliNPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CliNPnlMain.Enabled = false;
            this.CliNPnlMain.Location = new System.Drawing.Point(0, 88);
            this.CliNPnlMain.Name = "CliNPnlMain";
            this.CliNPnlMain.Size = new System.Drawing.Size(775, 214);
            this.CliNPnlMain.TabIndex = 8;
            // 
            // rflistView
            // 
            this.rflistView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rflistView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rflistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.rfFechaLv,
            this.rfDetalleLv,
            this.rfIDLv,
            this.rfCliente,
            this.rfTipoLv,
            this.rfIdTipoLv,
            this.rIdClienteLv,
            this.rEnviadoLv});
            this.rflistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rflistView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rflistView.FullRowSelect = true;
            this.rflistView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.rflistView.HideSelection = false;
            this.rflistView.Location = new System.Drawing.Point(0, 0);
            this.rflistView.MultiSelect = false;
            this.rflistView.Name = "rflistView";
            this.rflistView.Size = new System.Drawing.Size(775, 214);
            this.rflistView.SmallImageList = this.imageList1;
            this.rflistView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.rflistView.TabIndex = 5;
            this.rflistView.Tag = "nada";
            this.rflistView.UseCompatibleStateImageBehavior = false;
            this.rflistView.View = System.Windows.Forms.View.Details;
            this.rflistView.DoubleClick += new System.EventHandler(this.rflistView_DoubleClick);
            this.rflistView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rflistView_KeyDown);
            // 
            // rfFechaLv
            // 
            this.rfFechaLv.Text = "Fecha";
            this.rfFechaLv.Width = 80;
            // 
            // rfDetalleLv
            // 
            this.rfDetalleLv.Text = "Producto";
            this.rfDetalleLv.Width = 480;
            // 
            // rfIDLv
            // 
            this.rfIDLv.Text = "ID";
            this.rfIDLv.Width = 0;
            // 
            // rfCliente
            // 
            this.rfCliente.Text = "Cliente";
            this.rfCliente.Width = 220;
            // 
            // rfTipoLv
            // 
            this.rfTipoLv.Text = "Tipo";
            this.rfTipoLv.Width = 40;
            // 
            // rfIdTipoLv
            // 
            this.rfIdTipoLv.Text = "IdTipo";
            this.rfIdTipoLv.Width = 0;
            // 
            // rIdClienteLv
            // 
            this.rIdClienteLv.Text = "IdCliente";
            this.rIdClienteLv.Width = 0;
            // 
            // rEnviadoLv
            // 
            this.rEnviadoLv.Text = "Enviado";
            // 
            // CliNPnlTop
            // 
            this.CliNPnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CliNPnlTop.Controls.Add(this.rfTipoCbo);
            this.CliNPnlTop.Controls.Add(this.label4);
            this.CliNPnlTop.Controls.Add(this.rfIDLbl);
            this.CliNPnlTop.Controls.Add(this.rfAgregarBtn);
            this.CliNPnlTop.Controls.Add(this.rfFechaDtp);
            this.CliNPnlTop.Controls.Add(this.label13);
            this.CliNPnlTop.Controls.Add(this.rfNovedadTxt);
            this.CliNPnlTop.Controls.Add(this.rfDetalleLbl);
            this.CliNPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.CliNPnlTop.Enabled = false;
            this.CliNPnlTop.Location = new System.Drawing.Point(0, 32);
            this.CliNPnlTop.Name = "CliNPnlTop";
            this.CliNPnlTop.Size = new System.Drawing.Size(775, 56);
            this.CliNPnlTop.TabIndex = 7;
            // 
            // rfTipoCbo
            // 
            this.rfTipoCbo.FormattingEnabled = true;
            this.rfTipoCbo.Location = new System.Drawing.Point(10, 27);
            this.rfTipoCbo.Name = "rfTipoCbo";
            this.rfTipoCbo.Size = new System.Drawing.Size(142, 21);
            this.rfTipoCbo.TabIndex = 67;
            this.rfTipoCbo.SelectedIndexChanged += new System.EventHandler(this.rfTipoCbo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 66;
            this.label4.Text = "Tipo";
            // 
            // rfIDLbl
            // 
            this.rfIDLbl.AutoSize = true;
            this.rfIDLbl.Location = new System.Drawing.Point(757, 9);
            this.rfIDLbl.Name = "rfIDLbl";
            this.rfIDLbl.Size = new System.Drawing.Size(13, 13);
            this.rfIDLbl.TabIndex = 65;
            this.rfIDLbl.Text = "0";
            this.rfIDLbl.Visible = false;
            // 
            // rfAgregarBtn
            // 
            this.rfAgregarBtn.BackColor = System.Drawing.Color.Transparent;
            this.rfAgregarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rfAgregarBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rfAgregarBtn.FlatAppearance.BorderSize = 0;
            this.rfAgregarBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.rfAgregarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rfAgregarBtn.Location = new System.Drawing.Point(695, 25);
            this.rfAgregarBtn.Name = "rfAgregarBtn";
            this.rfAgregarBtn.Size = new System.Drawing.Size(75, 25);
            this.rfAgregarBtn.TabIndex = 64;
            this.rfAgregarBtn.Text = "Agregar";
            this.rfAgregarBtn.UseVisualStyleBackColor = false;
            this.rfAgregarBtn.Click += new System.EventHandler(this.rfAgregarBtn_Click);
            // 
            // rfFechaDtp
            // 
            this.rfFechaDtp.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rfFechaDtp.CustomFormat = "dd/MM/yyyy";
            this.rfFechaDtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rfFechaDtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.rfFechaDtp.Location = new System.Drawing.Point(591, 27);
            this.rfFechaDtp.Name = "rfFechaDtp";
            this.rfFechaDtp.Size = new System.Drawing.Size(98, 21);
            this.rfFechaDtp.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(588, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Fecha";
            // 
            // rfNovedadTxt
            // 
            this.rfNovedadTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rfNovedadTxt.Location = new System.Drawing.Point(169, 27);
            this.rfNovedadTxt.MaxLength = 255;
            this.rfNovedadTxt.Name = "rfNovedadTxt";
            this.rfNovedadTxt.Size = new System.Drawing.Size(394, 21);
            this.rfNovedadTxt.TabIndex = 3;
            // 
            // rfDetalleLbl
            // 
            this.rfDetalleLbl.AutoSize = true;
            this.rfDetalleLbl.Location = new System.Drawing.Point(166, 11);
            this.rfDetalleLbl.Name = "rfDetalleLbl";
            this.rfDetalleLbl.Size = new System.Drawing.Size(63, 13);
            this.rfDetalleLbl.TabIndex = 0;
            this.rfDetalleLbl.Text = "Descripción";
            // 
            // ucFaltante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.CliNPnlMain);
            this.Controls.Add(this.CliNPnlTop);
            this.Controls.Add(this.ucPnlTop);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ucFaltante";
            this.Size = new System.Drawing.Size(775, 324);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ucPnlTop.ResumeLayout(false);
            this.CliNPnlMain.ResumeLayout(false);
            this.CliNPnlTop.ResumeLayout(false);
            this.CliNPnlTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel ucPnlTop;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Panel CliNPnlMain;
        private System.Windows.Forms.ListView rflistView;
        private System.Windows.Forms.ColumnHeader rfFechaLv;
        private System.Windows.Forms.ColumnHeader rfDetalleLv;
        private System.Windows.Forms.ColumnHeader rfIDLv;
        private System.Windows.Forms.ColumnHeader rfCliente;
        private System.Windows.Forms.Panel CliNPnlTop;
        private System.Windows.Forms.Label rfIDLbl;
        private System.Windows.Forms.Button rfAgregarBtn;
        private System.Windows.Forms.DateTimePicker rfFechaDtp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox rfNovedadTxt;
        private System.Windows.Forms.Label rfDetalleLbl;
        private System.Windows.Forms.ColumnHeader rfTipoLv;
        private System.Windows.Forms.ComboBox rfTipoCbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader rfIdTipoLv;
        private System.Windows.Forms.ColumnHeader rIdClienteLv;
        private System.Windows.Forms.ColumnHeader rEnviadoLv;
    }
}