using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    internal static class DataGridExtension
    {
        public static Microsoft.Windows.Controls.DataGridRow GetSelectedRow(this Microsoft.Windows.Controls.DataGrid grid)
        {
            return (Microsoft.Windows.Controls.DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem);
        }


        public static Microsoft.Windows.Controls.DataGridCell GetCell(this Microsoft.Windows.Controls.DataGrid grid, Microsoft.Windows.Controls.DataGridRow row, int column)
        {
            if (row != null)
            {
                Microsoft.Windows.Controls.Primitives.DataGridCellsPresenter presenter = GetVisualChild<Microsoft.Windows.Controls.Primitives.DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<Microsoft.Windows.Controls.Primitives.DataGridCellsPresenter>(row);
                }

                Microsoft.Windows.Controls.DataGridCell cell = (Microsoft.Windows.Controls.DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        public static T GetVisualChild<T>(System.Windows.Media.Visual parent) where T : System.Windows.Media.Visual
        {
            T child = default(T);
            int numVisuals = System.Windows.Media.VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                System.Windows.Media.Visual v = (System.Windows.Media.Visual)System.Windows.Media.VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

    }
}
