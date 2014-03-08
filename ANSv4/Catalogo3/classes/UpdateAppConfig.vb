Option Explicit On

Public Class UpdateAppConfig

    Private Declare Function EbExecuteLine Lib "vba6.dll" _
                                      (ByVal pStringToExec As Long, ByVal Foo1 As Long, _
                                       ByVal Foo2 As Long, ByVal fCheckOnly As Long) As Long


    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsUpdateAppConfig"

    Private Cliente As WSUpdateAppConfig.appConfigSoapClient
    Private WebServiceInicializado As Boolean
    Public Event SincronizarAppProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)
    Private m_MacAddress As String
    Private m_ipAddress As String

    Public ReadOnly Property Inicializado() As Boolean
        Get
            Return WebServiceInicializado
        End Get
    End Property

    Public Sub Inicializar(ByVal MacAddress As String, _
                           ByVal ipAddress As String, _
                           ByVal ipAddressIntranet As String)

        Dim Conectado As Boolean
        Conectado = My.Computer.Network.Ping(ipAddress, 5000)
        Conectado = True
        If Not Conectado Then
            Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000)
        End If
        On Error GoTo errhandler
        If Not WebServiceInicializado Then
            If Conectado Then
                Cliente = New WSUpdateAppConfig.appConfigSoapClient("", "http://" & ipAddress & "/wsCatalogo3/appConfig.asmx?wsdl")
                If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
                    ' Ver http://stackoverflow.com/questions/951523/how-can-i-set-an-http-proxy-webproxy-on-a-wcf-client-side-service-proxy

                    'Dim b = DirectCast(Cliente.Endpoint.Binding, System.ServiceModel.BasicHttpBinding)
                    'b.ProxyAddress = New Uri("http://127.0.0.1:8888")
                    'b.BypassProxyOnLocal = False
                    'b.UseDefaultWebProxy = False
                    '                    Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
                End If

                m_MacAddress = MacAddress
                m_ipAddress = ipAddress
                WebServiceInicializado = True
            Else
                WebServiceInicializado = False
            End If
        End If

        Exit Sub

errhandler:
        If Err.Number = -2147024809 Then
            ' Intento con el ip interno
            Cliente = New WSUpdateAppConfig.appConfigSoapClient("", "http://" & ipAddressIntranet & "/wsCatalogo3/appConfig.asmx?wsdl")
            If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
                '                    Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
            End If

            m_MacAddress = MacAddress
            WebServiceInicializado = True
            m_ipAddress = ipAddressIntranet
            Err.Clear()
        Else
            Err.Raise(Err.Number, Err.Source, Err.Description)
        End If

    End Sub

    Private Sub SincroAppConfigCompletada(ByRef Cancel As Boolean)

        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
            ' Conexion no valida
            Cancel = True
            Exit Sub
        End If

        Cliente.SincronizacionAppConfigCompletada(m_MacAddress)

    End Sub

    'Public Sub SincronizarApp()

    '    Dim Cancel As Boolean

    '    Cancel = False

    '    RaiseEvent SincronizarAppProgress("Iniciando Sincronización App Config", 0, Cancel)
    '    If Not WebServiceInicializado Then
    '        Cancel = True
    '    End If

    '    If Not Cancel Then
    '        RaiseEvent SincronizarAppProgress("Sincronizando App Config", 40, Cancel)
    '    End If

    '    If Not Cancel Then
    '        Me.ObtenerInfo(Cancel)
    '    End If

    '    If Not Cancel Then
    '        RaiseEvent SincronizarAppProgress("Sincronizando comandos", 60, Cancel)
    '    End If

    '    If Not Cancel Then
    '        Me.ObtenerComandos(Cancel)
    '    End If

    '    If Not Cancel Then
    '        RaiseEvent SincronizarAppProgress("Sincronizando App Config", 75, Cancel)
    '    End If

    '    If Not Cancel Then

    '        If TenerQueEnviarInfo() Then

    '            '                Dim rs As ADODB.Recordset
    '            '                rs = adoModulo.adoComando(vg.Conexion, "SELECT TOP 1 * FROM v_appConfig2")

    '            Dim AuditarProceso As String
    '            Dim EnviarAuditoria As String

    '            If CBool(rs("EnviarAuditoria")) Then '#  And True
    '                EnviarAuditoria = "1"
    '            Else
    '                EnviarAuditoria = "0"
    '            End If

    '            If CBool(rs("Auditor")) Then '#  And True
    '                AuditarProceso = "1"
    '            Else
    '                AuditarProceso = "0"
    '            End If

    '            Cancel = Not EnviarInfo(rs("Cuit") & "", _
    '                              rs("RazonSocial") & "", _
    '                              rs("ApellidoNombre") & "", _
    '                              rs("Domicilio") & "", _
    '                              rs("Telefono") & "", _
    '                              rs("Ciudad") & "", _
    '                              rs("Email") & "", _
    '                              rs("IDans") & "", _
    '                              rs("appCaduca") & "", _
    '                              rs("dbCaduca") & "", _
    '                              rs("PIN") & "", _
    '                              rs("FechaUltimoAcceso") & "", _
    '                              rs("Mensaje") & "", _
    '                              EnviarAuditoria, _
    '                              rs("Url") & "", _
    '                              rs("Modem") & "", _
    '                              rs("appCVersion") & "", _
    '                              rs("Build") & "", _
    '                              rs("appCListaPrecio") & "", _
    '                              AuditarProceso)


    '        End If
    '    End If

    '    If Not Cancel Then
    '        RaiseEvent SincronizarAppProgress("Fin de la sincronizacion de App Config", 100, Cancel)
    '    End If

    '    If Not Cancel Then
    '        SincroAppConfigCompletada(Cancel)
    '    End If

    'End Sub

    Private Function TenerQueEnviarInfo() As Boolean

        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
            ' Conexion no valida
            Exit Function
        End If

        Dim resultado As Long
        resultado = Cliente.TenerQueEnviarInfo(m_MacAddress)
        Return (resultado = 1)

    End Function

    Public Function EnviarInfo( _
            ByVal Cuit As String, ByVal RazonSocial As String, _
            ByVal ApellidoNombre As String, ByVal Domicilio As String, _
            ByVal Telefono As String, ByVal Ciudad As String, _
            ByVal Email As String, ByVal IDAns As String, _
            ByVal appCaduca As String, ByVal dbCaduca As String, ByVal PIN As String, _
            ByVal FechaUltimoAcceso As String, ByVal Mensaje As String, _
            ByVal EnviarAuditoria As String, ByVal Url As String, ByVal Modem As String, _
            ByVal Version As String, ByVal build As String, ByVal ListaPrecio As String, _
            ByVal auditor As String) As Boolean


        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
            ' Conexion no valida
            Exit Function
        End If

        Dim resultado As String
        resultado = Cliente.EnviarInfo(m_MacAddress, _
                      Cuit, RazonSocial, ApellidoNombre, _
                      Domicilio, Telefono, Ciudad, _
                      Email, IDAns, appCaduca, _
                      dbCaduca, PIN, FechaUltimoAcceso, _
                      Mensaje, EnviarAuditoria, Url, Modem, _
                      Version, build, ListaPrecio, auditor)

        Return (resultado = 0)

    End Function

    '    Public Sub ObtenerInfo(ByVal Cancel As Boolean)

    '        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
    '            ' Conexion no valida
    '            Cancel = True
    '            Exit Sub
    '        End If

    '        Dim I As Long
    '        Dim s As String
    '        s = Cliente.ObtenerInfo(m_MacAddress)
    '        Dim fs As Scripting.FileSystemObject
    '        fs = New Scripting.FileSystemObject
    '        Dim ts As Scripting.TextStream

    '        Dim FileName As String
    '        FileName = tmpFileFolders.GetTempFileName

    '        ts = fs.CreateTextFile(FileName, True, True)
    '        ts.Write(s)
    '        ts.Close()

    '        Dim rs As ADODB.Recordset
    '        rs = New ADODB.Recordset
    '        rs.Open(FileName, "Provider=MSPersist;", adOpenStatic, adLockReadOnly)

    '        If rs.RecordCount > 0 Then
    '            rs.MoveLast() ' Can find little errors that can crop up.
    '            rs.MoveFirst()
    '            'i = 1

    '            On Error GoTo Proximo

    '            Do Until rs.EOF
    '                ' Aca va la funcion de actualizacion
    '                Debug.Print(rs("Campo") & "   " & rs("Valor") & "   " & rs("Tipo"))
    '                If (Trim(rs("Campo") & "") <> "") And (Trim(rs("Valor") & "") <> "") Then

    '                    If Left(Trim(rs("Tipo")), 1) = 2 Then 'Actualizo ansAppConfig
    '                        Select Case Trim(rs("Tipo"))
    '                            Case "N" 'Numerico
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE ansConfig SET " & Trim(rs("Campo")) & "=" & Trim(rs("valor")))
    '                            Case "C" ' Char
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE ansConfig SET " & Trim(rs("Campo")) & "='" & Trim(rs("valor")) & "'")
    '                            Case "R" 'Real
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE ansConfig SET " & Trim(rs("Campo")) & "='" & Trim(rs("valor")) & "'")
    '                            Case "D" 'Date
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE ansConfig SET " & Trim(rs("Campo")) & "=#" & Mid$(rs("valor"), 1, 10) & "#")
    '                            Case "X" 'Nulo
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE ansConfig SET " & Trim(rs("Campo")) & "=''")
    '                            Case "Y"
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE ansConfig SET ListaPrecio=" & Trim(rs("valor")))
    '                                If Trim(rs("valor")) = "2" Then
    '                                    adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Precio_upd")
    '                                End If
    '                        End Select
    '                    Else
    '                        Select Case Trim(rs("Tipo"))
    '                            Case "N" 'Numerico
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE AppConfig SET " & Trim(rs("Campo")) & "=" & Trim(rs("valor")))
    '                            Case "C" ' Char
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE AppConfig SET " & Trim(rs("Campo")) & "='" & Trim(rs("valor")) & "'")
    '                            Case "R" 'Real
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE AppConfig SET " & Trim(rs("Campo")) & "='" & Trim(rs("valor")) & "'")
    '                            Case "D" 'Date
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE AppConfig SET " & Trim(rs("Campo")) & "=#" & Mid$(rs("valor"), 1, 10) & "#")
    '                            Case "X" 'Nulo
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE AppConfig SET " & Trim(rs("Campo")) & "=''")
    '                            Case "Y"
    '                                adoModulo.adoComandoIU(vg.Conexion, "UPDATE AppConfig SET ListaPrecio=" & Trim(rs("valor")))
    '                                If Trim(rs("valor")) = "2" Then
    '                                    adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Precio_upd")
    '                                End If
    '                            Case "W"  'cambia el nro del ultimo Pedido,Recibo,Devolucion
    '                                adoModulo.adoComandoIU(vg.Conexion, "EXECUTE " & Trim(rs("Campo")) & " '" & Trim(rs("valor")) & "'")
    '                            Case "V"  'ejecuta cualquier consulta de la db SIN parametros
    '                                adoModulo.adoComandoIU(vg.Conexion, "EXECUTE " & Trim(rs("Campo")))
    '                        End Select
    '                    End If
    '                End If
    '                I = I + 1
    '                If I Mod 2 = 0 Then
    '                    DoEvents()
    '                End If

    'Proximo:
    '                rs.MoveNext()
    '            Loop
    '        End If
    '        On Error GoTo 0

    '        rs.Close()
    '        Kill(FileName)
    '    End Sub

    '    Public Sub ObtenerComandos(ByVal Cancel As Boolean)

    '        On Error Resume Next

    '        If Not My.Computer.Network.Ping(m_ipAddress, 5000) Then
    '            ' Conexion no valida
    '            Cancel = True
    '            Exit Sub
    '        End If

    '        Dim wSalir As Boolean
    '        Dim strComando As String
    '        Dim sComando() As String
    '        Dim Code As Long
    '        Dim I As Long
    '        Dim s As String

    '        wSalir = False

    '        s = Cliente.ObtenerComandos(m_MacAddress)
    '        Dim fs As Scripting.FileSystemObject
    '        fs = New Scripting.FileSystemObject
    '        Dim ts As Scripting.TextStream

    '        Dim FileName As String
    '        FileName = tmpFileFolders.GetTempFileName

    '        ts = fs.CreateTextFile(FileName, True, True)
    '        ts.Write(s)
    '        ts.Close()

    '        Dim rs As ADODB.Recordset
    '        rs = New ADODB.Recordset
    '        rs.Open(FileName, "Provider=MSPersist;", adOpenStatic, adLockReadOnly)

    '        If rs.RecordCount > 0 Then
    '            rs.MoveLast() ' Can find little errors that can crop up.
    '            rs.MoveFirst()
    '            'i = 1

    '            On Error GoTo Proximo

    '            Do Until rs.EOF
    '                ' Aca la funcion de ejecucion de los comandos....
    '                Debug.Print(rs("Comando"))
    '                If Trim(rs("Comando") & "") <> "" Then
    '                    Select Case Left(rs("Comando"), 2)
    '                        Case "sh"
    '                            Shell(Mid(rs("Comando"), 4), vbNormalFocus)
    '                        Case "vb"
    '                            'Alguna Funcion de mantenimiento de visual basic

    '                            If Len(Trim(Mid(rs("comando"), 4))) > 0 Then
    '                                strComando = Trim(Mid(rs("comando"), 4))
    '                                Code = EbExecuteLine(StrPtr(strComando), 0&, 0&, Abs(False)) = 0
    '                            End If

    '                        Case "sw" 'write key setting.ini
    '                            sComando = Split(Mid(rs("Comando"), 4), "//")
    '                            WriteINI(sComando(0), sComando(1), sComando(2))
    '                            If sComando(1) = "mdb" Then
    '                                wSalir = True
    '                            End If
    '                        Case "sd" 'delete key setting.ini
    '                            sComando = Split(Mid(rs("Comando"), 4), "//")
    '                            DeleteKeyINI(sComando(0), sComando(1))
    '                        Case "db"
    '                            adoModulo.adoComandoIU(vg.Conexion, Mid(rs("Comando"), 4))
    '                    End Select
    '                End If

    '                I = I + 1
    '                If I Mod 37 = 0 Then
    '                    DoEvents()
    '                End If

    'Proximo:
    '                rs.MoveNext()
    '            Loop
    '        End If
    '        On Error GoTo 0

    '        rs.Close()
    '        Kill(FileName)

    '        If wSalir Then
    '            If vg.TranActiva Then
    '                vg.Conexion.CommitTrans()
    '                vg.TranActiva = False
    '            End If
    '            MsgBox("Se han efectuado modificaciones en la aplicación," & vbCrLf & "ésta de cerrará, luego re-ingrese nuevamente", vbCritical, "Atención")
    '            mainMod.miEND()
    '        End If

    '    End Sub

End Class
