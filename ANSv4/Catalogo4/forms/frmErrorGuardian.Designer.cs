namespace Catalogo.Funciones
{
    partial class frmErrorGuardian
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
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAbortar = new System.Windows.Forms.Button();
            this.btnIgnorar = new System.Windows.Forms.Button();
            this.btnReintentar = new System.Windows.Forms.Button();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.TableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.TextBox1);
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(410, 100);
            this.GroupBox1.TabIndex = 2;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Detalles del error";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(6, 19);
            this.TextBox1.Multiline = true;
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(398, 75);
            this.TextBox1.TabIndex = 0;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.TextBox2);
            this.GroupBox2.Location = new System.Drawing.Point(12, 135);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(404, 100);
            this.GroupBox2.TabIndex = 3;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Como obtuvo este error?";
            // 
            // TextBox2
            // 
            this.TextBox2.AcceptsReturn = true;
            this.TextBox2.Location = new System.Drawing.Point(6, 19);
            this.TextBox2.Multiline = true;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(392, 75);
            this.TextBox2.TabIndex = 0;
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 3;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.TableLayoutPanel1.Controls.Add(this.btnAbortar, 0, 0);
            this.TableLayoutPanel1.Controls.Add(this.btnIgnorar, 1, 0);
            this.TableLayoutPanel1.Controls.Add(this.btnReintentar, 2, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(86, 246);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(276, 29);
            this.TableLayoutPanel1.TabIndex = 4;
            // 
            // btnAbortar
            // 
            this.btnAbortar.Location = new System.Drawing.Point(3, 3);
            this.btnAbortar.Name = "btnAbortar";
            this.btnAbortar.Size = new System.Drawing.Size(75, 23);
            this.btnAbortar.TabIndex = 0;
            this.btnAbortar.Text = "&Abortar";
            this.btnAbortar.UseVisualStyleBackColor = true;
            // 
            // btnIgnorar
            // 
            this.btnIgnorar.Location = new System.Drawing.Point(94, 3);
            this.btnIgnorar.Name = "btnIgnorar";
            this.btnIgnorar.Size = new System.Drawing.Size(75, 23);
            this.btnIgnorar.TabIndex = 1;
            this.btnIgnorar.Text = "&Ignorar";
            this.btnIgnorar.UseVisualStyleBackColor = true;
            // 
            // btnReintentar
            // 
            this.btnReintentar.Location = new System.Drawing.Point(185, 3);
            this.btnReintentar.Name = "btnReintentar";
            this.btnReintentar.Size = new System.Drawing.Size(61, 23);
            this.btnReintentar.TabIndex = 2;
            this.btnReintentar.Text = "&Reintentar";
            this.btnReintentar.UseVisualStyleBackColor = true;
            // 
            // frmErrorGuardian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 287);
            this.Controls.Add(this.TableLayoutPanel1);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Name = "frmErrorGuardian";
            this.Text = "frmErrorGuardian";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.TableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button btnAbortar;
        internal System.Windows.Forms.Button btnIgnorar;
        internal System.Windows.Forms.Button btnReintentar;
    }
}