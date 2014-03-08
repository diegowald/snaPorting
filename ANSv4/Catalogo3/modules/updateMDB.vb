Option Explicit On

Imports System.Data
Imports System.Data.OleDb

Module updateMDB

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "updateMDB"

    Public Sub Emergencia(ByVal db As String)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "Emergencia"
        On Error Resume Next 'GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Cursor.Current = Cursors.WaitCursor

        CambiarLinks(db)

        Dim sCN As String = My.Settings.catalogoConnectionString.ToString
        Dim adoCN As New System.Data.OleDb.OleDbConnection

        Funciones.adoModulo.adoConectar(adoCN, sCN)

        If vg.TranActiva Is Nothing Then
            vg.TranActiva = vg.Conexion.BeginTransaction
        End If

        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaAppConfig")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaPedidoEnc")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaPedidoDet")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaDevolucionEnc")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaDevolucionDet")
        Application.DoEvents()

        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaReciboEnc")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaReciboDet")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaReciboApp")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaReciboDed")
        Application.DoEvents()

        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexatblLineasPorcentaje")
        Application.DoEvents()

        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaClientes")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaCtaCte")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaClientesNovedades")

        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xIDsCatalogoBAK_Pedidos_Anexa")
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xIDsCatalogoBAK_Devolucion_Anexa")

        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaCatalogoBAK")

        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaAuditor")
        Application.DoEvents()

        'If TableExists("bkpTblInterDeposito1", adoCN) Then
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaInterDeposito")
        'If TableExists("bkpTblInterDeposito_Fac1", adoCN) Then
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaInterDeposito_Fac")
        'If TableExists("bkptblRendicion1", adoCN) Then
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaRendicion")
        'If TableExists("bkpbkptblRendicionValores1", adoCN) Then
        Funciones.adoModulo.adoComandoIU(adoCN, "EXEC xAnexaRendicionValores")

        Application.DoEvents()

        If Not vg.TranActiva Is Nothing Then
            vg.TranActiva.Commit()
            vg.TranActiva = Nothing
        End If

        Funciones.modINIs.DeleteKeyIni("update", "mdb")

        Kill(vg.Path & "\Reportes\Catalogo.mdb")

        Kill(vg.Path & "\up200706.exe")

        Funciones.adoModulo.adoDesconectar(adoCN)

        adoCN = Nothing

        Cursor.Current = Cursors.Default

        'Exit Sub

        '-------- ErrorGuardian Begin --------
        'ErrorGuardianLocalHandler:
        '
        '    If Err.Number = -2147467259 Then
        '        Resume Next
        '    Else
        '        DeleteKeyINI "update", "mdb"
        '        Kill vg.Path & "\Reportes\Catalogo.mdb"
        '        adoModulo.adoDesconectar adoCN
        '        Screen.MousePointer = vbDefault
        '
        '        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
        '            Case vbRetry
        '                Resume
        '            Case vbIgnore
        '                Resume Next
        '        End Select
        '    End If
        '-------- ErrorGuardian End ----------

    End Sub

    Public Sub CambiarLinks(ByVal db As String)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "CambiarLinks"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim sCN As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dstring & ";User Id=hpcd-rw;Password=data700mb;jet oledb:system database=C:\WINDOWS\Help\kbAppCat.hlp"

        Dim adoCN As New ADODB.Connection()

        Funciones.adoModulo.adoDbConectar(adoCN, sCN)

        ProcesarTablasLinks(adoCN, db)

        Funciones.adoModulo.adoDbDesconectar(adoCN)
        adoCN = Nothing

        Exit Sub

        '-------- ErrorGuardian Begin --------
ErrorGuardianLocalHandler:

        Funciones.adoModulo.adoDbDesconectar(adoCN)
        adoCN = Nothing

        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            Case vbRetry
                Resume
            Case vbIgnore
                Resume Next
        End Select
        '-------- ErrorGuardian End ----------

    End Sub

    Private Sub ProcesarTablasLinks(ByRef Conexion As ADODB.Connection, db As String)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "ProcesarTablasLinks"
        On Error Resume Next  'ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim Adox_Cat As ADOX.Catalog
        Dim Adox_Tbl As ADOX.Table

        Adox_Cat = New ADOX.Catalog
        Adox_Cat.ActiveConnection = Conexion

        For Each Adox_Tbl In Adox_Cat.Tables
            Application.DoEvents()
            If Left(LCase(db), 9) = "copiacata" Then
                If Adox_Tbl.Type = "LINK" And Left(LCase(Adox_Tbl.Name), 3) = "bkp" Then
                    Adox_Tbl.Properties("Jet OLEDB:Link Datasource").Value = vg.Path & "\datos\" & db
                End If
            ElseIf db = "ans.mdb" Then
                If Adox_Tbl.Type = "LINK" And Left(LCase(Adox_Tbl.Name), 3) <> "bkp" Then
                    Adox_Tbl.Properties("Jet OLEDB:Link Datasource").Value = vg.Path & "\datos\ans.mdb"
                End If
            End If
        Next

        Adox_Cat = Nothing

        Exit Sub

        '-------- ErrorGuardian Begin --------
ErrorGuardianLocalHandler:

        Adox_Cat = Nothing

        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            Case vbRetry
                Resume
            Case vbIgnore
                Resume Next
        End Select
        '-------- ErrorGuardian End ----------

    End Sub

    Public Sub CambiarQuery(ByVal pQueryNombre As String, ByVal pQueryComando As String)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "CambiarQuery"
        On Error Resume Next 'GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim sCN As String = My.Settings.catalogoConnectionString.ToString
        Dim adoCN As New ADODB.Connection

        Funciones.adoModulo.adoDbConectar(adoCN, sCN)

        '-- acá va el codigo del cambio de la consulta -----
        Dim adoCMD As New OleDbCommand
        Dim Adox_Cat As New ADOX.Catalog

        adoCMD.CommandText = pQueryComando

        Adox_Cat.ActiveConnection = adoCN
        Adox_Cat.Views.Delete(pQueryNombre)
        Adox_Cat.Views.Append(pQueryNombre, adoCMD)

        Adox_Cat = Nothing
        adoCMD = Nothing
        '-- fin cambio consulta ----------------------------

        Funciones.adoModulo.adoDbDesconectar(adoCN)
        adoCN = Nothing

        Exit Sub

        '-------- ErrorGuardian Begin --------
ErrorGuardianLocalHandler:

        Adox_Cat = Nothing
        adoCMD = Nothing

        Funciones.adoModulo.adoDbDesconectar(adoCN)
        adoCN = Nothing

        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            Case vbRetry
                Resume
            Case vbIgnore
                Resume Next
        End Select
        '-------- ErrorGuardian End ----------

    End Sub

    Public Sub Cambiar_usp(ByVal pQueryNombre As String, ByVal pQueryComando As String)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "Cambiar_usp"
        On Error Resume Next 'GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim sCN As String = My.Settings.catalogoConnectionString.ToString
        Dim adoCN As New ADODB.Connection

        Funciones.adoModulo.adoDbConectar(adoCN, sCN)

        '-- acá va el codigo del cambio de la consulta -----
        Dim adoCMD As New OleDbCommand
        Dim Adox_Cat As New ADOX.Catalog

        adoCMD.CommandText = pQueryComando

        Adox_Cat.ActiveConnection = adoCN
        Adox_Cat.Procedures.Delete(pQueryNombre)
        Adox_Cat.Procedures.Append(pQueryNombre, adoCMD)

        Adox_Cat = Nothing
        adoCMD = Nothing
        '-- fin cambio consulta ----------------------------

        Funciones.adoModulo.adoDbDesconectar(adoCN)
        adoCN = Nothing

        Exit Sub

        '-------- ErrorGuardian Begin --------
ErrorGuardianLocalHandler:

        Adox_Cat = Nothing
        adoCMD = Nothing

        Funciones.adoModulo.adoDbDesconectar(adoCN)
        adoCN = Nothing

        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            Case vbRetry
                Resume
            Case vbIgnore
                Resume Next
        End Select
        '-------- ErrorGuardian End ----------

    End Sub

    'Private Function TableExists(ByVal strTableName As String, ByVal oConn As OleDbConnection) As Boolean

    '    Dim oRS As DataSet
    '    On Error Resume Next

    '    oRS = oConn.Execute(strTableName, , adCmdTable)
    '    On Error GoTo 0

    '    TableExists = Not (oRS Is Nothing)

    '    If TableExists Then
    '        oRS = Nothing
    '    End If

    'End Function

End Module
