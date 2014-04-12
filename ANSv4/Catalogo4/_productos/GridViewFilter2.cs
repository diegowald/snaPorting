using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Catalogo.Funciones.emitter_receiver;


namespace Catalogo._productos
{
    public partial class GridViewFilter2 : UserControl,
        Funciones.emitter_receiver.IReceptor<string>,  // Para recibir el filtro de datos
        Funciones.emitter_receiver.IReceptor<float>, // Para recibir los porcentajes
        Funciones.emitter_receiver.IEmisor<DataGridViewRow>, // Para enviar el registro seleccionado
        Funciones.emitter_receiver.IEmisor2<util.Pair<int, int>>, // Para enviar la cantidad de registros encontrados.
        Funciones.emitter_receiver.IEmisor3<_pedidos.PedidosHelper.Acciones> // Para enviar acciones al pedido desde la grilla de productos.
    {

        private enum CCol
        {
            cSemaforo = 0,
            cCodigo = 1,
            cLinea = 2,
            cPrecio = 3,
            cFamilia = 4,
            cMarca = 5,
            cModelo = 6,
            cDescripcion = 7,
            cMotor = 8,
            cAño = 9,
            cMedidas = 10,
            cReemplazaA = 11,
            cContiene = 12,
            cEquivalencia = 13,
            cOriginal = 14,
            cPlista = 15,
            cPoferta = 16,
            cRotacion = 17,
            cEvolucion = 18,
            cPorclinea = 19,
            cId = 20,
            cControl = 21,
            cCodigoAns = 22,
            cMiCodigo = 23,
            cSuspendido = 24,
            cOfertaCantidad = 25,
            cTipo = 26,
            cVigencia = 27
        }

        private bool dataGridIdle = false;

        private DataTable dtProducts = new DataTable();
        private DataView dvProducts = new DataView();
        //private DataTable useTable = new DataTable();
        private bool xAplicoPorcentajeLinea = false;
        private float porcentajeLinea;

        private string filterString = string.Empty;

        private int currentRowCount = 0;
        private int dataRowCount = 0;

        // Dropdown Filter Collections
        private System.Collections.Specialized.OrderedDictionary Filter_Linea =
                   new System.Collections.Specialized.OrderedDictionary();
        private System.Collections.Specialized.OrderedDictionary Filter_Familia =
                          new System.Collections.Specialized.OrderedDictionary();
        private System.Collections.Specialized.OrderedDictionary Filter_Marca =
                         new System.Collections.Specialized.OrderedDictionary();
        private System.Collections.Specialized.OrderedDictionary Filter_Modelo =
                        new System.Collections.Specialized.OrderedDictionary();
        private System.Collections.Specialized.OrderedDictionary Filter_Otros =
                      new System.Collections.Specialized.OrderedDictionary();


        public GridViewFilter2()
        {

            InitializeComponent();          
            preload.Preloader.instance.productos.onWorkFinished += dataReady;

            xCargarDataControl();

        }

        public void onRecibir(string dato)
        {
            filterString = dato;
            loadDataGridView();
        }

        public void onRecibir(float dato)
        {
            porcentajeLinea = dato;
            loadDataGridView();
        }


        /// <summary>
        /// DIEGO PABLO OJO!!!!!!
        /// </summary>
        //private Funciones.emitter_receiver.emisorHandler<DataGridViewRow> _emisor;
        public Funciones.emitter_receiver.emisorHandler<DataGridViewRow> emisor
        {
            get;
            set;
        }

        public emisorHandler<util.Pair<int, int>> emisor2
        {
            get;
            set;
        }

        private void xCargarDataControl()
        {
            Cursor.Current = Cursors.WaitCursor;
            preload.Preloader.instance.productos.execute();
        }

       private void dataReady(System.Data.DataTable dataTable)
        {
            dtProducts = dataTable;

            // Load the DataGridView            
            loadDataGridView();
            Cursor.Current = Cursors.Default;
        }

        private void loadDataGridView()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            dataGridIdle = false;
            dataGridView1.SuspendLayout();
            // If rows in grid already exist, clear them
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            // For this example, make it a read-only datagrid
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView1.RowHeadersWidth = 4;
            dataGridView1.RowHeadersVisible = true;

            //Set AutoGenerateColumns False
            dataGridView1.AutoGenerateColumns = false;

            //Set Columns Count 
            dataGridView1.ColumnCount = 28;

            //Add Columns
            dataGridView1.Columns[(int)CCol.cSemaforo].Name = "Existencia";
            dataGridView1.Columns[(int)CCol.cSemaforo].HeaderText = "Existencia";

            dataGridView1.Columns[(int)CCol.cCodigo].Name = "C_Producto";
            dataGridView1.Columns[(int)CCol.cCodigo].HeaderText = "Código";
            dataGridView1.Columns[(int)CCol.cCodigo].DataPropertyName = "C_Producto";

            dataGridView1.Columns[(int)CCol.cLinea].Name = "Linea";
            dataGridView1.Columns[(int)CCol.cLinea].HeaderText = "Línea";
            dataGridView1.Columns[(int)CCol.cLinea].DataPropertyName = "Linea";

            dataGridView1.Columns[(int)CCol.cFamilia].Name = "Familia";
            dataGridView1.Columns[(int)CCol.cFamilia].HeaderText = "Familia";
            dataGridView1.Columns[(int)CCol.cFamilia].DataPropertyName = "Familia";

            dataGridView1.Columns[(int)CCol.cMarca].Name = "Marca";
            dataGridView1.Columns[(int)CCol.cMarca].HeaderText = "Marca";
            dataGridView1.Columns[(int)CCol.cMarca].DataPropertyName = "Marca";

            dataGridView1.Columns[(int)CCol.cModelo].Name = "Modelo";
            dataGridView1.Columns[(int)CCol.cModelo].HeaderText = "Modelo";
            dataGridView1.Columns[(int)CCol.cModelo].DataPropertyName = "Modelo";

            dataGridView1.Columns[(int)CCol.cDescripcion].Name = "N_Producto";
            dataGridView1.Columns[(int)CCol.cDescripcion].HeaderText = "Descripción";
            dataGridView1.Columns[(int)CCol.cDescripcion].DataPropertyName = "N_Producto";

            dataGridView1.Columns[(int)CCol.cMotor].Name = "Motor";
            dataGridView1.Columns[(int)CCol.cMotor].HeaderText = "Motor";
            dataGridView1.Columns[(int)CCol.cMotor].DataPropertyName = "Motor";

            dataGridView1.Columns[(int)CCol.cAño].Name = "Año";
            dataGridView1.Columns[(int)CCol.cAño].HeaderText = "Año";
            dataGridView1.Columns[(int)CCol.cAño].DataPropertyName = "Año";

            dataGridView1.Columns[(int)CCol.cMedidas].Name = "O_Producto";
            dataGridView1.Columns[(int)CCol.cMedidas].HeaderText = "Medidas";
            dataGridView1.Columns[(int)CCol.cMedidas].DataPropertyName = "O_Producto";

            dataGridView1.Columns[(int)CCol.cReemplazaA].Name = "ReemplazaA";
            dataGridView1.Columns[(int)CCol.cReemplazaA].HeaderText = "Reemplaza A";
            dataGridView1.Columns[(int)CCol.cReemplazaA].DataPropertyName = "ReemplazaA";

            dataGridView1.Columns[(int)CCol.cContiene].Name = "Contiene";
            dataGridView1.Columns[(int)CCol.cContiene].HeaderText = "Contiene";
            dataGridView1.Columns[(int)CCol.cContiene].DataPropertyName = "Contiene";

            dataGridView1.Columns[(int)CCol.cEquivalencia].Name = "Equivalencia";
            dataGridView1.Columns[(int)CCol.cEquivalencia].HeaderText = "Equivalencia";
            dataGridView1.Columns[(int)CCol.cEquivalencia].DataPropertyName = "Equivalencia";

            dataGridView1.Columns[(int)CCol.cOriginal].Name = "Original";
            dataGridView1.Columns[(int)CCol.cOriginal].HeaderText = "Original";
            dataGridView1.Columns[(int)CCol.cOriginal].DataPropertyName = "Original";

            dataGridView1.Columns[(int)CCol.cPrecio].Name = "Precio";
            dataGridView1.Columns[(int)CCol.cPrecio].HeaderText = "Precio";
            dataGridView1.Columns[(int)CCol.cPrecio].DataPropertyName = "Precio";

            dataGridView1.Columns[(int)CCol.cPlista].Name = "PrecioLista";
            dataGridView1.Columns[(int)CCol.cPlista].HeaderText = "$ Lista";
            dataGridView1.Columns[(int)CCol.cPlista].DataPropertyName = "PrecioLista";

            dataGridView1.Columns[(int)CCol.cPoferta].Name = "PrecioOferta";
            dataGridView1.Columns[(int)CCol.cPoferta].HeaderText = "$ Oferta";
            dataGridView1.Columns[(int)CCol.cPoferta].DataPropertyName = "PrecioOferta";

            dataGridView1.Columns[(int)CCol.cPorclinea].Name = "LineaPorcentaje";
            dataGridView1.Columns[(int)CCol.cPorclinea].HeaderText = "% Línea";
            dataGridView1.Columns[(int)CCol.cPorclinea].DataPropertyName = "LineaPorcentaje";

            dataGridView1.Columns[(int)CCol.cRotacion].Name = "Abc";
            dataGridView1.Columns[(int)CCol.cRotacion].HeaderText = "Rotación";
            dataGridView1.Columns[(int)CCol.cRotacion].DataPropertyName = "Abc";

            dataGridView1.Columns[(int)CCol.cEvolucion].Name = "Alerta";
            dataGridView1.Columns[(int)CCol.cEvolucion].HeaderText = "Evolución";
            dataGridView1.Columns[(int)CCol.cEvolucion].DataPropertyName = "Evolucion";
            dataGridView1.Columns[(int)CCol.cEvolucion].Visible = false;

            dataGridView1.Columns[(int)CCol.cVigencia].Name = "Vigencia";
            dataGridView1.Columns[(int)CCol.cVigencia].HeaderText = "Vigencia";
            dataGridView1.Columns[(int)CCol.cVigencia].DataPropertyName = "Vigencia";
            dataGridView1.Columns[(int)CCol.cVigencia].Visible = false;

            dataGridView1.Columns[(int)CCol.cId].Name = "ID";
            dataGridView1.Columns[(int)CCol.cId].HeaderText = "ID";
            dataGridView1.Columns[(int)CCol.cId].DataPropertyName = "ID";
            dataGridView1.Columns[(int)CCol.cId].Visible = false;

            dataGridView1.Columns[(int)CCol.cControl].Name = "Control";
            dataGridView1.Columns[(int)CCol.cControl].HeaderText = "Control";
            dataGridView1.Columns[(int)CCol.cControl].DataPropertyName = "Control";
            dataGridView1.Columns[(int)CCol.cControl].Visible = false;

            dataGridView1.Columns[(int)CCol.cCodigoAns].Name = "CodigoAns";
            dataGridView1.Columns[(int)CCol.cCodigoAns].HeaderText = "C_Producto";
            dataGridView1.Columns[(int)CCol.cCodigoAns].DataPropertyName = "CodigoAns";
            dataGridView1.Columns[(int)CCol.cCodigoAns].Visible = false;

            dataGridView1.Columns[(int)CCol.cMiCodigo].Name = "MiCodigo";
            dataGridView1.Columns[(int)CCol.cMiCodigo].HeaderText = "MiCodigo";
            dataGridView1.Columns[(int)CCol.cMiCodigo].DataPropertyName = "MiCodigo";
            dataGridView1.Columns[(int)CCol.cMiCodigo].Visible = false;

            dataGridView1.Columns[(int)CCol.cSuspendido].Name = "Suspendido";
            dataGridView1.Columns[(int)CCol.cSuspendido].HeaderText = "Suspendido";
            dataGridView1.Columns[(int)CCol.cSuspendido].DataPropertyName = "Suspendido";
            dataGridView1.Columns[(int)CCol.cSuspendido].Visible = false;

            dataGridView1.Columns[(int)CCol.cOfertaCantidad].Name = "OfertaCantidad";
            dataGridView1.Columns[(int)CCol.cOfertaCantidad].HeaderText = "Oferta.Ca.Mín.";
            dataGridView1.Columns[(int)CCol.cOfertaCantidad].DataPropertyName = "OfertaCantidad";

            dataGridView1.Columns[(int)CCol.cTipo].Name = "Tipo";
            dataGridView1.Columns[(int)CCol.cTipo].HeaderText = "Tipo";
            dataGridView1.Columns[(int)CCol.cTipo].DataPropertyName = "Tipo";
            dataGridView1.Columns[(int)CCol.cTipo].Visible = false;

            // Save the row count in the original datatable
            dataRowCount = dtProducts.Rows.Count;
            // Create a dataview of the datatable
            dvProducts.Table = dtProducts;
            // Filter the dataview
            dvProducts.RowFilter = filterString;

            if (porcentajeLinea != 0)
            {
                xAplicoPorcentajeLinea = true;
                float pct = 1 + porcentajeLinea / 100;
                foreach (DataRowView drv in dvProducts)
                {
                    drv["Precio"] = (float)drv["PrecioLista"] * pct;
                }
            }
            else
            {
                if (xAplicoPorcentajeLinea)
                {
                    xAplicoPorcentajeLinea = false;
                    foreach (DataRowView drv in dvProducts)
                    {
                        drv["Precio"] = drv["PrecioLista"];
                    }
                }
            }

            // Bind Datagrid view to the DataView
            dataGridView1.DataSource = dvProducts;
            //dataGridView1.Refresh();

            // Save the row count in the datagridview
            currentRowCount = dataGridView1.Rows.Count;
            dataGridView1.ResumeLayout();
            dataGridIdle = true;

            // Show the counts in the toolstrip
            this.emitir2(new util.Pair<int, int>(currentRowCount, dataRowCount));

            if (currentRowCount > 0)
            {
                dataGridView1.Rows[0].Selected = true;

                DataGridViewCell cell = dataGridView1[0, 0];
                if (cell != null)
                {
                    DataGridViewRow row = cell.OwningRow;
                    this.emitir(row);
                }
                else
                {
                    this.emitir(null);
                };
            }
            else
            {
                this.emitir(null);
            };
            watch.Stop();
            System.Diagnostics.Debug.WriteLine("Carga grilla: " + watch.ElapsedMilliseconds.ToString());
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridIdle)
            {
                DataGridViewCell cell = null;
                foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
                {
                    cell = selectedCell;
                    break;
                }
                if (cell != null)
                {
                    DataGridViewRow row = cell.OwningRow;
                    this.emitir(row);
                    // etc.
                }
                else
                {
                    this.emitir(null);
                }
            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)CCol.cSemaforo)
                {

                    DataGridViewCell cell = dataGridView1[e.ColumnIndex, e.RowIndex];

                    if (cell != null)
                    {
                        DataGridViewRow row = cell.OwningRow;
                        Catalogo.util.BackgroundTasks.ExistenciaProducto existencia = new util.BackgroundTasks.ExistenciaProducto(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
                        existencia.onCancelled += ExistenciaCancelled;
                        existencia.onFinished += ExistenciaFinished;
                        existencia.getExistencia(row.Cells["CodigoAns"].Value.ToString(), Global01.NroUsuario, cell);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void ExistenciaCancelled(System.Windows.Forms.DataGridViewCell cell)
        {
            throw new NotImplementedException();
        }

        private void ExistenciaFinished(string idProducto, string resultado, System.Windows.Forms.DataGridViewCell cell)
        {


          if (resultado.IndexOf(";") > 0)
          {
          
              string[] stringSeparators = new string[] {";"};
              string[] aResultado = resultado.Split(stringSeparators, StringSplitOptions.None);
     
              switch   (aResultado[0])
              {
                  case "r":
                    cell.Style.BackColor = Color.Red;
                    cell.ToolTipText= "NO Disponible";
                    break;
                  case "a":
                    cell.Style.BackColor = Color.Yellow;
                    cell.ToolTipText= "Disponibilidad Parcial \n valor de referencia (" + aResultado[1] + ") unidades";
                    break;
                  case "v":
                    cell.Style.BackColor = Color.Green;
                    cell.ToolTipText= "En Tránsito \n valor de referencia (" + aResultado[1] + ") unidades";
                    break;
                  case "VV":
                    cell.Style.BackColor = Color.YellowGreen;
                    cell.ToolTipText= "Disponible en 24 hs. \n valor de referencia (" + aResultado[1] + ") unidades";
                    cell.Value= "x";
                    break;
              }

            }

            //System.Diagnostics.Debug.WriteLine(String.Format("{0}: {1}", idProducto, resultado));
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                switch (e.KeyChar)
                {
                    case '+':
                        this.emitir3(_pedidos.PedidosHelper.Acciones.INCREMENTAR);
                        break;
                    case '-':
                        this.emitir3(_pedidos.PedidosHelper.Acciones.DECREMENTAR);
                        break;
                    case (char)Keys.Enter:
                        this.emitir3(_pedidos.PedidosHelper.Acciones.COMPRAR);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.emitir3(_pedidos.PedidosHelper.Acciones.COMPRAR);
            }
            catch (Exception ex)
            {
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        public emisorHandler<_pedidos.PedidosHelper.Acciones> emisor3
        {
            get;
            set;
        }

    }
}
