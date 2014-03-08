Option Explicit On

Public Class EnvioPedido

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsEnvioPedido"

    ' Esta clase realiza una consulta al servidor web y obtiene
    ' el numero de ip del servidor privado.
    Private Cliente As WSPedidos.PedidosSoapClient
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

    Public ReadOnly Property Inicializado() As Boolean
        Get
            Return WebServiceInicializado
        End Get
    End Property

    '    Public Function ObtenerDatos(ByVal NroPedido As String)

    '        DatosObtenidos = False

    '        Dim rs_Enc As ADODB.Recordset
    '        Dim rs_Det As ADODB.Recordset

    '        rs_Enc = New ADODB.Recordset
    '        rs_Det = New ADODB.Recordset

    '        rs_Enc = adoModulo.adoComando(vg.Conexion, "EXECUTE v_Pedido_Enc '" & NroPedido & "'")
    '        rs_Det = adoModulo.adoComando(vg.Conexion, "EXECUTE v_Pedido_Det '" & NroPedido & "'")

    '        m_NroPedido = NroPedido
    '        m_CodCliente = Format(Trim(rs_Enc("IDCliente")), "000000")
    '        m_Fecha = rs_Enc("F_Pedido")
    '        m_Observaciones = Replace(rs_Enc("Observaciones"), ",", " ") & ""
    '        m_Transporte = rs_Enc("Transporte") & ""

    '        'rs_Det.MoveFirst
    '        m_Detalle = ""
    '        Do Until rs_Det.EOF
    '0:          m_Detalle = m_Detalle & rs_Det("C_Producto") & ","
    '1:          m_Detalle = m_Detalle & Format(Trim(rs_Det("Cantidad")), "00000000") & "00,"
    '2:          m_Detalle = m_Detalle & rs_Det("miSimilar") & ","
    '3:          m_Detalle = m_Detalle & rs_Det("miOferta") & ","
    '4:          m_Detalle = m_Detalle & rs_Det("miBahia") & ","
    '5:          m_Detalle = m_Detalle & IIf(IsNull(rs_Det("miDeposito")), "   ", rs_Det("miDeposito")) & ","
    '6:          m_Detalle = m_Detalle & rs_Det("Observaciones") & ";"
    '            rs_Det.MoveNext()
    '        Loop

    '        rs_Enc = Nothing
    '        rs_Det = Nothing

    '        DatosObtenidos = True

    '    End Function

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
                           ByVal MacAddress As String)

        Dim Conectado As Boolean
        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        Conectado = True
        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If

        On Error GoTo ErrorHandler

        If Conectado Then
            If Not WebServiceInicializado Then
                Cliente = New WSPedidos.PedidosSoapClient("", "http://" & ipAddress & "/wsCatalogo3/Pedidos.asmx?wsdl")
                If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
                    '                    Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
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
            Cliente = New WSPedidos.PedidosSoapClient("", "http://" & ipAddressIntranet & "/wsCatalogo3/Pedidos.asmx?wsdl")
            If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
                '                Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
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
