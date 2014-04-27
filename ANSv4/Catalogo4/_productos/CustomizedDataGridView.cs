using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._productos
{
    public class CustomizedDataGridView:System.Windows.Forms.DataGridView
    {
        protected override bool ProcessDataGridViewKey(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Enter)
            {
                return true;
            }
            return base.ProcessDataGridViewKey(e);
        }
    }
}
