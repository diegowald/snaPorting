
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
            this.rTabRegistroFaltante = new System.Windows.Forms.TabPage();
            this.CliNPnlMain = new System.Windows.Forms.Panel();
            this.RFlistView = new System.Windows.Forms.ListView();
            this.rfFechaLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rfDetalleLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rfIDLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rfCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CliNPnlTop = new System.Windows.Forms.Panel();
            this.CliNidLbl = new System.Windows.Forms.Label();
            this.CliNAgregarBtn = new System.Windows.Forms.Button();
            this.CliNFechaDtp = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.CliNNovedadTxt = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.rTabsRegFaltantes = new System.Windows.Forms.TabControl();
            this.ucPnlTop = new System.Windows.Forms.Panel();
            this.btnVer = new System.Windows.Forms.Button();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.rTabRegistroFaltante.SuspendLayout();
            this.CliNPnlMain.SuspendLayout();
            this.CliNPnlTop.SuspendLayout();
            this.rTabsRegFaltantes.SuspendLayout();
            this.ucPnlTop.SuspendLayout();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
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
            // rTabRegistroFaltante
            // 
            this.rTabRegistroFaltante.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rTabRegistroFaltante.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rTabRegistroFaltante.Controls.Add(this.CliNPnlMain);
            this.rTabRegistroFaltante.Controls.Add(this.CliNPnlTop);
            this.rTabRegistroFaltante.Location = new System.Drawing.Point(4, 27);
            this.rTabRegistroFaltante.Name = "rTabRegistroFaltante";
            this.rTabRegistroFaltante.Size = new System.Drawing.Size(784, 395);
            this.rTabRegistroFaltante.TabIndex = 6;
            this.rTabRegistroFaltante.Text = "Registro de Faltantes";
            // 
            // CliNPnlMain
            // 
            this.CliNPnlMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CliNPnlMain.Controls.Add(this.RFlistView);
            this.CliNPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CliNPnlMain.Enabled = false;
            this.CliNPnlMain.Location = new System.Drawing.Point(0, 56);
            this.CliNPnlMain.Name = "CliNPnlMain";
            this.CliNPnlMain.Size = new System.Drawing.Size(784, 339);
            this.CliNPnlMain.TabIndex = 6;
            // 
            // RFlistView
            // 
            this.RFlistView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.RFlistView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RFlistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.rfFechaLv,
            this.rfDetalleLv,
            this.rfIDLv,
            this.rfCliente});
            this.RFlistView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RFlistView.FullRowSelect = true;
            this.RFlistView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.RFlistView.HideSelection = false;
            this.RFlistView.Location = new System.Drawing.Point(6, 6);
            this.RFlistView.MultiSelect = false;
            this.RFlistView.Name = "RFlistView";
            this.RFlistView.Size = new System.Drawing.Size(764, 317);
            this.RFlistView.SmallImageList = this.imageList1;
            this.RFlistView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.RFlistView.TabIndex = 5;
            this.RFlistView.Tag = "nada";
            this.RFlistView.UseCompatibleStateImageBehavior = false;
            this.RFlistView.View = System.Windows.Forms.View.Details;
            this.RFlistView.DoubleClick += new System.EventHandler(this.RFlistView_DoubleClick);
            this.RFlistView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RFlistView_KeyDown);
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
            this.rfCliente.Width = 280;
            // 
            // CliNPnlTop
            // 
            this.CliNPnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CliNPnlTop.Controls.Add(this.CliNidLbl);
            this.CliNPnlTop.Controls.Add(this.CliNAgregarBtn);
            this.CliNPnlTop.Controls.Add(this.CliNFechaDtp);
            this.CliNPnlTop.Controls.Add(this.label13);
            this.CliNPnlTop.Controls.Add(this.CliNNovedadTxt);
            this.CliNPnlTop.Controls.Add(this.label14);
            this.CliNPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.CliNPnlTop.Enabled = false;
            this.CliNPnlTop.Location = new System.Drawing.Point(0, 0);
            this.CliNPnlTop.Name = "CliNPnlTop";
            this.CliNPnlTop.Size = new System.Drawing.Size(784, 56);
            this.CliNPnlTop.TabIndex = 5;
            // 
            // CliNidLbl
            // 
            this.CliNidLbl.AutoSize = true;
            this.CliNidLbl.Location = new System.Drawing.Point(613, 9);
            this.CliNidLbl.Name = "CliNidLbl";
            this.CliNidLbl.Size = new System.Drawing.Size(14, 15);
            this.CliNidLbl.TabIndex = 65;
            this.CliNidLbl.Text = "0";
            this.CliNidLbl.Visible = false;
            // 
            // CliNAgregarBtn
            // 
            this.CliNAgregarBtn.BackColor = System.Drawing.Color.Transparent;
            this.CliNAgregarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CliNAgregarBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CliNAgregarBtn.FlatAppearance.BorderSize = 0;
            this.CliNAgregarBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.CliNAgregarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CliNAgregarBtn.Location = new System.Drawing.Point(695, 25);
            this.CliNAgregarBtn.Name = "CliNAgregarBtn";
            this.CliNAgregarBtn.Size = new System.Drawing.Size(75, 25);
            this.CliNAgregarBtn.TabIndex = 64;
            this.CliNAgregarBtn.Text = "Agregar";
            this.CliNAgregarBtn.UseVisualStyleBackColor = false;
            this.CliNAgregarBtn.Click += new System.EventHandler(this.CliNAgregarBtn_Click);
            // 
            // CliNFechaDtp
            // 
            this.CliNFechaDtp.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CliNFechaDtp.CustomFormat = "dd/MM/yyyy";
            this.CliNFechaDtp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CliNFechaDtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CliNFechaDtp.Location = new System.Drawing.Point(569, 29);
            this.CliNFechaDtp.Name = "CliNFechaDtp";
            this.CliNFechaDtp.Size = new System.Drawing.Size(120, 21);
            this.CliNFechaDtp.TabIndex = 17;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(566, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 15);
            this.label13.TabIndex = 16;
            this.label13.Text = "Fecha";
            // 
            // CliNNovedadTxt
            // 
            this.CliNNovedadTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CliNNovedadTxt.Location = new System.Drawing.Point(6, 29);
            this.CliNNovedadTxt.MaxLength = 255;
            this.CliNNovedadTxt.Name = "CliNNovedadTxt";
            this.CliNNovedadTxt.Size = new System.Drawing.Size(557, 21);
            this.CliNNovedadTxt.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(214, 15);
            this.label14.TabIndex = 0;
            this.label14.Text = "Producto (Código; Línea; Descripción)";
            // 
            // rTabsRegFaltantes
            // 
            this.rTabsRegFaltantes.Controls.Add(this.rTabRegistroFaltante);
            this.rTabsRegFaltantes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTabsRegFaltantes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTabsRegFaltantes.ItemSize = new System.Drawing.Size(127, 23);
            this.rTabsRegFaltantes.Location = new System.Drawing.Point(0, 32);
            this.rTabsRegFaltantes.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.rTabsRegFaltantes.Name = "rTabsRegFaltantes";
            this.rTabsRegFaltantes.SelectedIndex = 0;
            this.rTabsRegFaltantes.Size = new System.Drawing.Size(792, 426);
            this.rTabsRegFaltantes.TabIndex = 0;
            this.rTabsRegFaltantes.Tag = "nada";
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
            this.ucPnlTop.Size = new System.Drawing.Size(792, 32);
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
            this.cboCliente.Location = new System.Drawing.Point(417, 5);
            this.cboCliente.MaxDropDownItems = 16;
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(368, 23);
            this.cboCliente.TabIndex = 3;
            this.cboCliente.SelectedIndexChanged += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
            // 
            // ucFaltante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.rTabsRegFaltantes);
            this.Controls.Add(this.ucPnlTop);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ucFaltante";
            this.Size = new System.Drawing.Size(792, 480);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.rTabRegistroFaltante.ResumeLayout(false);
            this.CliNPnlMain.ResumeLayout(false);
            this.CliNPnlTop.ResumeLayout(false);
            this.CliNPnlTop.PerformLayout();
            this.rTabsRegFaltantes.ResumeLayout(false);
            this.ucPnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage rTabRegistroFaltante;
        private System.Windows.Forms.Panel CliNPnlMain;
        private System.Windows.Forms.Panel CliNPnlTop;
        private System.Windows.Forms.DateTimePicker CliNFechaDtp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox CliNNovedadTxt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabControl rTabsRegFaltantes;
        private System.Windows.Forms.Panel ucPnlTop;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button CliNAgregarBtn;
        private System.Windows.Forms.ListView RFlistView;
        private System.Windows.Forms.ColumnHeader rfFechaLv;
        private System.Windows.Forms.ColumnHeader rfDetalleLv;
        private System.Windows.Forms.ColumnHeader rfIDLv;
        private System.Windows.Forms.Label CliNidLbl;
        private System.Windows.Forms.ColumnHeader rfCliente;
    }
}