Option Explicit On

Public Class EnvioRendicion

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsEnvioRendicion"

    ' Esta clase realiza una consulta al servidor web y obtiene
    ' el numero de ip del servidor privado.
    Private Cliente As WSRendicion.RendicionSoapClient
    Private WebServiceInicializado As Boolean
    Private m_MacAddress As String
    Private m_ip As String

    Private m_NroRendicion As String
    Private m_IdViajante As String
    Private m_F_Rendicion As String
    Private m_Observaciones As String
    Private m_Efectivo As Double
    Private m_Dolares As Single
    Private m_Euros As Single
    Private m_ChequesCant As Byte
    Private m_ChequesMonto As Double
    Private m_CertificadosCant As Byte
    Private m_CertificadosMonto As Double

    Private m_DetalleValores As String
    Private m_DetalleRecibos As String

    Private DatosObtenidos As Boolean


    'Public Property Get Inicializado() As Boolean
    'Inicializado = WebServiceInicializado
    '    End Property

    '    Public Function ObtenerDatos(ByVal NroRendicion As String)

    '        DatosObtenidos = False

    '        Dim rs_Ren As ADODB.Recordset
    '        Dim rs_RenValores As ADODB.Recordset
    '        Dim rs_RenRecibos As ADODB.Recordset

    '        rs_Ren = New ADODB.Recordset
    '        rs_RenValores = New ADODB.Recordset
    '        rs_RenRecibos = New ADODB.Recordset

    '        rs_Ren = adoModulo.adoComando(vg.Conexion, "SELECT * FROM v_Rendicion WHERE Nro='" & NroRendicion & "'")
    '        rs_RenValores = adoModulo.adoComando(vg.Conexion, "SELECT * FROM  v_RendicionValores1 WHERE Nro='" & NroRendicion & "'")
    '        rs_RenRecibos = adoModulo.adoComando(vg.Conexion, "EXECUTE v_Rendicion_Recibos_rpt '" & Right(NroRendicion, 8) & "'")

    '        m_NroRendicion = NroRendicion
    '        m_IdViajante = Format(Trim(rs_Ren("IDCliente")), "000000")
    '        m_F_Rendicion = Mid(rs_Ren("F_Rendicion"), 7, 4) & Mid(rs_Ren("F_Rendicion"), 4, 2) & Mid(rs_Ren("F_Rendicion"), 1, 2)
    '        m_Observaciones = Trim(rs_Ren("Descripcion"))

    '        m_Efectivo = Format(Trim(rs_Ren("Efectivo_Monto") * 100), "00000000000000000")
    '        m_Dolares = Format(Trim(rs_Ren("Dolar_Cantidad") * 100), "00000000000000000")
    '        m_Euros = Format(Trim(rs_Ren("Euros_Cantidad") * 100), "00000000000000000")
    '        m_ChequesMonto = Format(Trim(rs_Ren("Cheques_Monto") * 100), "00000000000000000")
    '        m_CertificadosMonto = Format(Trim(rs_Ren("Certificados_Monto") * 100), "00000000000000000")
    '        m_ChequesCant = Format(rs_Ren("Cheques_Cantidad"), "00")
    '        m_CertificadosCant = Format(rs_Ren("Certificados_Cantidad"), "00")

    '        'rs_RenValores.MoveFirst
    '        m_DetalleValores = ""
    '        Do Until rs_RenValores.EOF
    '0:          m_DetalleValores = m_DetalleValores & rs_RenValores("Bco_Dep_Tipo") & ","
    '1:          m_DetalleValores = m_DetalleValores & Mid(rs_RenValores("Bco_Dep_Fecha"), 7, 4) & Mid(rs_RenValores("Bco_Dep_Fecha"), 4, 2) & Mid(rs_RenValores("Bco_Dep_Fecha"), 1, 2) & ","
    '2:          m_DetalleValores = m_DetalleValores & padLR(rs_RenValores("Bco_Dep_Numero"), 10, "0", 1) & ","
    '3:          m_DetalleValores = m_DetalleValores & Format(Trim(rs_RenValores("Bco_Dep_Monto") * 100), "00000000000000000") & ","
    '4:          m_DetalleValores = m_DetalleValores & Format(rs_RenValores("Bco_Dep_Ch_Cantidad"), "00") & ","
    '5:          m_DetalleValores = m_DetalleValores & padLR(Trim(" " & rs_RenValores("N_Cheque")), 15, " ", 2) & ","
    '6:          m_DetalleValores = m_DetalleValores & Format(rs_RenValores("IdBanco"), "000") & ";"
    '            rs_RenValores.MoveNext()
    '        Loop

    '        'rs_RenRecibos.MoveFirst
    '        m_DetalleRecibos = ""
    '        Do Until rs_RenRecibos.EOF
    '            '0:
    '            m_DetalleRecibos = m_DetalleRecibos & rs_RenRecibos!nroRecibo & ","
    '            '1:
    '            m_DetalleRecibos = m_DetalleRecibos & Mid(rs_RenRecibos("F_Recibo"), 7, 4) & Mid(rs_RenRecibos("F_Recibo"), 4, 2) & Mid(rs_RenRecibos("F_Recibo"), 1, 2) & ","
    '            '2:
    '            m_DetalleRecibos = m_DetalleRecibos & Format(Trim(rs_RenRecibos!Nro_Cuenta), "000000") & ","
    '            '3:
    '            m_DetalleRecibos = m_DetalleRecibos & Format(Trim(rs_RenRecibos!Efectivo * 100), "00000000000000000") & ","
    '            '4:
    '            m_DetalleRecibos = m_DetalleRecibos & Format(Trim(rs_RenRecibos!Divisas_Dolares * 100), "00000000000000000") & ","
    '            '5:
    '            m_DetalleRecibos = m_DetalleRecibos & Format(Trim(rs_RenRecibos!Divisas_Euros * 100), "00000000000000000") & ","
    '            '6:
    '            m_DetalleRecibos = m_DetalleRecibos & Format(Trim(rs_RenRecibos!Cheques_Total * 100), "00000000000000000") & ","
    '            '7:
    '            m_DetalleRecibos = m_DetalleRecibos & Format(rs_RenRecibos!Cheques_Cantidad, "00") & ","
    '            '8:
    '            m_DetalleRecibos = m_DetalleRecibos & Format(Trim(rs_RenRecibos!Certificados_Total * 100), "00000000000000000") & ","
    '            '9:
    '            m_DetalleRecibos = m_DetalleRecibos & Format(rs_RenRecibos!Certificados_Cantidad, "00") & ","
    '            '10:
    '            m_DetalleRecibos = m_DetalleRecibos & Format(Trim(rs_RenRecibos!TotalRecibo * 100), "00000000000000000") & ";"

    '            rs_RenRecibos.MoveNext()
    '        Loop

    '        rs_Ren = Nothing
    '        rs_RenValores = Nothing
    '        rs_RenRecibos = Nothing

    '        DatosObtenidos = True

    '    End Function

    Public Function EnviarRendicion() As Long

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

            resultado = Cliente.EnviarRendicion(m_MacAddress, m_NroRendicion, m_IdViajante, m_F_Rendicion, m_Observaciones, _
                        m_Efectivo, m_Dolares, m_Euros, m_ChequesCant, m_ChequesMonto, m_CertificadosCant, m_CertificadosMonto, _
                        m_DetalleValores, m_DetalleRecibos)

            If resultado = 0 Then
                '                adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Rendicion_Transmicion_Upd '" & Right(m_NroRendicion, 8) & "'")
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

            EnviarRendicion = resultado
        Else
            EnviarRendicion = -1
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
        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If

        On Error GoTo ErrorHandler

        If Conectado Then
            If Not WebServiceInicializado Then
                Cliente = New WSRendicion.RendicionSoapClient("", "http://" & ipAddress & "/wsCatalogo3/Rendicion.asmx?wsdl")
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
            Cliente = New WSRendicion.RendicionSoapClient("", "http://" & ipAddressIntranet & "/wsCatalogo3/Rendicion.asmx?wsdl")
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
