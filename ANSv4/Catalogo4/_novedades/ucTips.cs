using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._novedades
{
    public partial class ucTips : UserControl
    {

        public delegate void requestCloseFormDelegate();
        public requestCloseFormDelegate requestCloseForm;

        private static int tipActual;

        string[] arrayTips = null;

        public ucTips()
        {
            InitializeComponent();
            tipActual = -1;
        }

        private void mostrar_tip()
        {
            Random r = new Random();
            int n = 0;
           
            Random rnd = new Random();

            n = r.Next(arrayTips.Length);

            if (arrayTips.Length > 0)
            {

                while (n == tipActual)
                {
                    n = r.Next(arrayTips.Length);
                    System.Windows.Forms.Application.DoEvents();
                }

                lblTip.Text = arrayTips[n];
                tipActual = n;
            }
            else
            {
                fireRequestClose();
            }
        }

        internal void Cargar_Tips(string file)
        {
            arrayTips = System.IO.File.ReadAllLines(file, Encoding.UTF7);
            mostrar_tip();
        }

        private void fireRequestClose()
        {
            if (requestCloseForm != null)
            {
                requestCloseForm();
            }
        }


        internal void mostrarTips(string localFile)
        {
            Cargar_Tips(localFile);
            timer1.Start();
            timer1.Tag = "start";
            lblTip.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mostrar_tip();
        }

        private void lblSiguiente_Click(object sender, EventArgs e)
        {
            mostrar_tip();
        }

        private void lblTip_Click(object sender, EventArgs e)
        {
            if (timer1.Tag.ToString() == "start")
            {
                lblTip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; 
                timer1.Stop();
                timer1.Tag = "stop";
            }
            else
            {
                lblTip.BorderStyle = System.Windows.Forms.BorderStyle.None;
                timer1.Start();
                timer1.Tag = "start";
            }

        }
    }
}
