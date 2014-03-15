Option Explicit On

Public Class EnvioInterDeposito

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsEnvioInterDeposito"

    ' Esta clase realiza una consulta al servidor web y obtiene
    ' el numero de ip del servidor privado.
    Private Cliente As InterDepositoWS.InterDeposito
    Private WebServiceInicializado As Boolean
    Private m_MacAddress As String
    Private m_ip As String

    Private m_NroInterDeposito As String
    Private m_CodCliente As String
    Private m_Bco_Dep_Tipo As String
    Private m_Bco_Dep_Fecha As String
    Private m_Bco_Dep_Numero As String
    Private m_Bco_Dep_Monto As String
    Private m_Bco_Dep_Ch_Cantidad As String
    Private m_Bco_Dep_IdCta As String
    Private m_Observaciones As String

    Private m_Detalle As String

    Private DatosObtenidos As Boolean

    Private Conexion1 As System.Data.OleDb.OleDbConnection

    Public Sub New(ByVal Conexion As System.Data.OleDb.OleDbConnection, _
                   ByVal ipAddress As String, _
                   ByVal ipAddressIntranet As String, _
                   ByVal MacAddress As String,
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

    Public Sub ObtenerDatos(ByVal NroInterDeposito As String)

        DatosObtenidos = False

        Dim I As System.Data.OleDb.OleDbDataReader
        Dim Ifacturas As System.Data.OleDb.OleDbDataReader

        I = Funciones.adoModulo.adoComando(vg.Conexion, "EXECUTE v_InterDeposito_rpt '" & NroInterDeposito & "'")
        Ifacturas = Funciones.adoModulo.adoComando(vg.Conexion, "EXECUTE v_InterDepositoFacturas '" & NroInterDeposito & "'")

        m_NroInterDeposito = NroInterDeposito
        m_CodCliente = Format(Trim(I("IDCliente")), "000000")
        m_Bco_Dep_Fecha = Format(I("Bco_Dep_Fecha"), "ddMMyyyy")
        m_Bco_Dep_Tipo = I("Bco_Dep_Tipo")
        m_Bco_Dep_Numero = padLR(I("Bco_Dep_Numero"), 10, "0", 1)
        m_Bco_Dep_Monto = Format(Trim(I("Bco_Dep_Monto") * 100), "00000000000000000")
        m_Bco_Dep_Ch_Cantidad = Format(I("Bco_Dep_Ch_Cantidad"), "00")
        m_Bco_Dep_IdCta = Format(I("Bco_Dep_IdCta"), "000")

        m_Observaciones = ""

        'rs_Ifacturas.MoveFirst
        m_Detalle = ""
        If Ifacturas.HasRows Then
            Do While Ifacturas.Read
0:              m_Detalle = m_Detalle & Ifacturas("T_Comprobante") & "-" & Ifacturas("N_Comprobante") & ","
1:              m_Detalle = m_Detalle & Format(Trim(Ifacturas("Importe") * 100), "00000000000000000") & ";"
            Loop
        End If

        I = Nothing
        Ifacturas = Nothing

        DatosObtenidos = True

    End Sub

    Public Function EnviarInterDeposito() As Long

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

            resultado = Cliente.EnviarInterDeposito(m_MacAddress, m_NroInterDeposito, m_CodCliente, m_Bco_Dep_Tipo, m_Bco_Dep_Fecha, m_Bco_Dep_Numero, m_Bco_Dep_Monto, m_Bco_Dep_Ch_Cantidad, m_Bco_Dep_IdCta, m_Observaciones, m_Detalle)

            If resultado = 0 Then
                '                adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_InterDeposito_Transmicion_Upd '" & m_NroInterDeposito & "'")
                If Not (vg.TranActiva Is Nothing) Then
                    vg.TranActiva.Rollback()
                    vg.TranActiva = Nothing
                End If
            Else
                If Not (vg.TranActiva Is Nothing) Then
                    vg.TranActiva.Rollback()
                    vg.TranActiva = Nothing
                End If
            End If

            EnviarInterDeposito = resultado
        Else
            EnviarInterDeposito = -1
        End If

        Exit Function

ErrorHandler:
        Err.Raise(Err.Number, Err.Source, Err.Description)

    End Function

    Public Sub Inicializar(ByVal ipAddress As String, _
                           ByVal ipAddressIntranet As String, _
                           ByVal MacAddress As String,
                           ByVal usaProxy As Boolean, _
                           ByVal proxyServerAddress As String)

        Dim Conectado As Boolean
        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If

        On Error GoTo ErrorHandler

        If Conectado Then
            If Not WebServiceInicializado Then
                Cliente = New InterDepositoWS.InterDeposito
                Cliente.Url = "http://" & ipAddress & "/wsCatalogo3/InterDeposito.asmx?wsdl"
                If usaProxy Then
                    Cliente.Proxy = New System.Net.WebProxy(proxyServerAddress)
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
            Cliente = New InterDepositoWS.InterDeposito
            Cliente.Url = "http://" & ipAddressIntranet & "/wsCatalogo3/InterDeposito.asmx?wsdl"
            If usaProxy Then
                Cliente.Proxy = New System.Net.WebProxy(proxyServerAddress)
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
