Option Explicit On

Imports System.Text

Module Inis

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "modINIs"

    Private Declare Function GetPrivateProfileString Lib "kernel32" _
                    Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, _
                                                      ByVal lpKeyName As String, _
                                                      ByVal lpDefault As String, _
                                                      ByVal lpReturnedString As StringBuilder, _
                                                      ByVal nSize As Long, _
                                                      ByVal lpFileName As String) As Long

    Private Declare Function WritePrivateProfileString Lib "kernel32" _
                    Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, _
                                                        ByVal lpKeyName As String, _
                                                        ByVal lpString As String, _
                                                        ByVal lpFileName As String) As Long

    Public Function ReadINI(ByVal strSection As String, _
                            ByVal strKey As String, _
                            Optional ByVal ValorPorDefecto As String = vbNullString) As String

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "ReadINI"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim strBuffer As StringBuilder
        Dim Aux As String


        strBuffer = New StringBuilder(Chr(0), 4096)
        Dim res As Integer = GetPrivateProfileString(strSection, _
                                                     strKey.ToLower(), _
                                                     vbNullString, _
                                                     strBuffer, _
                                                     strBuffer.Length, _
                                                     Application.StartupPath & "\settings.ini")

        Aux = strBuffer.ToString
        If ValorPorDefecto.Length > 0 AndAlso Aux.Length = 0 Then
            Aux = ValorPorDefecto
        End If

        Return Aux

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

    Public Sub WriteINI(ByVal strSection As String, _
                        ByVal strKey As String, _
                        ByVal strkeyvalue As String)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "WriteINI"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        WritePrivateProfileString(strSection, _
                                  UCase$(strKey), _
                                  strkeyvalue, _
                                  Application.StartupPath & "\settings.ini")

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

    Public Sub DeleteKeyINI(ByVal strSection As String, ByVal strKey As String)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "DeleteKeyINI"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        'if the key exists, delete it from the section
        If Trim(strKey) <> "xx" Then
            WritePrivateProfileString(strSection, strKey, _
               vbNullString, Application.StartupPath & "\settings.ini")
        Else
            'no key specified, then delete section
            WritePrivateProfileString( _
               strSection, strKey, vbNullString, Application.StartupPath & "\settings.ini")
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

    Public Sub EliminarRegistroEnINI()

        WriteINI("DATOS", "MachineId", vbNullString)
        WriteINI("DATOS", "RegistrationKey", vbNullString)
        'WriteINI "DATOS", "Nombre", vbNullString
        'WriteINI "DATOS", "Apellido", vbNullString
        'WriteINI "DATOS", "RazonSocial", vbNullString
        'WriteINI "DATOS", "CUIT", vbNullString
        'WriteINI "DATOS", "EMAIL", vbNullString
    End Sub

    Public Function INI_Read(ByVal strSection As String, _
                             ByVal strKey As String, _
                             ByVal IniFile As String, Optional ByVal DefaultValue As String = "") As String

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "INI_READ"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        Dim strBuffer As StringBuilder
        Dim Aux As String

        strBuffer = New StringBuilder(Chr(0), 4096)
        Dim res As Integer = GetPrivateProfileString(strSection, _
                                                     strKey.ToLower, _
                                                     vbNullString, _
                                                     strBuffer, _
                                                     Len(strBuffer), _
                                                     Application.StartupPath & "\" & IniFile)

        Aux = strBuffer.ToString

        If Len(Trim(Aux)) = 0 Then
            INI_Read = DefaultValue
        Else
            INI_Read = Aux
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

    Public Sub INI_Write(ByVal strSection As String, _
                         ByVal strKey As String, _
                         ByVal strkeyvalue As String, _
                         ByVal IniFile As String)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "INI_Write"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        WritePrivateProfileString(strSection, _
                                  UCase$(strKey), _
                                  strkeyvalue, _
                                  Application.StartupPath & "\" & IniFile)

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

    Public Sub INI_DeleteKey(ByVal strSection As String, _
                             ByVal strKey As String, _
                             ByVal IniFile As String)

        '-------- ErrorGuardian Begin --------
        Const PROCNAME_ As String = "INI_DeleteKey"
        On Error GoTo ErrorGuardianLocalHandler
        '-------- ErrorGuardian End ----------

        'if the key exists, delete it from the section
        If Trim(strKey) <> "xx" Then
            WritePrivateProfileString(strSection, strKey, _
               vbNullString, Application.StartupPath & "\" & IniFile)
        Else
            'no key specified, then delete section
            WritePrivateProfileString( _
               strSection, strKey, vbNullString, Application.StartupPath & "\" & IniFile)
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

End Module
