using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Catalogo.util
{
    /// <summary>
    /// Interaction logic for WFHostOverlay.xaml
    /// </summary>
    public partial class WFHostOverlay : Window
    {
        private FrameworkElement _target;

        public WFHostOverlay(FrameworkElement target, System.Windows.Forms.Control child)
        {
            InitializeComponent();

            _target = target;
            wfh.Child = child;

            Owner = Window.GetWindow(_target);

            Owner.LocationChanged += new EventHandler(PositionAndResize);
            _target.LayoutUpdated += new EventHandler(PositionAndResize);
            PositionAndResize(null, null);

            if (Owner.IsVisible)
            {
                Show();
            }
            else
            {
                Owner.IsVisibleChanged += delegate
                {
                    if (Owner.IsVisible)
                    {
                        Show();
                    }
                    else
                    {
                        Hide();
                    }
                };
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Owner.LocationChanged -= new EventHandler(PositionAndResize);
            _target.LayoutUpdated -= new EventHandler(PositionAndResize);
        }

        private void PositionAndResize(object sender, EventArgs e)
        {
            try
            {
                Point p = _target.PointToScreen(new Point());
                Left = p.X;
                Top = p.Y;

                Height = _target.ActualHeight;
                Width = _target.ActualWidth;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
