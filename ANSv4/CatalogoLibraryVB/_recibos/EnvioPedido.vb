Option Explicit On

Public Class EnvioPedido

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsEnvioPedido"

    ' Esta clase realiza una consulta al servidor web y obtiene
    ' el numero de ip del servidor privado.
    Private Cliente As PedidosWS.Pedidos
    Private WebServiceInicializado As Boolean
    Private m_MacAddress As String
    Private m_ip As String

    Private m_NroPedido As String
    Private m_CodCliente As String
    Private m_Fecha As String
    Private m_Observaciones As String
    Private m_Transporte As String
    Private m_Detalle As String

    Private DatosObtenidos As Boolean

    Private Conexion1 As System.Data.OleDb.OleDbConnection

    Public Sub New(ByVal Conexion As System.Data.OleDb.OleDbConnection, _
                   ByVal ipAddress As String, _
                   ByVal ipAddressIntranet As String, _
                   ByVal MacAddress As String, _
                   ByVal usaProxy As Boolean, _
                   ByVal proxyServerAddress As String)
        Inicializar(ipAddress, ipAddressIntranet, MacAddress, usaProxy, proxyServerAddress)
        Conexion1 = Conexion
    End Sub

    Public ReadOnly Property Inicializado() As Boolean
        Get
            Return WebServiceInicializado
        End Get
    End Property

    Public Sub ObtenerDatos(ByVal NroPedido As String)

        DatosObtenidos = False

        Dim enc As System.Data.OleDb.OleDbDataReader
        Dim det As System.Data.OleDb.OleDbDataReader


        enc = Funciones.adoModulo.adoComando(Conexion1, "EXECUTE v_Pedido_Enc '" & NroPedido & "'")
        det=Funciones.adoModulo.adoComando(Conexion1 , "EXECUTE v_Pedido_Det '" & NroPedido & "'")

        m_NroPedido = NroPedido
        m_CodCliente = Format(Trim(enc("IDCliente")), "000000")
        m_Fecha = enc("F_Pedido")
        m_Observaciones = Replace(enc("Observaciones"), ",", " ") & ""
        m_Transporte = enc("Transporte") & ""

        If det.HasRows Then
            m_Detalle = ""
            Do While det.Read
0:              m_Detalle = m_Detalle & det("C_Producto") & ","
1:              m_Detalle = m_Detalle & Format(Trim(det("Cantidad")), "00000000") & "00,"
2:              m_Detalle = m_Detalle & det("miSimilar") & ","
3:              m_Detalle = m_Detalle & det("miOferta") & ","
4:              m_Detalle = m_Detalle & det("miBahia") & ","
5:              m_Detalle = m_Detalle & IIf(det("miDeposito") Is Nothing, "   ", det("miDeposito")) & ","
6:              m_Detalle = m_Detalle & det("Observaciones") & ";"
            Loop
        End If

        enc = Nothing
        det = Nothing

        DatosObtenidos = True

    End Sub

    Public Function EnviarPedido() As Long

        On Error GoTo ErrorHandler

        Dim Cancel As Boolean

        Cancel = False

        Dim resultado As Long

        If Not WebServiceInicializado Then
            Cancel = True
        End If

        If Not Cancel Then

            If vg.TranActiva Is Nothing Then
                vg.TranActiva = vg.Conexion.BeginTransaction
            End If

            resultado = Cliente.EnviarPedido7(m_MacAddress, m_NroPedido, m_CodCliente, m_Fecha, m_Observaciones, m_Transporte, m_Detalle)

            If resultado = 0 Then
                '                adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Pedido_Transmicion_Upd '" & m_NroPedido & "'")
                If Not vg.TranActiva Is Nothing Then
                    vg.TranActiva.Commit()
                    vg.TranActiva = Nothing
                End If
            Else
                If Not vg.TranActiva Is Nothing Then
                    vg.TranActiva.Commit()
                    vg.TranActiva = Nothing
                End If
            End If

            EnviarPedido = resultado
        Else
            EnviarPedido = -1
        End If

        Exit Function

ErrorHandler:
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Function

    Public Sub Inicializar(ByVal ipAddress As String, _
                           ByVal ipAddressIntranet As String, _
                           ByVal MacAddress As String, _
                           ByVal usaProxy As Boolean, _
                           ByVal proxyserverAddress As String)

        Dim Conectado As Boolean
        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        Conectado = True
        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If

        On Error GoTo ErrorHandler

        If Conectado Then
            If Not WebServiceInicializado Then
                Cliente = New PedidosWS.Pedidos
                Cliente.Url = "http://" & ipAddress & "/wsCatalogo3/Pedidos.asmx?wsdl"
                If usaProxy Then
                    Cliente.Proxy = New System.Net.WebProxy(proxyserverAddress)
                End If

                m_MacAddress = MacAddress
                m_ip = ipAddress
                WebServiceInicializado = True
            End If
        Else
            WebServiceInicializado = False
        End If

        Exit Sub

ErrorHandler:

        If Err.Number = -2147024809 Then
            ' Intento con el ip interno
            Cliente = New PedidosWS.Pedidos
            Cliente.Url = "http://" & ipAddressIntranet & "/wsCatalogo3/Pedidos.asmx?wsdl"
            If usaProxy Then
                Cliente.Proxy = New System.Net.WebProxy(proxyserverAddress)
            End If

            m_MacAddress = MacAddress
            m_ip = ipAddressIntranet
            WebServiceInicializado = True
            Err.Clear()
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If

    End Sub

End Class
