using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Catalogo._recibos
{
public class EnvioRecibo
{

    // Esta clase realiza una consulta al servidor web y obtiene
    // el numero de ip del servidor privado.

    // Define como se llama este modulo para el control de errores

    private RecibosWS.Recibos_v_303 cliente;
    private bool webServiceInicializado;
    private string _MacAddress;

    private string _ip;
    private string _NroRecibo;
    private string _CodCliente;
    private string _Fecha;
    private string _Total;
    private string _Bahia;
    private string _Detalle;
    private string _Facturas;

    private string _NotasCredito;

    private bool DatosObtenidos;
    private System.Data.OleDb.OleDbConnection Conexion1;

    public EnvioRecibo(System.Data.OleDb.OleDbConnection Conexion, string ipAddress, string ipAddressIntranet, string MacAddress, bool usaProxy, string proxyServerAddress)
    {
        Inicializar(ipAddress, ipAddressIntranet, MacAddress, usaProxy, proxyServerAddress);
        Conexion1 = Conexion;
    }


    public bool Inicializado
    {
        get 
        { 
            return webServiceInicializado; 
        }
    }


    public void obtenerDatos(string nroRecibo)
	{
		DatosObtenidos = false;

		System.Data.OleDb.OleDbDataReader Enc = Funciones.oleDbFunciones.Comando(Conexion1, "EXEC v_Recibo_Enc '" + nroRecibo + "'");
		System.Data.OleDb.OleDbDataReader Det = Funciones.oleDbFunciones.Comando(Conexion1, "EXEC v_Recibo_Det '" + nroRecibo + "'");
		System.Data.OleDb.OleDbDataReader Fac = Funciones.oleDbFunciones.Comando(Conexion1, "EXEC v_Recibo_App '" + nroRecibo + "'");
		System.Data.OleDb.OleDbDataReader nCre = Funciones.oleDbFunciones.Comando(Conexion1, "EXEC v_Recibo_Deducir_Normal '" + nroRecibo + "'");

		_NroRecibo = nroRecibo;
		_CodCliente = String.Format("000000", Enc["IDCliente"].ToString().Trim());
		_Fecha = Enc["F_Recibo"].ToString().Substring(7, 4) + Enc["F_Recibo"].ToString().Substring(4, 2) + Enc["F_Recibo"].ToString().Substring(1, 2);
		_Total = String.Format("00000000000000000", (float)Enc["Total"] * 100);
		_Bahia = ((bool)Enc["Bahia"] ? "si" : "no");

		if (Det.HasRows) {
			while (Det.Read()) 
            {
				_Detalle += Det["D_Valor"].ToString().Trim().PadLeft(30, ' ') + ",";

				if ((int) Det["TipoValor"] == 3 | (int) Det["TipoValor"] == 4) 
                {
					_Detalle += String.Format("00000000000000000", (float)Det["Divisas"] * 100) + ",";
				} 
                else 
                {
					_Detalle += String.Format("00000000000000000", (float) Det["Importe"] * 100) + ",";
				}

				if (object.ReferenceEquals(Det["N_Cheque"], DBNull.Value)) 
                {
					_Detalle += new string(' ', 20) + ",";
				} 
                else 
                {
					_Detalle += Det["N_Cheque"].ToString().Trim().PadLeft(20, ' ') + ",";
				}

				if (object.ReferenceEquals(Det["F_EmiCheque"], DBNull.Value)) 
                {
					_Detalle += new string(' ', 10) + ",";
				} 
                else 
                {
					_Detalle += Det["F_EmiCheque"].ToString().Substring(7, 4) 
                        + Det["F_EmiCheque"].ToString().Substring(4, 2) 
                        + Det["F_EmiCheque"].ToString().Substring(1, 2) + ",";
				}

				if (object.ReferenceEquals(Det["F_CobroCheque"], DBNull.Value)) 
                {
					_Detalle += new string(' ', 10) + ",";
				} else {
					_Detalle += Det["F_CobroCheque"].ToString().Substring(7, 4) 
                        + Det["F_CobroCheque"].ToString().Substring(4, 2) 
                        + Det["F_CobroCheque"].ToString().Substring(1, 2) + ",";
				}

				if (object.ReferenceEquals(Det["Banco"], DBNull.Value)) {
					_Detalle += new string(' ', 50) + ",";
				} else {
					_Detalle += Det["Banco"].ToString().Trim().PadLeft(50, ' ') + ",";
				}

				if (object.ReferenceEquals(Det["CPA"], DBNull.Value)) {
					_Detalle += "    " + ",";
				} else {
					_Detalle += Det["CPA"].ToString() + ",";
				}

				if (object.ReferenceEquals(Det["N_Cuenta"], DBNull.Value)) {
					_Detalle += new string(' ', 20) + ",";
				} else {
					_Detalle += Det["N_Cuenta"].ToString().Trim().PadLeft(20, ' ') + ",";
				}

                _Detalle += Det["chPropio"].ToString() + ",";

				_Detalle += String.Format("00000000000000000", (float) Det["T_Cambio"] * 100) + ";";

			}
		}

		if (Fac.HasRows) {

			while (Fac.Read()) {
				_Facturas += Fac["Concepto"].ToString() + ",";
				_Facturas += String.Format("00000000000000000", (float) Fac["Importe"] * 100) + ",";
				_Facturas += Fac["Total"].ToString() + ";";
			}
		}

		//        'Notas de Credito
		if (nCre.HasRows) {
			while (nCre.Read()) {
				if (nCre["Concepto"].ToString().Substring(0, 4) == "CRE-") 
                {
					_NotasCredito += nCre["Concepto"] + ",";
					_NotasCredito += String.Format("00000000000000000", (float) nCre["TotaldeduN"] * 100) + ";";
				}
			}
		}

		Enc = null;
		Det = null;
		Fac = null;
		nCre = null;


		DatosObtenidos = true;

	}

    public long EnviarRecibo()
    {
        bool Cancel = false;
        long resultado = 0;

        if (!webServiceInicializado)
        {
            Cancel = true;
        }


        if (!Cancel)
        {
//            if (vg.TranActiva == null)
//            {
//                vg.TranActiva = vg.Conexion.BeginTransaction;
//            }

            resultado = cliente.EnviarRecibo317(_MacAddress, _NroRecibo, _CodCliente, _Fecha, _Bahia, _Total, _Detalle, _Facturas, _NotasCredito);

            if (resultado == 0)
            {
                //               adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Recibo_Transmicion_Upd '" & m_NroRecibo & "'")
//                if ((vg.TranActiva != null))
//                {
//                    vg.TranActiva.Commit();
//                    vg.TranActiva = null;
//                }
            }
            else
            {
 //               if ((vg.TranActiva != null))
 //               {
 //                   vg.TranActiva.Rollback();
 //                   vg.TranActiva = null;
 //               }
            }

            return resultado;
        }
        else
        {
            return -1;
        }
    }


    public void Inicializar(string ipAddress, string ipAddressIntranet, string MacAddress, bool usaProxy, string proxyServerAddress)
    {
        // ERROR: Not supported in C#: OnErrorStatement
        try
        {
            bool Conectado = util.SimplePing.ping(ipAddress, 5000);
            if (!Conectado)
            {
                Conectado = util.SimplePing.ping(ipAddressIntranet, 5000);
            }
            if (Conectado)
            {
                if (!webServiceInicializado)
                {
                    cliente = new RecibosWS.Recibos_v_303();
                    cliente.Url = "http://" + ipAddress + "/wsCatalogo3/Recibos_v_303.asmx?wsdl";
                    if (usaProxy)
                    {
                        cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
                    }

                    _MacAddress = MacAddress;
                    _ip = ipAddress;
                    webServiceInicializado = true;
                }
            }
            else
            {
                webServiceInicializado = false;
            }
        }
        catch
        {
            //        if (Err().Number == -2147024809)
            {
                // Intento con el ip interno
                cliente = new RecibosWS.Recibos_v_303();
                cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo3/Recibos_v_303.asmx?wsdl";
                if (usaProxy)
                {
                    cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
                }

                _MacAddress = MacAddress;
                webServiceInicializado = true;
                _ip = ipAddressIntranet;
            }
            //      else
            //      {
            //          Err().Raise(Err().Number, Err().Source, Err().Description);
            //      }
        }
    }
}
}