﻿#define usarSemaforoImagen
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
        Funciones.emitter_receiver.IReceptor<Keys>, // Para recibir notificacion de Tab
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

        private DataTable dtProducts = new DataTable();
        private DataView dvProducts = new DataView();
        
        private bool dataGridIdle = false;
        private float porcentajeLinea;
        private string filterString = string.Empty;
        private int currentRowCount = 0;
        private int dataRowCount = 0;

        public GridViewFilter2()
        {
            InitializeComponent();

            dataGridView1.RowPostPaint += OnRowPostPaint;

#if usarSemaforoImagen
            dataGridView1.CellPainting += OnCellPainting;
#endif
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
            preload.Preloader.instance.productos.execute();
        }

        private void dataReady(System.Data.DataTable dataTable)
        {
            dtProducts = dataTable;

            // Load the DataGridView            
            loadDataGridView();
        }

        private void loadDataGridView()
        {
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
            if (Global01.AppActiva) dataGridView1.Columns[(int)CCol.cSemaforo].Visible = true;

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
            dataGridView1.Columns[(int)CCol.cPrecio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)CCol.cPrecio].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns[(int)CCol.cPlista].Name = "PrecioLista";
            dataGridView1.Columns[(int)CCol.cPlista].HeaderText = "$ Lista";
            dataGridView1.Columns[(int)CCol.cPlista].DataPropertyName = "PrecioLista";
            dataGridView1.Columns[(int)CCol.cPlista].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)CCol.cPlista].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns[(int)CCol.cPoferta].Name = "PrecioOferta";
            dataGridView1.Columns[(int)CCol.cPoferta].HeaderText = "$ Oferta";
            dataGridView1.Columns[(int)CCol.cPoferta].DataPropertyName = "PrecioOferta";
            dataGridView1.Columns[(int)CCol.cPoferta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[(int)CCol.cPorclinea].Name = "LineaPorcentaje";
            dataGridView1.Columns[(int)CCol.cPorclinea].HeaderText = "% Línea";
            dataGridView1.Columns[(int)CCol.cPorclinea].DataPropertyName = "LineaPorcentaje";
            dataGridView1.Columns[(int)CCol.cPorclinea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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
                Global01.xAplicoPorcentajeLinea = true;
                dataGridView1.Columns[(int)CCol.cPrecio].HeaderCell.Style.BackColor = Color.Red;
            }
            else
            {
                dataGridView1.Columns[(int)CCol.cPrecio].HeaderCell.Style.BackColor = System.Drawing.SystemColors.Control;
            }

            if (Global01.xAplicoPorcentajeLinea)
            {
                double pct = 1 + porcentajeLinea / 100;
                foreach (DataRowView drv in dvProducts)
                {
                    drv["Precio"] = (double)drv["PrecioLista"] * pct;
                }
                if (pct == 1) Global01.xAplicoPorcentajeLinea = false;
            }

            ////if (porcentajeLinea != 0)
            ////{
            ////    Global01.xAplicoPorcentajeLinea = true;
            ////    float pct = 1 + porcentajeLinea / 100;
            ////    foreach (DataRowView drv in dvProducts)
            ////    {
            ////        drv["Precio"] = (float)drv["PrecioLista"] * pct;
            ////        //drv["Precio"] = string.Format("{0:N2}", float.Parse(drv["PrecioLista"].ToString()) * pct);
            ////    }
            ////    dataGridView1.Columns[(int)CCol.cPrecio].HeaderCell.Style.BackColor = Color.Red;
            ////}
            ////else
            ////{
            ////    if (Global01.xAplicoPorcentajeLinea)
            ////    {
            ////        Global01.xAplicoPorcentajeLinea = false;
            ////        foreach (DataRowView drv in dvProducts)
            ////        {
            ////            drv["Precio"] = drv["PrecioLista"];
            ////        }
            ////        dataGridView1.Columns[(int)CCol.cPrecio].HeaderCell.Style.BackColor = System.Drawing.SystemColors.Control;
            ////    }
            ////}

            // Bind Datagrid view to the DataView
            dataGridView1.DataSource = dvProducts;

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
                dataGridView1.Visible = true;
                dataGridView1.Focus();
            }
            else
            {
                this.emitir(null);
            };
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                else
                {
                    this.emitir3(_pedidos.PedidosHelper.Acciones.COMPRAR);
                }

            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
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
                string[] stringSeparators = new string[] { ";" };
                string[] aResultado = resultado.Split(stringSeparators, StringSplitOptions.None);
                cell.Tag = aResultado[0];

                if (aResultado[0].Trim().Length > 3)
                {
                    cell.ToolTipText = ((Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) ? aResultado[0] : ""); 
                }
                else
                {
                    switch (aResultado[0])
                    {
                        case "r":
#if !usarSemaforoImagen
                        cell.Style.BackColor = Color.Red;
#endif
                            cell.ToolTipText = "NO Disponible";
                            break;
                        case "a":
#if !usarSemaforoImagen
                        cell.Style.BackColor = Color.Yellow;
#endif
                            cell.ToolTipText = "Disponibilidad Parcial" + ((Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) ? "\n valor de referencia (" + aResultado[1] + ") unidades" : "");
                            break;
                        case "v":
#if !usarSemaforoImagen
                        cell.Style.BackColor = Color.YellowGreen;
                        cell.Value = "x";
#endif
                            cell.ToolTipText = "En Tránsito" + ((Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) ? "\n valor de referencia (" + aResultado[1] + ") unidades" : "");

                            break;
                        case "VV":
#if !usarSemaforoImagen
                        cell.Style.BackColor = Color.Green;
#endif
                            //cell.ToolTipText = "Disponible en 24 hs." + ((Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) ? "\n valor de referencia (" + aResultado[1] + ") unidades" : "");
                            cell.ToolTipText = ((Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) ? " valor de referencia (" + aResultado[1] + ") unidades" : "");
                            break;
                    }
                }
            }
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
                        if (Char.IsLetterOrDigit(e.KeyChar))
                        {
                            for (int i = 0; i < (dataGridView1.Rows.Count); i++)
                            {
                                if (dataGridView1.Rows[i].Cells["C_Producto"].Value.ToString().StartsWith(e.KeyChar.ToString(), true, System.Globalization.CultureInfo.InvariantCulture))
                                {
                                    dataGridView1.Rows[i].Cells[0].Selected = true;
                                    return; // stop looping
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        //private void dataGridView1_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.emitir3(_pedidos.PedidosHelper.Acciones.COMPRAR);
        //    }
        //    catch (Exception ex)
        //    {
        //        util.errorHandling.ErrorLogger.LogMessage(ex);
        //        throw ex;  //util.errorHandling.ErrorForm.show();
        //    }
        //}

        public emisorHandler<_pedidos.PedidosHelper.Acciones> emisor3
        {
            get;
            set;
        }

        void OnRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (Funciones.modINIs.ReadINI("DATOS", "Turbo", Global01.setDef_Turbo) == "0")
            {
                string s = " ";
                Color xColor = Color.Black;
                string xTipo = dataGridView1.Rows[e.RowIndex].Cells[(int)CCol.cTipo].Value.ToString();
                bool xBold = (float.Parse(dataGridView1.Rows[e.RowIndex].Cells[(int)CCol.cPorclinea].Value.ToString()) != 0);
                bool xOferta = (dataGridView1.Rows[e.RowIndex].Cells[(int)CCol.cControl].Value.ToString() == "O");

                if (xTipo == "prod_n")
                {
                    xColor = Color.Blue;
                    s += " prod. nuevo;";
                }
                else if (xTipo == "apli_n")
                {
                    xColor = Color.Green;
                    s += " apli. nueva;";
                }

                if (xOferta)
                {
                    xColor = Color.Red;
                    s += " Oferta!;";
                }

                if (xBold)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[(int)CCol.cCodigo].Style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    s += " $ modificado;";
                }

                if (xColor != Color.Black) dataGridView1.Rows[e.RowIndex].Cells[(int)CCol.cCodigo].Style.ForeColor = xColor;
                if (s.Trim().Length > 0) dataGridView1.Rows[e.RowIndex].Cells[(int)CCol.cCodigo].ToolTipText = s.Trim();
            }
        }

#if usarSemaforoImagen
        void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 0)
            {
                System.Windows.Forms.DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell != null)
                {
                    Brush brush;
                    bool dibujarCentro = false;
                    if (cell.Tag != null)
                    {
                        string existencia = (string)cell.Tag;
                        switch (existencia)
                        {
                            case "r":
                                brush = Brushes.Red;
                                break;
                            case "a":
                                brush = Brushes.Yellow;
                                break;
                            case "v":
                                brush = Brushes.Green;
                                dibujarCentro = true;
                                //cell.Value = "x";
                                break;
                            case "VV":
                                brush = Brushes.Green;
                                break;
                            default:
                                if (existencia.Trim().Length > 3)
                                {
                                    brush = Brushes.Cyan;
                                }
                                else
                                {
                                    brush = Brushes.DarkGray;
                                }
                                break;
                        }
                    }
                    else
                    {
                        // aca hay que dibujar sin informacion
                        brush = Brushes.DarkGray;
                    }

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    Rectangle rect = e.CellBounds;
                   
                    rect.Inflate(-5, -4);
           
                    //rect = new SolidBrush(Color.Blue);

                    e.Graphics.FillEllipse(brush, rect);
                    if (dibujarCentro)
                    {
                        Rectangle r = rect;
                        r.Inflate(-3, -2);
                        e.Graphics.FillEllipse(Brushes.Black, r);
                    }
                    e.Handled = true;
                }
            }
        }
#endif

        public void onRecibir(Keys dato)
        {
            if (dato == Keys.Tab)
            {
                this.ActiveControl = dataGridView1;
                dataGridView1.Focus();
                dataGridView1.Select();
            }
        }

        internal void ocultarGrilla()
        {
            dataGridView1.ClearSelection();
            dataGridView1.Visible = false;            
        }

    }
}