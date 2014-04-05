using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo.util
{
    public partial class FlashControl : UserControl
    {
        public FlashControl()
        {
            InitializeComponent();
        }

        public string file
        {
            get
            {
                return axShockwaveFlash1.Movie;
            }
            set
            {
                axShockwaveFlash1.Movie = value;
            }
        }


        public void play()
        {
            try
            {
                if (axShockwaveFlash1.Movie.Length != 0)
                {
                    axShockwaveFlash1.Play();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

    }
}
