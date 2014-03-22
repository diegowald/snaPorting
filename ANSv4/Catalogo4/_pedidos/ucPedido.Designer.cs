
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPedido));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cboCliente = new System.Windows.Forms.ToolStripComboBox();
            this.tsBtnVer = new System.Windows.Forms.ToolStripButton();
            this.tsBtnConfirmar = new System.Windows.Forms.ToolStripButton();
            this.tsBtnIniciar = new System.Windows.Forms.ToolStripButton();
            this.PedidoTab = new System.Windows.Forms.TabControl();
            this.nvTab = new System.Windows.Forms.TabPage();
            this.raBottonPnl = new System.Windows.Forms.Panel();
            this.PedidoObservacionesTxt = new System.Windows.Forms.TextBox();
            this.raObservacionesLbl = new System.Windows.Forms.Label();
            this.PedidoImporteTotalLbl = new System.Windows.Forms.Label();
            this.raMainPnl = new System.Windows.Forms.Panel();
            this.PedidoDataGridView = new System.Windows.Forms.DataGridView();
            this.raTopPnl = new System.Windows.Forms.Panel();
            this.PedidoCantidadTxt = new System.Windows.Forms.NumericUpDown();
            this.PedidoTransporteBuscarBtn = new System.Windows.Forms.Button();
            this.PedidoTransporteCbo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PedidoDepositoCbo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PedidoSimilarChk = new System.Windows.Forms.CheckBox();
            this.PedidoComprarBtn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.nvAntTab = new System.Windows.Forms.TabPage();
            this.ccMainPnl = new System.Windows.Forms.Panel();
            this.PedidoAnterioresDataGridView = new System.Windows.Forms.DataGridView();
            this.ccBottonPnl = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.PedidoTab.SuspendLayout();
            this.nvTab.SuspendLayout();
            this.raBottonPnl.SuspendLayout();
            this.raMainPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PedidoDataGridView)).BeginInit();
            this.raTopPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PedidoCantidadTxt)).BeginInit();
            this.nvAntTab.SuspendLayout();
            this.ccMainPnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PedidoAnterioresDataGridView)).BeginInit();
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
            // PedidoTab
            // 
            this.PedidoTab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.PedidoTab.Controls.Add(this.nvTab);
            this.PedidoTab.Controls.Add(this.nvAntTab);
            this.PedidoTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PedidoTab.Location = new System.Drawing.Point(0, 25);
            this.PedidoTab.Name = "PedidoTab";
            this.PedidoTab.SelectedIndex = 0;
            this.PedidoTab.Size = new System.Drawing.Size(753, 364);
            this.PedidoTab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.PedidoTab.TabIndex = 1;
            // 
            // nvTab
            // 
            this.nvTab.Controls.Add(this.raBottonPnl);
            this.nvTab.Controls.Add(this.raMainPnl);
            this.nvTab.Controls.Add(this.raTopPnl);
            this.nvTab.Location = new System.Drawing.Point(4, 25);
            this.nvTab.Name = "nvTab";
            this.nvTab.Padding = new System.Windows.Forms.Padding(3);
            this.nvTab.Size = new System.Drawing.Size(745, 335);
            this.nvTab.TabIndex = 0;
            this.nvTab.Text = "Nota de Venta";
            this.nvTab.UseVisualStyleBackColor = true;
            // 
            // raBottonPnl
            // 
            this.raBottonPnl.Controls.Add(this.PedidoObservacionesTxt);
            this.raBottonPnl.Controls.Add(this.raObservacionesLbl);
            this.raBottonPnl.Controls.Add(this.PedidoImporteTotalLbl);
            this.raBottonPnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.raBottonPnl.Location = new System.Drawing.Point(3, 299);
            this.raBottonPnl.Name = "raBottonPnl";
            this.raBottonPnl.Size = new System.Drawing.Size(739, 33);
            this.raBottonPnl.TabIndex = 2;
            // 
            // PedidoObservacionesTxt
            // 
            this.PedidoObservacionesTxt.Location = new System.Drawing.Point(126, 6);
            this.PedidoObservacionesTxt.Name = "PedidoObservacionesTxt";
            this.PedidoObservacionesTxt.Size = new System.Drawing.Size(411, 20);
            this.PedidoObservacionesTxt.TabIndex = 1;
            // 
            // raObservacionesLbl
            // 
            this.raObservacionesLbl.AutoSize = true;
            this.raObservacionesLbl.Location = new System.Drawing.Point(42, 9);
            this.raObservacionesLbl.Name = "raObservacionesLbl";
            this.raObservacionesLbl.Size = new System.Drawing.Size(78, 13);
            this.raObservacionesLbl.TabIndex = 0;
            this.raObservacionesLbl.Text = "Observaciones";
            // 
            // PedidoImporteTotalLbl
            // 
            this.PedidoImporteTotalLbl.AutoSize = true;
            this.PedidoImporteTotalLbl.Location = new System.Drawing.Point(543, 9);
            this.PedidoImporteTotalLbl.Name = "PedidoImporteTotalLbl";
            this.PedidoImporteTotalLbl.Size = new System.Drawing.Size(122, 13);
            this.PedidoImporteTotalLbl.TabIndex = 0;
            this.PedidoImporteTotalLbl.Text = "Importe Total del Pedido";
            // 
            // raMainPnl
            // 
            this.raMainPnl.Controls.Add(this.PedidoDataGridView);
            this.raMainPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.raMainPnl.Location = new System.Drawing.Point(3, 52);
            this.raMainPnl.Name = "raMainPnl";
            this.raMainPnl.Size = new System.Drawing.Size(739, 280);
            this.raMainPnl.TabIndex = 1;
            // 
            // PedidoDataGridView
            // 
            this.PedidoDataGridView.AllowUserToAddRows = false;
            this.PedidoDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PedidoDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PedidoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PedidoDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PedidoDataGridView.Location = new System.Drawing.Point(0, 0);
            this.PedidoDataGridView.MultiSelect = false;
            this.PedidoDataGridView.Name = "PedidoDataGridView";
            this.PedidoDataGridView.ReadOnly = true;
            this.PedidoDataGridView.RowHeadersWidth = 4;
            this.PedidoDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PedidoDataGridView.Size = new System.Drawing.Size(739, 280);
            this.PedidoDataGridView.TabIndex = 0;
            // 
            // raTopPnl
            // 
            this.raTopPnl.Controls.Add(this.PedidoCantidadTxt);
            this.raTopPnl.Controls.Add(this.PedidoTransporteBuscarBtn);
            this.raTopPnl.Controls.Add(this.PedidoTransporteCbo);
            this.raTopPnl.Controls.Add(this.label1);
            this.raTopPnl.Controls.Add(this.PedidoDepositoCbo);
            this.raTopPnl.Controls.Add(this.label4);
            this.raTopPnl.Controls.Add(this.PedidoSimilarChk);
            this.raTopPnl.Controls.Add(this.PedidoComprarBtn);
            this.raTopPnl.Controls.Add(this.label13);
            this.raTopPnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.raTopPnl.Location = new System.Drawing.Point(3, 3);
            this.raTopPnl.Name = "raTopPnl";
            this.raTopPnl.Size = new System.Drawing.Size(739, 49);
            this.raTopPnl.TabIndex = 0;
            // 
            // PedidoCantidadTxt
            // 
            this.PedidoCantidadTxt.Location = new System.Drawing.Point(89, 20);
            this.PedidoCantidadTxt.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.PedidoCantidadTxt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PedidoCantidadTxt.Name = "PedidoCantidadTxt";
            this.PedidoCantidadTxt.Size = new System.Drawing.Size(46, 20);
            this.PedidoCantidadTxt.TabIndex = 34;
            this.PedidoCantidadTxt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PedidoTransporteBuscarBtn
            // 
            this.PedidoTransporteBuscarBtn.Location = new System.Drawing.Point(601, 20);
            this.PedidoTransporteBuscarBtn.Name = "PedidoTransporteBuscarBtn";
            this.PedidoTransporteBuscarBtn.Size = new System.Drawing.Size(43, 20);
            this.PedidoTransporteBuscarBtn.TabIndex = 33;
            this.PedidoTransporteBuscarBtn.Text = ". . .";
            this.PedidoTransporteBuscarBtn.UseVisualStyleBackColor = true;
            // 
            // PedidoTransporteCbo
            // 
            this.PedidoTransporteCbo.FormattingEnabled = true;
            this.PedidoTransporteCbo.Location = new System.Drawing.Point(390, 20);
            this.PedidoTransporteCbo.Name = "PedidoTransporteCbo";
            this.PedidoTransporteCbo.Size = new System.Drawing.Size(205, 21);
            this.PedidoTransporteCbo.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(387, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Transporte";
            // 
            // PedidoDepositoCbo
            // 
            this.PedidoDepositoCbo.FormattingEnabled = true;
            this.PedidoDepositoCbo.Location = new System.Drawing.Point(6, 20);
            this.PedidoDepositoCbo.Name = "PedidoDepositoCbo";
            this.PedidoDepositoCbo.Size = new System.Drawing.Size(77, 21);
            this.PedidoDepositoCbo.TabIndex = 30;
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
            // PedidoSimilarChk
            // 
            this.PedidoSimilarChk.AutoSize = true;
            this.PedidoSimilarChk.Location = new System.Drawing.Point(141, 22);
            this.PedidoSimilarChk.Name = "PedidoSimilarChk";
            this.PedidoSimilarChk.Size = new System.Drawing.Size(56, 17);
            this.PedidoSimilarChk.TabIndex = 28;
            this.PedidoSimilarChk.Text = "Similar";
            this.PedidoSimilarChk.UseVisualStyleBackColor = true;
            // 
            // PedidoComprarBtn
            // 
            this.PedidoComprarBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PedidoComprarBtn.Location = new System.Drawing.Point(203, 20);
            this.PedidoComprarBtn.Name = "PedidoComprarBtn";
            this.PedidoComprarBtn.Size = new System.Drawing.Size(55, 21);
            this.PedidoComprarBtn.TabIndex = 25;
            this.PedidoComprarBtn.Text = "Comprar";
            this.PedidoComprarBtn.UseVisualStyleBackColor = false;
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
            // nvAntTab
            // 
            this.nvAntTab.Controls.Add(this.ccMainPnl);
            this.nvAntTab.Controls.Add(this.ccBottonPnl);
            this.nvAntTab.Location = new System.Drawing.Point(4, 25);
            this.nvAntTab.Name = "nvAntTab";
            this.nvAntTab.Padding = new System.Windows.Forms.Padding(3);
            this.nvAntTab.Size = new System.Drawing.Size(745, 335);
            this.nvAntTab.TabIndex = 4;
            this.nvAntTab.Text = "Anteriores";
            this.nvAntTab.UseVisualStyleBackColor = true;
            // 
            // ccMainPnl
            // 
            this.ccMainPnl.Controls.Add(this.PedidoAnterioresDataGridView);
            this.ccMainPnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ccMainPnl.Location = new System.Drawing.Point(3, 3);
            this.ccMainPnl.Name = "ccMainPnl";
            this.ccMainPnl.Size = new System.Drawing.Size(739, 295);
            this.ccMainPnl.TabIndex = 0;
            // 
            // PedidoAnterioresDataGridView
            // 
            this.PedidoAnterioresDataGridView.AllowUserToAddRows = false;
            this.PedidoAnterioresDataGridView.AllowUserToDeleteRows = false;
            this.PedidoAnterioresDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PedidoAnterioresDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.PedidoAnterioresDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PedidoAnterioresDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PedidoAnterioresDataGridView.Location = new System.Drawing.Point(0, 0);
            this.PedidoAnterioresDataGridView.MultiSelect = false;
            this.PedidoAnterioresDataGridView.Name = "PedidoAnterioresDataGridView";
            this.PedidoAnterioresDataGridView.ReadOnly = true;
            this.PedidoAnterioresDataGridView.RowHeadersWidth = 4;
            this.PedidoAnterioresDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PedidoAnterioresDataGridView.Size = new System.Drawing.Size(739, 295);
            this.PedidoAnterioresDataGridView.TabIndex = 0;
            // 
            // ccBottonPnl
            // 
            this.ccBottonPnl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ccBottonPnl.Location = new System.Drawing.Point(3, 298);
            this.ccBottonPnl.Name = "ccBottonPnl";
            this.ccBottonPnl.Size = new System.Drawing.Size(739, 34);
            this.ccBottonPnl.TabIndex = 1;
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
            // ucPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PedidoTab);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ucPedido";
            this.Size = new System.Drawing.Size(753, 411);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.PedidoTab.ResumeLayout(false);
            this.nvTab.ResumeLayout(false);
            this.raBottonPnl.ResumeLayout(false);
            this.raBottonPnl.PerformLayout();
            this.raMainPnl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PedidoDataGridView)).EndInit();
            this.raTopPnl.ResumeLayout(false);
            this.raTopPnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PedidoCantidadTxt)).EndInit();
            this.nvAntTab.ResumeLayout(false);
            this.ccMainPnl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PedidoAnterioresDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl PedidoTab;
        private System.Windows.Forms.TabPage nvTab;
        private System.Windows.Forms.TabPage nvAntTab;
        private System.Windows.Forms.ToolStripButton tsBtnVer;
        private System.Windows.Forms.ToolStripButton tsBtnConfirmar;
        private System.Windows.Forms.ToolStripButton tsBtnIniciar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel raBottonPnl;
        private System.Windows.Forms.TextBox PedidoObservacionesTxt;
        private System.Windows.Forms.Label raObservacionesLbl;
        private System.Windows.Forms.Panel raMainPnl;
        private System.Windows.Forms.DataGridView PedidoDataGridView;
        private System.Windows.Forms.Panel raTopPnl;
        private System.Windows.Forms.Label PedidoImporteTotalLbl;
        private System.Windows.Forms.ToolStripComboBox cboCliente;
        private System.Windows.Forms.Panel ccBottonPnl;
        private System.Windows.Forms.Panel ccMainPnl;
        private System.Windows.Forms.DataGridView PedidoAnterioresDataGridView;
        private System.Windows.Forms.ComboBox PedidoDepositoCbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox PedidoSimilarChk;
        private System.Windows.Forms.Button PedidoComprarBtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox PedidoTransporteCbo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown PedidoCantidadTxt;
        private System.Windows.Forms.Button PedidoTransporteBuscarBtn;
    }
}