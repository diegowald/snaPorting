Option Explicit On
Public Class EnvioRecibo

    ' Esta clase realiza una consulta al servidor web y obtiene
    ' el numero de ip del servidor privado.

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsEnvioRecibo"

    Private Cliente As WSRecibos.Recibos_v_303SoapClient
    Private WebServiceInicializado As Boolean
    Private m_MacAddress As String
    Private m_ip As String

    Private m_NroRecibo As String
    Private m_CodCliente As String
    Private m_Fecha As String
    Private m_Total As String
    Private m_Bahia As String
    Private m_Detalle As String
    Private m_Facturas As String
    Private m_NotasCredito As String

    Private DatosObtenidos As Boolean

    Public ReadOnly Property Inicializado() As Boolean
        Get
            Return WebServiceInicializado
        End Get
    End Property

    '    Public Function ObtenerDatos(ByVal nroRecibo As String)

    '        DatosObtenidos = False

    '        Dim rs_Enc As ADODB.Recordset
    '        Dim rs_Det As ADODB.Recordset
    '        Dim rs_Fac As ADODB.Recordset
    '        Dim rs_nCre As ADODB.Recordset

    '        rs_Enc = New ADODB.Recordset
    '        rs_Det = New ADODB.Recordset
    '        rs_Fac = New ADODB.Recordset
    '        rs_nCre = New ADODB.Recordset

    '        rs_Enc = adoModulo.adoComando(vg.Conexion, "EXEC v_Recibo_Enc '" & nroRecibo & "'")
    '        rs_Det = adoModulo.adoComando(vg.Conexion, "EXEC v_Recibo_Det '" & nroRecibo & "'")
    '        rs_Fac = adoModulo.adoComando(vg.Conexion, "EXEC v_Recibo_App '" & nroRecibo & "'")
    '        rs_nCre = adoModulo.adoComando(vg.Conexion, "EXEC v_Recibo_Deducir_Normal '" & nroRecibo & "'")

    '        m_NroRecibo = nroRecibo
    '        m_CodCliente = Format(Trim(rs_Enc("IDCliente")), "000000")
    '        m_Fecha = Mid(rs_Enc("F_Recibo"), 7, 4) & Mid(rs_Enc("F_Recibo"), 4, 2) & Mid(rs_Enc("F_Recibo"), 1, 2)
    '        m_Total = Format(Trim(rs_Enc("Total") * 100), "00000000000000000")
    '        m_Bahia = IIf(rs_Enc("Bahia") = True, "si", "no")

    '        'rs_Det.MoveFirst
    '        Do While Not rs_Det.EOF

    '0:          m_Detalle = m_Detalle & padLR(Trim(rs_Det("D_Valor")), 30, " ", 2) & ","

    '            If rs_Det("TipoValor") = 3 Or rs_Det("TipoValor") = 4 Then
    '1:              m_Detalle = m_Detalle & Format(Trim(rs_Det("Divisas") * 100), "00000000000000000") & ","
    '            Else
    '                m_Detalle = m_Detalle & Format(Trim(rs_Det("Importe") * 100), "00000000000000000") & ","
    '            End If

    '            If IsNull(rs_Det("N_Cheque")) Then
    '2:              m_Detalle = m_Detalle & Space(20) & ","
    '            Else
    '                m_Detalle = m_Detalle & padLR(Trim(rs_Det("N_Cheque")), 20, " ", 2) & ","
    '            End If

    '            If IsNull(rs_Det("F_EmiCheque")) Then
    '3:              m_Detalle = m_Detalle & Space(10) & ","
    '            Else
    '                m_Detalle = m_Detalle & Mid(rs_Det("F_EmiCheque"), 7, 4) & Mid(rs_Det("F_EmiCheque"), 4, 2) & Mid(rs_Det("F_EmiCheque"), 1, 2) & ","
    '            End If

    '            If IsNull(rs_Det("F_CobroCheque")) Then
    '4:              m_Detalle = m_Detalle & Space(10) & ","
    '            Else
    '                m_Detalle = m_Detalle & Mid(rs_Det("F_CobroCheque"), 7, 4) & Mid(rs_Det("F_CobroCheque"), 4, 2) & Mid(rs_Det("F_CobroCheque"), 1, 2) & ","
    '            End If

    '            If IsNull(rs_Det("Banco")) Then
    '5:              m_Detalle = m_Detalle & Space(50) & ","
    '            Else
    '                m_Detalle = m_Detalle & padLR(Trim(rs_Det("Banco")), 50, " ", 2) & ","
    '            End If

    '            If IsNull(rs_Det("CPA")) Then
    '6:              m_Detalle = m_Detalle & "    " & ","
    '            Else
    '                m_Detalle = m_Detalle & rs_Det("CPA") & ","
    '            End If

    '            If IsNull(rs_Det("N_Cuenta")) Then
    '7:              m_Detalle = m_Detalle & Space(20) & ","
    '            Else
    '                m_Detalle = m_Detalle & padLR(Trim(rs_Det("N_Cuenta")), 20, " ", 1) & ","
    '            End If

    '8:          m_Detalle = m_Detalle & rs_Det("chPropio") & ","

    '9:          m_Detalle = m_Detalle & Format(Trim(rs_Det("T_Cambio") * 100), "00000000000000000") & ";"

    '            rs_Det.MoveNext()
    '        Loop

    '        'rs_Fac.MoveFirst
    '        Do While Not rs_Fac.EOF

    '            '0:
    '            m_Facturas = m_Facturas & rs_Fac("Concepto") & ","
    '            '1:
    '            m_Facturas = m_Facturas & Format(Trim(rs_Fac("Importe") * 100), "00000000000000000") & ","
    '            '2:
    '            m_Facturas = m_Facturas & rs_Fac("Total") & ";"

    '            rs_Fac.MoveNext()
    '        Loop

    '        'Notas de Credito
    '        Do While Not rs_nCre.EOF
    '            If Left(rs_nCre("Concepto"), 4) = "CRE-" Then
    '                m_NotasCredito = m_NotasCredito & rs_nCre("Concepto") & ","
    '                m_NotasCredito = m_NotasCredito & Format(Trim(rs_nCre("TotaldeduN") * 100), "00000000000000000") & ";"
    '            End If
    '            rs_nCre.MoveNext()
    '        Loop

    '        rs_Enc = Nothing
    '        rs_Det = Nothing
    '        rs_Fac = Nothing
    '        rs_nCre = Nothing


    '        DatosObtenidos = True

    '    End Function

    Public Function EnviarRecibo() As Long

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

            resultado = Cliente.EnviarRecibo317(m_MacAddress, m_NroRecibo, _
                m_CodCliente, m_Fecha, m_Bahia, m_Total, m_Detalle, m_Facturas, m_NotasCredito)

            If resultado = 0 Then
                '               adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Recibo_Transmicion_Upd '" & m_NroRecibo & "'")
                If Not vg.TranActiva Is Nothing Then
                    vg.TranActiva.Commit()
                    vg.TranActiva = Nothing
                End If
            Else
                If Not vg.TranActiva Is Nothing Then
                    vg.TranActiva.Rollback()
                    vg.TranActiva = Nothing
                End If
            End If

            EnviarRecibo = resultado
        Else
            EnviarRecibo = -1
        End If

        Exit Function

ErrorHandler:
        Err.Raise(Err.Number, Err.Source, Err.Description)
    End Function

    Public Sub Inicializar(ByVal ipAddress As String, _
                           ByVal ipAddressIntranet As String, _
                           ByVal MacAddress As String)

        On Error GoTo errhandler
        Dim Conectado As Boolean
        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If
        If Conectado Then
            If Not WebServiceInicializado Then
                Cliente = New WSRecibos.Recibos_v_303SoapClient ("", "http://" & ipAddress & "/wsCatalogo3/Recibos_v_303.asmx?wsdl")
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
errhandler:
        If Err.Number = -2147024809 Then
            ' Intento con el ip interno
            Cliente = New WSRecibos.Recibos_v_303SoapClient("", "http://" & ipAddressIntranet & "/wsCatalogo3/Recibos_v_303.asmx?wsdl")
            If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
                '                Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
            End If

            m_MacAddress = MacAddress
            WebServiceInicializado = True
            m_ip = ipAddressIntranet
            Err.Clear()
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If
    End Sub



End Class
