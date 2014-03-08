using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    public static class DatagridExtension
    {
        public static Microsoft.Windows.Controls.DataGridRow selectedRow(this Microsoft.Windows.Controls.DataGrid grid)
        {
            return (Microsoft.Windows.Controls.DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem);
        }
    }
}
