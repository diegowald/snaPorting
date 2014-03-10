Option Explicit On

Module Main

    '---------------------------------------------------------------
    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "MainMod"

    Public Sub Main()

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "Main"
        ' On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

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

    Function PrevInstance() As Boolean
        Return UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0
    End Function

End Module
