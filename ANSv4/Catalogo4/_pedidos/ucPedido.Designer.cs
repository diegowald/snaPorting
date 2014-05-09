
namespace Catalogo._pedidos
{

    partial class ucPedido
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PedidoTab = new System.Windows.Forms.TabControl();
            this.nvActTab = new System.Windows.Forms.TabPage();
            this.nvPnlMain = new System.Windows.Forms.Panel();
            this.nvlistView = new System.Windows.Forms.ListView();
            this.nvCodigoLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvNombreLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvPrecioU = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvCantidadLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvSubTotalLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvSimilarLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvDepositoLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvOfertaLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvIDLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvCodigoAnsLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvObservacionesLv = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nvPnlTop = new System.Windows.Forms.Panel();
            this.nvEsOfertaChk = new System.Windows.Forms.CheckBox();
            this.nvTransporteBuscarBtn = new System.Windows.Forms.Button();
            this.nvComprarBtn = new System.Windows.Forms.Button();
            this.nvObservacionesTxt = new System.Windows.Forms.TextBox();
            this.nvObservacionesLbl = new System.Windows.Forms.Label();
            this.nvCantidadTxt = new System.Windows.Forms.NumericUpDown();
            this.nvTransporteCbo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nvDepositoCbo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nvSimilarChk = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nvAntTab = new System.Windows.Forms.TabPage();
            this.paDataGridView = new System.Windows.Forms.DataGridView();
            this.Estado = new System.Windows.Forms.DataGridViewButtonColumn();
            this.paPnlTop = new System.Windows.Forms.Panel();
            this.EnviarBtn = new System.Windows.Forms.Button();
            this.paEnviosCbo = new System.Windows.Forms.ComboBox();
            this.PedidoAnterioresDataGridView = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.nvImporteTotalLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.ucPnlTop = new System.Windows.Forms.Panel();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.PedidoTab.SuspendLayout();
            this.nvActTab.SuspendLayout();
            this.nvPnlMain.SuspendLayout();
            this.nvPnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nvCantidadTxt)).BeginInit();
            this.nvAntTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paDataGridView)).BeginInit();
            this.paPnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PedidoAnterioresDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.ucPnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // PedidoTab
            // 
            this.PedidoTab.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.PedidoTab.Controls.Add(this.nvActTab);
            this.PedidoTab.Controls.Add(this.nvAntTab);
            this.PedidoTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PedidoTab.Location = new System.Drawing.Point(0, 32);
            this.PedidoTab.Multiline = true;
            this.PedidoTab.Name = "PedidoTab";
            this.PedidoTab.SelectedIndex = 0;
            this.PedidoTab.Size = new System.Drawing.Size(887, 357);
            this.PedidoTab.TabIndex = 1;
            this.PedidoTab.Visible = false;
            // 
            // nvActTab
            // 
            this.nvActTab.AutoScroll = true;
            this.nvActTab.Controls.Add(this.nvPnlMain);
            this.nvActTab.Controls.Add(this.nvPnlTop);
            this.nvActTab.Location = new System.Drawing.Point(4, 4);
            this.nvActTab.Name = "nvActTab";
            this.nvActTab.Padding = new System.Windows.Forms.Padding(3);
            this.nvActTab.Size = new System.Drawing.Size(860, 349);
            this.nvActTab.TabIndex = 0;
            this.nvActTab.Text = "Pedido";
            this.nvActTab.UseVisualStyleBackColor = true;
            // 
            // nvPnlMain
            // 
            this.nvPnlMain.Controls.Add(this.nvlistView);
            this.nvPnlMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nvPnlMain.Location = new System.Drawing.Point(3, 47);
            this.nvPnlMain.Name = "nvPnlMain";
            this.nvPnlMain.Size = new System.Drawing.Size(837, 303);
            this.nvPnlMain.TabIndex = 1;
            // 
            // nvlistView
            // 
            this.nvlistView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.nvlistView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nvlistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nvCodigoLv,
            this.nvNombreLv,
            this.nvPrecioU,
            this.nvCantidadLv,
            this.nvSubTotalLv,
            this.nvSimilarLv,
            this.nvDepositoLv,
            this.nvOfertaLv,
            this.nvIDLv,
            this.nvCodigoAnsLv,
            this.nvObservacionesLv});
            this.nvlistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nvlistView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nvlistView.FullRowSelect = true;
            this.nvlistView.GridLines = true;
            this.nvlistView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.nvlistView.HideSelection = false;
            this.nvlistView.Location = new System.Drawing.Point(0, 0);
            this.nvlistView.MultiSelect = false;
            this.nvlistView.Name = "nvlistView";
            this.nvlistView.Size = new System.Drawing.Size(837, 303);
            this.nvlistView.TabIndex = 4;
            this.nvlistView.Tag = "nada";
            this.nvlistView.UseCompatibleStateImageBehavior = false;
            this.nvlistView.View = System.Windows.Forms.View.Details;
            this.nvlistView.DoubleClick += new System.EventHandler(this.nvlistView_DoubleClick);
            this.nvlistView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nvlistView_KeyDown);
            // 
            // nvCodigoLv
            // 
            this.nvCodigoLv.Text = "Código";
            // 
            // nvNombreLv
            // 
            this.nvNombreLv.Text = "Nombre";
            this.nvNombreLv.Width = 100;
            // 
            // nvPrecioU
            // 
            this.nvPrecioU.Text = "Precio U.";
            // 
            // nvCantidadLv
            // 
            this.nvCantidadLv.Text = "Cantidad";
            this.nvCantidadLv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nvSubTotalLv
            // 
            this.nvSubTotalLv.Text = "SubTotal";
            this.nvSubTotalLv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nvSimilarLv
            // 
            this.nvSimilarLv.Text = "Similar";
            // 
            // nvDepositoLv
            // 
            this.nvDepositoLv.Text = "Depósito";
            // 
            // nvOfertaLv
            // 
            this.nvOfertaLv.Text = "Oferta";
            this.nvOfertaLv.Width = 0;
            // 
            // nvIDLv
            // 
            this.nvIDLv.Text = "ID";
            this.nvIDLv.Width = 0;
            // 
            // nvCodigoAnsLv
            // 
            this.nvCodigoAnsLv.Text = "CodigoAns";
            this.nvCodigoAnsLv.Width = 0;
            // 
            // nvObservacionesLv
            // 
            this.nvObservacionesLv.Text = "Observaciones";
            this.nvObservacionesLv.Width = 300;
            // 
            // nvPnlTop
            // 
            this.nvPnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.nvPnlTop.Controls.Add(this.nvEsOfertaChk);
            this.nvPnlTop.Controls.Add(this.nvTransporteBuscarBtn);
            this.nvPnlTop.Controls.Add(this.nvComprarBtn);
            this.nvPnlTop.Controls.Add(this.nvObservacionesTxt);
            this.nvPnlTop.Controls.Add(this.nvObservacionesLbl);
            this.nvPnlTop.Controls.Add(this.nvCantidadTxt);
            this.nvPnlTop.Controls.Add(this.nvTransporteCbo);
            this.nvPnlTop.Controls.Add(this.label1);
            this.nvPnlTop.Controls.Add(this.nvDepositoCbo);
            this.nvPnlTop.Controls.Add(this.label4);
            this.nvPnlTop.Controls.Add(this.nvSimilarChk);
            this.nvPnlTop.Controls.Add(this.label13);
            this.nvPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.nvPnlTop.Location = new System.Drawing.Point(3, 3);
            this.nvPnlTop.Name = "nvPnlTop";
            this.nvPnlTop.Size = new System.Drawing.Size(837, 47);
            this.nvPnlTop.TabIndex = 0;
            // 
            // nvEsOfertaChk
            // 
            this.nvEsOfertaChk.AutoSize = true;
            this.nvEsOfertaChk.Enabled = false;
            this.nvEsOfertaChk.Location = new System.Drawing.Point(127, 21);
            this.nvEsOfertaChk.Name = "nvEsOfertaChk";
            this.nvEsOfertaChk.Size = new System.Drawing.Size(70, 17);
            this.nvEsOfertaChk.TabIndex = 40;
            this.nvEsOfertaChk.Text = "Es Oferta";
            this.nvEsOfertaChk.UseVisualStyleBackColor = true;
            // 
            // nvTransporteBuscarBtn
            // 
            this.nvTransporteBuscarBtn.BackColor = System.Drawing.Color.Transparent;
            this.nvTransporteBuscarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nvTransporteBuscarBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.nvTransporteBuscarBtn.FlatAppearance.BorderSize = 0;
            this.nvTransporteBuscarBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.nvTransporteBuscarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nvTransporteBuscarBtn.Location = new System.Drawing.Point(504, 15);
            this.nvTransporteBuscarBtn.Name = "nvTransporteBuscarBtn";
            this.nvTransporteBuscarBtn.Size = new System.Drawing.Size(30, 23);
            this.nvTransporteBuscarBtn.TabIndex = 39;
            this.nvTransporteBuscarBtn.Text = "...";
            this.nvTransporteBuscarBtn.UseVisualStyleBackColor = false;
            this.nvTransporteBuscarBtn.Click += new System.EventHandler(this.nvTransporteBuscarBtn_Click);
            // 
            // nvComprarBtn
            // 
            this.nvComprarBtn.BackColor = System.Drawing.Color.Transparent;
            this.nvComprarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nvComprarBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.nvComprarBtn.FlatAppearance.BorderSize = 0;
            this.nvComprarBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.nvComprarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nvComprarBtn.Location = new System.Drawing.Point(60, 13);
            this.nvComprarBtn.Name = "nvComprarBtn";
            this.nvComprarBtn.Size = new System.Drawing.Size(60, 25);
            this.nvComprarBtn.TabIndex = 38;
            this.nvComprarBtn.Text = "Comprar";
            this.nvComprarBtn.UseVisualStyleBackColor = false;
            this.nvComprarBtn.Click += new System.EventHandler(this.nvComprarBtn_Click);
            // 
            // nvObservacionesTxt
            // 
            this.nvObservacionesTxt.Location = new System.Drawing.Point(537, 18);
            this.nvObservacionesTxt.MaxLength = 80;
            this.nvObservacionesTxt.Name = "nvObservacionesTxt";
            this.nvObservacionesTxt.Size = new System.Drawing.Size(318, 20);
            this.nvObservacionesTxt.TabIndex = 37;
            // 
            // nvObservacionesLbl
            // 
            this.nvObservacionesLbl.AutoSize = true;
            this.nvObservacionesLbl.Location = new System.Drawing.Point(534, 2);
            this.nvObservacionesLbl.Name = "nvObservacionesLbl";
            this.nvObservacionesLbl.Size = new System.Drawing.Size(78, 13);
            this.nvObservacionesLbl.TabIndex = 35;
            this.nvObservacionesLbl.Text = "Observaciones";
            // 
            // nvCantidadTxt
            // 
            this.nvCantidadTxt.Location = new System.Drawing.Point(8, 18);
            this.nvCantidadTxt.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nvCantidadTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nvCantidadTxt.Name = "nvCantidadTxt";
            this.nvCantidadTxt.Size = new System.Drawing.Size(46, 20);
            this.nvCantidadTxt.TabIndex = 34;
            this.nvCantidadTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nvTransporteCbo
            // 
            this.nvTransporteCbo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.nvTransporteCbo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.nvTransporteCbo.FormattingEnabled = true;
            this.nvTransporteCbo.Location = new System.Drawing.Point(286, 17);
            this.nvTransporteCbo.Name = "nvTransporteCbo";
            this.nvTransporteCbo.Size = new System.Drawing.Size(214, 21);
            this.nvTransporteCbo.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(283, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Transporte";
            // 
            // nvDepositoCbo
            // 
            this.nvDepositoCbo.FormattingEnabled = true;
            this.nvDepositoCbo.Location = new System.Drawing.Point(203, 17);
            this.nvDepositoCbo.Name = "nvDepositoCbo";
            this.nvDepositoCbo.Size = new System.Drawing.Size(77, 21);
            this.nvDepositoCbo.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Depósito";
            // 
            // nvSimilarChk
            // 
            this.nvSimilarChk.AutoSize = true;
            this.nvSimilarChk.Location = new System.Drawing.Point(127, 6);
            this.nvSimilarChk.Name = "nvSimilarChk";
            this.nvSimilarChk.Size = new System.Drawing.Size(56, 17);
            this.nvSimilarChk.TabIndex = 28;
            this.nvSimilarChk.Text = "Similar";
            this.nvSimilarChk.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Cantidad";
            // 
            // nvAntTab
            // 
            this.nvAntTab.Controls.Add(this.paDataGridView);
            this.nvAntTab.Controls.Add(this.paPnlTop);
            this.nvAntTab.Controls.Add(this.PedidoAnterioresDataGridView);
            this.nvAntTab.Location = new System.Drawing.Point(4, 4);
            this.nvAntTab.Name = "nvAntTab";
            this.nvAntTab.Padding = new System.Windows.Forms.Padding(3);
            this.nvAntTab.Size = new System.Drawing.Size(860, 349);
            this.nvAntTab.TabIndex = 4;
            this.nvAntTab.Text = "Anteriores";
            this.nvAntTab.UseVisualStyleBackColor = true;
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
            this.paDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Estado});
            this.paDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.paDataGridView.Location = new System.Drawing.Point(3, 45);
            this.paDataGridView.MultiSelect = false;
            this.paDataGridView.Name = "paDataGridView";
            this.paDataGridView.ReadOnly = true;
            this.paDataGridView.RowHeadersWidth = 4;
            this.paDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.paDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.paDataGridView.Size = new System.Drawing.Size(854, 301);
            this.paDataGridView.TabIndex = 3;
            this.paDataGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.paDataGridView_CellContentDoubleClick);
            this.paDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.paDataGridView_KeyDown);
            // 
            // Estado
            // 
            this.Estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Transparent;
            this.Estado.DefaultCellStyle = dataGridViewCellStyle2;
            this.Estado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Estado.HeaderText = "Estado";
            this.Estado.MinimumWidth = 20;
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Estado.Text = "E";
            this.Estado.ToolTipText = "ver Estado";
            this.Estado.Visible = false;
            this.Estado.Width = 26;
            // 
            // paPnlTop
            // 
            this.paPnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.paPnlTop.Controls.Add(this.EnviarBtn);
            this.paPnlTop.Controls.Add(this.paEnviosCbo);
            this.paPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.paPnlTop.Location = new System.Drawing.Point(3, 3);
            this.paPnlTop.Name = "paPnlTop";
            this.paPnlTop.Size = new System.Drawing.Size(854, 42);
            this.paPnlTop.TabIndex = 2;
            // 
            // EnviarBtn
            // 
            this.EnviarBtn.BackColor = System.Drawing.Color.Transparent;
            this.EnviarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.EnviarBtn.FlatAppearance.BorderSize = 0;
            this.EnviarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnviarBtn.Location = new System.Drawing.Point(231, 9);
            this.EnviarBtn.Name = "EnviarBtn";
            this.EnviarBtn.Size = new System.Drawing.Size(75, 23);
            this.EnviarBtn.TabIndex = 40;
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
            this.paEnviosCbo.Location = new System.Drawing.Point(5, 10);
            this.paEnviosCbo.Name = "paEnviosCbo";
            this.paEnviosCbo.Size = new System.Drawing.Size(214, 21);
            this.paEnviosCbo.TabIndex = 32;
            this.paEnviosCbo.Text = "(todos)";
            this.paEnviosCbo.SelectedIndexChanged += new System.EventHandler(this.paEnviosCbo_SelectedIndexChanged);
            // 
            // PedidoAnterioresDataGridView
            // 
            this.PedidoAnterioresDataGridView.AllowUserToAddRows = false;
            this.PedidoAnterioresDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            this.PedidoAnterioresDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.PedidoAnterioresDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PedidoAnterioresDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PedidoAnterioresDataGridView.Location = new System.Drawing.Point(3, 3);
            this.PedidoAnterioresDataGridView.MultiSelect = false;
            this.PedidoAnterioresDataGridView.Name = "PedidoAnterioresDataGridView";
            this.PedidoAnterioresDataGridView.ReadOnly = true;
            this.PedidoAnterioresDataGridView.RowHeadersWidth = 4;
            this.PedidoAnterioresDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PedidoAnterioresDataGridView.Size = new System.Drawing.Size(854, 343);
            this.PedidoAnterioresDataGridView.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.nvImporteTotalLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(887, 22);
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
            // nvImporteTotalLbl
            // 
            this.nvImporteTotalLbl.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.nvImporteTotalLbl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.nvImporteTotalLbl.ForeColor = System.Drawing.Color.Red;
            this.nvImporteTotalLbl.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.nvImporteTotalLbl.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.nvImporteTotalLbl.Name = "nvImporteTotalLbl";
            this.nvImporteTotalLbl.Size = new System.Drawing.Size(32, 19);
            this.nvImporteTotalLbl.Text = "0,00";
            this.nvImporteTotalLbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.nvImporteTotalLbl.Visible = false;
            // 
            // ucPnlTop
            // 
            this.ucPnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucPnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ucPnlTop.Controls.Add(this.cboCliente);
            this.ucPnlTop.Controls.Add(this.btnIniciar);
            this.ucPnlTop.Controls.Add(this.btnImprimir);
            this.ucPnlTop.Controls.Add(this.btnVer);
            this.ucPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucPnlTop.Location = new System.Drawing.Point(0, 0);
            this.ucPnlTop.Name = "ucPnlTop";
            this.ucPnlTop.Size = new System.Drawing.Size(887, 32);
            this.ucPnlTop.TabIndex = 4;
            // 
            // cboCliente
            // 
            this.cboCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.Location = new System.Drawing.Point(512, 5);
            this.cboCliente.MaxDropDownItems = 16;
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(368, 23);
            this.cboCliente.TabIndex = 3;
            this.cboCliente.SelectedIndexChanged += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackColor = System.Drawing.Color.Transparent;
            this.btnIniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnIniciar.Enabled = false;
            this.btnIniciar.FlatAppearance.BorderSize = 0;
            this.btnIniciar.Location = new System.Drawing.Point(169, 3);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 2;
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
            this.btnImprimir.Location = new System.Drawing.Point(88, 3);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 1;
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
            this.btnVer.Location = new System.Drawing.Point(7, 3);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(75, 23);
            this.btnVer.TabIndex = 0;
            this.btnVer.Text = "Ver";
            this.btnVer.UseVisualStyleBackColor = false;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // ucPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.PedidoTab);
            this.Controls.Add(this.ucPnlTop);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ucPedido";
            this.Size = new System.Drawing.Size(887, 411);
            this.PedidoTab.ResumeLayout(false);
            this.nvActTab.ResumeLayout(false);
            this.nvPnlMain.ResumeLayout(false);
            this.nvPnlTop.ResumeLayout(false);
            this.nvPnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nvCantidadTxt)).EndInit();
            this.nvAntTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paDataGridView)).EndInit();
            this.paPnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PedidoAnterioresDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ucPnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl PedidoTab;
        private System.Windows.Forms.TabPage nvActTab;
        private System.Windows.Forms.TabPage nvAntTab;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel nvPnlMain;
        private System.Windows.Forms.Panel nvPnlTop;
        private System.Windows.Forms.ComboBox nvDepositoCbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox nvSimilarChk;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox nvTransporteCbo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nvCantidadTxt;
        private System.Windows.Forms.TextBox nvObservacionesTxt;
        private System.Windows.Forms.Label nvObservacionesLbl;
        private System.Windows.Forms.Panel ucPnlTop;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button nvComprarBtn;
        private System.Windows.Forms.DataGridView PedidoAnterioresDataGridView;
        private System.Windows.Forms.ListView nvlistView;
        private System.Windows.Forms.ColumnHeader nvCodigoLv;
        private System.Windows.Forms.ColumnHeader nvNombreLv;
        private System.Windows.Forms.ColumnHeader nvPrecioU;
        private System.Windows.Forms.ColumnHeader nvCantidadLv;
        private System.Windows.Forms.ColumnHeader nvSubTotalLv;
        private System.Windows.Forms.ColumnHeader nvSimilarLv;
        private System.Windows.Forms.ColumnHeader nvDepositoLv;
        private System.Windows.Forms.ColumnHeader nvOfertaLv;
        private System.Windows.Forms.ColumnHeader nvIDLv;
        private System.Windows.Forms.ColumnHeader nvCodigoAnsLv;
        private System.Windows.Forms.ColumnHeader nvObservacionesLv;
        private System.Windows.Forms.Button nvTransporteBuscarBtn;
        private System.Windows.Forms.CheckBox nvEsOfertaChk;
        private System.Windows.Forms.Panel paPnlTop;
        private System.Windows.Forms.ComboBox paEnviosCbo;
        private System.Windows.Forms.DataGridView paDataGridView;
        private System.Windows.Forms.Button EnviarBtn;
        private System.Windows.Forms.ToolStripStatusLabel nvImporteTotalLbl;
        private System.Windows.Forms.DataGridViewButtonColumn Estado;
    }
}