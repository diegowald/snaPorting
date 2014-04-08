using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Drawing;
using CrystalDecisions.ReportAppServer.DataDefModel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Catalogo.Funciones
{
    class util
    {

        const string m_sMODULENAME_ = "util";

        internal static void aMayuscula(ref KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        internal static bool SoloDigitos(KeyPressEventArgs e)
        {
            bool bResultado = false;
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '\b')
            {
                bResultado = true;
            };
            return bResultado;
        }

        internal static void EsImporte(object sender, ref System.Windows.Forms.KeyPressEventArgs e)
        {
            
            if (e.KeyChar == ',' && (sender as System.Windows.Forms.TextBox).Text.ToString().IndexOf(',') > 0 |
                e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.ToString().IndexOf('.') > 0)
            {
                e.Handled = true;
            }
            else
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.' && e.KeyChar != '\b')
                {
                    e.Handled = true;
                };
            };

            if (e.KeyChar == '.') { e.KeyChar = ','; };

        }

        internal static void BuscarIndiceEnCombo(ref System.Windows.Forms.ComboBox combo, string strBuscar, bool EsList)
        {
            int iFoundIndex = 0;
            iFoundIndex = combo.FindStringExact(strBuscar);
            combo.SelectedIndex = iFoundIndex;
        }

        //acá el combo debe ser System.Windows.Forms.ToolStripComboBox
        internal static void CargaCombo(System.Data.OleDb.OleDbConnection conexion, ref System.Windows.Forms.ToolStripComboBox combo, string Tabla, string Campo1, string Campo2, string Condicion = "ALL", string Orden = "NONE", bool AceptaNulo = true, bool Concatena = false)
        {
            combo.Enabled = false;

            System.Data.DataTable  dt = Catalogo.Funciones.oleDbFunciones.xGetDt(conexion, Tabla, Condicion, Orden, Campo1 + "," + Campo2);
           
            if (AceptaNulo)
            {
                System.Data.DataRow dr = dt.NewRow();
                dr[1] = 0;
                dr[0] = "- seleccione -";
                dt.Rows.InsertAt(dr, 0);
            }

            // Populate the Control 
            combo.ComboBox.DisplayMember = Campo1;
            combo.ComboBox.ValueMember = Campo2;
            combo.ComboBox.DataSource = dt.DefaultView;

            combo.SelectedIndex = 0;
            combo.Enabled = true;
        }

        //acá el combo debe ser System.Windows.Forms.ComboBox
        internal static void CargaCombo(System.Data.OleDb.OleDbConnection conexion, ref System.Windows.Forms.ComboBox combo, string Tabla, string Campo1, string Campo2, string Condicion = "ALL", string Orden = "NONE", bool AceptaNulo = false, bool Concatena = false, string OtrosCampos = "NONE")
        {
            combo.Enabled = false;

            System.Data.DataTable dt = null;

            if (OtrosCampos == "NONE")
            {
                dt = Catalogo.Funciones.oleDbFunciones.xGetDt(conexion, Tabla, Condicion, Orden, Campo1 + "," + Campo2);
            }
            else
            {
                dt = Catalogo.Funciones.oleDbFunciones.xGetDt(conexion, Tabla, Condicion, Orden, OtrosCampos);
            };

            if (AceptaNulo)
            {
                System.Data.DataRow dr = dt.NewRow();
                dr[1] = 0;
                if (Tabla=="v_Deposito")
                {
                    dr[0] = "- DEP -";
                }
                else
                {
                    dr[0] = "- seleccione -";
                };
                dt.Rows.InsertAt(dr, 0);
            }

            // Populate the Control 
            combo.DisplayMember = Campo1;
            combo.ValueMember = Campo2;
            combo.DataSource = dt.DefaultView;

            combo.SelectedIndex = 0;
            combo.Enabled = true;
        }

        internal static void AutoSizeLVColumnas(ref System.Windows.Forms.ListView MyListview1)
        {

            float wAnchoCol = 0;

            for (int i = 0; i < MyListview1.Columns.Count; i++)
            {
                if (MyListview1.Columns[i].Text.ToString().Length > 2 && (MyListview1.Columns[i].Text.ToString().ToLower() == "fecha" | MyListview1.Columns[i].Text.ToString().Substring(0,2).ToLower() == "f." ))
                {
                    MyListview1.Columns[i].Width = 75;
                }
                else
                {
                    if (MyListview1.Columns[i].Width > 1)
                    {
                        wAnchoCol = MyListview1.Columns[i].Width;
                        MyListview1.AutoResizeColumn(i, System.Windows.Forms.ColumnHeaderAutoResizeStyle.ColumnContent);

                        if (MyListview1.Columns[i].Width < wAnchoCol)
                        {
                            MyListview1.AutoResizeColumn(i, System.Windows.Forms.ColumnHeaderAutoResizeStyle.HeaderSize);
                        };
                    };
                };               
            };

        }

        internal static void GrabarLVColumnas(ref System.Windows.Forms.ListView MyListview1)
        {

            foreach (System.Windows.Forms.ColumnHeader lvwcolumn in MyListview1.Columns)
            {
                Funciones.modINIs.INIWrite(Global01.AppPath + "\\" + "setting.ini", MyListview1.Name, lvwcolumn.Text, Convert.ToString(lvwcolumn.Width));
            }

        }

        internal static void LeerLVColumnas(ref System.Windows.Forms.ListView MyListview1)
        {
            int i = 0;


            foreach (System.Windows.Forms.ColumnHeader lvwcolumn in MyListview1.Columns)
            {
                i = Convert.ToInt32(Funciones.modINIs.INIRead(Global01.AppPath + "\\" + "setting.ini", MyListview1.Name, lvwcolumn.Text, "0"));
                if (i > 0)
                {
                    lvwcolumn.Width = i;
                }
            }

        }

        internal static void CargarLV(ref  System.Windows.Forms.ListView MyListview1, System.Data.OleDb.OleDbDataReader MyData, string MiColor = "gris")
        {
            System.Windows.Forms.ColumnHeader lvwColumn = default(System.Windows.Forms.ColumnHeader);
            System.Windows.Forms.ListViewItem itmListItem = default(System.Windows.Forms.ListViewItem);
            string strTest = null;
            int i = 0;
            short shtCntr = 0;

            MyListview1.Clear();

            for (shtCntr = 0; shtCntr < Convert.ToInt16(MyData.FieldCount); shtCntr++)
            {
                lvwColumn = new System.Windows.Forms.ColumnHeader();

                switch (MyData.GetProviderSpecificFieldType(shtCntr).FullName.ToLower())
                {
                   //System.Data.OleDb.OleDbType.VarChar 
                    case "system.data.sqltypes.sqlstring":
                        lvwColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
                        break;
                    default:
                        lvwColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                        break;
                }

                if (MyData.GetName(shtCntr).ToLower() == "codigo")
                {
                    lvwColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                }

                lvwColumn.Text = MyData.GetName(shtCntr);

                MyListview1.Columns.Add(lvwColumn);
            }

            while (MyData.Read())
            {
                i = i + 1;
                itmListItem = new System.Windows.Forms.ListViewItem();
                strTest = Convert.ToString((MyData.IsDBNull(0) ? "" : MyData.GetValue(0)));

                itmListItem.Text = strTest;

                for (shtCntr = 0; shtCntr < Convert.ToInt16(MyData.FieldCount); shtCntr++)
                {
                    if (MyData.IsDBNull(shtCntr))
                    {
                        itmListItem.SubItems.Add("");
                    }
                    else
                    {
                        itmListItem.SubItems.Add(MyData.GetValue(shtCntr).ToString());
                    }
                }

                if (i % 2 == 0)
                {
                    itmListItem.BackColor = System.Drawing.Color.White;

                }
                else
                {
                    if (MiColor == "amarillo")
                    {
                        itmListItem.BackColor = System.Drawing.Color.LightYellow;
                    }
                    else if (MiColor == "gris")
                    {
                        itmListItem.BackColor = System.Drawing.Color.LightGray;
                    }

                }
                MyListview1.Items.Add(itmListItem);

            }

            LeerLVColumnas(ref MyListview1);

        }

        internal static long ErrorGuardianGlobalHandler(string m_sMODULENAME_, string PROCNAME_)
        {
            //throw new Exception("Part 1 must be numeric");
     
            //System.Windows.Forms.DialogResult ErrorGuardianUserReply;

            long functionReturnValue = 0;

            return functionReturnValue;
        }

        public static DialogResult InputBox(string title, string promptText, int MaxLength, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            textBox.MaxLength = MaxLength;
            
            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        internal static void ChangeReportConnectionInfo(ref ReportDocument boReportDocument)
        {

            PropertyBag boMainPropertyBag = new PropertyBag();
            PropertyBag boInnerPropertyBag = new PropertyBag();
            
            //Set the attributes for the boInnerPropertyBag
            boInnerPropertyBag.Add("Database Name", Global01.dstring.ToString());
            boInnerPropertyBag.Add("Database Type", "Access");
            boInnerPropertyBag.Add("Session UserID", "inVent");
            boInnerPropertyBag.Add("Session Password", "video80min");
            boInnerPropertyBag.Add("System Database Path", Global01.sstring.ToString());

            //Set the attributes for the boMainPropertyBag
            boMainPropertyBag.Add("Database DLL", "crdb_dao.dll");
            boMainPropertyBag.Add("QE_DatabaseName", Global01.dstring.ToString());
            boMainPropertyBag.Add("QE_DatabaseType", "");

            //Add the QE_LogonProperties we set in the boInnerPropertyBag Object
            boMainPropertyBag.Add("QE_LogonProperties", boInnerPropertyBag);
            boMainPropertyBag.Add("QE_ServerDescription", Global01.dstring.ToString());
            boMainPropertyBag.Add("QE_SQLDB", "False");
            boMainPropertyBag.Add("SSO Enabled", "False");

            //Create a new ConnectionInfo object
            CrystalDecisions.ReportAppServer.DataDefModel.ConnectionInfo boConnectionInfo =
            new CrystalDecisions.ReportAppServer.DataDefModel.ConnectionInfo();
            //Pass the database properties to a connection info object
            boConnectionInfo.Attributes = boMainPropertyBag;
            //Set the connection kind
            boConnectionInfo.Kind = CrConnectionInfoKindEnum.crConnectionInfoKindCRQE;

            //boConnectionInfo.UserName = "inVent";
            //boConnectionInfo.Password = "video80min";

            foreach (CrystalDecisions.ReportAppServer.DataDefModel.Table table in boReportDocument.ReportClientDocument.DatabaseController.Database.Tables)
            {
                CrystalDecisions.ReportAppServer.DataDefModel.Procedure boTable = new CrystalDecisions.ReportAppServer.DataDefModel.Procedure();

                boTable.ConnectionInfo = boConnectionInfo;
                boTable.Name = table.Name;
                boTable.QualifiedName = table.Name;
                boTable.Alias = table.Name;

                boReportDocument.ReportClientDocument.DatabaseController.SetTableLocation(table, boTable);
            }

            foreach (ReportDocument subreport in boReportDocument.Subreports)
            {
                foreach (CrystalDecisions.ReportAppServer.DataDefModel.Table table in boReportDocument.ReportClientDocument.SubreportController.GetSubreportDatabase(subreport.Name).Tables)
                {
                    CrystalDecisions.ReportAppServer.DataDefModel.Procedure newTable = new CrystalDecisions.ReportAppServer.DataDefModel.Procedure();

                    newTable.ConnectionInfo = boConnectionInfo;
                    newTable.Name = subreport.Database.Tables[0].Name;
                    newTable.Alias = subreport.Database.Tables[0].Name;
                    newTable.QualifiedName = subreport.Database.Tables[0].Name;
                    boReportDocument.ReportClientDocument.SubreportController.SetTableLocation(subreport.Name, table, newTable);
                }
            }

            boReportDocument.VerifyDatabase();

        }

        // internal static void sendmail()
        //{
        //    try
        //    {
        //        SmtpMail.SmtpServer.Insert(0, "your hostname");
        //        MailMessage Msg = new MailMessage();
        //        Msg.To = "to address here";
        //        Msg.From = "from address here";
        //        Msg.Subject = "Crystal Report Attachment ";
        //        Msg.Body = "Crystal Report Attachment ";
        //        Msg.Attachments.Add(new MailAttachment(pdfFile));
        //        System.Web.Mail.SmtpMail.Send(Msg);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //protected void ExportAndEmail(ReportDocument crystalReport)
        //{

        //    using (MailMessage mm = new MailMessage("sender@gmail.com", "receiver@gmail.com"))
        //    {
        //        mm.Subject = "Crystal Report PDF example";
        //        mm.Body = "Crystal Report PDF example";
        //        mm.Attachments.Add(new Attachment(crystalReport.ExportToStream(ExportFormatType.PortableDocFormat), "Report.pdf"));
        //        mm.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.Host = "smtp.gmail.com";
        //        NetworkCredential credential = new NetworkCredential();
        //        credential.UserName = "sender@gmail.com";
        //        credential.Password = "xxxxx";
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = credential;
        //        smtp.Port = 587;
        //        smtp.EnableSsl = true;
        //        smtp.Send(mm);
        //    }
        //}

    }
}
