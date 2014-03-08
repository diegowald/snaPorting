Option Explicit On

Module Main

    '---------------------------------------------------------------
    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "MainMod"

    Public Const u2String As String = "?"
    Public Const c2String As String = "?"
 
    ' VARIABLES GLOBALES
    Public Structure vAPP

        Public miSABOR As LosSabores     ' = SABOR

        Public IDMaquina As String 'vg.misabor & obtenerCRC(WMI)
        Public IDMaquinaCRC As String 'obtenerCRC(vg.IDMaquina)
        Public IDMaquinaREG As String 'obtenerCRC(vg.IDmaquina & vg.IDmaquinaCRC)
        Public LLaveViajante As String
        Public AppActiva As Boolean

        Public Conexion As System.Data.OleDb.OleDbConnection

        Public Cuit As String
        Public RazonSocial As String
        Public ApellidoNombre As String
        Public Domicilio As String
        Public Telefono As String
        Public Ciudad As String
        Public Email As String
        Public NroUsuario As String
        Public NroPDR As String
        Public NroImprimir As String
        Public dbCaduca As Date
        Public appCaduca As Date
        Public FechaUltimoAcceso As Date
        Public F_ActCatalogo As Date
        Public F_ActClientes As Date
        Public EnviarAuditoria As Boolean
        Public AuditarProceso As Boolean
        Public IpSettingIni As Boolean
        Public EsGerente As Boolean
        Public LoginSucceeded As Boolean
        Public ActualizarCatalogo As Boolean
        Public ActualizarCuentas As Boolean
        Public RecienRegistrado As Boolean
        Public xError As Boolean ' Se tuvo que cambiar el nombre de error a err        
        Public TranActiva As System.Data.OleDb.OleDbTransaction
        Public auditor As Auditor
        Public URL_ANS As String
        Public URL_ANS2 As String
        Public ExploradorCaption As String
        Public ValeCombo As Boolean
        Public NoConn As Boolean
        Public Path As String
        Public MiBuild As Integer
        Public ListaPrecio As Byte
        Public FileBak As String
        Public PathAcrobat As String
        Public Dolar As Single
        Public Euro As Single
    End Structure

    Public cstring As String = ""
    Public qstring As String = ""
    Public dstring As String = ""
    Public sstring As String = ""
    Public ArchCerradura As String = ""
    Public ArchLlave As String = ""

    Public vg As vAPP

    Public Sub Main()

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "Main"
        ' On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim I As Integer
        Dim adoREC As OleDb.OleDbDataReader

        ' para evitar varias ejecuciones a la vez
        If PrevInstance() Then
            miEND()
        End If

        Cursor.Current = Cursors.WaitCursor

        If Len(Trim(Dir(Application.StartupPath & "\ans?2??.exe"))) > 0 Then
            MsgBox("Ésta versión NO admite actualización!, Comuniquese con auto náutica sur", vbCritical, "ERROR")
            miEND()
        End If

        'Kill("ans3???.exe")

        ' Determina como se va a comportar la Aplicacion
#If Sabor = "1" Then
  vg.miSABOR = LosSabores.A1_Todos
#ElseIf Sabor = "2" Then
  vg.miSABOR = LosSabores.A2_Clientes
#ElseIf Sabor = "3" Then
  vg.miSABOR = LosSabores.A3_Viajantes
#ElseIf Sabor = "4" Then
  vg.miSABOR = LosSabores.A4_Administrador
#End If

#If NoConn = "1" Then
   vg.NoConn = True
#Else
        vg.NoConn = False
#End If

        vg.AppActiva = True
        vg.RecienRegistrado = False
        vg.AuditarProceso = True
        vg.MiBuild = 0
        vg.Path = Funciones.modINIs.ReadINI("Datos", "Path", Replace(Application.StartupPath, "\bin\Debug", ""))
        vg.PathAcrobat = Funciones.modINIs.ReadINI("Datos", "PathAcrobat", "")
        vg.FileBak = "CopiaCata.001"

        cstring = vg.Path & "\datos\ans.mdb"
        dstring = vg.Path & "\datos\catalogo.mdb"
        sstring = Environ$("windir") & "\Help\KbAppCat.hlp"

        qstring = My.Settings.catalogoConnectionString.ToString


        '--- Pregunta ¿ Está todo en su lugar ? ----------------------
        If Len(Trim(Dir(dstring))) = 0 Then
            Cursor.Current = Cursors.Default
            MsgBox("Error en la instalación del archivo Catalogo", vbCritical, "ERROR")
            miEND()
        End If

        If Len(Trim(Dir(cstring))) = 0 Then
            Cursor.Current = Cursors.Default
            MsgBox("Error en la instalación del archivo de Datos", vbCritical, "ERROR")
            miEND()
        End If

        If Len(Trim(Dir(sstring))) = 0 Then
            Cursor.Current = Cursors.Default
            MsgBox("Error en la instalación del archivo de Seguridad", vbCritical, "ERROR")
            miEND()
        End If
        '--- Fin de Pregunta -------------------------------

        Dim Splash As frmSplash
        Splash = New frmSplash

        Splash.Show()

        Application.DoEvents()

        '--- Verifico si hay actualización de Emergencia de la MDB -----------
        If Trim(Funciones.modINIs.ReadINI("update", "mdb")) = "up200706" Then 'Update MDB

            System.IO.File.Copy(vg.Path & "\Datos\Catalogo.mdb", vg.Path & "\Datos\" & vg.FileBak, False)
            Application.DoEvents()
            System.IO.File.Copy(vg.Path & "\Reportes\Catalogo.mdb", vg.Path & "\Datos\Catalogo.mdb", True)
            Application.DoEvents()

            updateMDB.Emergencia(vg.FileBak)

        End If
        '--- termina update de emergencia ----------------------------------------------

        CambiarLinks("ans.mdb")
        Application.DoEvents()

        '--- Verifico que la version de la MDB y del Ejecutable sean compatible --------
        vg.Conexion = Funciones.adoModulo.GetConn(qstring)

        adoREC = Funciones.adoModulo.adoComando(vg.Conexion, "SELECT * FROM v_appConfig2")
        If Not adoREC.HasRows Then
            MsgBox("Aplicación NO inicializada! (error=Version y Tipo), Comuniquese con auto náutica sur", vbCritical, "Atención")
            miEND() 'End
        Else
            adoREC.Read()
            vg.ListaPrecio = IIf(IsDBNull(adoREC!appCListaPrecio), 0, adoREC!appCListaPrecio)
            vg.MiBuild = adoREC!build

            'If Mid(adoREC!appCVersion, 1, 3) <> Application.ProductVersion.Substring(1, 3) Then
            If False Then 'acá pablo
                MsgBox("INCONSISTENCIA en la versión de la Aplicación!, Comuniquese con auto náutica sur", vbCritical, "Atención")
                miEND()
            End If

            vg.URL_ANS = Trim(Funciones.modINIs.ReadINI("DATOS", "IP", ""))
            If Len(Trim(vg.URL_ANS)) > 0 Then
                vg.IpSettingIni = True
            Else
                vg.URL_ANS2 = IIf(IsDBNull(adoREC!url2), " ", Trim(adoREC!url2))
                vg.URL_ANS = IIf(IsDBNull(adoREC!Url), " ", Trim(adoREC!Url))
                vg.IpSettingIni = False
            End If
        End If
        adoREC = Nothing
        Funciones.adoModulo.adoDesconectar(vg.Conexion)
        '--- Fin chequeo de Version --------

        Application.DoEvents()

        '--- Comienza Registro de la Aplicación ------------------------------
#If Sabor > "1" Then

        '- Tomo el IDMaquina y el REAL Tambien -
        vg.IDMaquina = ObtenerIDMaquina()

        'Creo una ID de Computadora si esta no existe
        vg.IDMaquinaCRC = Trim(Funciones.modINIs.ReadINI("DATOS", "MachineId"))

        Application.DoEvents()

        ' lo siguiente esta anulado por diego
        If Len(vg.IDMaquinaCRC) = 0 Then
            vg.IDMaquinaCRC = modRegistro.ObtenerCRC(vg.IDMaquina)
            modRegistro.EliminarRegistroEnINI()
            Funciones.modINIs.WriteINI("DATOS", "MachineId", vg.IDMaquinaCRC)
        Else
            If Not modRegistro.ValidateMachineId(vg.IDMaquinaCRC) Then
                Cursor.Current = Cursors.Default
                MsgBox("Código Guardado Adulterado." & vbCr & _
                      "Se Genera Codigo de Registro Nuevo." & vbCr & _
                      "Ahora la Aplicacion termina.", vbCritical)

                miEND() 'End
            End If
        End If

AcaRegistro:

        ' Valido Registracion si es que existe
        vg.IDMaquinaREG = Trim(Funciones.modINIs.ReadINI("DATOS", "RegistrationKey"))

        If Not modRegistro.ValidateRegistration(vg.IDMaquinaREG) Then
            'OJO! NO ESTA Registrado
            If vg.miSABOR >= 2 Then
                vg.LLaveViajante = Trim(Funciones.modINIs.ReadINI("DATOS", "LlaveViajante"))
                If Len(Trim(vg.LLaveViajante)) > 0 Then

                    vg.AppActiva = False

                    If vg.NoConn Then
                        'No es necesario activar
                    Else

                        If MsgBox("¿ Desea ACTIVAR la aplicación ahora ?" & vbCrLf & vbCrLf & _
                                "si la aplicación no se activa, NO se pueden realizar actualizaciones" & vbCrLf & vbCrLf & _
                                "- DEBE ESTAR CONECTADO A INTERNET -", _
                                vbQuestion + vbYesNo, "Atención") = vbYes Then

                            ActivarApplicacion()

                        End If
                    End If
                Else
                    '-- Sin Registrar Sabor 2 -----------------------
                    Funciones.modINIs.WriteINI("DATOS", "RegistrationKey", vbNullString)
                    If Not (Splash Is Nothing) Then
                        Splash.Hide()
                        Splash = Nothing
                    End If

                    Cursor.Current = Cursors.Default

                    '- Registro del Programa -
                    frmRegistro.ShowDialog() '-- REGISTRO AUTOMATICO POR INTERNET --
                    If Not vg.RecienRegistrado Then
                        miEND()
                    Else
                        GoTo AcaRegistro
                    End If

                    Cursor.Current = Cursors.WaitCursor
                End If
            End If

        Else    'Registrado OK
            'OK ya esta registrado
            If vg.miSABOR >= 2 Then
                '- CHEQUEO POR UPDATE DEL CATALOGO -------------------------------------------'
                If Not (Splash Is Nothing) Then
                    Splash.Visible = False
                End If
                Cursor.Current = Cursors.Default

                Dim fu As New frmUpdate
VadeNuevo:
                fu.SoloCatalogo = CBool(Funciones.modINIs.ReadINI("DATOS", "SoloCatalogo", "false"))
                fu.Url = vg.URL_ANS
                fu.ShowDialog()

                If vg.xError Then
                    vg.xError = False
                    If MsgBox("Error de Conexión al Servidor, ¿quiere intentar de nuevo?", vbExclamation + vbYesNo, "atención") = vbYes Then
                        vg.URL_ANS = vg.URL_ANS2
                        GoTo VadeNuevo
                    End If
                End If
                fu = Nothing

                Splash.Visible = True
                Cursor.Current = Cursors.WaitCursor
            End If
        End If

#End If '- FIN SESIÓN REGISTRO

        Application.DoEvents()

        '- ABRE LA CONEXION ----------------------------------------------------------'
        vg.Conexion = Funciones.adoModulo.GetConn(qstring)

        vg.auditor = New Auditor
        vg.auditor.Conexion = vg.Conexion

        If Funciones.modINIs.ReadINI("DATOS", "VALECOMBO", "1") = "1" Then
            vg.ValeCombo = True
        Else
            vg.ValeCombo = False
        End If

        Application.DoEvents()

#If Sabor = "1" Then
    'Lectura de las variables de aplicacion
    Set adoREC = adoModulo.adoComando(vg.Conexion, "SELECT * FROM v_appConfig2")
    If adoREC.EOF Then
       MsgBox "Aplicación NO inicializada! (error=appConfig1), Comuniquese con auto náutica sur", vbCritical, "Atención"
       miEND
    Else
      vg.RazonSocial = IIf(IsDBNull(adoREC!RazonSocial), " ", adoREC!RazonSocial)
      vg.ApellidoNombre = IIf(IsDBNull(adoREC!ApellidoNombre), " ", adoREC!ApellidoNombre)
      vg.Domicilio = IIf(IsDBNull(adoREC!Domicilio), " ", adoREC!Domicilio)
      vg.Ciudad = IIf(IsDBNull(adoREC!Ciudad), " ", adoREC!Ciudad)
      vg.Cuit = IIf(IsDBNull(adoREC!Cuit), "0", adoREC!Cuit)
      vg.Email = IIf(IsDBNull(adoREC!Email), " ", adoREC!Email)
      vg.NroUsuario = IIf(IsDBNull(adoREC!IDAns), "00000", Format(adoREC!IDAns, "00000"))
      vg.dbCaduca = IIf(IsDBNull(adoREC!dbCaduca), CDate("01/01/1900"), adoREC!dbCaduca)
      vg.appCaduca = IIf(IsDBNull(adoREC!appCaduca), CDate("01/01/1900"), adoREC!appCaduca)
      vg.F_ActCatalogo = IIf(IsDBNull(adoREC!F_ActCatalogo), CDate("01/01/1900"), adoREC!F_ActCatalogo)
      vg.F_ActClientes = IIf(IsDBNull(adoREC!F_ActClientes), CDate("01/01/1900"), adoREC!F_ActClientes)
      vg.EnviarAuditoria = IIf(IsDBNull(adoREC!EnviarAuditoria), False, adoREC!EnviarAuditoria)
      vg.Modem = IIf(IsDBNull(adoREC!Modem), "Solomon USB Modem", adoREC!Modem)
      vg.URL_ANS = IIf(IsDBNull(adoREC!Url), " ", Trim(adoREC!Url))
      vg.URL_ANS2 = IIf(IsDBNull(adoREC!url2), " ", Trim(adoREC!url2))
    End If
    Set adoREC = Nothing

   'Valido Base de datos
    If (Format(Date, "yyyy/mm/dd") > Format(vg.dbCaduca, "yyyy/mm/dd")) Or _
       (Format(Date, "yyyy/mm/dd") > Format((vg.F_ActCatalogo + 15), "yyyy/mm/dd")) Then
      MsgBox "La vigencia del Catálogo EXPIRO!, debe actualizar por internet o comuniquese con su viajante", vbCritical, "Atención"
    Else
       If ((vg.dbCaduca - Date) < 3) Or (((vg.F_ActCatalogo + 15) - Date) < 3) Then
          MsgBox "Quedan menos de 3 días para la valides del Catálogo, debe actualizar por internet o comuniquese con su viajante", vbExclamation, "Atención"
       End If
    End If

#End If

#If Sabor >= "2" Then    ' Para login

        vg.auditor.Guardar(ans.Enumerados.ObjetosAuditados.Programa, ans.Enumerados.AccionesAuditadas.INICIA, "version=" & vg.miSABOR & "." & String.Format("Versión {0}", My.Application.Info.Version.ToString))


        If Trim(Funciones.modINIs.ReadINI("DATOS", "INFO", "1")) = "0" Or vg.RecienRegistrado Or vg.NoConn Then
            ' Anula Transmicion
            vg.auditor.Guardar(ans.Enumerados.ObjetosAuditados.Seguridad, ans.Enumerados.AccionesAuditadas.CANCELA, "No chequea AppConfigServer")
        Else
            If Funciones.Ping.ConectadoAInternet(vg.URL_ANS, 5, 2) Then
                ActualizarAppConfig()
            End If
        End If

        If Trim(Funciones.modINIs.ReadINI("DATOS", "GERENTE")) = "1" Then
            vg.EsGerente = True
        Else
            vg.EsGerente = False
        End If

        'Lectura de las variables de aplicacion
        adoREC = Funciones.adoModulo.adoComando(vg.Conexion, "SELECT * FROM v_appConfig2")
        If Not adoREC.HasRows Then
            MsgBox("Aplicación NO inicializada! (error=appConfig2), Comuniquese con auto náutica sur", vbCritical, "Atención")
            miEND()
        Else
            adoREC.Read()
            vg.RazonSocial = IIf(IsDBNull(adoREC!RazonSocial), " ", adoREC!RazonSocial)
            vg.ApellidoNombre = IIf(IsDBNull(adoREC!ApellidoNombre), " ", adoREC!ApellidoNombre)
            vg.Domicilio = IIf(IsDBNull(adoREC!Domicilio), " ", adoREC!Domicilio)
            vg.Ciudad = IIf(IsDBNull(adoREC!Ciudad), " ", adoREC!Ciudad)
            vg.Cuit = IIf(IsDBNull(adoREC!Cuit), "0", adoREC!Cuit)
            vg.Email = IIf(IsDBNull(adoREC!Email), " ", adoREC!Email)
            vg.NroUsuario = IIf(IsDBNull(adoREC!IDAns), "00000", Format(adoREC!IDAns, "00000"))
            vg.dbCaduca = IIf(IsDBNull(adoREC!dbCaduca), CDate("01/01/1900"), adoREC!dbCaduca)
            vg.appCaduca = IIf(IsDBNull(adoREC!appCaduca), CDate("01/01/1900"), adoREC!appCaduca)
            vg.F_ActCatalogo = IIf(IsDBNull(adoREC!F_ActCatalogo), CDate("01/01/1900"), adoREC!F_ActCatalogo)
            vg.F_ActClientes = IIf(IsDBNull(adoREC!F_ActClientes), CDate("01/01/1900"), adoREC!F_ActClientes)
            vg.EnviarAuditoria = IIf(IsDBNull(adoREC!EnviarAuditoria), False, adoREC!EnviarAuditoria)
            vg.AuditarProceso = IIf(IsDBNull(adoREC!auditor), False, adoREC!auditor)
            vg.ListaPrecio = IIf(IsDBNull(adoREC!appCListaPrecio), 1, Trim(adoREC!appCListaPrecio))
            vg.MiBuild = IIf(IsDBNull(adoREC!build), " ", Trim(adoREC!build))
            vg.URL_ANS2 = IIf(IsDBNull(adoREC!url2), " ", Trim(adoREC!url2))

            If vg.IDMaquina = "30C3D7F6D9BA6EABFB5CB0F54EF5B35D8" Then vg.IDMaquina = "30C3D7F6D9BA6EABFB5CB0F54EF5B35D8-" & vg.NroUsuario

            If Not vg.IpSettingIni Then
                vg.URL_ANS = IIf(IsDBNull(adoREC!Url), "", Trim(adoREC!Url))
            End If

        End If

        vg.Dolar = CSng(Replace(Funciones.modINIs.INIRead(Application.StartupPath & "\cambio.ini", "General", "dolar", 1), ".", ","))
        vg.Euro = CSng(Replace(Funciones.modINIs.INIRead(Application.StartupPath & "\cambio.ini", "General", "euro", 1), ".", ","))

        Cursor.Current = Cursors.Default

        If CLng(vg.NroUsuario) <= 0 Or CDbl(vg.Cuit) <= 0 Then
            MsgBox("Error en nº de Cuenta ó Cuit, Comuniquese con auto náutica sur", vbCritical, "Atención")
            vg.auditor.Guardar(ans.Enumerados.ObjetosAuditados.Programa, ans.Enumerados.AccionesAuditadas.TERMINA, "Error en nº de Cuenta ó Cuit")
            miEND()
        End If

        If Not IsDBNull(adoREC!Mensaje) Then
            If Len(Trim(adoREC!Mensaje)) > 0 Then
                Call MsgBox(Trim(adoREC!Mensaje), vbInformation Or vbMsgBoxRtlReading, "Tienen un mensaje de auto náutica sur s.r.l.")
                Funciones.adoModulo.adoComandoIU(vg.Conexion, "UPDATE AppConfig SET Mensaje = ''")
                vg.auditor.Guardar(ans.Enumerados.ObjetosAuditados.Rutina, ans.Enumerados.AccionesAuditadas.EXITOSO, "Leyo Mensaje")
            End If
        End If

        Application.DoEvents()

        If Not (IsDBNull(adoREC!FechaUltimoAcceso)) Then
            If Format(adoREC!FechaUltimoAcceso, "yyyy/mm/dd") > Format(Date.Today, "yyyy/mm/dd") Then
                MsgBox("Hora del Sistema con ERROR", vbCritical, "Atención")
                vg.auditor.Guardar(ans.Enumerados.ObjetosAuditados.Programa, ans.Enumerados.AccionesAuditadas.TERMINA, "Hora del Sistema con ERROR")
                miEND()
            Else
                Funciones.adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_UltimoAcceso_upd")
            End If
        Else
            Funciones.adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_UltimoAcceso_upd")
        End If

        adoREC = Nothing

        ' Valido aplicacion
        If Format(Date.Today, "yyyy/mm/dd") > Format(vg.appCaduca, "yyyy/mm/dd") Then
            MsgBox("El uso de la aplicación EXPIRO!, Comuniquese con auto náutica sur", vbCritical, "Atención")
            vg.auditor.Guardar(ans.Enumerados.ObjetosAuditados.Programa, ans.Enumerados.AccionesAuditadas.TERMINA, "El uso de la aplicación EXPIRO!")
            miEND()
        End If

        'Valido Base de datos
        If (Format(Date.Today(), "yyyy/mm/dd") > Format(vg.dbCaduca, "yyyy/mm/dd")) Or _
           (Format(Date.Today(), "yyyy/mm/dd") > Format((vg.F_ActCatalogo.AddDays(15)), "yyyy/mm/dd")) Then
            MsgBox("La vigencia del Catálogo EXPIRO!,  debe actualizar por internet o comuniquese con su viajante", vbCritical, "Atención")
            vg.auditor.Guardar(ans.Enumerados.ObjetosAuditados.Programa, ans.Enumerados.AccionesAuditadas.TERMINA, "La vigencia del Catálogo EXPIRO!")

        Else

            If (DateDiff(DateInterval.Day, vg.dbCaduca, Date.Today) < 3) Or ((DateDiff(DateInterval.Day, vg.F_ActCatalogo.AddDays(15), Date.Today) < 3)) Then
                MsgBox("Quedan menos de 3 días para la valides del Catálogo,  debe actualizar por internet o comuniquese con su viajante", vbExclamation, "Atención")
                vg.auditor.Guardar(ans.Enumerados.ObjetosAuditados.Programa, ans.Enumerados.AccionesAuditadas.INFORMA, "Quedan menos de 5 días para la la validez del Catálogo")
            End If
        End If

        Application.DoEvents()

        If Not (Splash Is Nothing) Then
            Splash.Hide()
            Splash = Nothing
        End If

        vg.ActualizarCatalogo = False
        vg.ActualizarCuentas = False

        If vg.miSABOR > 2 Then     ' Para login
            '-### INICIA LOGIN
            Dim fLogin As frmLogin
            fLogin = New frmLogin

            fLogin.ShowDialog()
            If Not vg.LoginSucceeded Then
                miEND()
            End If

            'YA LOGUEADO
            If vg.miSABOR > 2 Then
                RealizarActualizacion(vg.ActualizarCuentas)
            End If

        End If

#End If

        Cursor.Current = Cursors.Default

        'diego        frmExplorador.Show()

        '-------- ErrorGuardian Begin --------
        Exit Sub

ErrorGuardianLocalHandler:
        If Err.Number = 3265 Then 'No se Encontro el Ordinal del Campo pedido en la tabla
            MsgBox("Inconsistencia en la MDB, Build + Version", vbCritical, "Atención")
            miEND()
        ElseIf Err.Number = 58 Then ' el archivo ya existe CopiaCata.001
            I = CInt(Right(vg.FileBak, 3)) + 1
            vg.FileBak = Left(vg.FileBak, 10) & Format(I, "000")
            Resume
        ElseIf Err.Number = 53 Then ' el archivo de VersionAnterior NO EXISTE
            Resume Next
        Else
            Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
                Case vbRetry
                    Resume
                Case vbIgnore
                    Resume Next
            End Select
        End If
        '-------- ErrorGuardian End ----------

    End Sub

    Public Sub ActualizarCuentas()

        'diego        Dim dlg As New frmConexionUpdate

        'diego        dlg.modoUpdate = UpdateCuentas
        'diego   dlg.Show(vbModal)
        Application.DoEvents()

    End Sub

    Public Sub ActualizarAppConfig()

        'diego Dim dlg As New frmConexionUpdate

        'diego        dlg.modoUpdate = UpdateAppConfig
        'diego   dlg.Show(vbModal)
        Application.DoEvents()

    End Sub

    Public Sub ActivarApplicacion()

        'diego        Dim dlg As New frmConexionUpdate

        'diego        dlg.modoUpdate = ActivarApp
        'diego  dlg.Show(vbModal)
        Application.DoEvents()

    End Sub

    Private Sub RealizarActualizacion(ByVal bActualizarClientes As Boolean)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "RealizarActualizacion"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        If bActualizarClientes Then

            If True Then ' pablo  If ProbarConexion Then

                If bActualizarClientes Then
                    vg.auditor.Guardar(ObjetosAuditados.ActualizacionCliente, AccionesAuditadas.INICIA)
                    ActualizarCuentas()
                    vg.auditor.Guardar(ObjetosAuditados.ActualizacionCliente, AccionesAuditadas.TERMINA)
                End If

                Dim Splash As frmSplash
                Splash = New frmSplash
                Splash.Show()

                ' Despues de una actualizacion hago el mantenimiento correspondiente
                If bActualizarClientes Then
                    vg.auditor.Guardar(ObjetosAuditados.SubRutina, AccionesAuditadas.INICIA, "Compactado DB")
                    Funciones.adoModulo.adoDesconectar(vg.Conexion)

                    Funciones.adoModulo.CompactDatabase()

                    Funciones.adoModulo.adoConectar(vg.Conexion, qstring)

                    vg.auditor.Guardar(ObjetosAuditados.SubRutina, AccionesAuditadas.TERMINA, "Compactado DB")

                    Application.DoEvents()

                    Dim adoREC As System.Data.OleDb.OleDbDataReader
                    adoREC = Funciones.adoModulo.adoComando(vg.Conexion, "SELECT URL FROM v_appConfig2")
                    If Not adoREC.HasRows Then
                        MsgBox("Aplicación NO inicializada! (error=Url3), Comuniquese con auto náutica sur", vbCritical, "Atención")
                        miEND()
                    Else
                        adoREC.Read()
                        adoREC = Funciones.adoModulo.adoComando(vg.Conexion, "SELECT * FROM v_appConfig2")
                        vg.F_ActClientes = IIf(IsDBNull(adoREC!F_ActClientes), CDate("01/01/1900"), adoREC!F_ActClientes)
                    End If

                End If

                If Not (Splash Is Nothing) Then
                    Splash.Hide()
                    Splash = Nothing
                End If

            End If
        End If

        '-------- ErrorGuardian Begin --------
        Exit Sub

ErrorGuardianLocalHandler:
        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            Case vbRetry
                Resume
            Case vbIgnore
                Resume Next
        End Select
        '-------- ErrorGuardian End ----------

    End Sub

    Public Sub AbortarYA()

        On Error Resume Next

        Dim I As Integer
        Dim J As Integer

        J = Application.OpenForms.Count - 1

        'close all sub forms
        For I = J To 0 Step -1
            On Error Resume Next
            Application.OpenForms(I).Close()    'Unload(Forms(I))
            If Not (Err() Is Nothing) Then
                If Err.Number = 9 Then
                    Err.Clear()
                Else
                    Err.Raise(Err.Number)
                End If
            End If
            On Error GoTo 0
        Next I

        If Not (vg.auditor Is Nothing) Then
            vg.auditor = Nothing
        End If

        If Not (vg.Conexion Is Nothing) Then
            vg.Conexion = Nothing
        End If

        End

    End Sub

    Public Sub miEND()

        On Error Resume Next

        Dim I As Integer
        Dim J As Integer

        J = Application.OpenForms.Count - 1

        'close all sub forms
        For I = J To 0 Step -1
            On Error Resume Next
            Application.OpenForms(I).Close()
            If Not (Err() Is Nothing) Then
                If Err.Number = 9 Then
                    Err.Clear()
                Else
                End If
            End If
            On Error GoTo 0
        Next I

        If Not (vg.auditor Is Nothing) Then
            vg.auditor = Nothing
        End If

        If Not (vg.Conexion Is Nothing) Then
            vg.Conexion = Nothing
        End If

        If J <= 0 Then
            End
        End If

    End Sub

    Function PrevInstance() As Boolean
        Return UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0
    End Function

End Module
