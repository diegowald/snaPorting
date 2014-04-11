using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Catalogo._novedades
{
    public partial class ucTips : UserControl
    {

        string[] arrayTips = null;
        bool bMouseMove = false;
        
        public ucTips()
        {
            InitializeComponent();
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

            short n = 0;

            Frame1.Left = 120;
            
            Random rnd = new Random();

            n = short.Parse(Information.UBound(arrayTips) * VBMath.Rnd());

        

            if (Information.UBound(arrayTips) > 0)
            {

                while (n == static_mostrar_tip_nTipActual)
                {
                    n = Conversion.Int(Information.UBound(arrayTips) * VBMath.Rnd());
                    System.Windows.Forms.Application.DoEvents();
                }

                lblTip.Text = arrayTips(n);
                static_mostrar_tip_nTipActual = n;
                while (VB6.PixelsToTwipsX(Frame1.Left) <= VB6.PixelsToTwipsX(this.ClientRectangle.Width))
                {
                    Frame1.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Frame1.Left) + 1);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            else
            {
                this.Close();
            }

        }

        public object Cargar_Tips()
        {

            string Linea = null;
            string ArchivoTips = null;

            if (vg.miSABOR >= 3)
            {
                ArchivoTips = Global01.AppPath  + "\\Reportes\\TipsV.txt";
            }
            else
            {
                ArchivoTips = Global01.AppPath + "\\Reportes\\Tips.txt";
            }

            VBMath.Randomize();

            FileSystem.FileOpen(1, ArchivoTips, OpenMode.Input);

            // ERROR: Not supported in C#: ReDimStatement


            while (!FileSystem.EOF(1))
            {
                Linea = FileSystem.LineInput(1);
                if (Linea != Constants.vbNullString)
                {
                    arrayTips(Information.UBound(arrayTips)) = Linea;
                    Array.Resize(ref arrayTips, Information.UBound(arrayTips) + 2);
                }
            }
            FileSystem.FileClose();

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

            Cargar_Tips();
        }


        private void frmTip_MouseMove(System.Object eventSender, System.Windows.Forms.MouseEventArgs eventArgs)
        {
            short Button = eventArgs.Button / 0x100000;
            short Shift = System.Windows.Forms.Control.ModifierKeys / 0x10000;
            float X = VB6.PixelsToTwipsX(eventArgs.X);
            float Y = VB6.PixelsToTwipsY(eventArgs.Y);
            if (bMouseMove == false)
            {
                bMouseMove = true;
                Restaurar_Labels();
            }

        }

        private void Label3_Click(System.Object eventSender, System.EventArgs eventArgs)
        {
            short Index = Label3.GetIndex(eventSender);
            switch (Index)
            {
                case 0:
                    mostrar_tip();
                    break;
                case 1:
                    this.Close();
                    break;
            }
        }

        private void Label3_MouseMove(System.Object eventSender, System.Windows.Forms.MouseEventArgs eventArgs)
        {
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
            }
        }

        public void Restaurar_Labels()
        {
            Label3(0).Font = VB6.FontChangeUnderline(Label3(0).Font, false);
            Label3(1).Font = VB6.FontChangeUnderline(Label3(1).Font, false);
        }

    }
}
