using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace Catalogo.Funciones
{
    class util
    {

        const string m_sMODULENAME_ = "util";



        internal static void BuscarIndiceEnCombo(ref System.Windows.Forms.ComboBox combo, string strBuscar, bool EsList)
        {
            int iFoundIndex = 0;
            iFoundIndex = combo.FindStringExact(strBuscar);
            combo.SelectedIndex = iFoundIndex;
        }

        //acá el combo debe ser System.Windows.Forms.ToolStripComboBox
        internal static void CargaCombo(ref System.Data.OleDb.OleDbConnection conexion, ref System.Windows.Forms.ToolStripComboBox combo, string Tabla, string Campo1, string Campo2, string Condicion = "ALL", string Orden = "NONE", bool AceptaNulo = true, bool Concatena = false)
        {
            combo.Enabled = false;

            System.Data.DataTable  dt = Catalogo.Funciones.oleDbFunciones.xGetDt(ref conexion, Tabla, Condicion, Orden, Campo1 + "," + Campo2);
           
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
        internal static void CargaCombo(ref System.Data.OleDb.OleDbConnection conexion, ref System.Windows.Forms.ComboBox combo, string Tabla, string Campo1, string Campo2, string Condicion = "ALL", string Orden = "NONE", bool AceptaNulo = false, bool Concatena = false)
        {
            combo.Enabled = false;

            System.Data.DataTable dt = Catalogo.Funciones.oleDbFunciones.xGetDt(ref conexion, Tabla, Condicion, Orden, Campo1 + "," + Campo2);

            if (AceptaNulo)
            {
                System.Data.DataRow dr = dt.NewRow();
                dr[1] = 0;
                dr[0] = "- seleccione -";
                dt.Rows.InsertAt(dr, 0);
            }

            // Populate the Control 
            combo.DisplayMember = Campo1;
            combo.ValueMember = Campo2;
            combo.DataSource = dt.DefaultView;

            combo.SelectedIndex = 0;
            combo.Enabled = true;
        }

        internal static void GrabarLVColumnas(ref System.Windows.Forms.ListView MyListview1)
        {

            foreach (System.Windows.Forms.ColumnHeader lvwcolumn in MyListview1.Columns)
            {
                Funciones.modINIs.INIWrite(System.IO.Directory.GetCurrentDirectory() + "\\" + "setting.ini", MyListview1.Name, lvwcolumn.Text, Convert.ToString(lvwcolumn.Width));
            }

        }

        internal static void LeerLVColumnas(ref System.Windows.Forms.ListView MyListview1)
        {
            int i = 0;


            foreach (System.Windows.Forms.ColumnHeader lvwcolumn in MyListview1.Columns)
            {
                i = Convert.ToInt32(Funciones.modINIs.INIRead(System.IO.Directory.GetCurrentDirectory() + "\\" + "setting.ini", MyListview1.Name, lvwcolumn.Text, "0"));
                if (i > 0)
                {
                    lvwcolumn.Width = i;
                }
            }

        }

        internal static void CargarLV(ref  System.Windows.Forms.ListView MyListview1, ref System.Data.SqlClient.SqlDataReader MyData, string MiColor = "gris")
        {
            System.Windows.Forms.ColumnHeader lvwColumn = default(System.Windows.Forms.ColumnHeader);
            System.Windows.Forms.ListViewItem itmListItem = default(System.Windows.Forms.ListViewItem);
            string strTest = null;
            int i = 0;
            short shtCntr = 0;

            MyListview1.Clear();

            for (shtCntr = 0; shtCntr <= Convert.ToInt16(MyData.FieldCount - 1); shtCntr++)
            {
                lvwColumn = new System.Windows.Forms.ColumnHeader();

                switch (MyData.GetProviderSpecificFieldType(shtCntr).FullName.ToLower())
                {
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

                for (shtCntr = 1; shtCntr <= Convert.ToInt16(MyData.FieldCount - 1); shtCntr++)
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
    
    }
}
