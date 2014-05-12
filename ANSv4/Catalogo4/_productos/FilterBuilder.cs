//
//       FILE:  FilterBuilder.cs
//
//     AUTHOR:  Kenneth Minear
//
//
//  COPYRIGHT:  Copyright 2011
//              KennWare Solutions LLC
//              7100 Bellaire Ave.
//              Windsor Heights, IA 50324 USA
//

using System;
using System.Data;
using System.Collections;
using System.Globalization;
using System.Windows.Forms;


namespace Catalogo._productos
{
    public class FilterBuilder
    {
        /// <summary>
        /// Create a filter for a DataGridView or ListView column
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        /// ColumnName,     the name of the column for the filter statement
        /// ColumnType,     the type of column (string, bool, datetime)
        /// ColumnValue,    the value to filter against, can contain special
        ///                 values such as "(Blanks)", "(NonBlanks)", "(False)", "(True)"
        /// ColumnOperator, can be "=", "<>", "<", "<=", ">", ">="
        /// </para>
        /// </remarks>
        /// 

        // Ordered Dictionary collections of column filter values
        private System.Collections.Specialized.OrderedDictionary comboItems =
            new System.Collections.Specialized.OrderedDictionary();


        
        public FilterBuilder()
        {

        }

        #region "Populate ComboBox with Columns for a filter"
        /// <summary>
        /// Modify a ComboBox with items for a filter
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        /// ComboBox,       the ComboBox control to poputlate (passed by reference)
        /// DataTable,      the DataTable of the underlying data
        /// ColumnName,     the name of the column in the DataTable to populate the ComboBox with
        /// </para>
        /// </remarks>
        /// 
        public void PopulateFilterColumns(ref ComboBox comboBox, DataTable dataTable, string colName)
        {
            // Make sure all components exist
            if (comboBox == null) { return; }
            if (dataTable == null) { return; }
            if (string.IsNullOrEmpty(colName)) { return; }

            PopulateFilter(dataTable, colName);
            String[] filterArray = new String[comboItems.Count];
            comboItems.Keys.CopyTo(filterArray, 0);
            comboBox.Items.Clear();
            comboBox.Items.AddRange(filterArray);
        }

        private void PopulateFilter(DataTable dataTable, string columnName)
        {
            string addValue;

            // Reset the filters dictionary and initialize some flags
            // to track whether special filter options are needed. 
            comboItems.Clear();
            Boolean containsBlanks = false;
            Boolean containsNonBlanks = false;

            // Initialize an ArrayList to store the values in their original
            // types. This enables the values to be sorted appropriately. 
            ArrayList list = new ArrayList(dataTable.Rows.Count);

            // Retrieve each value and add it to the ArrayList if it isn't
            // already present. 
            foreach (DataRow item in dataTable.Rows)
            {
                Object value = null;
                value = Convert.ToString(item[columnName]).Trim();

                // Skip empty values, but note that they are present. 
                if (value == null | value == DBNull.Value | value.ToString() == String.Empty)
                {
                    containsBlanks = true;
                    continue;
                }
                else
                {
                    // Add values to the ArrayList if they are not already there.
                    if (!list.Contains(value))
                    {
                        list.Add(value);
                    }
                
                }

            }

            // Sort the ArrayList. The default Sort method uses the IComparable 
            // implementation of the stored values so that string, numeric, and 
            // date values will all be sorted correctly. 
            list.Sort();

            // Convert each value in the ArrayList to its formatted representation
            // and store both the formatted and unformatted string representations
            // in the filters dictionary. 
            foreach (Object value in list)
            {
                String formattedValue = null;
                formattedValue = (string)value;

                if (String.IsNullOrEmpty(formattedValue))
                {
                    // Skip empty values, but note that they are present.
                    containsBlanks = true;
                }
                else if (!comboItems.Contains(formattedValue))
                {
                    // Note whether non-empty values are present. 
                    containsNonBlanks = true;

                    // For all non-empty values, add the formatted and 
                    // unformatted string representations to the filters 
                    // dictionary.

                    addValue = value.ToString();
                    addValue = addValue.Trim();
                    comboItems.Add(formattedValue, addValue);
                    //filters.Add(formattedValue, value.ToString());
                }
            }

            // Add special filter options to the filters dictionary
            // along with null values, since unformatted representations
            // are not needed. 
            comboItems.Insert(0, "(todos)", null);

            if (containsBlanks && containsNonBlanks)
            {
                //comboItems.Add("(Blanks)", null);
                //comboItems.Add("(NonBlanks)", null);
            }
        }
        #endregion

        #region "GetFilter"
        /// <summary>
        /// Gets the current filter value for a column in the filter string
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        /// FilterString,   the filter string
        /// ColumnName,     the name of the column in the DataTable to populate the ComboBox with
        /// </para>
        /// </remarks>
        /// 
        public string GetFilter(string filterString, string columnName)
        {
            int index1;
            int index2;
            int index3;

            string dataProperty;
            string colValue;

            dataProperty = columnName.Replace("]", @"\]");
            index1 = filterString.IndexOf(dataProperty);
             if (index1 == -1) // Not filtered
            {
                return "(todos)";
            }
            else
            {
                index2 = filterString.IndexOf("'", index1 + 1);
                index3 = filterString.IndexOf("'", index2 + 1);
                colValue = filterString.Substring(index2 + 1, index3 - index2 - 1);
                return colValue;
            }
        }
        #endregion

        #region "RemoveFilter"
        /// <summary>
        /// Removes the current filter in the filter string
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        /// FilterString,   the filter string
        /// ColumnName,     the name of the column in the DataTable to populate the ComboBox with
        /// </para>
        /// </remarks>
        /// 
        public void RemoveFilter(ref string filterString, string columnName)
        {
            int index1;
            int index2;
            int index3;

            // Get cdurrent filter
            string currentFilter = string.Empty;

            index1 = filterString.IndexOf(columnName);
            if (index1 == -1) // Not filtered
            {
                return;
            }
            else
            {
                index2 = filterString.IndexOf("'", index1 + 1);
                index3 = filterString.IndexOf("'", index2 + 1);
                currentFilter = filterString.Substring(index1 - 1, index3 - index1 + 2);
            }

            string currentAndFilterAnd = "AND " + currentFilter + " AND";
            string currentAndFilter = "AND " + currentFilter;
            string currentFilterAnd = currentFilter + " AND";

            string currentOrFilterOr = "OR " + currentFilter + " OR";
            string currentOrFilter = "OR " + currentFilter;
            string currentFilterOr = currentFilter + " OR";

            filterString = filterString.Replace(currentOrFilterOr, string.Empty);
            filterString = filterString.Replace(currentOrFilter, string.Empty);
            filterString = filterString.Replace(currentFilterOr, string.Empty);

            filterString = filterString.Replace(currentAndFilterAnd, string.Empty);
            filterString = filterString.Replace(currentAndFilter, string.Empty);
            filterString = filterString.Replace(currentFilterAnd, string.Empty);

            filterString = filterString.Replace(currentFilter, string.Empty);
            //return;
            // Recursion
            RemoveFilter(ref filterString, columnName);
        }
        #endregion

        #region "Apply Filter"
        /// <summary>
        /// Adds a filter for a column in the filter string
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        /// FilterString,   the filter string (passed by reference)
        /// ColName,        the name of the column 
        /// ColValue        the object value to add to the filter
        /// </para>
        /// </remarks>
        /// 
        public void ApplyFilter(ref string filterString, string colName, object colValue)
        {
            // Catch all - if value is "(All") just return, nothing to filter
            if ((Convert.ToString(colValue) == "(todos)")) { return; }

            string colFilter = addFilter(colName, colValue);
            if (colFilter.Length > 0)
            {
                if (string.IsNullOrEmpty(filterString))
                {
                    filterString += colFilter;
                }
                else
                {
                    filterString += " AND " + colFilter;
                }
            }
        }
        
        public void ApplyFilterStartDate(ref string filterString, string colName, object colValue)
        {
            string colFilter = addFilterStartDate(colName, colValue);
            if (string.IsNullOrEmpty(filterString))
            {
                filterString += colFilter;
            }
            else
            {
                filterString += " AND " + colFilter;
            }
        }

        public void ReplaceFilterStartDate(ref string filterString, string colName, object colValue)
        {
            string foundFilter = GetFilter(filterString, colName);
            if (foundFilter == "(todos)")
            {
                // Filter not present
            }
            else
            {
                // Filter already in filter string

            }
            string colFilter = addFilterStartDate(colName, colValue);
            if (string.IsNullOrEmpty(filterString))
            {
                filterString += colFilter;
            }
            else
            {
                filterString += " AND " + colFilter;
            }
        }

        private string addFilter(string colName, object colValue)
        {
            string columnFilter = string.Empty;
            Type dataType = colValue.GetType();

            if (dataType == typeof(String))
            {
                switch (colValue.ToString())
                {
                    case "(todos)":
                        // Do not filter this column
                        break;
                    case "(Blanks)":
                        columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') = 'NULLVALUE'";
                        break;
                    case "(NonBlanks)":
                        columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') <> 'NULLVALUE'";
                        break;
                    case "(False)":
                        //columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') = 'NULLVALUE'";
                        columnFilter = string.Format("[{0}] IS NOT NULL", colName);
                        break;
                    case "(True)":
                        //columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') <> 'NULLVALUE'";
                        columnFilter = string.Format("[{0}] IS NULL", colName);
                        break;
                    default:
                        if (colName == "(txtBuscar)")
                        {
                            columnFilter= whereTxtBuscar(((String)colValue).Replace("'", "''"));
                            //columnFilter = string.Format("[{0}] LIKE '%{1}%'", "N_Producto", ((String)colValue).Replace("'", "''"));
                        }
                        else if (colName == "Vigencia")
                        {
                            //columnFilter = "[Tipo]='prod_n' OR [Tipo]='apli_n'";
                            columnFilter = string.Format("[{0}]<={1}", colName, ((String)colValue).Replace("'", "''"));
                        }
                        else
                        {
                            columnFilter = string.Format("[{0}]='{1}'", colName, ((String)colValue).Replace("'", "''"));
                        }
                        break;
                        
                }
            }
            
            if (dataType == typeof(String) && colValue.ToString() == "todos") { return columnFilter; }
            
            if (dataType == typeof(DateTime))
            {
                DateTime dtm = Convert.ToDateTime(colValue);
                dtm = dtm.AddDays(1);
                //columnFilter = "[{0}]>='" + colValue + "' AND [{0}]< '" + FormatValue(dtm, dataType) + "'";
                //columnFilter = string.Format("[{0}]>='" + colValue + "' AND [{0}]< '" + FormatValue(dtm, dataType) + "'", colName);
                columnFilter = string.Format("[{0}]>='" + FormatValue(colValue, dataType) + "' AND [{0}]< '" + FormatValue(dtm, dataType) + "'", colName);
            }
            
            return columnFilter;
        }

        private string whereTxtBuscar(string txtBuscar)
        {
            bool filtro2 = false;
            bool filtro3 = false;

            if (txtBuscar.Trim().Substring(0, 2) == "++")
            {
                filtro3 = true;
                txtBuscar = txtBuscar.Trim().Substring(2);
            } 
            else if (txtBuscar.Trim().Substring(0, 1) == "+")
            {
                filtro2 = true;
                txtBuscar = txtBuscar.Trim().Substring(1);
            }

            string sqlWhere = null;
            string[] PalabrasClave = txtBuscar.Split(' ');
               
	        if (!string.IsNullOrEmpty(txtBuscar)) 
            {
                foreach (string palabra in PalabrasClave)
                {
                    if (!string.IsNullOrEmpty(palabra))
                    {
                        if (!string.IsNullOrEmpty(sqlWhere))
                        {
                            sqlWhere += " AND ";
                        }
                        if (filtro2)
                        {
                            sqlWhere += " (c_producto + ' ' +  Equivalencia + ' ' + Original LIKE '";
                            sqlWhere += palabra;
                            sqlWhere += "%')";
                        }
                        else if (filtro3)
                        {
                            sqlWhere += " (c_producto + ' ' +  Equivalencia + ' ' + Original LIKE '%";
                            sqlWhere += palabra;
                            sqlWhere += "%')";
                        }
                        else
                        {
                            sqlWhere += " (MiCodigo  + ' ' +  c_producto + ' ' + Equivalencia + ' ' + Marca + ' ' + Modelo + ' ' + Familia + ' ' + Linea + ' ' + n_producto + ' ' +  O_Producto + ' ' + Original + ' ' + Contiene + ' ' + ReemplazaA + ' ' + Motor  LIKE '%";
                            sqlWhere += palabra;
                            sqlWhere += "%')";
                        }
                    }
                }
	        }

            return sqlWhere;
        }


        public void ApplyFilter(ref string filterString, string colName, object colValue, object colValue2)
        {
            string colFilter = addFilter(colName, colValue, colValue2);
            if (string.IsNullOrEmpty(filterString))
            {
                filterString += colFilter;
            }
            else
            {
                filterString += " AND " + colFilter;
            }
        }

        // If you are passsing date values for filtering, you can
        // addFilter("ColumnX", SomeDate, DateTime.MaxValue) ==> returns rows with dates >= than SomeDate
        // addFilter("ColumnX", DateTime.MinValue, SomeDate) ==> returns rows with dates <= than SomeDate
        // addFilter("ColumnX", SomeDate, SomeDate2)         ==> returns rows >= first date and < second date
        private string addFilter(string colName, object colValue, object colValue2)
        {
            string columnFilter = "";
            Type dataType = colValue.GetType();
            Type dataType2 = colValue2.GetType();

            // Data Type of both values should match
            if (dataType2 != dataType) { return string.Empty; }

            if (dataType == typeof(String))
            {
                switch (colValue.ToString())
                {
                    case "(Blanks)":
                        columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') = 'NULLVALUE'";
                        break;
                    case "(NonBlanks)":
                        columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') <> 'NULLVALUE'";
                        break;
                    case "(False)":
                        //columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') = 'NULLVALUE'";
                        columnFilter = string.Format("[{0}] ' IS NOT NULL'", colName);
                        //columnFilter = string.Format("ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') = 'NULLVALUE'", colName);
                        break;
                    case "(True)":
                        //columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') <> 'NULLVALUE'";
                        columnFilter = string.Format("[{0}] ' IS NULL'", colName);
                        //columnFilter = string.Format("ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') <> 'NULLVALUE'", colName);
                        break;
                    default:
                        string columnFilter1 = string.Empty;
                        string columnFilter2 = string.Empty;
                        columnFilter1 = string.Format("[{0}]>='{1}'", colName, ((String)colValue).Replace("'", "''"));
                        columnFilter2 = string.Format("[{0}]<='{1}'", colName, ((String)colValue2).Replace("'", "''"));
                        columnFilter = columnFilter1 + " " + columnFilter2;
                        break;
                }
            }

            if (dataType == typeof(DateTime))
            {
                string columnFilter1 = string.Empty;
                string columnFilter2 = string.Empty;

                if (Convert.ToDateTime(colValue2) == DateTime.MaxValue)
                {
                    columnFilter1 = string.Format("[{0}]>='" + FormatValue(colValue, dataType) + "'", colName);
                    return columnFilter1;
                }

                if (Convert.ToDateTime(colValue) == DateTime.MinValue)
                {
                    columnFilter2 = string.Format("[{0}]<= '" + FormatValue(colValue2, dataType) + "'", colName);
                    return columnFilter2;
                }

                DateTime dtm = Convert.ToDateTime(colValue2);
                dtm = dtm.AddDays(1);
                columnFilter1 = string.Format("[{0}]>='" + FormatValue(colValue, dataType) + "'", colName);
                columnFilter2 = string.Format("[{0}]< '" + FormatValue(dtm, dataType) + "'", colName);
                columnFilter = columnFilter1 + " " + columnFilter2;
            }

            return columnFilter;
        }

        private string addFilterStartDate(string colName, object colValue)
        {
            string columnFilter = "";
            Type dataType = colValue.GetType();

            if (dataType == typeof(String))
            {
                switch (colValue.ToString())
                {
                    case "(Blanks)":
                        columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') = 'NULLVALUE'";
                        break;
                    case "(NonBlanks)":
                        columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') <> 'NULLVALUE'";
                        break;
                    // False/True used when databases don't support full boolean values,
                    // in this case, null = false and not null = true
                    case "(False)":
                        //columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') = 'NULLVALUE'";
                        columnFilter = string.Format("ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') = 'NULLVALUE'", colName);
                        break;
                    case "(True)":
                        //columnFilter = "ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') <> 'NULLVALUE'";
                        columnFilter = string.Format("ISNULL(CONVERT([{0}], 'System.String'),'NULLVALUE') <> 'NULLVALUE'",colName);
                        break;
                    default:
                        columnFilter = string.Format("[{0}]='{1}'", colName, ((String)colValue).Replace("'", "''"));
                        break;
                }
            }

            if (dataType == typeof(DateTime))
            {
                columnFilter = string.Format("[{0}]>='" + FormatValue(colValue, dataType) + "'", colName);
            }

            return columnFilter;
        }

        private string FormatValue(object value, Type targetType)
        {
            if (targetType == typeof(string))
            {
                return value.ToString();
            }
            if (targetType == typeof(bool))
            {
                return ((bool)value) ? "1" : "0";
            }
            if (targetType == typeof(DateTime))
            {
                //return ((DateTime)value).ToString("yyyy'-'MM'-'dd");
                return ((DateTime)value).ToString("MM'/'dd'/'yyyy");
            }
            // Numeric Types
            return ((IFormattable)value).ToString(null, NumberFormatInfo.InvariantInfo);
        }
        #endregion

        #region "Populate Filter"
        /// <summary>
        /// Updates and Ordered Dictionary with unique values for a column in a DataTable
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        /// filters,        an ordered dictionary that holds unique column values
        /// dtSource,       a DataTable that is the data source for a DataView
        /// colname,        the name of the column to populate for
        /// </para>
        /// </remarks>
        /// 
        public void PopulateFilter(ref System.Collections.Specialized.OrderedDictionary filters, DataTable dtSource, string columnName)
        {
            string addValue;
            string xMarca = "";
            string xColumnName = columnName;
            Boolean xEsMarcaModelo = false;

            if (columnName.StartsWith("_mm"))
            {
                xEsMarcaModelo = true;
                xColumnName = "Modelo";
                xMarca = columnName.Substring(3);
            }

            // Reset the filters dictionary and initialize some flags
            // to track whether special filter options are needed. 
            filters.Clear();
            Boolean containsBlanks = false;
            Boolean containsNonBlanks = false;

            // Initialize an ArrayList to store the values in their original
            // types. This enables the values to be sorted appropriately. 

// DIEGO!!!! ACA SE ME CAE SEGUIDO PORQUE dtSource=NULL ---------------------------------------------------//
            ArrayList list = new ArrayList(dtSource.Rows.Count);

            // Retrieve each value and add it to the ArrayList if it isn't
            // already present. 
            foreach (DataRow item in dtSource.Rows)
            {
                
                if (xEsMarcaModelo && item["Marca"].ToString()!=xMarca) continue;
                
                Object value = null;
                value = Convert.ToString(item[xColumnName]).Trim();

                // Skip empty values, but note that they are present. 
                if (value == null | value == DBNull.Value | value.ToString() == String.Empty)
                {
                    containsBlanks = true;
                    continue;
                }
                else
                {
                    // Add values to the ArrayList if they are not already there.
                    if (!list.Contains(value))
                    {
                        list.Add(value);
                    }

                }
                
            }

            // Sort the ArrayList. The default Sort method uses the IComparable 
            // implementation of the stored values so that string, numeric, and 
            // date values will all be sorted correctly. 
            list.Sort();

            // Convert each value in the ArrayList to its formatted representation
            // and store both the formatted and unformatted string representations
            // in the filters dictionary. 
            foreach (Object value in list)
            {
                String formattedValue = null;
                formattedValue = (string)value;

                if (String.IsNullOrEmpty(formattedValue))
                {
                    // Skip empty values, but note that they are present.
                    containsBlanks = true;
                }
                else if (!filters.Contains(formattedValue))
                {
                    // Note whether non-empty values are present. 
                    containsNonBlanks = true;

                    // For all non-empty values, add the formatted and 
                    // unformatted string representations to the filters 
                    // dictionary.

                    addValue = value.ToString();
                    addValue = addValue.Trim();
                    filters.Add(formattedValue, addValue);
                    //filters.Add(formattedValue, value.ToString());
                }
            }

            // Add special filter options to the filters dictionary
            // along with null values, since unformatted representations
            // are not needed. 
            filters.Insert(0, "(todos)", null);

            if (containsBlanks && containsNonBlanks)
            {
                //filters.Add("(Blanks)", null);
                //filters.Add("(NonBlanks)", null);
            }
        }
        #endregion
    }
}