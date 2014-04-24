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
        bool bMouseMove = false;
        
        public ucTips()
        {
            InitializeComponent();
            tipActual = -1;
            chkVer.Visible = false;
            _Label3_1.Visible = false;
        }


    //        Frame1.Left = 120
    
    //n = Int((UBound(arrayTips) * Rnd))
    
    //If UBound(arrayTips) > 0 Then
        
    //    While n = nTipActual
    //        n = Int((UBound(arrayTips) * Rnd))
    //        DoEvents
    //    Wend
        
    //    lblTip.Caption = arrayTips(n)
    //    nTipActual = n
    //    While Frame1.Left <= Me.ScaleWidth
    //        Frame1.Left = Frame1.Left + 1
    //        DoEvents
    //    Wend
    //Else
    //    Unload Me
    //End If


        private void mostrar_tip()
        {
            Random r = new Random();
            int n = 0;

            Frame1.Left = 120;
            
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
/*                while (VB6.PixelsToTwipsX(Frame1.Left) <= VB6.PixelsToTwipsX(this.ClientRectangle.Width))
                {
                    Frame1.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Frame1.Left) + 1);
                    System.Windows.Forms.Application.DoEvents();
                }*/
            }
            else
            {
                fireRequestClose();
            }

        }

        public void Cargar_Tips(string file)
        {
/*            string ArchivoTips = null;

            if ((int) Global01.miSABOR >=3 )
            {
                ArchivoTips = Global01.AppPath  + "\\Reportes\\TipsV.txt";
            }
            else
            {
                ArchivoTips = Global01.AppPath + "\\Reportes\\Tips.txt";
            }

            arrayTips = System.IO.File.ReadAllLines(ArchivoTips);*/
            arrayTips = System.IO.File.ReadAllLines(file);
            mostrar_tip();
        }

        //UPGRADE_WARNING: Event chkVer.CheckStateChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
        private void chkVer_CheckStateChanged(System.Object eventSender, System.EventArgs eventArgs)
        {

        }

        private void frmTip_Load(System.Object eventSender, System.EventArgs eventArgs)
        {



            bMouseMove = true;

            //Label3(0).ForeColor = RGB(60, 140, 210)
            //Label3(1).ForeColor = RGB(60, 140, 210)
            //Label1.ForeColor = RGB(60, 140, 210)

//            Cargar_Tips();
        }


        private void frmTip_MouseMove(System.Object eventSender, System.Windows.Forms.MouseEventArgs eventArgs)
        {/*
            short Button = eventArgs.Button / 0x100000;
            short Shift = System.Windows.Forms.Control.ModifierKeys / 0x10000;
            float X = VB6.PixelsToTwipsX(eventArgs.X);
            float Y = VB6.PixelsToTwipsY(eventArgs.Y);
            if (bMouseMove == false)
            {
                bMouseMove = true;
                Restaurar_Labels();
            }
            */
        }

        private void Label3_MouseMove(System.Object eventSender, System.Windows.Forms.MouseEventArgs eventArgs)
        {/*
            short Button = eventArgs.Button / 0x100000;
            short Shift = System.Windows.Forms.Control.ModifierKeys / 0x10000;
            float X = VB6.PixelsToTwipsX(eventArgs.X);
            float Y = VB6.PixelsToTwipsY(eventArgs.Y);
            short Index = Label3.GetIndex(eventSender);
            if (bMouseMove)
            {
                Restaurar_Labels();
                Label3(Index).Font = VB6.FontChangeUnderline(Label3(Index).Font, true);
                bMouseMove = false;
            }*/
        }

/*        public void Restaurar_Labels()
        {
            Label3(0).Font = VB6.FontChangeUnderline(Label3(0).Font, false);
            Label3(1).Font = VB6.FontChangeUnderline(Label3(1).Font, false);
        }
*/
        private void fireRequestClose()
        {
            if (requestCloseForm != null)
            {
                requestCloseForm();
            }
        }

        private void _Label3_0_Click(object sender, EventArgs e)
        {
            mostrar_tip();
        }

        private void _Label3_1_Click(object sender, EventArgs e)
        {
            fireRequestClose();
        }


        internal void mostrarTips(string localFile)
        {
            Cargar_Tips(localFile);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mostrar_tip();
        }
    }
}
