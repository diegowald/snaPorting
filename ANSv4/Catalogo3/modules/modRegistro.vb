Module modRegistro

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "modRegistro"


    Structure WMIPair
        Public first As String
        Public second As String
    End Structure

    Public Function ValidateRegistration(ByVal s As String) As Boolean

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "ValidateRegistration"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        If Len(Trim(s)) = 0 Then
            ValidateRegistration = False
        Else
            ValidateRegistration = s = ObtenerCRC(vg.IDMaquina & vg.IDMaquinaCRC)
        End If

        '-------- ErrorGuardian Begin --------
        Exit Function

ErrorGuardianLocalHandler:
        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            Case vbRetry
                Resume
            Case vbIgnore
                Resume Next
        End Select
        '-------- ErrorGuardian End ----------
    End Function


    ' Modulo Para tomar y comprobar el IDMaquina
    '   Requiere:  Microsoft WMI Scripting V1.2 Library

    Public Function ObtenerIDMaquina() As String

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "ObtenerIDMaquina"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim varSerial(4) As String
        Dim varObjectToID(4) As WMIPair

        'HackerScan
        varObjectToID(1).first = "Win32_BIOS"
        varObjectToID(1).second = "SerialNumber"
        varObjectToID(2).first = "Win32_BaseBoard"
        varObjectToID(2).second = "SerialNumber"
        varObjectToID(3).first = "Win32_Processor"
        varObjectToID(3).second = "ProcessorId"
        varObjectToID(4).first = "Win32_OperatingSystem"
        varObjectToID(4).second = "SerialNumber"

        varSerial(1) = TomarInfoWMI(varObjectToID(1))
        varSerial(2) = TomarInfoWMI(varObjectToID(2))
        varSerial(3) = TomarInfoWMI(varObjectToID(3))
        varSerial(4) = TomarInfoWMI(varObjectToID(4))

        Return vg.miSABOR & ObtenerCRC(varSerial(1) & varSerial(2) & varSerial(3) & varSerial(4))
        '-------- ErrorGuardian Begin --------
        Exit Function

ErrorGuardianLocalHandler:
        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            Case vbRetry
                Resume
            Case vbIgnore
                Resume Next
        End Select
        '-------- ErrorGuardian End ----------

    End Function

    Private Function TomarInfoWMI(ByRef clave As WMIPair) As String

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "TomarInfoWMI"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim obj As New System.Management.ManagementObjectSearcher("Select " & clave.second & " from " & clave.first)
        Dim col = obj.Get()
        For Each mgn In col
            Return mgn.Item(clave.second)
        Next

        Return ""
        '-------- ErrorGuardian Begin --------
        Exit Function

ErrorGuardianLocalHandler:

        Select ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            Case vbRetry
                Resume
            Case vbIgnore
                Resume Next
        End Select
        '-------- ErrorGuardian End ----------

    End Function

    Public Function ObtenerCRC(ByRef s As String) As String

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "ObtenerCRC"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim m As System.Security.Cryptography.MD5 = System.Security.Cryptography.MD5.Create
        Dim data As Byte() = m.ComputeHash(System.Text.Encoding.UTF8.GetBytes(s))

        Dim sBuilder As New System.Text.StringBuilder
        For Each b In data
            sBuilder.Append(b.ToString("x2"))
        Next
        Return sBuilder.ToString

        '-------- ErrorGuardian Begin --------
        Exit Function

ErrorGuardianLocalHandler:
        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            Case vbRetry
                Resume
            Case vbIgnore
                Resume Next
        End Select
        '-------- ErrorGuardian End ----------

    End Function

    Public Sub EliminarRegistroEnINI()

    End Sub

    Public Function ValidateMachineId(ByVal IDMaquinaCRC As String) As Boolean

        Return True

    End Function

End Module
