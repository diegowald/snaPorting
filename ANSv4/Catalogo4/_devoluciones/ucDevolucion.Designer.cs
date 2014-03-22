
namespace Catalogo._devoluciones
{

    partial class ucDevolucion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDevolucion));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cboCliente = new System.Windows.Forms.ToolStripComboBox();
            this.tsBtnVer = new System.Windows.Forms.ToolStripButton();
            this.tsBtnConfirmar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnIniciar = new System.Windows.Forms.ToolStripButton();
            this.DevolucionTab = new System.Windows.Forms.TabControl();
            this.DevMnTab = new System.Windows.Forms.TabPage();
            this.DevMnPnlMain = new System.Windows.Forms.Panel();
            this.DevMnDataGridView = new System.Windows.Forms.DataGridView();
            this.DevMnPnlTop = new System.Windows.Forms.Panel();
            this.DevMnErrorModeloRb = new System.Windows.Forms.RadioButton();
            this.DevMnErrorPedidoRb = new System.Windows.Forms.RadioButton();
            this.DevMnMalEnviadoRb = new System.Windows.Forms.RadioButton();
            this.DevMnMalSolicitadoRb = new System.Windows.Forms.RadioButton();
            this.DevMnFacturaTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DevMnObservacionesTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DevMnCantidadTxt = new System.Windows.Forms.NumericUpDown();
            this.DevMnDepositoCbo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DevMnAgragarBtn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.DevMfTab = new System.Windows.Forms.TabPage();
            this.DevMfPnlMain = new System.Windows.Forms.Panel();
            this.DevMfdataGridView = new System.Windows.Forms.DataGridView();
            this.DevMfPnlTop = new System.Windows.Forms.Panel();
            this.DevMfModeloCbo = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DevMfVehiculoCbo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.DevMfKmTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.DevMfMotorTxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DevMfFacturaTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DevMfObservacionesTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DevMfCantidadTxt = new System.Windows.Forms.NumericUpDown();
            this.DevMfDepositoCbo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DevMfAgregarBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.DevAntTab = new System.Windows.Forms.TabPage();
            this.DevAntPnlMain = new System.Windows.Forms.Panel();
            this.DevAntDataGridView = new System.Windows.Forms.DataGridView();
            this.DevAntPnlBotton = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.DevolucionTab.SuspendLayout();
            this.DevMnTab.SuspendLayout();
            this.DevMnPnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevMnDataGridView)).BeginInit();
            this.DevMnPnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevMnCantidadTxt)).BeginInit();
            this.DevMfTab.SuspendLayout();
            this.DevMfPnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevMfdataGridView)).BeginInit();
            this.DevMfPnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevMfCantidadTxt)).BeginInit();
            this.DevAntTab.SuspendLayout();
            this.DevAntPnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevAntDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboCliente,
            this.tsBtnVer,
            this.tsBtnConfirmar,
            this.tsBtnIniciar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(753, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cboCliente
            // 
            this.cboCliente.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cboCliente.Margin = new System.Windows.Forms.Padding(1, 0, 5, 0);
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(221, 25);
            this.cboCliente.SelectedIndexChanged += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
            // 
            // tsBtnVer
            // 
            this.tsBtnVer.AutoSize = false;
            this.tsBtnVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tsBtnVer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnVer.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnVer.Image")));
            this.tsBtnVer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnVer.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.tsBtnVer.Name = "tsBtnVer";
            this.tsBtnVer.Size = new System.Drawing.Size(65, 22);
            this.tsBtnVer.Text = "Ver";
            this.tsBtnVer.ToolTipText = "Ver pedido";
            // 
            // tsBtnConfirmar
            // 
            this.tsBtnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tsBtnConfirmar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnConfirmar.Image")));
            this.tsBtnConfirmar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnConfirmar.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.tsBtnConfirmar.Name = "tsBtnConfirmar";
            this.tsBtnConfirmar.Size = new System.Drawing.Size(65, 22);
            this.tsBtnConfirmar.Text = "Confirmar";
            this.tsBtnConfirmar.ToolTipText = "Confirmar pedido";
            // 
            // tsBtnIniciar
            // 
            this.tsBtnIniciar.AutoSize = false;
            this.tsBtnIniciar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tsBtnIniciar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnIniciar.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnIniciar.Image")));
            this.tsBtnIniciar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnIniciar.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.tsBtnIniciar.Name = "tsBtnIniciar";
            this.tsBtnIniciar.Size = new System.Drawing.Size(65, 22);
            this.tsBtnIniciar.Tag = "INICIAR";
            this.tsBtnIniciar.Text = "Iniciar";
            this.tsBtnIniciar.ToolTipText = "Iniciar un pedido nuevo";
            // 
            // DevolucionTab
            // 
            this.DevolucionTab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.DevolucionTab.Controls.Add(this.DevMnTab);
            this.DevolucionTab.Controls.Add(this.DevMfTab);
            this.DevolucionTab.Controls.Add(this.DevAntTab);
            this.DevolucionTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevolucionTab.Location = new System.Drawing.Point(0, 25);
            this.DevolucionTab.Name = "DevolucionTab";
            this.DevolucionTab.SelectedIndex = 0;
            this.DevolucionTab.Size = new System.Drawing.Size(753, 364);
            this.DevolucionTab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.DevolucionTab.TabIndex = 1;
            // 
            // DevMnTab
            // 
            this.DevMnTab.Controls.Add(this.DevMnPnlMain);
            this.DevMnTab.Controls.Add(this.DevMnPnlTop);
            this.DevMnTab.Location = new System.Drawing.Point(4, 25);
            this.DevMnTab.Name = "DevMnTab";
            this.DevMnTab.Padding = new System.Windows.Forms.Padding(3);
            this.DevMnTab.Size = new System.Drawing.Size(745, 335);
            this.DevMnTab.TabIndex = 0;
            this.DevMnTab.Text = "Mercaderia Nueva";
            this.DevMnTab.UseVisualStyleBackColor = true;
            // 
            // DevMnPnlMain
            // 
            this.DevMnPnlMain.Controls.Add(this.DevMnDataGridView);
            this.DevMnPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevMnPnlMain.Location = new System.Drawing.Point(3, 89);
            this.DevMnPnlMain.Name = "DevMnPnlMain";
            this.DevMnPnlMain.Size = new System.Drawing.Size(739, 243);
            this.DevMnPnlMain.TabIndex = 1;
            // 
            // DevMnDataGridView
            // 
            this.DevMnDataGridView.AllowUserToAddRows = false;
            this.DevMnDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DevMnDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DevMnDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DevMnDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevMnDataGridView.Location = new System.Drawing.Point(0, 0);
            this.DevMnDataGridView.MultiSelect = false;
            this.DevMnDataGridView.Name = "DevMnDataGridView";
            this.DevMnDataGridView.ReadOnly = true;
            this.DevMnDataGridView.RowHeadersWidth = 4;
            this.DevMnDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DevMnDataGridView.Size = new System.Drawing.Size(739, 243);
            this.DevMnDataGridView.TabIndex = 0;
            // 
            // DevMnPnlTop
            // 
            this.DevMnPnlTop.Controls.Add(this.DevMnErrorModeloRb);
            this.DevMnPnlTop.Controls.Add(this.DevMnErrorPedidoRb);
            this.DevMnPnlTop.Controls.Add(this.DevMnMalEnviadoRb);
            this.DevMnPnlTop.Controls.Add(this.DevMnMalSolicitadoRb);
            this.DevMnPnlTop.Controls.Add(this.DevMnFacturaTxt);
            this.DevMnPnlTop.Controls.Add(this.label2);
            this.DevMnPnlTop.Controls.Add(this.DevMnObservacionesTxt);
            this.DevMnPnlTop.Controls.Add(this.label5);
            this.DevMnPnlTop.Controls.Add(this.DevMnCantidadTxt);
            this.DevMnPnlTop.Controls.Add(this.DevMnDepositoCbo);
            this.DevMnPnlTop.Controls.Add(this.label4);
            this.DevMnPnlTop.Controls.Add(this.DevMnAgragarBtn);
            this.DevMnPnlTop.Controls.Add(this.label13);
            this.DevMnPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.DevMnPnlTop.Location = new System.Drawing.Point(3, 3);
            this.DevMnPnlTop.Name = "DevMnPnlTop";
            this.DevMnPnlTop.Size = new System.Drawing.Size(739, 86);
            this.DevMnPnlTop.TabIndex = 0;
            // 
            // DevMnErrorModeloRb
            // 
            this.DevMnErrorModeloRb.AutoSize = true;
            this.DevMnErrorModeloRb.Location = new System.Drawing.Point(542, 29);
            this.DevMnErrorModeloRb.Name = "DevMnErrorModeloRb";
            this.DevMnErrorModeloRb.Size = new System.Drawing.Size(98, 17);
            this.DevMnErrorModeloRb.TabIndex = 47;
            this.DevMnErrorModeloRb.TabStop = true;
            this.DevMnErrorModeloRb.Text = "error de modelo";
            this.DevMnErrorModeloRb.UseVisualStyleBackColor = true;
            // 
            // DevMnErrorPedidoRb
            // 
            this.DevMnErrorPedidoRb.AutoSize = true;
            this.DevMnErrorPedidoRb.Location = new System.Drawing.Point(404, 29);
            this.DevMnErrorPedidoRb.Name = "DevMnErrorPedidoRb";
            this.DevMnErrorPedidoRb.Size = new System.Drawing.Size(132, 17);
            this.DevMnErrorPedidoRb.TabIndex = 46;
            this.DevMnErrorPedidoRb.TabStop = true;
            this.DevMnErrorPedidoRb.Text = "error al tomar el pedido";
            this.DevMnErrorPedidoRb.UseVisualStyleBackColor = true;
            // 
            // DevMnMalEnviadoRb
            // 
            this.DevMnMalEnviadoRb.AutoSize = true;
            this.DevMnMalEnviadoRb.Location = new System.Drawing.Point(542, 9);
            this.DevMnMalEnviadoRb.Name = "DevMnMalEnviadoRb";
            this.DevMnMalEnviadoRb.Size = new System.Drawing.Size(82, 17);
            this.DevMnMalEnviadoRb.TabIndex = 45;
            this.DevMnMalEnviadoRb.TabStop = true;
            this.DevMnMalEnviadoRb.Text = "mal enviado";
            this.DevMnMalEnviadoRb.UseVisualStyleBackColor = true;
            // 
            // DevMnMalSolicitadoRb
            // 
            this.DevMnMalSolicitadoRb.AutoSize = true;
            this.DevMnMalSolicitadoRb.Location = new System.Drawing.Point(404, 9);
            this.DevMnMalSolicitadoRb.Name = "DevMnMalSolicitadoRb";
            this.DevMnMalSolicitadoRb.Size = new System.Drawing.Size(88, 17);
            this.DevMnMalSolicitadoRb.TabIndex = 44;
            this.DevMnMalSolicitadoRb.TabStop = true;
            this.DevMnMalSolicitadoRb.Text = "mal solicitado";
            this.DevMnMalSolicitadoRb.UseVisualStyleBackColor = true;
            // 
            // DevMnFacturaTxt
            // 
            this.DevMnFacturaTxt.Location = new System.Drawing.Point(245, 20);
            this.DevMnFacturaTxt.Name = "DevMnFacturaTxt";
            this.DevMnFacturaTxt.Size = new System.Drawing.Size(124, 20);
            this.DevMnFacturaTxt.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Factura";
            // 
            // DevMnObservacionesTxt
            // 
            this.DevMnObservacionesTxt.Location = new System.Drawing.Point(74, 53);
            this.DevMnObservacionesTxt.Name = "DevMnObservacionesTxt";
            this.DevMnObservacionesTxt.Size = new System.Drawing.Size(328, 20);
            this.DevMnObservacionesTxt.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Comentarios";
            // 
            // DevMnCantidadTxt
            // 
            this.DevMnCantidadTxt.Location = new System.Drawing.Point(89, 20);
            this.DevMnCantidadTxt.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.DevMnCantidadTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DevMnCantidadTxt.Name = "DevMnCantidadTxt";
            this.DevMnCantidadTxt.Size = new System.Drawing.Size(46, 20);
            this.DevMnCantidadTxt.TabIndex = 34;
            this.DevMnCantidadTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DevMnDepositoCbo
            // 
            this.DevMnDepositoCbo.FormattingEnabled = true;
            this.DevMnDepositoCbo.Location = new System.Drawing.Point(6, 20);
            this.DevMnDepositoCbo.Name = "DevMnDepositoCbo";
            this.DevMnDepositoCbo.Size = new System.Drawing.Size(77, 21);
            this.DevMnDepositoCbo.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Depósito";
            // 
            // DevMnAgragarBtn
            // 
            this.DevMnAgragarBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DevMnAgragarBtn.Location = new System.Drawing.Point(142, 20);
            this.DevMnAgragarBtn.Name = "DevMnAgragarBtn";
            this.DevMnAgragarBtn.Size = new System.Drawing.Size(55, 21);
            this.DevMnAgragarBtn.TabIndex = 25;
            this.DevMnAgragarBtn.Text = "Agregar";
            this.DevMnAgragarBtn.UseVisualStyleBackColor = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(86, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Cantidad";
            // 
            // DevMfTab
            // 
            this.DevMfTab.Controls.Add(this.DevMfPnlMain);
            this.DevMfTab.Controls.Add(this.DevMfPnlTop);
            this.DevMfTab.Location = new System.Drawing.Point(4, 25);
            this.DevMfTab.Name = "DevMfTab";
            this.DevMfTab.Padding = new System.Windows.Forms.Padding(3);
            this.DevMfTab.Size = new System.Drawing.Size(745, 335);
            this.DevMfTab.TabIndex = 1;
            this.DevMfTab.Text = "Mercaderia Fallada";
            this.DevMfTab.UseVisualStyleBackColor = true;
            // 
            // DevMfPnlMain
            // 
            this.DevMfPnlMain.Controls.Add(this.DevMfdataGridView);
            this.DevMfPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevMfPnlMain.Location = new System.Drawing.Point(3, 100);
            this.DevMfPnlMain.Name = "DevMfPnlMain";
            this.DevMfPnlMain.Size = new System.Drawing.Size(739, 232);
            this.DevMfPnlMain.TabIndex = 2;
            // 
            // DevMfdataGridView
            // 
            this.DevMfdataGridView.AllowUserToAddRows = false;
            this.DevMfdataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DevMfdataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DevMfdataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DevMfdataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevMfdataGridView.Location = new System.Drawing.Point(0, 0);
            this.DevMfdataGridView.MultiSelect = false;
            this.DevMfdataGridView.Name = "DevMfdataGridView";
            this.DevMfdataGridView.ReadOnly = true;
            this.DevMfdataGridView.RowHeadersWidth = 4;
            this.DevMfdataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DevMfdataGridView.Size = new System.Drawing.Size(739, 232);
            this.DevMfdataGridView.TabIndex = 0;
            // 
            // DevMfPnlTop
            // 
            this.DevMfPnlTop.Controls.Add(this.DevMfModeloCbo);
            this.DevMfPnlTop.Controls.Add(this.label11);
            this.DevMfPnlTop.Controls.Add(this.DevMfVehiculoCbo);
            this.DevMfPnlTop.Controls.Add(this.label10);
            this.DevMfPnlTop.Controls.Add(this.DevMfKmTxt);
            this.DevMfPnlTop.Controls.Add(this.label9);
            this.DevMfPnlTop.Controls.Add(this.DevMfMotorTxt);
            this.DevMfPnlTop.Controls.Add(this.label8);
            this.DevMfPnlTop.Controls.Add(this.DevMfFacturaTxt);
            this.DevMfPnlTop.Controls.Add(this.label1);
            this.DevMfPnlTop.Controls.Add(this.DevMfObservacionesTxt);
            this.DevMfPnlTop.Controls.Add(this.label3);
            this.DevMfPnlTop.Controls.Add(this.DevMfCantidadTxt);
            this.DevMfPnlTop.Controls.Add(this.DevMfDepositoCbo);
            this.DevMfPnlTop.Controls.Add(this.label6);
            this.DevMfPnlTop.Controls.Add(this.DevMfAgregarBtn);
            this.DevMfPnlTop.Controls.Add(this.label7);
            this.DevMfPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.DevMfPnlTop.Location = new System.Drawing.Point(3, 3);
            this.DevMfPnlTop.Name = "DevMfPnlTop";
            this.DevMfPnlTop.Size = new System.Drawing.Size(739, 97);
            this.DevMfPnlTop.TabIndex = 1;
            // 
            // DevMfModeloCbo
            // 
            this.DevMfModeloCbo.FormattingEnabled = true;
            this.DevMfModeloCbo.Location = new System.Drawing.Point(503, 20);
            this.DevMfModeloCbo.Name = "DevMfModeloCbo";
            this.DevMfModeloCbo.Size = new System.Drawing.Size(112, 21);
            this.DevMfModeloCbo.TabIndex = 55;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(500, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "Modelo";
            // 
            // DevMfVehiculoCbo
            // 
            this.DevMfVehiculoCbo.FormattingEnabled = true;
            this.DevMfVehiculoCbo.Location = new System.Drawing.Point(388, 20);
            this.DevMfVehiculoCbo.Name = "DevMfVehiculoCbo";
            this.DevMfVehiculoCbo.Size = new System.Drawing.Size(109, 21);
            this.DevMfVehiculoCbo.TabIndex = 53;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(385, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "Vehículo";
            // 
            // DevMfKmTxt
            // 
            this.DevMfKmTxt.Location = new System.Drawing.Point(142, 69);
            this.DevMfKmTxt.Name = "DevMfKmTxt";
            this.DevMfKmTxt.Size = new System.Drawing.Size(100, 20);
            this.DevMfKmTxt.TabIndex = 51;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(139, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 50;
            this.label9.Text = "KM Recorridos";
            // 
            // DevMfMotorTxt
            // 
            this.DevMfMotorTxt.Location = new System.Drawing.Point(6, 69);
            this.DevMfMotorTxt.Name = "DevMfMotorTxt";
            this.DevMfMotorTxt.Size = new System.Drawing.Size(124, 20);
            this.DevMfMotorTxt.TabIndex = 49;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 48;
            this.label8.Text = "Motor";
            // 
            // DevMfFacturaTxt
            // 
            this.DevMfFacturaTxt.Location = new System.Drawing.Point(245, 20);
            this.DevMfFacturaTxt.Name = "DevMfFacturaTxt";
            this.DevMfFacturaTxt.Size = new System.Drawing.Size(124, 20);
            this.DevMfFacturaTxt.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(242, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Factura";
            // 
            // DevMfObservacionesTxt
            // 
            this.DevMfObservacionesTxt.Location = new System.Drawing.Point(268, 69);
            this.DevMfObservacionesTxt.Name = "DevMfObservacionesTxt";
            this.DevMfObservacionesTxt.Size = new System.Drawing.Size(328, 20);
            this.DevMfObservacionesTxt.TabIndex = 37;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(265, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Comentarios";
            // 
            // DevMfCantidadTxt
            // 
            this.DevMfCantidadTxt.Location = new System.Drawing.Point(89, 20);
            this.DevMfCantidadTxt.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.DevMfCantidadTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DevMfCantidadTxt.Name = "DevMfCantidadTxt";
            this.DevMfCantidadTxt.Size = new System.Drawing.Size(46, 20);
            this.DevMfCantidadTxt.TabIndex = 34;
            this.DevMfCantidadTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DevMfDepositoCbo
            // 
            this.DevMfDepositoCbo.FormattingEnabled = true;
            this.DevMfDepositoCbo.Location = new System.Drawing.Point(6, 20);
            this.DevMfDepositoCbo.Name = "DevMfDepositoCbo";
            this.DevMfDepositoCbo.Size = new System.Drawing.Size(77, 21);
            this.DevMfDepositoCbo.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Depósito";
            // 
            // DevMfAgregarBtn
            // 
            this.DevMfAgregarBtn.Location = new System.Drawing.Point(142, 20);
            this.DevMfAgregarBtn.Name = "DevMfAgregarBtn";
            this.DevMfAgregarBtn.Size = new System.Drawing.Size(55, 21);
            this.DevMfAgregarBtn.TabIndex = 25;
            this.DevMfAgregarBtn.Text = "Agregar";
            this.DevMfAgregarBtn.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(86, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Cantidad";
            // 
            // DevAntTab
            // 
            this.DevAntTab.Controls.Add(this.DevAntPnlMain);
            this.DevAntTab.Controls.Add(this.DevAntPnlBotton);
            this.DevAntTab.Location = new System.Drawing.Point(4, 25);
            this.DevAntTab.Name = "DevAntTab";
            this.DevAntTab.Size = new System.Drawing.Size(745, 335);
            this.DevAntTab.TabIndex = 2;
            this.DevAntTab.Text = "Anteriores";
            this.DevAntTab.UseVisualStyleBackColor = true;
            // 
            // DevAntPnlMain
            // 
            this.DevAntPnlMain.Controls.Add(this.DevAntDataGridView);
            this.DevAntPnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevAntPnlMain.Location = new System.Drawing.Point(0, 0);
            this.DevAntPnlMain.Name = "DevAntPnlMain";
            this.DevAntPnlMain.Size = new System.Drawing.Size(745, 301);
            this.DevAntPnlMain.TabIndex = 1;
            // 
            // DevAntDataGridView
            // 
            this.DevAntDataGridView.AllowUserToAddRows = false;
            this.DevAntDataGridView.AllowUserToDeleteRows = false;
            this.DevAntDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.DevAntDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DevAntDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DevAntDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevAntDataGridView.Location = new System.Drawing.Point(0, 0);
            this.DevAntDataGridView.MultiSelect = false;
            this.DevAntDataGridView.Name = "DevAntDataGridView";
            this.DevAntDataGridView.ReadOnly = true;
            this.DevAntDataGridView.RowHeadersWidth = 4;
            this.DevAntDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DevAntDataGridView.Size = new System.Drawing.Size(745, 301);
            this.DevAntDataGridView.TabIndex = 0;
            // 
            // DevAntPnlBotton
            // 
            this.DevAntPnlBotton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DevAntPnlBotton.Location = new System.Drawing.Point(0, 301);
            this.DevAntPnlBotton.Name = "DevAntPnlBotton";
            this.DevAntPnlBotton.Size = new System.Drawing.Size(745, 34);
            this.DevAntPnlBotton.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(753, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel1.Text = ". . .";
            // 
            // ucDevolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DevolucionTab);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ucDevolucion";
            this.Size = new System.Drawing.Size(753, 411);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.DevolucionTab.ResumeLayout(false);
            this.DevMnTab.ResumeLayout(false);
            this.DevMnPnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevMnDataGridView)).EndInit();
            this.DevMnPnlTop.ResumeLayout(false);
            this.DevMnPnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevMnCantidadTxt)).EndInit();
            this.DevMfTab.ResumeLayout(false);
            this.DevMfPnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevMfdataGridView)).EndInit();
            this.DevMfPnlTop.ResumeLayout(false);
            this.DevMfPnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DevMfCantidadTxt)).EndInit();
            this.DevAntTab.ResumeLayout(false);
            this.DevAntPnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DevAntDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl DevolucionTab;
        private System.Windows.Forms.TabPage DevMnTab;
        private System.Windows.Forms.ToolStripButton tsBtnVer;
        private System.Windows.Forms.ToolStripButton tsBtnConfirmar;
        private System.Windows.Forms.ToolStripButton tsBtnIniciar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel DevMnPnlMain;
        private System.Windows.Forms.DataGridView DevMnDataGridView;
        private System.Windows.Forms.Panel DevMnPnlTop;
        private System.Windows.Forms.ToolStripComboBox cboCliente;
        private System.Windows.Forms.ComboBox DevMnDepositoCbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button DevMnAgragarBtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown DevMnCantidadTxt;
        private System.Windows.Forms.TextBox DevMnFacturaTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DevMnObservacionesTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage DevMfTab;
        private System.Windows.Forms.Panel DevMfPnlMain;
        private System.Windows.Forms.DataGridView DevMfdataGridView;
        private System.Windows.Forms.Panel DevMfPnlTop;
        private System.Windows.Forms.TextBox DevMfFacturaTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DevMfObservacionesTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown DevMfCantidadTxt;
        private System.Windows.Forms.ComboBox DevMfDepositoCbo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button DevMfAgregarBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton DevMnErrorModeloRb;
        private System.Windows.Forms.RadioButton DevMnErrorPedidoRb;
        private System.Windows.Forms.RadioButton DevMnMalEnviadoRb;
        private System.Windows.Forms.RadioButton DevMnMalSolicitadoRb;
        private System.Windows.Forms.ComboBox DevMfModeloCbo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox DevMfVehiculoCbo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox DevMfKmTxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox DevMfMotorTxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage DevAntTab;
        private System.Windows.Forms.Panel DevAntPnlMain;
        private System.Windows.Forms.DataGridView DevAntDataGridView;
        private System.Windows.Forms.Panel DevAntPnlBotton;
    }
}