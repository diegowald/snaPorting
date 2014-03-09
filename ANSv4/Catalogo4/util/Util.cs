using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.Funciones
{
    class util
    {
        internal static void CargarCombo(ref System.Windows.Forms.ToolStripComboBox combo, string Tabla, string Campo1, string Campo2, string strConexion, string Condicion = "ALL", string Orden = "NONE", bool AceptaNulo = false, bool Concatena = false)
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConexion);

            //System.Data.OleDb.OleDbDataReader rdr = Catalogo.Funciones.oleDbFunciones.xGetDR(ref conn, Tabla,Condicion,Orden );
            System.Data.OleDb.OleDbDataReader rdr = Catalogo.Funciones.oleDbFunciones.Comando(ref conn, " SELECT * FROM " + Tabla + " WHERE " + Condicion + " ORDER BY " + Orden);

            Catalogo.Funciones.xListItemComboBox newListItem = new Catalogo.Funciones.xListItemComboBox();

            newListItem = new Catalogo.Funciones.xListItemComboBox("- seleccione -", 0);

            combo.Items.Add(newListItem);

            // Populate the Control 
            while (rdr.Read())
            {
                newListItem = new Catalogo.Funciones.xListItemComboBox(rdr[Campo1].ToString(), int.Parse(rdr[Campo2].ToString()));
                combo.Items.Add(newListItem);
            }

            combo.SelectedIndex = 0;

        }

        internal static void CargarCombo(ref System.Windows.Forms.ComboBox combo, string Tabla, string Campo1, string Campo2, string strConexion, string Condicion = "ALL", string Orden = "NONE", bool AceptaNulo = false, bool Concatena = false)
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConexion);

            System.Data.OleDb.OleDbDataReader rdr = Catalogo.Funciones.oleDbFunciones.xGetDR(ref conn, Tabla, Condicion, Orden);

            Catalogo.Funciones.xListItemComboBox newListItem = new Catalogo.Funciones.xListItemComboBox();

            newListItem = new Catalogo.Funciones.xListItemComboBox("- seleccione -", 0);

            combo.Items.Add(newListItem);

            // Populate the Control 
            while (rdr.Read())
            {
                newListItem = new Catalogo.Funciones.xListItemComboBox((string)(rdr[Campo1]), (int)(rdr[Campo2]));
                combo.Items.Add(newListItem);
            }

            combo.SelectedIndex = 0;

        }


        internal static void CargaCombo2(ref System.Windows.Forms.ComboBox Combo, string Tabla, string Campo1, string Campo2, string strConexion, string Condicion = "ALL", string Orden = "NONE", bool AceptaNulo = false, bool Concatena = false)
        {
           
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConexion);
            
            System.Data.DataSet ds = new System.Data.DataSet();

            Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            ds = Funciones.oleDbFunciones.xGetDS(ref conn, Tabla, Condicion, Orden);

            if (ds.Tables[Tabla].Rows.Count > 0)
            {
                if (AceptaNulo)
                {
                    System.Data.DataRow dr = null;
                    dr = ds.Tables[Tabla].NewRow();
                    dr[0] = 0;
                    dr[1] = "- seleccione -";
                    ds.Tables[Tabla].Rows.InsertAt(dr, 0);
                }

                Combo.DataSource = ds.Tables[Tabla];

                Combo.DisplayMember = Campo1;
                //CStr(Trim(Tabla) & "." & Trim(Campo1))
                Combo.ValueMember = Campo2;
                //CStr(Trim(Tabla) & "." & Trim(Campo2))

                //Combo.DataBindings.Add("SelectedValue", ds, CStr(Trim(Tabla) & "." & Trim(Campo2)))
                //Combo.DataBindings.Add("Text", ds, CStr(Trim(Tabla) & "." & Trim(Campo1)))

                Combo.SelectedIndex = 0;

            }

            ds = null;

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


        internal static void BuscarIndiceEnCombo(ref  System.Windows.Forms.ComboBox Combo, string strBuscar, bool EsList)
        {

            int iFoundIndex = 0;
            iFoundIndex = Combo.FindStringExact(strBuscar);
            Combo.SelectedIndex = iFoundIndex;

        }

        
        internal static void AutoCompleteCombo_Leave(ref  System.Windows.Forms.ComboBox cbo)
        {

            int iFoundIndex = 0;
            iFoundIndex = cbo.FindStringExact(cbo.Text);
            cbo.SelectedIndex = iFoundIndex;

        }

    }
}
