namespace Catalogo._productos
{
    partial class GridViewFilter2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Semáforo = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnProductoDetalle = new System.Windows.Forms.Panel();
            this.lblProductoDetalle1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnProductoDetalle.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Semáforo});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 4;
            this.dataGridView1.Size = new System.Drawing.Size(682, 507);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Semáforo
            // 
            this.Semáforo.HeaderText = "Semáforo";
            this.Semáforo.Name = "Semáforo";
            this.Semáforo.ReadOnly = true;
            this.Semáforo.Text = "s";
            this.Semáforo.ToolTipText = "ver existencia";
            this.Semáforo.Width = 30;
            // 
            // pnProductoDetalle
            // 
            this.pnProductoDetalle.Controls.Add(this.lblProductoDetalle1);
            this.pnProductoDetalle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnProductoDetalle.Location = new System.Drawing.Point(0, 507);
            this.pnProductoDetalle.Name = "pnProductoDetalle";
            this.pnProductoDetalle.Size = new System.Drawing.Size(682, 40);
            this.pnProductoDetalle.TabIndex = 3;
            // 
            // lblProductoDetalle1
            // 
            this.lblProductoDetalle1.AutoSize = true;
            this.lblProductoDetalle1.Location = new System.Drawing.Point(291, 13);
            this.lblProductoDetalle1.Name = "lblProductoDetalle1";
            this.lblProductoDetalle1.Size = new System.Drawing.Size(35, 13);
            this.lblProductoDetalle1.TabIndex = 0;
            this.lblProductoDetalle1.Text = "label1";
            // 
            // GridViewFilter2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pnProductoDetalle);
            this.Name = "GridViewFilter2";
            this.Size = new System.Drawing.Size(682, 547);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnProductoDetalle.ResumeLayout(false);
            this.pnProductoDetalle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel pnProductoDetalle;
        private System.Windows.Forms.Label lblProductoDetalle1;
        private System.Windows.Forms.DataGridViewButtonColumn Semáforo;
    }
}
