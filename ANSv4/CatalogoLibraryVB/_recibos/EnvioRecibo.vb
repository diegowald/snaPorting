Option Explicit On
Public Class EnvioRecibo

    ' Esta clase realiza una consulta al servidor web y obtiene
    ' el numero de ip del servidor privado.

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsEnvioRecibo"

    Private Cliente As RecibosWS.Recibos_v_303
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

    Public Sub ObtenerDatos(ByVal nroRecibo As String)

        DatosObtenidos = False

        Dim Enc As System.Data.OleDb.OleDbDataReader
        Dim Det As System.Data.OleDb.OleDbDataReader
        Dim Fac As System.Data.OleDb.OleDbDataReader
        Dim nCre As System.Data.OleDb.OleDbDataReader


        Enc = Funciones.adoModulo.adoComando(vg.Conexion, "EXEC v_Recibo_Enc '" & nroRecibo & "'")
        Det = Funciones.adoModulo.adoComando(vg.Conexion, "EXEC v_Recibo_Det '" & nroRecibo & "'")
        Fac = Funciones.adoModulo.adoComando(vg.Conexion, "EXEC v_Recibo_App '" & nroRecibo & "'")
        nCre = Funciones.adoModulo.adoComando(vg.Conexion, "EXEC v_Recibo_Deducir_Normal '" & nroRecibo & "'")

        m_NroRecibo = nroRecibo
        m_CodCliente = Format(Trim(Enc("IDCliente")), "000000")
        m_Fecha = Mid(Enc("F_Recibo"), 7, 4) & Mid(Enc("F_Recibo"), 4, 2) & Mid(Enc("F_Recibo"), 1, 2)
        m_Total = Format(Trim(Enc("Total") * 100), "00000000000000000")
        m_Bahia = IIf(Enc("Bahia") = True, "si", "no")

        If Det.HasRows Then
            Do While Det.Read

0:              m_Detalle = m_Detalle & padLR(Trim(Det("D_Valor")), 30, " ", 2) & ","

                If Det("TipoValor") = 3 Or Det("TipoValor") = 4 Then
1:                  m_Detalle = m_Detalle & Format(Trim(Det("Divisas") * 100), "00000000000000000") & ","
                Else
                    m_Detalle = m_Detalle & Format(Trim(Det("Importe") * 100), "00000000000000000") & ","
                End If

                If Det("N_Cheque") Is DBNull.Value Then
2:                  m_Detalle = m_Detalle & Space(20) & ","
                Else
                    m_Detalle = m_Detalle & padLR(Trim(Det("N_Cheque")), 20, " ", 2) & ","
                End If

                If Det("F_EmiCheque") Is DBNull.Value Then
3:                  m_Detalle = m_Detalle & Space(10) & ","
                Else
                    m_Detalle = m_Detalle & Mid(Det("F_EmiCheque"), 7, 4) & Mid(Det("F_EmiCheque"), 4, 2) & Mid(Det("F_EmiCheque"), 1, 2) & ","
                End If

                If Det("F_CobroCheque") Is DBNull.Value Then
4:                  m_Detalle = m_Detalle & Space(10) & ","
                Else
                    m_Detalle = m_Detalle & Mid(Det("F_CobroCheque"), 7, 4) & Mid(Det("F_CobroCheque"), 4, 2) & Mid(Det("F_CobroCheque"), 1, 2) & ","
                End If

                If Det("Banco") Is DBNull.Value Then
5:                  m_Detalle = m_Detalle & Space(50) & ","
                Else
                    m_Detalle = m_Detalle & padLR(Trim(Det("Banco")), 50, " ", 2) & ","
                End If

                If Det("CPA") Is DBNull.Value Then
6:                  m_Detalle = m_Detalle & "    " & ","
                Else
                    m_Detalle = m_Detalle & Det("CPA") & ","
                End If

                If Det("N_Cuenta") Is DBNull.Value Then
7:                  m_Detalle = m_Detalle & Space(20) & ","
                Else
                    m_Detalle = m_Detalle & padLR(Trim(Det("N_Cuenta")), 20, " ", 1) & ","
                End If

8:              m_Detalle = m_Detalle & Det("chPropio") & ","

9:              m_Detalle = m_Detalle & Format(Trim(Det("T_Cambio") * 100), "00000000000000000") & ";"

            Loop
        End If

        If Fac.HasRows Then
            Do While Fac.Read

                '0:
                m_Facturas = m_Facturas & Fac("Concepto") & ","
                '1:
                m_Facturas = m_Facturas & Format(Trim(Fac("Importe") * 100), "00000000000000000") & ","
                '2:
                m_Facturas = m_Facturas & Fac("Total") & ";"
            Loop
        End If

        '        'Notas de Credito
        If nCre.HasRows Then
            Do While nCre.Read
                If Left(nCre("Concepto"), 4) = "CRE-" Then
                    m_NotasCredito = m_NotasCredito & nCre("Concepto") & ","
                    m_NotasCredito = m_NotasCredito & Format(Trim(nCre("TotaldeduN") * 100), "00000000000000000") & ";"
                End If
            Loop
        End If

        Enc = Nothing
        Det = Nothing
        Fac = Nothing
        nCre = Nothing


        DatosObtenidos = True

    End Sub

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
                           ByVal MacAddress As String, _
                           ByVal usaProxy As Boolean, _
                           ByVal proxyServerAddress As String)

        On Error GoTo errhandler
        Dim Conectado As Boolean
        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If
        If Conectado Then
            If Not WebServiceInicializado Then
                Cliente = New RecibosWS.Recibos_v_303
                Cliente.Url = "http://" & ipAddress & "/wsCatalogo3/Recibos_v_303.asmx?wsdl"
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
errhandler:
        If Err.Number = -2147024809 Then
            ' Intento con el ip interno
            Cliente = New RecibosWS.Recibos_v_303
            Cliente.Url = "http://" & ipAddressIntranet & "/wsCatalogo3/Recibos_v_303.asmx?wsdl"
            If usaProxy Then
                Cliente.Proxy = New System.Net.WebProxy(proxyServerAddress)
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
