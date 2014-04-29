
namespace Catalogo._interdeposito
{

    partial class ucInterDeposito
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.rTabAnteriores = new System.Windows.Forms.TabPage();
            this.paDataGridView = new System.Windows.Forms.DataGridView();
            this.paPnlTop = new System.Windows.Forms.Panel();
            this.EnviarBtn = new System.Windows.Forms.Button();
            this.paEnviosCbo = new System.Windows.Forms.ComboBox();
            this.rTabActual = new System.Windows.Forms.TabPage();
            this.raPnlMain = new System.Windows.Forms.Panel();
            this.ralistView = new System.Windows.Forms.ListView();
            this.raT_ComprobanteLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.raN_ComprobanteLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.raImporteLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.raNadaLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.raPnlTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.bdTipoChequesRb = new System.Windows.Forms.RadioButton();
            this.bdTipoEfectivoRb = new System.Windows.Forms.RadioButton();
            this.cvCancelarBtn = new System.Windows.Forms.Button();
            this.cvAceptarBtn = new System.Windows.Forms.Button();
            this.bdNumeroTxt = new System.Windows.Forms.TextBox();
            this.bdFechaDt = new System.Windows.Forms.DateTimePicker();
            this.bdCaChequesTxt = new System.Windows.Forms.TextBox();
            this.bdFacturasTxt = new System.Windows.Forms.TextBox();
            this.bdImporteTxt = new System.Windows.Forms.TextBox();
            this.bdBancoCbo = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cvFecEmiLbl = new System.Windows.Forms.Label();
            this.cvNroChequeLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.raPnlBotton = new System.Windows.Forms.Panel();
            this.bdObservacionesTxt = new System.Windows.Forms.TextBox();
            this.raObservacionesLbl = new System.Windows.Forms.Label();
            this.rTabsInterDeposito = new System.Windows.Forms.TabControl();
            this.ucPnlTop = new System.Windows.Forms.Panel();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.rTabAnteriores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paDataGridView)).BeginInit();
            this.paPnlTop.SuspendLayout();
            this.rTabActual.SuspendLayout();
            this.raPnlMain.SuspendLayout();
            this.raPnlTop.SuspendLayout();
            this.raPnlBotton.SuspendLayout();
            this.rTabsInterDeposito.SuspendLayout();
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
            // rTabAnteriores
            // 
            this.rTabAnteriores.BackColor = System.Drawing.Color.Transparent;
            this.rTabAnteriores.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rTabAnteriores.Controls.Add(this.paDataGridView);
            this.rTabAnteriores.Controls.Add(this.paPnlTop);
            this.rTabAnteriores.Location = new System.Drawing.Point(4, 27);
            this.rTabAnteriores.Name = "rTabAnteriores";
            this.rTabAnteriores.Padding = new System.Windows.Forms.Padding(3);
            this.rTabAnteriores.Size = new System.Drawing.Size(784, 395);
            this.rTabAnteriores.TabIndex = 7;
            this.rTabAnteriores.Text = "Anteriores";
            // 
            // paDataGridView
            // 
            this.paDataGridView.AllowUserToAddRows = false;
            this.paDataGridView.AllowUserToDeleteRows = false;
            this.paDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.paDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.paDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.paDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.paDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.paDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.paDataGridView.Location = new System.Drawing.Point(3, 45);
            this.paDataGridView.MultiSelect = false;
            this.paDataGridView.Name = "paDataGridView";
            this.paDataGridView.ReadOnly = true;
            this.paDataGridView.RowHeadersWidth = 4;
            this.paDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.paDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.paDataGridView.Size = new System.Drawing.Size(778, 347);
            this.paDataGridView.TabIndex = 5;
            this.paDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.paDataGridView_CellContentDoubleClick);
            // 
            // paPnlTop
            // 
            this.paPnlTop.BackColor = System.Drawing.Color.Transparent;
            this.paPnlTop.Controls.Add(this.EnviarBtn);
            this.paPnlTop.Controls.Add(this.paEnviosCbo);
            this.paPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.paPnlTop.Location = new System.Drawing.Point(3, 3);
            this.paPnlTop.Name = "paPnlTop";
            this.paPnlTop.Size = new System.Drawing.Size(778, 42);
            this.paPnlTop.TabIndex = 4;
            // 
            // EnviarBtn
            // 
            this.EnviarBtn.BackColor = System.Drawing.Color.Transparent;
            this.EnviarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.EnviarBtn.FlatAppearance.BorderSize = 0;
            this.EnviarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnviarBtn.Location = new System.Drawing.Point(230, 9);
            this.EnviarBtn.Name = "EnviarBtn";
            this.EnviarBtn.Size = new System.Drawing.Size(75, 23);
            this.EnviarBtn.TabIndex = 41;
            this.EnviarBtn.Text = "Enviar";
            this.EnviarBtn.UseVisualStyleBackColor = false;
            this.EnviarBtn.Click += new System.EventHandler(this.EnviarBtn_Click);
            // 
            // paEnviosCbo
            // 
            this.paEnviosCbo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.paEnviosCbo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.paEnviosCbo.FormattingEnabled = true;
            this.paEnviosCbo.Items.AddRange(new object[] {
            "(todos)",
            "Enviados",
            "NO Enviados"});
            this.paEnviosCbo.Location = new System.Drawing.Point(5, 9);
            this.paEnviosCbo.Name = "paEnviosCbo";
            this.paEnviosCbo.Size = new System.Drawing.Size(214, 23);
            this.paEnviosCbo.TabIndex = 32;
            this.paEnviosCbo.Text = "(todos)";
            this.paEnviosCbo.SelectedIndexChanged += new System.EventHandler(this.paEnviosCbo_SelectedIndexChanged);
            // 
            // rTabActual
            // 
            this.rTabActual.BackColor = System.Drawing.Color.Transparent;
            this.rTabActual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rTabActual.Controls.Add(this.raPnlMain);
            this.rTabActual.Controls.Add(this.raPnlTop);
            this.rTabActual.Controls.Add(this.raPnlBotton);
            this.rTabActual.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rTabActual.Location = new System.Drawing.Point(4, 27);
            this.rTabActual.Name = "rTabActual";
            this.rTabActual.Padding = new System.Windows.Forms.Padding(3);
            this.rTabActual.Size = new System.Drawing.Size(784, 395);
            this.rTabActual.TabIndex = 0;
            this.rTabActual.Text = "B. Dep.";
            // 
            // raPnlMain
            // 
            this.raPnlMain.Controls.Add(this.ralistView);
            this.raPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.raPnlMain.Location = new System.Drawing.Point(3, 194);
            this.raPnlMain.Name = "raPnlMain";
            this.raPnlMain.Size = new System.Drawing.Size(778, 166);
            this.raPnlMain.TabIndex = 1;
            // 
            // ralistView
            // 
            this.ralistView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ralistView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ralistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.raT_ComprobanteLv,
            this.raN_ComprobanteLv,
            this.raImporteLv,
            this.raNadaLv});
            this.ralistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ralistView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ralistView.FullRowSelect = true;
            this.ralistView.GridLines = true;
            this.ralistView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ralistView.HideSelection = false;
            this.ralistView.Location = new System.Drawing.Point(0, 0);
            this.ralistView.MultiSelect = false;
            this.ralistView.Name = "ralistView";
            this.ralistView.Size = new System.Drawing.Size(778, 166);
            this.ralistView.SmallImageList = this.imageList1;
            this.ralistView.TabIndex = 0;
            this.ralistView.Tag = "nada";
            this.ralistView.UseCompatibleStateImageBehavior = false;
            this.ralistView.View = System.Windows.Forms.View.Details;
            this.ralistView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ralistView_KeyDown);
            // 
            // raT_ComprobanteLv
            // 
            this.raT_ComprobanteLv.Text = "T. Comprobante";
            this.raT_ComprobanteLv.Width = 120;
            // 
            // raN_ComprobanteLv
            // 
            this.raN_ComprobanteLv.Text = "n° Comprobante";
            this.raN_ComprobanteLv.Width = 120;
            // 
            // raImporteLv
            // 
            this.raImporteLv.Text = "Importe";
            this.raImporteLv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.raImporteLv.Width = 120;
            // 
            // raNadaLv
            // 
            this.raNadaLv.Text = "...";
            // 
            // raPnlTop
            // 
            this.raPnlTop.BackColor = System.Drawing.Color.Transparent;
            this.raPnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.raPnlTop.Controls.Add(this.label1);
            this.raPnlTop.Controls.Add(this.bdTipoChequesRb);
            this.raPnlTop.Controls.Add(this.bdTipoEfectivoRb);
            this.raPnlTop.Controls.Add(this.cvCancelarBtn);
            this.raPnlTop.Controls.Add(this.cvAceptarBtn);
            this.raPnlTop.Controls.Add(this.bdNumeroTxt);
            this.raPnlTop.Controls.Add(this.bdFechaDt);
            this.raPnlTop.Controls.Add(this.bdCaChequesTxt);
            this.raPnlTop.Controls.Add(this.bdFacturasTxt);
            this.raPnlTop.Controls.Add(this.bdImporteTxt);
            this.raPnlTop.Controls.Add(this.bdBancoCbo);
            this.raPnlTop.Controls.Add(this.label9);
            this.raPnlTop.Controls.Add(this.cvFecEmiLbl);
            this.raPnlTop.Controls.Add(this.cvNroChequeLbl);
            this.raPnlTop.Controls.Add(this.label5);
            this.raPnlTop.Controls.Add(this.label4);
            this.raPnlTop.Controls.Add(this.label2);
            this.raPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.raPnlTop.Location = new System.Drawing.Point(3, 3);
            this.raPnlTop.Name = "raPnlTop";
            this.raPnlTop.Size = new System.Drawing.Size(778, 191);
            this.raPnlTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 25;
            this.label1.Text = "Boleta de Depósito";
            // 
            // bdTipoChequesRb
            // 
            this.bdTipoChequesRb.AutoSize = true;
            this.bdTipoChequesRb.Location = new System.Drawing.Point(216, 7);
            this.bdTipoChequesRb.Name = "bdTipoChequesRb";
            this.bdTipoChequesRb.Size = new System.Drawing.Size(74, 19);
            this.bdTipoChequesRb.TabIndex = 5;
            this.bdTipoChequesRb.TabStop = true;
            this.bdTipoChequesRb.Text = "Cheques";
            this.bdTipoChequesRb.UseVisualStyleBackColor = true;
            this.bdTipoChequesRb.CheckedChanged += new System.EventHandler(this.bdTipoChequesRb_CheckedChanged);
            // 
            // bdTipoEfectivoRb
            // 
            this.bdTipoEfectivoRb.AutoSize = true;
            this.bdTipoEfectivoRb.Location = new System.Drawing.Point(147, 7);
            this.bdTipoEfectivoRb.Name = "bdTipoEfectivoRb";
            this.bdTipoEfectivoRb.Size = new System.Drawing.Size(67, 19);
            this.bdTipoEfectivoRb.TabIndex = 4;
            this.bdTipoEfectivoRb.TabStop = true;
            this.bdTipoEfectivoRb.Text = "Efectivo";
            this.bdTipoEfectivoRb.UseVisualStyleBackColor = true;
            this.bdTipoEfectivoRb.CheckedChanged += new System.EventHandler(this.bdTipoEfectivoRb_CheckedChanged);
            // 
            // cvCancelarBtn
            // 
            this.cvCancelarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cvCancelarBtn.FlatAppearance.BorderSize = 0;
            this.cvCancelarBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cvCancelarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cvCancelarBtn.Location = new System.Drawing.Point(325, 143);
            this.cvCancelarBtn.Name = "cvCancelarBtn";
            this.cvCancelarBtn.Size = new System.Drawing.Size(75, 25);
            this.cvCancelarBtn.TabIndex = 13;
            this.cvCancelarBtn.Text = "Cancelar";
            this.cvCancelarBtn.UseVisualStyleBackColor = false;
            this.cvCancelarBtn.Click += new System.EventHandler(this.cvCancelarBtn_Click);
            // 
            // cvAceptarBtn
            // 
            this.cvAceptarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cvAceptarBtn.FlatAppearance.BorderSize = 0;
            this.cvAceptarBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cvAceptarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cvAceptarBtn.Location = new System.Drawing.Point(244, 143);
            this.cvAceptarBtn.Name = "cvAceptarBtn";
            this.cvAceptarBtn.Size = new System.Drawing.Size(75, 25);
            this.cvAceptarBtn.TabIndex = 12;
            this.cvAceptarBtn.Text = "Agregar";
            this.cvAceptarBtn.UseVisualStyleBackColor = false;
            this.cvAceptarBtn.Click += new System.EventHandler(this.cvAgregarBtn_Click);
            // 
            // bdNumeroTxt
            // 
            this.bdNumeroTxt.Location = new System.Drawing.Point(114, 54);
            this.bdNumeroTxt.MaxLength = 10;
            this.bdNumeroTxt.Name = "bdNumeroTxt";
            this.bdNumeroTxt.Size = new System.Drawing.Size(100, 21);
            this.bdNumeroTxt.TabIndex = 7;
            this.bdNumeroTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bdNumeroTxt_KeyPress);
            // 
            // bdFechaDt
            // 
            this.bdFechaDt.CustomFormat = "dd/MM/yyyy";
            this.bdFechaDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.bdFechaDt.Location = new System.Drawing.Point(9, 54);
            this.bdFechaDt.Name = "bdFechaDt";
            this.bdFechaDt.Size = new System.Drawing.Size(100, 21);
            this.bdFechaDt.TabIndex = 6;
            // 
            // bdCaChequesTxt
            // 
            this.bdCaChequesTxt.Enabled = false;
            this.bdCaChequesTxt.Location = new System.Drawing.Point(327, 54);
            this.bdCaChequesTxt.MaxLength = 2;
            this.bdCaChequesTxt.Name = "bdCaChequesTxt";
            this.bdCaChequesTxt.Size = new System.Drawing.Size(73, 21);
            this.bdCaChequesTxt.TabIndex = 9;
            this.bdCaChequesTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bdCaChequesTxt_KeyPress);
            // 
            // bdFacturasTxt
            // 
            this.bdFacturasTxt.Location = new System.Drawing.Point(289, 104);
            this.bdFacturasTxt.MaxLength = 15;
            this.bdFacturasTxt.Name = "bdFacturasTxt";
            this.bdFacturasTxt.Size = new System.Drawing.Size(111, 21);
            this.bdFacturasTxt.TabIndex = 11;
            // 
            // bdImporteTxt
            // 
            this.bdImporteTxt.Location = new System.Drawing.Point(220, 54);
            this.bdImporteTxt.MaxLength = 10;
            this.bdImporteTxt.Name = "bdImporteTxt";
            this.bdImporteTxt.Size = new System.Drawing.Size(100, 21);
            this.bdImporteTxt.TabIndex = 8;
            this.bdImporteTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.bdImporteTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bdImporteTxt_KeyPress);
            // 
            // bdBancoCbo
            // 
            this.bdBancoCbo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.bdBancoCbo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.bdBancoCbo.Enabled = false;
            this.bdBancoCbo.FormattingEnabled = true;
            this.bdBancoCbo.Location = new System.Drawing.Point(9, 103);
            this.bdBancoCbo.Name = "bdBancoCbo";
            this.bdBancoCbo.Size = new System.Drawing.Size(270, 23);
            this.bdBancoCbo.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(111, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "n° B.D.";
            // 
            // cvFecEmiLbl
            // 
            this.cvFecEmiLbl.AutoSize = true;
            this.cvFecEmiLbl.Location = new System.Drawing.Point(9, 35);
            this.cvFecEmiLbl.Name = "cvFecEmiLbl";
            this.cvFecEmiLbl.Size = new System.Drawing.Size(41, 15);
            this.cvFecEmiLbl.TabIndex = 18;
            this.cvFecEmiLbl.Text = "Fecha";
            // 
            // cvNroChequeLbl
            // 
            this.cvNroChequeLbl.AutoSize = true;
            this.cvNroChequeLbl.Location = new System.Drawing.Point(323, 35);
            this.cvNroChequeLbl.Name = "cvNroChequeLbl";
            this.cvNroChequeLbl.Size = new System.Drawing.Size(77, 15);
            this.cvNroChequeLbl.TabIndex = 19;
            this.cvNroChequeLbl.Text = "Ca. Cheques";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(286, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "Facturas que afecta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 15);
            this.label4.TabIndex = 21;
            this.label4.Text = "En cuenta de Banco";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Monto $";
            // 
            // raPnlBotton
            // 
            this.raPnlBotton.Controls.Add(this.bdObservacionesTxt);
            this.raPnlBotton.Controls.Add(this.raObservacionesLbl);
            this.raPnlBotton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.raPnlBotton.Location = new System.Drawing.Point(3, 360);
            this.raPnlBotton.Name = "raPnlBotton";
            this.raPnlBotton.Size = new System.Drawing.Size(778, 32);
            this.raPnlBotton.TabIndex = 2;
            // 
            // bdObservacionesTxt
            // 
            this.bdObservacionesTxt.Location = new System.Drawing.Point(122, 7);
            this.bdObservacionesTxt.Name = "bdObservacionesTxt";
            this.bdObservacionesTxt.Size = new System.Drawing.Size(324, 21);
            this.bdObservacionesTxt.TabIndex = 0;
            // 
            // raObservacionesLbl
            // 
            this.raObservacionesLbl.AutoSize = true;
            this.raObservacionesLbl.Location = new System.Drawing.Point(29, 9);
            this.raObservacionesLbl.Name = "raObservacionesLbl";
            this.raObservacionesLbl.Size = new System.Drawing.Size(88, 15);
            this.raObservacionesLbl.TabIndex = 0;
            this.raObservacionesLbl.Text = "Observaciones";
            // 
            // rTabsInterDeposito
            // 
            this.rTabsInterDeposito.Controls.Add(this.rTabActual);
            this.rTabsInterDeposito.Controls.Add(this.rTabAnteriores);
            this.rTabsInterDeposito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTabsInterDeposito.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTabsInterDeposito.ItemSize = new System.Drawing.Size(87, 23);
            this.rTabsInterDeposito.Location = new System.Drawing.Point(0, 32);
            this.rTabsInterDeposito.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.rTabsInterDeposito.Name = "rTabsInterDeposito";
            this.rTabsInterDeposito.SelectedIndex = 0;
            this.rTabsInterDeposito.Size = new System.Drawing.Size(792, 426);
            this.rTabsInterDeposito.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.rTabsInterDeposito.TabIndex = 0;
            this.rTabsInterDeposito.Tag = "nada";
            this.rTabsInterDeposito.Visible = false;
            // 
            // ucPnlTop
            // 
            this.ucPnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucPnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ucPnlTop.Controls.Add(this.btnIniciar);
            this.ucPnlTop.Controls.Add(this.btnImprimir);
            this.ucPnlTop.Controls.Add(this.btnVer);
            this.ucPnlTop.Controls.Add(this.cboCliente);
            this.ucPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucPnlTop.Location = new System.Drawing.Point(0, 0);
            this.ucPnlTop.Name = "ucPnlTop";
            this.ucPnlTop.Size = new System.Drawing.Size(792, 32);
            this.ucPnlTop.TabIndex = 5;
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.Color.Transparent;
            this.btnIniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnIniciar.Enabled = false;
            this.btnIniciar.FlatAppearance.BorderSize = 0;
            this.btnIniciar.Location = new System.Drawing.Point(167, 5);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 6;
            this.btnIniciar.Tag = "INICIAR";
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = false;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Transparent;
            this.btnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.Location = new System.Drawing.Point(86, 5);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Confirmar";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnVer
            // 
            this.btnVer.BackColor = System.Drawing.Color.Transparent;
            this.btnVer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnVer.Enabled = false;
            this.btnVer.FlatAppearance.BorderSize = 0;
            this.btnVer.Location = new System.Drawing.Point(5, 5);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(75, 23);
            this.btnVer.TabIndex = 4;
            this.btnVer.Text = "Ver";
            this.btnVer.UseVisualStyleBackColor = false;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
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
            // ucInterDeposito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.rTabsInterDeposito);
            this.Controls.Add(this.ucPnlTop);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ucInterDeposito";
            this.Size = new System.Drawing.Size(792, 480);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.rTabAnteriores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paDataGridView)).EndInit();
            this.paPnlTop.ResumeLayout(false);
            this.rTabActual.ResumeLayout(false);
            this.raPnlMain.ResumeLayout(false);
            this.raPnlTop.ResumeLayout(false);
            this.raPnlTop.PerformLayout();
            this.raPnlBotton.ResumeLayout(false);
            this.raPnlBotton.PerformLayout();
            this.rTabsInterDeposito.ResumeLayout(false);
            this.ucPnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage rTabAnteriores;
        private System.Windows.Forms.TabPage rTabActual;
        private System.Windows.Forms.Panel raPnlBotton;
        private System.Windows.Forms.TextBox bdObservacionesTxt;
        private System.Windows.Forms.Label raObservacionesLbl;
        private System.Windows.Forms.Panel raPnlTop;
        private System.Windows.Forms.Button cvCancelarBtn;
        private System.Windows.Forms.Button cvAceptarBtn;
        private System.Windows.Forms.TextBox bdNumeroTxt;
        private System.Windows.Forms.DateTimePicker bdFechaDt;
        private System.Windows.Forms.TextBox bdCaChequesTxt;
        private System.Windows.Forms.TextBox bdFacturasTxt;
        private System.Windows.Forms.TextBox bdImporteTxt;
        private System.Windows.Forms.ComboBox bdBancoCbo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label cvFecEmiLbl;
        private System.Windows.Forms.Label cvNroChequeLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel raPnlMain;
        private System.Windows.Forms.ListView ralistView;
        private System.Windows.Forms.ColumnHeader raT_ComprobanteLv;
        private System.Windows.Forms.ColumnHeader raImporteLv;
        private System.Windows.Forms.ColumnHeader raN_ComprobanteLv;
        private System.Windows.Forms.TabControl rTabsInterDeposito;
        private System.Windows.Forms.Panel ucPnlTop;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.DataGridView paDataGridView;
        private System.Windows.Forms.Panel paPnlTop;
        private System.Windows.Forms.ComboBox paEnviosCbo;
        private System.Windows.Forms.RadioButton bdTipoChequesRb;
        private System.Windows.Forms.RadioButton bdTipoEfectivoRb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader raNadaLv;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button EnviarBtn;
    }
}