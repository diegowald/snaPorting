﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._pedidos
{
    public class EnvioPedido
    {
        /* Esta clase realiza una consulta al servidor web y obtiene
         * el numero de ip del servidor privado.
         **/

        private PedidosWS.Pedidos cliente;
        private bool webServiceInicializado;
        private string _ip;
        private string _MacAddress;
        private string _NroPedido;
        private string _CodCliente;
        private string _Fecha;
        private string _Observaciones;
        private string _Transporte;
        private string _Detalle;

        private bool DatosObtenidos;

        private System.Data.OleDb.OleDbConnection Conexion1;

        public EnvioPedido(System.Data.OleDb.OleDbConnection Conexion, string ipAddress, string ipAddressIntranet, string MacAddress)
        {
            inicializar(ipAddress, ipAddressIntranet, MacAddress);
            Conexion1 = Conexion;
        }

        public bool Inicializado
        {
            get
            {
                return webServiceInicializado;
            }
        }


        public void obtenerDatos(string NroPedido)
        {
            DatosObtenidos = false;

            System.Data.OleDb.OleDbDataReader enc = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_Pedido_Enc '" + NroPedido + "'");
            System.Data.OleDb.OleDbDataReader det = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_Pedido_Det '" + NroPedido + "'");

            _NroPedido = NroPedido;
            enc.Read();
            _CodCliente = enc["IDCliente"].ToString().Trim().PadLeft(6, '0');
            _Fecha = string.Format("{0:yyyyMMdd}", DateTime.Parse(enc["F_Pedido"].ToString()));
            _Observaciones = enc["Observaciones"].ToString().Replace(",", " ");
            _Transporte = enc["Transporte"].ToString();

            if (det.HasRows)
            {
                _Detalle = "";
                while (det.Read())
                {
                    _Detalle += det["C_Producto"].ToString() + ","
                        + det["Cantidad"].ToString().Trim().PadLeft(8, '0') + "00,"
                        + det["miSimilar"].ToString() + ","
                        + det["miOferta"].ToString() + ","
                        + det["miBahia"].ToString() + ","
                        + (det["miDeposito"] == null ? "   " : det["miDeposito"]) + ","
                        + det["Observaciones"].ToString() + ";";
                }
            }

            DatosObtenidos = true;
        }

        public long enviarPedido()
        {
            bool cancel = false;
            long resultado = -1;

            if (!webServiceInicializado)
            {
                cancel = true;
            }

            if (!cancel)
            {
                if (Global01.TranActiva == null)
                {
                    Global01.TranActiva = Conexion1.BeginTransaction();
                }

                resultado = cliente.EnviarPedido7(_MacAddress, _NroPedido, _CodCliente, _Fecha, _Observaciones, _Transporte, _Detalle);

                if (resultado == 0)
                {
                    Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_Pedido_Transmicion_Upd '" + _NroPedido + "'");
                    if (Global01.TranActiva != null)
                    {
                        Global01.TranActiva.Commit();
                        Global01.TranActiva = null;
                    }
                }
                else
                {
                    if (Global01.TranActiva != null)
                    {
                        Global01.TranActiva.Rollback();
                        Global01.TranActiva = null;
                    }
                }

                return resultado;
            }
            else
            {
                return -1;
            }
        }


        public void inicializar(string ipAddress, string ipAddressIntranet, string MacAddress)
        {
            bool conectado = util.SimplePing.ping(ipAddress, 5000);
            if (!conectado)
            {
                conectado = util.SimplePing.ping(ipAddressIntranet, 5000);
            }

            try
            {
                if (conectado)
                {
                    if (!webServiceInicializado)
                    {
                        cliente = new PedidosWS.Pedidos();
                        cliente.Url = "http://" + ipAddress + "/wsCatalogo4/Pedidos.asmx?wsdl";
                        if (Global01.proxyServerAddress != "0.0.0.0")
                        {
                            cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                        }

                        _MacAddress = MacAddress;
                        _ip = ipAddress;
                        webServiceInicializado = true;
                    }
                    else
                    {
                        webServiceInicializado = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (System.Runtime.InteropServices.Marshal.GetExceptionCode() == -2147024809)
                {
                    cliente = new PedidosWS.Pedidos();
                    cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/Pedidos.asmx?wsdl";
                    if (Global01.proxyServerAddress != "0.0.0.0")
                    {
                        cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                    }

                    _MacAddress = MacAddress;
                    _ip = ipAddressIntranet;
                    webServiceInicializado = true;
                }
                else
                {
                    throw ex;
                }
            }
        }

    }
}