using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Catalogo
{
    
    public partial class fLogin : Form
    {

        //[DllImport("User32.dll", CharSet = CharSet.Auto)]  
        //public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        //[DllImport("User32.dll")] 
        //private static extern IntPtr GetWindowDC(IntPtr hWnd);
 
        public bool TodoBien { get; set; }

        public fLogin()
        {
            InitializeComponent();

           this.pictureBox1.BackColor = Color.Transparent;
           this.errormessage.BackColor = Color.Transparent;
           this.errormessage.ForeColor = Color.Red;

            TodoBien = false;
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            this.lblUsuario.Text = Global01.RazonSocial.ToString() + " - (" + Global01.NroUsuario.ToString() + ")";

            if (Global01.pin.Trim().Length == 0)
            {
                btnNuevo.Visible = true;
            }
            else
            {
                btnIngresar.Enabled = true;
                chkActualizarClientes.Enabled = true;
            };

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtPIN.Text.Trim().Length == 0)
            {
                errormessage.Text = "ingrese pin.";
                txtPIN.Focus();
            }
            else
            {
                if (txtPIN.Text.Trim().ToUpper()==Global01.pin.ToString().ToUpper())
                {
                    Funciones.oleDbFunciones.ComandoIU(ref Global01.Conexion, "UPDATE appConfig SET PIN='" + txtPIN.Text.Trim().ToUpper() +"'");
                    TodoBien = true;
                    Close();
                }
                else
                {
                    errormessage.Text = "El PIN de ingreso no es válido.";
                }
            }
        }



        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
            if (btnNuevo.Text=="generar")
            {
                if (txtPIN.Text.Trim().Length == 0)
                {
                    errormessage.Text = "ingrese pin.";
                    txtPIN.Focus();
                    return;
                };

                txtPIN2.Text = "";
                txtPIN.Visible = false;
                txtPIN.Enabled = false;
                lblPIN.Text = "Reingresar PIN";
                btnNuevo.Text = "verificar";
                txtPIN2.Visible = true;
                txtPIN2.Enabled = true;
                txtPIN2.Focus();                
            }
            else 
            {
                if (txtPIN2.Text.Trim().Length == 0)
                {
                    errormessage.Text = "re-ingrese pin.";
                    txtPIN2.Focus();
                    return;
                };

                if (txtPIN.Text.Trim().ToUpper() == txtPIN2.Text.Trim().ToUpper())
                {
                    btnIngresar.Enabled = true;
                    chkActualizarClientes.Enabled = true;
                    txtPIN2.Enabled = false;
                    btnNuevo.Enabled = false;
                    Global01.pin = txtPIN.Text;
                    MessageBox.Show("PIN generado con éxito!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else 
                {
                    txtPIN.Text = "";
                    txtPIN2.Text = "";
                    txtPIN2.Visible = false;
                    txtPIN2.Enabled = false;
                    lblPIN.Text = "PIN";
                    btnNuevo.Text = "generar";
                    txtPIN.Visible = true;
                    txtPIN.Enabled = true;
                    txtPIN.Focus();

                    MessageBox.Show("Error: Ingrese el PIN nuevamente!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.None);
                };

            };


        }

        //private void fLogin_Paint(object sender, PaintEventArgs e)
        //{

        //    e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle(0, 0, this.Width - 15, this.Height - 30));

        //}



        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    const int WM_NCPAINT = 0x85;
        //    if (m.Msg == WM_NCPAINT)
        //    {
        //        IntPtr hdc = GetWindowDC(m.HWnd);
        //        if ((int)hdc != 0)
        //        {
        //            Graphics g = Graphics.FromHdc(hdc);
        //            g.FillRectangle(Brushes.Green, new Rectangle(0, 0, 4800, 23));
        //            g.Flush();
        //            ReleaseDC(m.HWnd, hdc);
        //        }
        //    }
        //}

    // Win32 API calls, often based on those from pinvoke.net
    //[DllImport("gdi32.dll")] static extern bool DeleteObject(int hObject);
    //[DllImport("user32.dll")] static extern int SetSysColorsTemp(int[] lpaElements, int [] lpaRgbValues, int cElements);
    //[DllImport("gdi32.dll")] static extern int CreateSolidBrush(int crColor);
    //[DllImport("user32.dll")] static extern int GetSysColorBrush(int nIndex);
    //[DllImport("user32.dll")] static extern int GetSysColor(int nIndex);
    //[DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();

    //// Magic constants
    //const int SlotLeft = 2;
    //const int SlotRight = 27;
    //const int SlotCount = 28; // Math.Max(SlotLeft, SlotRight) + 1;

    //// The colors/brushes to use
    //int[] Colors = new int[SlotCount];
    //int[] Brushes = new int[SlotCount];

    //// The colors the user wants to use
    //Color titleBarLeft, titleBarRight;
    //public Color TitleBarLeft{get{return titleBarLeft;} set{titleBarLeft=value; UpdateBrush(SlotLeft, value);}}
    //public Color TitleBarRight{get{return titleBarRight;} set{titleBarRight=value; UpdateBrush(SlotRight, value);}}

    //void CreateBrushes()
    //{
    //    for (int i = 0; i < SlotCount; i++)
    //    {
    //        Colors[i] = GetSysColor(i);
    //        Brushes[i] = GetSysColorBrush(i);
    //    }
    //    titleBarLeft = ColorTranslator.FromWin32(Colors[SlotLeft]);
    //    titleBarRight = ColorTranslator.FromWin32(Colors[SlotRight]);
    //}

    //void UpdateBrush(int Slot, Color c)
    //{
    //    DeleteObject(Brushes[Slot]);
    //    Colors[Slot] = ColorTranslator.ToWin32(c);
    //    Brushes[Slot] = CreateSolidBrush(Colors[Slot]);
    //}

    //void DestroyBrushes()
    //{
    //    DeleteObject(Brushes[SlotLeft]);
    //    DeleteObject(Brushes[SlotRight]);           
    //}

    //// Hook up to the Window

    //void GradientForm()
    //{
    //    CreateBrushes();
    //}

    ////protected override void Dispose(bool disposing)
    ////{
    ////    if (disposing) DestroyBrushes();
    ////    base.Dispose(disposing);
    ////}

    //protected override void WndProc(ref System.Windows.Forms.Message m) 
    //{
    //    const int WM_NCPAINT = 0x85; 
    //    const int WM_NCACTIVATE = 0x86;

    //    if ((m.Msg == WM_NCACTIVATE && m.WParam.ToInt32() != 0) ||
    //        (m.Msg == WM_NCPAINT && GetForegroundWindow() == this.Handle))
    //    {

    //        int k = SetSysColorsTemp(Colors, Brushes, Colors.Length);
    //        base.WndProc(ref m); 
    //        SetSysColorsTemp(null, null, k);
    //    }
    //    else
    //        base.WndProc(ref m); 
    //}


    }

}
