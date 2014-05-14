using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo.varios
{
    public partial class SplashScreenForm : Form, 
        Catalogo.Funciones.emitter_receiver.IReceptor<Catalogo.varios.complexMessage>,
        Catalogo.Funciones.emitter_receiver.ICancellableReceiver
    {
        delegate void StringParameterDelegate(string Text);
        delegate void StringParameterWithStatusDelegate(string Text, TypeOfMessage tom);
        delegate void SplashShowCloseDelegate();
        delegate void RecepcionDelegate(Catalogo.varios.complexMessage dato);
        delegate void RequestShowCancelButtonDelegate();

        /// <summary>
        /// To ensure splash screen is closed using the API and not by keyboard or any other things
        /// </summary>
        bool CloseSplashScreenFlag = false;

        bool doCancel = false;

        /// <summary>
        /// Base constructor
        /// </summary>
        public SplashScreenForm() 
        {
            InitializeComponent();
            
            //this.label1.Parent = this.pictureBox1;
            //this.label1.BackColor = Color.Transparent;
            //this.pictureBox1.BackColor = Color.Transparent;
            //label1.ForeColor = Color.Green;
            label1.Dock = DockStyle.Bottom;
            
            //this.progressBar1.Parent = this.pictureBox1;
            //this.progressBar1.BackColor = Color.Transparent;

            //progressBar1.Show();
            Catalogo.varios.NotificationCenter.instance.attachReceptor(this);
            Catalogo.varios.NotificationCenter.instance.attachCancellableReceptor(this);
            btnCancel.Hide();
        }

        /// <summary>
        /// Displays the splashscreen
        /// </summary>
        internal void ShowSplashScreen()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(ShowSplashScreen));
                return;
            }

            this.Show();
            Application.Run(this);
        }

        /// <summary>
        /// Closes the SplashScreen
        /// </summary>
        internal void CloseSplashScreen()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(CloseSplashScreen));
                return;
            }
            CloseSplashScreenFlag = true;
            this.Close();
        }

        /// <summary>
        /// Update text in default green color of success message
        /// </summary>
        /// <param name="Text">Message</param>
        internal void UdpateStatusText(string Text)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(UdpateStatusText), new object[] { Text });
                return;
            }
            // Must be on the UI thread if we've got this far
            label1.ForeColor = Color.Black;
            label1.Text = Text;
        }

        internal void UpdateStatusText2(string Text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new StringParameterDelegate(UpdateStatusText2), new object[] { Text });
                return;
            }
            label2.Visible = (Text.Length > 0);
            label2.ForeColor = Color.Black;
            label2.Text = Text;
        }

        /// <summary>
        /// Update text with message color defined as green/yellow/red/ for success/warning/failure
        /// </summary>
        /// <param name="Text">Message</param>
        /// <param name="tom">Type of Message</param>
        internal void UdpateStatusTextWithStatus(string Text, TypeOfMessage tom)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterWithStatusDelegate(UdpateStatusTextWithStatus), new object[] { Text, tom });
                return;
            }
            // Must be on the UI thread if we've got this far
            switch (tom)
            {
                case TypeOfMessage.Error:
                    label1.ForeColor = Color.Red;
                    break;
                case TypeOfMessage.Warning:
                    label1.ForeColor = Color.Yellow;
                    break;
                case TypeOfMessage.Success:
                    label1.ForeColor = Color.Black;
                    break;
            }
            label1.Text = Text;
        }

        /// <summary>
        /// Prevents the closing of form other than by calling the CloseSplashScreen function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseSplashScreenFlag == false)
                e.Cancel = true;
        }

        public void onRecibir(Catalogo.varios.complexMessage dato)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new RecepcionDelegate(onRecibir), new object[] { dato });
                return;
            }

            if (dato.progress1.second != -1)
            {
                label1.Dock = DockStyle.None;
                progressBar1.Show();
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Value = (int)dato.progress1.second;
            }
            else
            {
                progressBar1.Hide();
            }

            if (dato.progress2.second != -1)
            {
                progressBar2.Show();
                progressBar2.Minimum = 0;
                progressBar2.Maximum = 100;
                progressBar2.Value = (int)dato.progress2.second;
            }
            else
            {
                progressBar2.Hide();
            }

            //this.UdpateStatusText(dato.first + ": " + dato.second.ToString() + "%");
            this.UdpateStatusText(dato.progress1.first);
            this.UpdateStatusText2(dato.progress2.first);
        }

        private void showCancelButton()
        {
            btnCancel.Show();
        }

        public void onRequestCancel(ref bool cancel)
        {
            cancel = doCancel;
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new RequestShowCancelButtonDelegate(showCancelButton), new object[] { });
                return;
            }
            btnCancel.Show();
        }

    
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            doCancel = true;
            btnCancel.Hide();
        }
    }
}
