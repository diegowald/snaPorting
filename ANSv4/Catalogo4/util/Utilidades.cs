//using Microsoft.VisualBasic;
using System;
//using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
//using System.Diagnostics;
using System.Windows.Forms;


namespace Catalogo.Funciones
{

	public class util
	{

		public static void CargarComboDR(ref System.Windows.Forms.ComboBox Combo, string Tabla, string Campo1, string Campo2, string strConexion, string Condicion = "ALL", string Orden = "NONE", bool AceptaNulo = false, bool Concatena = false)
		{
           
           OleDbConnection conn = new System.Data.OleDb.OleDbConnection(strConexion);

           OleDbDataReader rdr = null;

			Catalogo.Funciones.xListItemComboBox newListItem = new Catalogo.Funciones.xListItemComboBox();

			newListItem = new Catalogo.Funciones.xListItemComboBox("- seleccione -", 0);

			Combo.Items.Add(newListItem);

			// Populate the Control 
			while (rdr.Read()) {
				newListItem = new Catalogo.Funciones.xListItemComboBox(Convert.ToString(rdr[Campo1]), Convert.ToInt32(rdr[Campo2]));
				Combo.Items.Add(newListItem);
			}

			Combo.SelectedIndex = 0;

		}


        //public static void CargarCombo(ref ComboBox Combo, string Tabla, string Campo1, string Campo2, string strConexion, string Condicion = "ALL", string Orden = "NONE", bool AceptaNulo = false, bool Concatena = false)
        //{
        //    DataSet ds = new DataSet();

        //    Combo.DropDownStyle = ComboBoxStyle.DropDownList;

        //    ds = Funciones.adoModulo.xGetDS(Funciones.adoModulo.GetConn(strConexion), Tabla, Condicion, Orden);

        //    if (ds.Tables[Tabla].Rows.Count > 0) {
        //        if (AceptaNulo) {
        //            DataRow dr = null;
        //            dr = ds.Tables[Tabla].NewRow();
        //            dr[0] = 0;
        //            dr[1] = "- seleccione -";
        //            ds.Tables[Tabla].Rows.InsertAt(dr, 0);
        //        }

        //        Combo.DataSource = ds.Tables[Tabla];

        //        Combo.DisplayMember = Campo1;
        //        //CStr(Trim(Tabla) & "." & Trim(Campo1))
        //        Combo.ValueMember = Campo2;
        //        //CStr(Trim(Tabla) & "." & Trim(Campo2))



        //        //Combo.DataBindings.Add("SelectedValue", ds, CStr(Trim(Tabla) & "." & Trim(Campo2)))
        //        //Combo.DataBindings.Add("Text", ds, CStr(Trim(Tabla) & "." & Trim(Campo1)))

        //        Combo.SelectedIndex = 0;

        //    }

        //    ds = null;

        //}

        ////Public Shared Sub SizeLastColumn(ByVal MyListview1 As ListView)

        ////    MyListview1.Columns(MyListview1.Columns.Count - 1).Width = -1

        ////End Sub


        //public static void GrabarLVColumnas(ref ListView MyListview1)
        //{

        //    foreach (ColumnHeader lvwcolumn in MyListview1.Columns) {
        //        Funciones.modINIs.INIWrite(Application.StartupPath + "\\" + "setting.ini", MyListview1.Name, lvwcolumn.Text, Convert.ToString(lvwcolumn.Width));
        //    }

        //}


        //public static void LeerLVColumnas(ref ListView MyListview1)
        //{
        //    int i = 0;


        //    foreach (ColumnHeader lvwcolumn in MyListview1.Columns) {
        //        i = Convert.ToInt32(Funciones.modINIs.INIRead(Application.StartupPath + "\\" + "setting.ini", MyListview1.Name, lvwcolumn.Text, "0"));
        //        if (i > 0) {
        //            lvwcolumn.Width = i;
        //        }
        //    }

        //}


        //public static void CargarLV(ref ListView MyListview1, ref System.Data.SqlClient.SqlDataReader MyData, string MiColor = "gris")
        //{
        //    ColumnHeader lvwColumn = default(ColumnHeader);
        //    ListViewItem itmListItem = default(ListViewItem);
        //    string strTest = null;
        //    int i = 0;
        //    short shtCntr = 0;

        //    MyListview1.Clear();

        //    for (shtCntr = 0; shtCntr <= Convert.ToInt16(MyData.FieldCount() - 1); shtCntr++) {
        //        lvwColumn = new ColumnHeader();

        //        switch (MyData.GetProviderSpecificFieldType(shtCntr).FullName.ToLower()) {
        //            case "system.data.sqltypes.sqlstring":
        //                lvwColumn.TextAlign = HorizontalAlignment.Left;
        //                break;
        //            default:
        //                lvwColumn.TextAlign = HorizontalAlignment.Right;
        //                break;
        //        }

        //        if (MyData.GetName(shtCntr).ToLower() == "codigo") {
        //            lvwColumn.TextAlign = HorizontalAlignment.Right;
        //        }

        //        lvwColumn.Text = MyData.GetName(shtCntr);

        //        MyListview1.Columns.Add(lvwColumn);
        //    }

        //    while (MyData.Read()) {
        //        i = i + 1;
        //        itmListItem = new ListViewItem();
        //        strTest = Convert.ToString((MyData.IsDBNull(0) ? "" : MyData.GetValue(0)));

        //        itmListItem.Text = strTest;

        //        for (shtCntr = 1; shtCntr <= Convert.ToInt16(MyData.FieldCount() - 1); shtCntr++) {
        //            if (MyData.IsDBNull(shtCntr)) {
        //                itmListItem.SubItems.Add("");
        //            } else {
        //                itmListItem.SubItems.Add(Strings.Trim(Convert.ToString(MyData.GetValue(shtCntr))));
        //            }
        //        }

        //        if (i % 2 == 0) {
        //            itmListItem.BackColor = Color.White;

        //        } else {
        //            if (MiColor == "amarillo") {
        //                itmListItem.BackColor = Color.LightYellow;
        //            } else if (MiColor == "gris") {
        //                itmListItem.BackColor = Color.LightGray;
        //            }

        //        }
        //        MyListview1.Items.Add(itmListItem);

        //    }

        //    LeerLVColumnas(ref MyListview1);

        //}


        //public static void BuscarIndiceEnCombo(ref ComboBox Combo, string strBuscar, bool EsList)
        //{
        //    //Dim aIndex As Integer

        //    //For aIndex = 0 To Combo.Items.Count - 1
        //    //    If LCase(CType(Combo.Items(aIndex)(1), String).Trim) = LCase(strBuscar.Trim) Then
        //    //        Combo.SelectedIndex = aIndex
        //    //        Exit For
        //    //    End If
        //    //Next

        //    //If aIndex >= Combo.Items.Count Then Combo.SelectedIndex = -1

        //    int iFoundIndex = 0;
        //    iFoundIndex = Combo.FindStringExact(strBuscar);
        //    Combo.SelectedIndex = iFoundIndex;

        //}


        //public static void AutoCompleteCombo_KeyUp(ref ComboBox cbo, KeyEventArgs e)
        //{
        //    string sTypedText = null;
        //    int iFoundIndex = 0;
        //    object oFoundItem = null;
        //    string sFoundText = null;
        //    string sAppendText = null;

        //    //Allow select keys without Autocompleting
        //    switch (e.KeyCode) {
        //        case Keys.Back:
        //        case Keys.Left:
        //        case Keys.Right:
        //        case Keys.Up:
        //        case Keys.Delete:
        //        case Keys.Down:
        //            return;
        //    }

        //    //Get the Typed Text and Find it in the list
        //    sTypedText = cbo.Text;
        //    iFoundIndex = cbo.FindString(sTypedText);

        //    //If we found the Typed Text in the list then Autocomplete

        //    if (iFoundIndex >= 0) {
        //        //Get the Item from the list (Return Type depends if Datasource was bound 
        //        // or List Created)
        //        oFoundItem = cbo.Items(iFoundIndex);

        //        //Use the ListControl.GetItemText to resolve the Name in case the Combo 
        //        // was Data bound
        //        sFoundText = cbo.GetItemText(oFoundItem);

        //        //Append then found text to the typed text to preserve case
        //        sAppendText = sFoundText.Substring(sTypedText.Length);
        //        cbo.Text = sTypedText + sAppendText;

        //        //Select the Appended Text
        //        cbo.SelectionStart = sTypedText.Length;
        //        cbo.SelectionLength = sAppendText.Length;

        //    }

        //}


        //public static void AutoComplete(ref ComboBox cbo, System.Windows.Forms.KeyEventArgs e)
        //{
        //    // Call this from your form passing in the name 
        //    // of your combobox and the event arg:
        //    // AutoComplete(cboState, e)
        //    int iIndex = 0;
        //    string sActual = null;
        //    string sFound = null;
        //    bool bMatchFound = false;

        //    //check if the text is blank or not, if not then only proceed
        //    //if the text is not blank then only proceed
        //    if (!string.IsNullOrEmpty(cbo.Text)) {

        //        // If backspace then remove the last character 
        //        // that was typed in and try to find 
        //        // a match. note that the selected text from the 
        //        // last character typed in to the 
        //        // end of the combo text field will also be deleted.
        //        if (e.KeyCode == Keys.Back) {
        //            cbo.Text = Strings.Mid(cbo.Text, 1, Strings.Len(cbo.Text) - 1);
        //           String.
        //        }

        //        // Do nothing for some keys such as navigation keys...
        //        if (((e.KeyCode == Keys.Left) | (e.KeyCode == Keys.Right) | (e.KeyCode == Keys.Up) | (e.KeyCode == Keys.Down) | (e.KeyCode == Keys.PageUp) | (e.KeyCode == Keys.PageDown) | (e.KeyCode == Keys.Home) | (e.KeyCode == Keys.End))) {
        //            return;
        //        }


        //        do {
        //            // Store the actual text that has been typed.
        //            sActual = cbo.Text;
        //            // Find the first match for the typed value.
        //            iIndex = cbo.FindString(sActual);
        //            // Get the text of the first match.
        //            // if index > -1 then a match was found.

        //            //** FOUND SECTION **
        //            if ((iIndex > -1)) {
        //                sFound = cbo.Items(iIndex).ToString();
        //                // Select this item from the list.
        //                cbo.SelectedIndex = iIndex;
        //                // Select the portion of the text that was automatically
        //                // added so that additional typing will replace it.
        //                cbo.SelectionStart = sActual.Length;
        //                cbo.SelectionLength = sFound.Length;
        //                bMatchFound = true;
        //            //** NOT FOUND SECTION **
        //            } else {

        //                //if there isn't a match and the text typed in is only 1 character 
        //                //or nothing then just select the first entry in the combo box.

        //                if (sActual.Length == 1 | sActual.Length == 0) {
        //                    cbo.SelectedIndex = 0;
        //                    cbo.SelectionStart = 0;
        //                    cbo.SelectionLength = Strings.Len(cbo.Text);
        //                    bMatchFound = true;


        //                } else {
        //                    //if there isn't a match for the text typed in then 
        //                    //remove the last character of the text typed in
        //                    //and try to find a match.

        //                    //************************** NOTE **************************
        //                    //COMMENT THE FOLLOWING THREE LINES AND UNCOMMENT 
        //                    //THE (bMatchFound = True) LINE 
        //                    //INCASE YOU WANT TO ALLOW TEXTS TO BE TYPED IN
        //                    // WHICH ARE NOT IN THE LIST. ELSE IF 
        //                    //YOU WANT TO RESTRICT THE USER TO TYPE TEXTS WHICH ARE 
        //                    //NOT IN THE LIST THEN LEAVE THE FOLLOWING THREE LINES AS IT IS
        //                    //AND COMMENT THE (bMatchFound = True) LINE.
        //                    //***********************************************************
        //                    ///////// THREE LINES SECTION STARTS HERE ///////////
        //                    cbo.SelectionStart = sActual.Length - 1;
        //                    cbo.SelectionLength = sActual.Length - 1;
        //                    cbo.Text = Strings.Mid(cbo.Text, 1, Strings.Len(cbo.Text) - 1);
        //                    ///////// THREE LINES SECTION ENDS HERE /////////////
        //                    //bMatchFound = True

        //                }

        //            }

        //        } while (!(bMatchFound));

        //    }

        //}


        //public static int AutoCompleteCombo_Leave(ref ComboBox cbo)
        //{

        //    int iFoundIndex = 0;
        //    iFoundIndex = cbo.FindStringExact(cbo.Text);
        //    cbo.SelectedIndex = iFoundIndex;

        //}

	}

}

 