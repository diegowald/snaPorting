
namespace Catalogo._movimientos
{

    partial class ucMovimientos
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.PnlTop = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DocTipoCbo = new System.Windows.Forms.ComboBox();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.EnviarBtn = new System.Windows.Forms.Button();
            this.paEnviosCbo = new System.Windows.Forms.ComboBox();
            this.movDataGridView = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            this.PnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.movDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
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
            // PnlTop
            // 
            this.PnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PnlTop.Controls.Add(this.label2);
            this.PnlTop.Controls.Add(this.label1);
            this.PnlTop.Controls.Add(this.DocTipoCbo);
            this.PnlTop.Controls.Add(this.cboCliente);
            this.PnlTop.Controls.Add(this.EnviarBtn);
            this.PnlTop.Controls.Add(this.paEnviosCbo);
            this.PnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlTop.Location = new System.Drawing.Point(0, 0);
            this.PnlTop.Name = "PnlTop";
            this.PnlTop.Size = new System.Drawing.Size(887, 37);
            this.PnlTop.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(188, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Tipo Documento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Estado";
            // 
            // DocTipoCbo
            // 
            this.DocTipoCbo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.DocTipoCbo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.DocTipoCbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DocTipoCbo.FormattingEnabled = true;
            this.DocTipoCbo.Items.AddRange(new object[] {
            "(todos)",
            "Nota de Venta",
            "Devoluciones",
            "Recibos",
            "Rendiciones",
            "InterDepósitos"});
            this.DocTipoCbo.Location = new System.Drawing.Point(274, 6);
            this.DocTipoCbo.Name = "DocTipoCbo";
            this.DocTipoCbo.Size = new System.Drawing.Size(124, 23);
            this.DocTipoCbo.TabIndex = 41;
            this.DocTipoCbo.Text = "(todos)";
            this.DocTipoCbo.SelectedIndexChanged += new System.EventHandler(this.DocTipoCbo_SelectedIndexChanged);
            // 
            // cboCliente
            // 
            this.cboCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCliente.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboCliente.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCliente.FormattingEnabled = true;
            this.cboCliente.Location = new System.Drawing.Point(544, 6);
            this.cboCliente.MaxDropDownItems = 16;
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Size = new System.Drawing.Size(331, 23);
            this.cboCliente.TabIndex = 40;
            this.cboCliente.SelectedIndexChanged += new System.EventHandler(this.cboCliente_SelectedIndexChanged);
            // 
            // EnviarBtn
            // 
            this.EnviarBtn.BackColor = System.Drawing.Color.Transparent;
            this.EnviarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.EnviarBtn.FlatAppearance.BorderSize = 0;
            this.EnviarBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnviarBtn.Location = new System.Drawing.Point(402, 6);
            this.EnviarBtn.Name = "EnviarBtn";
            this.EnviarBtn.Size = new System.Drawing.Size(75, 23);
            this.EnviarBtn.TabIndex = 39;
            this.EnviarBtn.Text = "Enviar";
            this.EnviarBtn.UseVisualStyleBackColor = false;
            this.EnviarBtn.Click += new System.EventHandler(this.EnviarBtn_Click);
            // 
            // paEnviosCbo
            // 
            this.paEnviosCbo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.paEnviosCbo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.paEnviosCbo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paEnviosCbo.FormattingEnabled = true;
            this.paEnviosCbo.Items.AddRange(new object[] {
            "(todos)",
            "Enviados",
            "NO Enviados"});
            this.paEnviosCbo.Location = new System.Drawing.Point(46, 6);
            this.paEnviosCbo.Name = "paEnviosCbo";
            this.paEnviosCbo.Size = new System.Drawing.Size(138, 23);
            this.paEnviosCbo.TabIndex = 32;
            this.paEnviosCbo.Text = "(todos)";
            this.paEnviosCbo.SelectedIndexChanged += new System.EventHandler(this.EnviosCbo_SelectedIndexChanged);
            // 
            // movDataGridView
            // 
            this.movDataGridView.AllowUserToAddRows = false;
            this.movDataGridView.AllowUserToDeleteRows = false;
            this.movDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            this.movDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.movDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.movDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.movDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.movDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.movDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movDataGridView.GridColor = System.Drawing.SystemColors.Control;
            this.movDataGridView.Location = new System.Drawing.Point(0, 37);
            this.movDataGridView.Name = "movDataGridView";
            this.movDataGridView.RowHeadersWidth = 4;
            this.movDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.movDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.movDataGridView.Size = new System.Drawing.Size(887, 352);
            this.movDataGridView.TabIndex = 6;
            this.movDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.movDataGridView_CellContentDoubleClick);
            this.movDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.movDataGridView_KeyDown);
            // 
            // ucMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.movDataGridView);
            this.Controls.Add(this.PnlTop);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ucMovimientos";
            this.Size = new System.Drawing.Size(887, 411);
            this.Load += new System.EventHandler(this.ucMovimientos_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.PnlTop.ResumeLayout(false);
            this.PnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.movDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel PnlTop;
        private System.Windows.Forms.ComboBox DocTipoCbo;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Button EnviarBtn;
        private System.Windows.Forms.ComboBox paEnviosCbo;
        private System.Windows.Forms.DataGridView movDataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}