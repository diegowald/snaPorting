using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using Microsoft.VisualBasic;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Windows.Forms;


namespace Catalogo.Funciones
{
    public partial class frmErrorGuardian : Form
    {


        //- ErrorGuardianSkipModule - Do not remove this line !
        public long ErrorGuardianUserReply;
        private string BuildNumber;

        private string mModuloName;

        private string mErrCompleto;
        public string ModuloName
        {
            get { return mModuloName; }
            set { mModuloName = value; }
        }

        public string ErrCompleto
        {
            get { return mErrCompleto; }
            set { mErrCompleto = value; }
        }

        public string ErrDetails
        {
            set
            {
                TextBox1.Text = value;
                TextBox2.Text = null;
            }
        }


        private void btnAbortar_Click(System.Object sender, System.EventArgs e)
        {

        }


        private void btnIgnorar_Click(System.Object sender, System.EventArgs e)
        {
            string sRep = null;

            ErrorGuardianUserReply = System.Windows.Forms.DialogResult.Ignore;
            sRep = "Ignorar Pulsado...";

            //On Error Resume Next
            //vg.Auditor.Guardar ErrordePrograma, EXITOSO, sRep
            //On Error GoTo 0

            ErrLog(sRep);

            this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.Close();
        }


        private void btnReintentar_Click(System.Object sender, System.EventArgs e)
        {
            string sRep = null;

            ErrorGuardianUserReply = System.Windows.Forms.DialogResult.Retry;
            sRep = "Reintentar Pulsado...";

            //On Error Resume Next
            //vg.Auditor.Guardar ErrordePrograma, EXITOSO, sRep
            //On Error GoTo 0

            ErrLog(sRep);

            this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.Close();
        }


        string static_ErrLog_Separator;

        private void ErrLog(string sRep)
        {
            string fn = null;

            static_ErrLog_Separator = new String(' ', 78).Replace(" ", "-");

            fn = Application.StartupPath + "\\ErrorGuardianLog.txt";
            System.IO.TextWriter file = new System.IO.StreamWriter(fn);

            file.WriteLine(mErrCompleto);
            file.WriteLine("");
            if (TextBox2.Text.Trim().Length > 0)
            {
                file.WriteLine(TextBox2.Text);
                file.WriteLine("");
            }
            //Print #FF, sRep
            file.WriteLine(static_ErrLog_Separator);
            file.Flush();
            file.Close();

        }

    }
}

