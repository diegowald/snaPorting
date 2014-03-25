
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
            this.PedidoTab = new System.Windows.Forms.TabControl();
            this.nvActTab = new System.Windows.Forms.TabPage();
            this.PnlMain = new System.Windows.Forms.Panel();
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
            this.PnlTop = new System.Windows.Forms.Panel();
            this.nvTransporteBuscarBtn = new System.Windows.Forms.Button();
            this.nvComprarBtn = new System.Windows.Forms.Button();
            this.nvObservacionesTxt = new System.Windows.Forms.TextBox();
            this.raObservacionesLbl = new System.Windows.Forms.Label();
            this.nvImporteTotalLbl = new System.Windows.Forms.Label();
            this.nvCantidadTxt = new System.Windows.Forms.NumericUpDown();
            this.nvTransporteCbo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nvDepositoCbo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nvSimilarChk = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nvAntTab = new System.Windows.Forms.TabPage();
            this.PedidoAnterioresDataGridView = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ucPecPnlTop = new System.Windows.Forms.Panel();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.PedidoTab.SuspendLayout();
            this.nvActTab.SuspendLayout();
            this.PnlMain.SuspendLayout();
            this.PnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nvCantidadTxt)).BeginInit();
            this.nvAntTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PedidoAnterioresDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.ucPecPnlTop.SuspendLayout();
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
            this.nvActTab.Controls.Add(this.PnlMain);
            this.nvActTab.Controls.Add(this.PnlTop);
            this.nvActTab.Location = new System.Drawing.Point(4, 4);
            this.nvActTab.Name = "nvActTab";
            this.nvActTab.Padding = new System.Windows.Forms.Padding(3);
            this.nvActTab.Size = new System.Drawing.Size(860, 349);
            this.nvActTab.TabIndex = 0;
            this.nvActTab.Text = "Nota de Venta";
            this.nvActTab.UseVisualStyleBackColor = true;
            // 
            // PnlMain
            // 
            this.PnlMain.Controls.Add(this.nvlistView);
            this.PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlMain.Location = new System.Drawing.Point(3, 49);
            this.PnlMain.Name = "PnlMain";
            this.PnlMain.Size = new System.Drawing.Size(854, 297);
            this.PnlMain.TabIndex = 1;
            // 
            // nvlistView
            // 
            this.nvlistView.BackColor = System.Drawing.SystemColors.Control;
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
            this.nvlistView.Size = new System.Drawing.Size(854, 297);
            this.nvlistView.TabIndex = 4;
            this.nvlistView.Tag = "none";
            this.nvlistView.UseCompatibleStateImageBehavior = false;
            this.nvlistView.View = System.Windows.Forms.View.Details;
            // 
            // nvCodigoLv
            // 
            this.nvCodigoLv.Text = "Código";
            // 
            // nvNombreLv
            // 
            this.nvNombreLv.Text = "Nombre";
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
            // 
            // PnlTop
            // 
            this.PnlTop.Controls.Add(this.nvTransporteBuscarBtn);
            this.PnlTop.Controls.Add(this.nvComprarBtn);
            this.PnlTop.Controls.Add(this.nvObservacionesTxt);
            this.PnlTop.Controls.Add(this.raObservacionesLbl);
            this.PnlTop.Controls.Add(this.nvImporteTotalLbl);
            this.PnlTop.Controls.Add(this.nvCantidadTxt);
            this.PnlTop.Controls.Add(this.nvTransporteCbo);
            this.PnlTop.Controls.Add(this.label1);
            this.PnlTop.Controls.Add(this.nvDepositoCbo);
            this.PnlTop.Controls.Add(this.label4);
            this.PnlTop.Controls.Add(this.nvSimilarChk);
            this.PnlTop.Controls.Add(this.label13);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTop.Location = new System.Drawing.Point(3, 3);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(854, 46);
            this.PnlTop.TabIndex = 0;
            // 
            // nvTransporteBuscarBtn
            // 
            this.nvTransporteBuscarBtn.BackColor = System.Drawing.Color.Red;
            this.nvTransporteBuscarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nvTransporteBuscarBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.nvTransporteBuscarBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.nvTransporteBuscarBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nvTransporteBuscarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nvTransporteBuscarBtn.ForeColor = System.Drawing.Color.White;
            this.nvTransporteBuscarBtn.Location = new System.Drawing.Point(505, 17);
            this.nvTransporteBuscarBtn.Name = "nvTransporteBuscarBtn";
            this.nvTransporteBuscarBtn.Size = new System.Drawing.Size(26, 23);
            this.nvTransporteBuscarBtn.TabIndex = 39;
            this.nvTransporteBuscarBtn.Text = "...";
            this.nvTransporteBuscarBtn.UseVisualStyleBackColor = false;
            // 
            // nvComprarBtn
            // 
            this.nvComprarBtn.BackColor = System.Drawing.Color.Red;
            this.nvComprarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.nvComprarBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.nvComprarBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.nvComprarBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nvComprarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nvComprarBtn.ForeColor = System.Drawing.Color.White;
            this.nvComprarBtn.Location = new System.Drawing.Point(60, 17);
            this.nvComprarBtn.Name = "nvComprarBtn";
            this.nvComprarBtn.Size = new System.Drawing.Size(75, 23);
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
            this.nvObservacionesTxt.Size = new System.Drawing.Size(322, 20);
            this.nvObservacionesTxt.TabIndex = 37;
            // 
            // raObservacionesLbl
            // 
            this.raObservacionesLbl.AutoSize = true;
            this.raObservacionesLbl.Location = new System.Drawing.Point(534, 2);
            this.raObservacionesLbl.Name = "raObservacionesLbl";
            this.raObservacionesLbl.Size = new System.Drawing.Size(78, 13);
            this.raObservacionesLbl.TabIndex = 35;
            this.raObservacionesLbl.Text = "Observaciones";
            // 
            // nvImporteTotalLbl
            // 
            this.nvImporteTotalLbl.AutoSize = true;
            this.nvImporteTotalLbl.Location = new System.Drawing.Point(78, 2);
            this.nvImporteTotalLbl.Name = "nvImporteTotalLbl";
            this.nvImporteTotalLbl.Size = new System.Drawing.Size(122, 13);
            this.nvImporteTotalLbl.TabIndex = 36;
            this.nvImporteTotalLbl.Text = "Importe Total del Pedido";
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
            this.nvTransporteCbo.Location = new System.Drawing.Point(286, 18);
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
            this.nvDepositoCbo.Location = new System.Drawing.Point(203, 18);
            this.nvDepositoCbo.Name = "nvDepositoCbo";
            this.nvDepositoCbo.Size = new System.Drawing.Size(77, 21);
            this.nvDepositoCbo.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Depósito";
            // 
            // nvSimilarChk
            // 
            this.nvSimilarChk.AutoSize = true;
            this.nvSimilarChk.Location = new System.Drawing.Point(141, 20);
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
            this.nvAntTab.Controls.Add(this.PedidoAnterioresDataGridView);
            this.nvAntTab.Location = new System.Drawing.Point(4, 4);
            this.nvAntTab.Name = "nvAntTab";
            this.nvAntTab.Padding = new System.Windows.Forms.Padding(3);
            this.nvAntTab.Size = new System.Drawing.Size(860, 349);
            this.nvAntTab.TabIndex = 4;
            this.nvAntTab.Text = "Anteriores";
            this.nvAntTab.UseVisualStyleBackColor = true;
            // 
            // PedidoAnterioresDataGridView
            // 
            this.PedidoAnterioresDataGridView.AllowUserToAddRows = false;
            this.PedidoAnterioresDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PedidoAnterioresDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
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
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 389);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(887, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel1.Text = ". . .";
            // 
            // ucPecPnlTop
            // 
            this.ucPecPnlTop.Controls.Add(this.cboCliente);
            this.ucPecPnlTop.Controls.Add(this.btnIniciar);
            this.ucPecPnlTop.Controls.Add(this.btnImprimir);
            this.ucPecPnlTop.Controls.Add(this.btnVer);
            this.ucPecPnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucPecPnlTop.Location = new System.Drawing.Point(0, 0);
            this.ucPecPnlTop.Name = "ucPecPnlTop";
            this.ucPecPnlTop.Size = new System.Drawing.Size(887, 32);
            this.ucPecPnlTop.TabIndex = 4;
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
            this.btnIniciar.BackColor = System.Drawing.Color.Red;
            this.btnIniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnIniciar.Enabled = false;
            this.btnIniciar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnIniciar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnIniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciar.ForeColor = System.Drawing.Color.White;
            this.btnIniciar.Location = new System.Drawing.Point(177, 3);
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
            this.btnImprimir.BackColor = System.Drawing.Color.Red;
            this.btnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(96, 3);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 1;
            this.btnImprimir.Text = "Confirmar";
            this.btnImprimir.UseVisualStyleBackColor = false;
            // 
            // btnVer
            // 
            this.btnVer.BackColor = System.Drawing.Color.Red;
            this.btnVer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnVer.Enabled = false;
            this.btnVer.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnVer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnVer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVer.ForeColor = System.Drawing.Color.White;
            this.btnVer.Location = new System.Drawing.Point(15, 3);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(75, 23);
            this.btnVer.TabIndex = 0;
            this.btnVer.Text = "Ver";
            this.btnVer.UseVisualStyleBackColor = false;
            // 
            // ucPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PedidoTab);
            this.Controls.Add(this.ucPecPnlTop);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ucPedido";
            this.Size = new System.Drawing.Size(887, 411);
            this.Load += new System.EventHandler(this.ucPedido_Load);
            this.PedidoTab.ResumeLayout(false);
            this.nvActTab.ResumeLayout(false);
            this.PnlMain.ResumeLayout(false);
            this.PnlTop.ResumeLayout(false);
            this.PnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nvCantidadTxt)).EndInit();
            this.nvAntTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PedidoAnterioresDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ucPecPnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl PedidoTab;
        private System.Windows.Forms.TabPage nvActTab;
        private System.Windows.Forms.TabPage nvAntTab;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel PnlMain;
        private System.Windows.Forms.Panel PnlTop;
        private System.Windows.Forms.ComboBox nvDepositoCbo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox nvSimilarChk;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox nvTransporteCbo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nvCantidadTxt;
        private System.Windows.Forms.TextBox nvObservacionesTxt;
        private System.Windows.Forms.Label raObservacionesLbl;
        private System.Windows.Forms.Label nvImporteTotalLbl;
        private System.Windows.Forms.Panel ucPecPnlTop;
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
    }
}