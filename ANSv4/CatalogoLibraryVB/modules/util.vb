Option Explicit On

Module util

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "Util"

    '- ErrorGuardianSkipModule - Do not remove this line !
    Public ErrorGuardianUserReply As Long
    Private BuildNumber As String


    '- Declaraciones usadas para PC NAME --
    Private Declare Function GetComputerName Lib "kernel32" Alias "GetComputerNameA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long

    '- Declaraciones para Icon Program SetApplicationIcon---

    '- Declaraciones usadas para setear CAPSLOCK --
    Private kbArray As KeyboardBytes

    Private Class KeyboardBytes
        <VBFixedArray(256)> Public kbByte() As Byte
        Public Sub New()
            ReDim kbByte(0 To 255)
        End Sub
    End Class


    Private Const VK_CAPITAL As Long = &H14
    '- Seleccion de linea completa en el listview --
    Private Const LVM_FIRST As Long = &H1000

    Private Const WM_SETREDRAW As Long = &HB
    '    Private Const m_sSTRETCHMODE As Long = vbPaletteModeNone

    Public Structure Seleccione
        Public Campo1 As String
        Public Campo2 As String
        Public variable1 As Object
        Public variable2 As Object
        Public Titulo As String
        Public txtComando As String
    End Structure
    Public selec As Seleccione

    '- Tipos válidos para el manejador de errores --
    Public Enum tError
        pResume = 1
        pResumeNext = 2
        pEnd = 3
        pExit = 4
    End Enum

#If False Then 'Trick preserves Case of Enums when typing in IDE
Private pResume, pResumeNext, pEnd, pExit
#End If

    Private wTranActiva As Boolean
    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal Hwnd As Long, _
                                                                                ByVal nIndex As Long) As Long
    '    Private Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal Hwnd As Long, _
    '                                                                           ByVal wMsg As Long, _
    '                                                                          ByVal wParam As Long, _
    '                                                                         ByVal lParam As Any) As Long
    Private Declare Function GetKeyboardState Lib "user32" (ByVal kbArray As KeyboardBytes) As Long
    Private Declare Function SetKeyboardState Lib "user32" (ByVal kbArray As KeyboardBytes) As Long
    Public Declare Function SendMessageLong Lib "user32" Alias "SendMessageA" (ByVal Hwnd As Long, _
                                                                               ByVal msg As Long, _
                                                                               ByVal wParam As Long, _
                                                                               ByVal lParam As Long) As Long
    Private Declare Function StretchBlt Lib "gdi32" (ByVal hDC As Long, _
                                                     ByVal X As Long, _
                                                     ByVal Y As Long, _
                                                     ByVal nWidth As Long, _
                                                     ByVal nHeight As Long, _
                                                     ByVal hSrcDC As Long, _
                                                     ByVal xSrc As Long, _
                                                     ByVal ySrc As Long, _
                                                     ByVal nSrcWidth As Long, _
                                                     ByVal nSrcHeight As Long, _
                                                     ByVal dwRop As Long) As Long
    Private Declare Function SetStretchBltMode Lib "gdi32" (ByVal hDC As Long, _
                                                            ByVal hStretchMode As Long) As Long
    Private Declare Function SelectObject Lib "gdi32" (ByVal hDC As Long, ByVal hObject As Long) As Long
    Private Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hDC As Long) As Long
    Private Declare Function GetStretchBltMode Lib "gdi32" (ByVal hDC As Long) As Long

    Private Declare Function GetModuleHandle Lib "kernel32.dll" Alias "GetModuleHandleA" (ByVal lpModuleName As String) As Long

    '    Public Function ErrorGuardianGlobalHandler(ByVal m_sMODULENAME_ As String, _
    '                                      ByVal PROCNAME_ As String) As Long

    '    Dim errLogo As MsgBoxStyle
    '    Dim errTipo As String
    '    Dim sErr As String

    'Dim sErrCompleto As String
    'Dim verMSG As Byte
    'Dim TratarError As Long

    '    If Not (vg.TranActiva Is Nothing) Then
    '        vg.TranActiva.Rollback()
    '        vg.TranActiva = Nothing
    '    End If

    '    verMSG = 0
    '    errLogo = vbExclamation
    '    errTipo = "Atención"
    '    sErr = ""
    '    sErrCompleto = ""

    '    TratarError = vbIgnore

    '    Select Case Err.Number
    '        Case -2147467259
    '            verMSG = 0
    '            sErr = "La operación NO se ha podido efectuar en éste momento, intente más tarde, " & _
    '            "si el error persite, comuniquese con auto náutica sur"
    '            TratarError = vbIgnore
    '        Case -2147217873
    '            verMSG = 2
    '            sErr = "Registro existente ó  se está usando en otro archivo"
    '            TratarError = vbIgnore
    '        Case -2147217900
    '            verMSG = 2
    '            sErr = "Clave, DUPLICADA"
    '            TratarError = vbIgnore
    '        Case 20514 'Crystal Reports: archivo encontrado hay que sobreescribir
    '            If MsgBox("El archivo ya existe, desea sobreescribirlo?", vbExclamation + vbYesNo, "Atención") = vbYes Then
    ''Kill frmliscalle.CD.FileName
    '                TratarError = vbIgnore
    '            Else 'NOT MSGBOX("EL ARCHIVO YA EXISTE, DESEA SOBREESCRIBIRLO?",...
    '                TratarError = vbAbort
    '            End If
    '        Case -2147024809
    '            verMSG = 2
    '            sErr = "Fallo el Servico de WEB, Verifique conexión a Internet"
    '            TratarError = vbIgnore
    '    End Select

    '    If verMSG = 2 Then
    '        MsgBox(sErr, errLogo, errTipo)
    '        ErrorGuardianGlobalHandler = TratarError
    '        Err.Clear()
    '    Else 'NOT VERMSG...
    '        If verMSG = 99 Then '-- ERROR GRAVISIMO y ABORTAR --
    '' NO HACER NADA ........
    '            ErrorGuardianGlobalHandler = vbAbort
    '            Err.Clear()
    '        ElseIf verMSG = 98 Then '-- NO HACER NADA e IGNORAR ----
    '            ErrorGuardianGlobalHandler = vbIgnore
    '            Err.Clear()
    '        ElseIf verMSG = 97 Then '-- NO HACER NADA y REINTENTAR ----
    '            ErrorGuardianGlobalHandler = vbRetry
    '            Err.Clear()
    '        Else
    '            sErrCompleto = "Error Numero    : " & Err.Number & vbNewLine & _
    '                "Build Number    : " & Application.ProductVersion & "." & vbNewLine & _
    '                "Module Nombre   : " & m_sMODULENAME_ & vbNewLine & _
    '                "Procedure Name  : " & PROCNAME_ & vbNewLine & _
    '                "Numero de Línea : " & Err.Source & " (" & Erl() & ")" & vbNewLine & _
    '                "Día y Hora      : " & Now & vbNewLine & _
    '                "Descripción     : " & Err.Description

    '            If Len(Trim(sErr)) = 0 Then
    '                sErr = sErrCompleto
    '            End If

    'Dim f As frmErrorGuardian
    '            f = New frmErrorGuardian

    '            f.ModuloName = m_sMODULENAME_
    '            f.ErrDetails = sErr
    '            f.ErrCompleto = sErrCompleto

    '            f.ShowDialog()

    '            ErrorGuardianGlobalHandler = ErrorGuardianUserReply

    '            Err.Clear()

    '            f = Nothing

    '            If ErrorGuardianGlobalHandler = vbAbort Then
    '                AbortarYA()
    '            End If

    '        End If
    '    End If

    'End Function


    ' Me indica si estoy en el entorno de desarrollo o no
    Public Function InIDE() As Boolean
        InIDE = (GetModuleHandle("vba6") <> 0)
    End Function

    ' Carga un AVI desde los RES al Control
    '  Public Sub AVIResPLAY(ByVal Este As Object, ByRef Control As Animation)
    '      Dim FileNum As Integer
    '      Dim DataArray() As Byte

    '      Control.Tag = GetTempFileName()

    '      DataArray = LoadResData(Este, "AVIS")

    '      FileNum = FreeFile()
    'Open Control.Tag For Binary As #FileNum
    '   Put #FileNum, 1, DataArray()
    'Close #FileNum

    '      Control.Visible = True
    '      Control.Open(Control.Tag)
    '      Control.Play()

    '  End Sub

    ' Descarga y frena la animacion del Control cargado con el RES
    ' La propiedad del control NO DEBE estar en Autoplay
    'Public Sub AVIResSTOP(ByRef Control As Animation)
    '    Control.Stop()
    '    Control.Close()
    '    Control.Visible = False
    '    Kill(Control.Tag)
    'End Sub

    'Public Function CargarImagen(ByVal NombreArchImagen As String, _
    '                             ByRef ControlDest As PictureBox) As String

    '    Dim AuxPicture As StdPicture
    '    Dim wPicture As String
    '    Dim W As Long
    '    Dim H As Long
    '    Dim wd As Long
    '    Dim hd As Long
    '    Dim wdMax As Long
    '    Dim hdMax As Long
    '    Dim Escala As Double
    '    Dim lDC As Long
    '    Dim lOldObj As Long

    '    If LenB(NombreArchImagen) = 0 Then
    '        NombreArchImagen = "default.jpg"
    '    End If

    '    wPicture = NombreArchImagen

    '    If LenB(Dir(wPicture)) = 0 Then
    '        AuxPicture = LoadPicture(vg.Path & "\imagenes\articulos\default.jpg")
    '        CargarImagen = vg.Path & "\imagenes\articulos\default.jpg"
    '    Else 'NOT LENB(DIR(WPICTURE))...
    '        AuxPicture = LoadPicture(wPicture)
    '        CargarImagen = wPicture
    '    End If

    '    W = AuxPicture.Width
    '    H = AuxPicture.Height

    '    With ControlDest
    '        wd = .Width
    '        hd = .Height
    '        wdMax = .Width
    '        hdMax = .Height
    '    End With 'ControlDest

    '    If H > W Then
    '        Escala = hd / H
    '        wd = W * Escala
    '        If wd > wdMax Then
    '            Escala = Escala * wdMax / wd
    '            wd = wdMax
    '            hd = H * Escala
    '        End If
    '    Else 'NOT H...
    '        Escala = wd / W
    '        hd = H * Escala
    '        If hd > hdMax Then
    '            Escala = Escala * hdMax / hd
    '            hd = hdMax
    '            wd = W * Escala
    '        End If
    '    End If

    '    ControlDest.PaintPicture(AuxPicture, 0, 0, wd, hd)
    'ControlDest.Line (0, hd - 2 * Screen.TwipsPerPixelY)-(wdMax, hdMax), RGB(255, 255, 255), B
    'ControlDest.Line (wd - 2 * Screen.TwipsPerPixelX, 0)-(wdMax, hdMax), RGB(255, 255, 255), B
    '    ControlDest.Refresh()


    'End Function

    'Public Function BuscarIndiceEnListView(ByRef List As ListView, ByVal Buscar As String, ByVal ColBuscar As Integer) As Integer

    '    Dim strTexto As String
    '    Dim blnFound As Boolean
    '    Dim itm As ListViewItem

    '    On Error Resume Next

    '    BuscarIndiceEnListView = -1

    '    If ColBuscar = 0 Then
    '        itm = List.FindItem(Buscar, ColBuscar, , lvwPartial)
    '        If itm Is Nothing Then   ' Si no hay coincidencia, informa al                                     ' usuario y sale.
    '            BuscarIndiceEnListView = -1
    '        Else
    '            BuscarIndiceEnListView = itm.Index
    '        End If
    '    Else
    '        For Each itm In List.ListItems
    '            If ColBuscar = 0 Then
    '                strTexto = Left$(itm.Text, Len(Buscar))
    '            Else
    '                strTexto = Left$(itm.SubItems(ColBuscar), Len(Buscar))
    '            End If

    '            If UCase$(strTexto) = UCase$(Buscar) Then
    '                BuscarIndiceEnListView = itm.Index
    '                Exit For
    '            End If
    '        Next itm
    '    End If

    'End Function

    'Public Function BuscarIndiceEnCombo(ByRef Combo As ComboBox, _
    '                                    ByVal ItemData As String, _
    '                                    ByVal EsList As Boolean) As Integer

    '    Dim iIndice As Integer

    '    If Combo.ListCount = 0 Then
    '        Err.Raise(vbObjectError + 1000, "Función BuscarIndiceEnCombo(Combo, ID)", "No se puede buscar el indice de un itemdata en un combo vacio")
    '    Else 'NOT COMBO.LISTCOUNT...
    '        If EsList Then
    '            Combo.ListIndex = 0
    '        End If
    '        For iIndice = 0 To Combo.ListCount - 1
    '            If EsList Then
    '                If UCase$(Trim$(Combo.Text)) = UCase$(Trim$(ItemData)) Then
    '                    BuscarIndiceEnCombo = Combo.ListIndex
    '                    Exit For
    '                End If
    '                Combo.ListIndex = Combo.ListIndex + 1
    '            Else 'ESLIST = FALSE/0
    '                If Combo.ItemData(iIndice) = ItemData Then
    '                    BuscarIndiceEnCombo = iIndice
    '                    Exit For
    '                End If
    '            End If
    '        Next iIndice
    '    End If

    'End Function

    'Public Sub CargarCombo(ByRef Combo As ComboBox, _
    '                       ByVal Tabla As String, _
    '                       ByVal Campo1 As String, _
    '                       ByVal Campo2 As String, _
    '                       ByRef Conexion As OleDbConnection, _
    '                       Optional ByVal Concatena As Boolean = False)

    '    On Error Resume Next

    '    Dim rs As New ADODB.Recordset

    '    If LCase(Trim(Tabla)) = LCase("v_Recibo_Enc_noR_cbo") Then
    '        rs = adoComando(Conexion, "select " & Campo1 & "," & Campo2 & " from " & Tabla & " order by " & Campo1 & " desc")
    '    Else
    '        rs = adoComando(Conexion, "select " & Campo1 & "," & Campo2 & " from " & Tabla & " order by " & Campo1)
    '    End If

    '    'Set rs = xGetRS(Conexion, Tabla, "ALL", Campo1)

    '    If Not (rs.BOF And rs.EOF) Then
    '        rs.MoveFirst()
    '        Do While Not rs.EOF
    '            If Not IsNull(rs.Fields(Campo1).value) Then
    '                If Concatena Then
    '                    Combo.ADDItem(rs.Fields(Campo2).value & " - " & rs.Fields(Campo1).value)
    '                Else 'CONCATENA = FALSE/0
    '                    Combo.ADDItem(rs.Fields(Campo1).value)
    '                End If
    '                If IsNumeric(rs.Fields(Campo2).value) Then
    '                    Combo.ItemData(Combo.NewIndex) = rs.Fields(Campo2).value
    '                End If
    '            End If
    '            rs.MoveNext()
    '        Loop
    '    End If
    '    rs = Nothing

    'End Sub

    'Public Sub Centrarfrm(ByRef pForm As Form)
    '    With pForm
    '        .Left = Funciones.modINIs.ReadINI("Preferencias", "MainLeft", 1000)
    '        .Top = Funciones.modINIs.ReadINI("Preferencias", "MainTop", 1000)
    '        .Width = Funciones.modINIs.ReadINI("Preferencias", "MainWidth", .Width)
    '        .Height = Funciones.modINIs.ReadINI("Preferencias", "MainHeight", .Height)
    '        .Top = (Screen.Height - .Height) / 2 - 750
    '        .Left = (Screen.Width - .Width) / 2
    '    End With 'pForm
    'End Sub

    'Public Sub GuardarFormatoForm(ByRef pForm As Form)
    '    With pForm
    '        Funciones.modINIs.WriteINI("Preferencias", "MainLeft", .Left)
    '        Funciones.modINIs.WriteINI("Preferencias", "MainTop", .Top)
    '        Funciones.modINIs.WriteINI("Preferencias", "MainWidth", .Width)
    '        Funciones.modINIs.WriteINI("Preferencias", "MainHeight", .Height)
    '    End With 'pForm
    'End Sub

    Public Function noquote(ByVal wTexto As Object) As String


        Dim wPos As Integer
        Dim wLong As Integer

        wPos = InStr(1, wTexto, "'", vbTextCompare)
        wLong = Len(wTexto)
        Do While wPos <> 0
            If wPos = 1 Then
                wTexto = "´" & Mid$(wTexto, 2)
            Else 'NOT WPOS...
                If wPos = wLong Then
                    wTexto = Mid$(wTexto, 1, wLong - 1) & "´"
                Else 'NOT WPOS...
                    wTexto = Mid$(wTexto, 1, wPos - 1) & "´" & Mid$(wTexto, wPos + 1)
                End If
            End If
            wPos = InStr(1, wTexto, "'", vbTextCompare)
        Loop
        noquote = wTexto

    End Function

    Public Sub SetApplicationIcon(ByVal hIcon As Long, _
                                  ByVal mainHwnd As Long)

        ' hIcon must be a 32x32 pixel icon mainHwnd can be any open form
        ' This will not work in IDE but works when compiled

        Dim tHwnd As Long
        Dim cParent As Long
        Const ICON_BIG As Long = 1
        Const GWL_HWNDPARENT As Long = (-8)
        Const WM_SETICON As Long = &H80

        ' Get starting point
        tHwnd = GetWindowLong(mainHwnd, GWL_HWNDPARENT)
        ' Get EXE's wrapper class (all VB compiled apps have them)
        Do While tHwnd
            cParent = tHwnd
            tHwnd = GetWindowLong(cParent, GWL_HWNDPARENT)
        Loop
        On Error Resume Next
        ' tell the wrapper class what icon to use
        'PostMessage cParent, WM_SETICON, ICON_BIG, ByVal hIcon
        On Error GoTo 0

    End Sub

    'Public Sub SoloMontos(ByRef miTextbox As TextBox, ByRef KeyAscii As Integer)

    '    On Error Resume Next

    '    If KeyAscii = 44 Or KeyAscii = 46 Then
    '        If InStr(1, miTextbox.Text, ",", vbTextCompare) > 0 Or InStr(1, miTextbox.Text, ".", vbTextCompare) > 0 Then
    '            KeyAscii = 0
    '        Else
    '            SoloDigitos(KeyAscii)
    '        End If
    '    Else
    '        SoloDigitos(KeyAscii)
    '    End If

    'End Sub

    Public Sub SoloDigitos(ByRef pKeyAscii As Integer)

        If pKeyAscii = 46 Then
            pKeyAscii = 44
        Else 'NOT PKEYASCII...
            If pKeyAscii < 48 Or pKeyAscii > 57 Then
                If pKeyAscii <> 8 Then
                    If pKeyAscii <> 44 Then
                        If pKeyAscii <> 46 Then
                            pKeyAscii = 7
                        End If
                    End If
                End If
            End If
        End If

    End Sub

    Public Function strSqlValido(ByVal pKeyAscii As Integer) As Integer

        Dim CharNoValidos As String
        CharNoValidos = "ºª!|@#·$%&¬()=?¿Ç'¡ç}`+^*[]´¨{_:.;," & Chr(34)
        If InStr(1, CharNoValidos, Chr(pKeyAscii)) > 0 Then
            strSqlValido = 0
        Else
            strSqlValido = pKeyAscii
        End If

    End Function

    '- MANEJO DE IMPRESION --
    'Public Sub TextoEn(ByVal X As Single, _
    '                   ByVal Y As Single, _
    '                   ByVal Texto As String, _
    '                   Optional ByVal Centrado As Boolean = False)


    '    If Centrado Then
    '        Printer.CurrentX = (Printer.ScaleLeft + (Printer.ScaleWidth - Printer.TextWidth(Trim$(Texto))) / 2)
    '        'Printer.CurrentY = Printer.ScaleTop + (Printer.ScaleHeight - hgt) / 2
    '        Printer.CurrentY = Y
    '    Else 'CENTRADO = FALSE/0
    '        Printer.CurrentX = X
    '        Printer.CurrentY = Y
    '    End If
    '    Printer.Print(Texto)

    'End Sub

    Public Function TimeToMinutes(ByVal strTime As String, _
                                  Optional ByVal Separator As String = ":") As Long

        TimeToMinutes = Val(Left$(strTime, InStr(1, strTime, Separator) - 1)) * 60 + Val(Mid$(strTime, InStr(1, strTime, Separator) + 1))

    End Function

    'Public Sub CargarCombo2(ByVal Combo As ComboBox, ByVal Campo1 As String, ByVal Campo2 As String, ByVal rs As ADODB.Recordset, Optional ByVal Concatena As Boolean = False)

    '    On Error Resume Next

    '    rs.MoveFirst()
    '    Do While Not rs.EOF
    '        If Not IsNull(rs.Fields(Campo1).value) Then
    '            If Concatena Then
    '                Combo.ADDItem(rs.Fields(Campo2).value & " - " & rs.Fields(Campo1).value)
    '            Else
    '                Combo.ADDItem(rs.Fields(Campo1).value)
    '            End If

    '            If LCase(Campo2) = "newindex" Then
    '                Combo.ItemData(Combo.NewIndex) = Combo.NewIndex
    '            Else
    '                If IsNumeric(rs.Fields(Campo2).value) Then
    '                    Combo.ItemData(Combo.NewIndex) = rs.Fields(Campo2).value
    '                End If
    '            End If

    '        End If
    '        rs.MoveNext()
    '    Loop

    'End Sub

    'Public Sub CargarLV(ByRef l As ListView, ByRef pItem As ListViewItem, ByRef REC As ADODB.Recordset)

    '    Dim iIndice

    '    l.ColumnHeaders.Clear()
    '    For iIndice = 0 To REC.Fields.Count - 1
    '        If iIndice > 0 And (REC.Fields(iIndice).Type = adSingle) Or (REC.Fields(iIndice).Type = adDBTimeStamp) Then
    '            l.ColumnHeaders.Add, , REC.Fields(iIndice).Name, , lvwColumnRight
    '        Else
    '            l.ColumnHeaders.Add, , REC.Fields(iIndice).Name, , lvwColumnLeft
    '        End If
    '    Next iIndice

    '    Do While Not REC.EOF

    '        If Not IsNull(REC.Fields(0).value) Then
    '            pItem = l.ListItems.Add(, , REC.Fields(0).value)
    '        Else
    '            pItem = l.ListItems.Add(, , " ")
    '        End If

    '        For iIndice = 1 To REC.Fields.Count - 1
    '            If Not IsNull(REC.Fields(iIndice).value) Then
    '                If REC.Fields(iIndice).Type = adSingle Then
    '                    pItem.SubItems(iIndice) = Format(REC.Fields(iIndice).value, "fixed")
    '                ElseIf REC.Fields(iIndice).Type = adDBTimeStamp Then
    '                    pItem.SubItems(iIndice) = Format(REC.Fields(iIndice).value, "yyyy-MM-dd")
    '                Else
    '                    pItem.SubItems(iIndice) = IIf(IsNull(REC.Fields(iIndice).value), " ", REC.Fields(iIndice).value)
    '                End If
    '            End If
    '        Next iIndice

    '        REC.MoveNext()
    '    Loop

    '    pItem = l.ListItems(1)
    '    l.ListItems(1).Selected = True

    '    LeerLVColumnas2(l, l.Tag)

    'End Sub

    'Public Sub CargarLVsinHeader(ByRef l As ListView, ByRef pItem As ListViewItem, ByRef REC As ADODB.Recordset)

    '    Dim iIndice As Integer

    '    With REC
    '        Do While Not .EOF
    '            If Not IsNull(.Fields(0).value) Then
    '                pItem = l.ListItems.Add(, , .Fields(0).value)
    '            Else
    '                pItem = l.ListItems.Add(, , " ")
    '            End If

    '            For iIndice = 1 To .Fields.Count - 1
    '                If Not IsNull(.Fields(iIndice).value) Then
    '                    pItem.SubItems(iIndice) = .Fields(iIndice).value
    '                Else
    '                    pItem.SubItems(iIndice) = " "
    '                End If
    '            Next iIndice

    '            .MoveNext()

    '        Loop
    '    End With 'REC

    '    pItem = l.ListItems(1)

    '    l.ListItems(1).Selected = True

    'End Sub

    'Public Sub aMayuscula(ByRef pKeyAscii As Integer)
    '    pKeyAscii = Asc(UCase$(Chr$(pKeyAscii)))
    'End Sub

    Function pgp(ByVal s As String, ByVal op As Byte) As String

        Dim Key As Long
        Dim salt As Boolean

        Key = 21536679 'Or any other postive integer
        salt = False
        Dim result As String = ""
        If Len(Trim(s)) = 0 Then
            result = ""
        Else
            If op = 1 Then
                result = StrEncode(s, Key, salt)
            Else
                result = StrDecode(s, Key, salt)
            End If
        End If
        Return result
    End Function

    Function StrEncode(ByVal s As String, ByVal Key As Long, ByVal salt As Boolean) As String

        'Written by Gary Ardell.
        'free from all copyright restrictions

        'Modified by Ben Baird in the following way(s):
        '- Converted all "Next i" statements
        '  to merely "Next" to speed up the code
        '  (Yes, that really does make a difference!)
        '

        Dim n As Long, I As Long, ss As String
        Dim k1 As Long, k2 As Long, k3 As Long, k4 As Long, t As Long
        Dim sn() As Long
        Static saltvalue As String '* 4

        If salt Then
            For I = 1 To 4
                t = 100 * (1 + Asc(Mid(saltvalue, I, 1))) * Rnd() * ((CDbl((DateTime.Now.Ticks Mod &HC92A69C000)) / 10000000) + 1)
                Mid(saltvalue, I, 1) = Chr(t Mod 256)
            Next
            s = Mid(saltvalue, 1, 2) & s & Mid(saltvalue, 3, 2)
        End If

        n = Len(s)
        ss = Space(n)

        ReDim sn(n)

        k1 = 11 + (Key Mod 233) : k2 = 7 + (Key Mod 239)
        k3 = 5 + (Key Mod 241) : k4 = 3 + (Key Mod 251)

        For I = 1 To n : sn(I) = Asc(Mid(s, I, 1)) : Next I

        For I = 2 To n : sn(I) = sn(I) Xor sn(I - 1) Xor ((k1 * sn(I - 1)) Mod 256) : Next
        For I = n - 1 To 1 Step -1 : sn(I) = sn(I) Xor sn(I + 1) Xor (k2 * sn(I + 1)) Mod 256 : Next
        For I = 3 To n : sn(I) = sn(I) Xor sn(I - 2) Xor (k3 * sn(I - 1)) Mod 256 : Next
        For I = n - 2 To 1 Step -1 : sn(I) = sn(I) Xor sn(I + 2) Xor (k4 * sn(I + 1)) Mod 256 : Next

        For I = 1 To n : Mid(ss, I, 1) = Chr(sn(I)) : Next I

        StrEncode = ss
        saltvalue = Mid(ss, Len(ss) / 2, 4)

    End Function

    Function StrDecode(ByVal s As String, ByVal Key As Long, ByVal salt As Boolean) As String

        'Written by Gary Ardell.
        'free from all copyright restrictions

        'Modified by Ben Baird in the following way(s):
        '- Converted all "Next i" statements
        '  to merely "Next" to speed up the code
        '  (Yes, that really does make a difference!)
        '

        Dim n As Long, I As Long, ss As String
        Dim k1 As Long, k2 As Long, k3 As Long, k4 As Long
        Dim sn() As Long
        n = Len(s)
        ss = Space(n)
        ReDim sn(n)

        k1 = 11 + (Key Mod 233) : k2 = 7 + (Key Mod 239)
        k3 = 5 + (Key Mod 241) : k4 = 3 + (Key Mod 251)

        For I = 1 To n : sn(I) = Asc(Mid(s, I, 1)) : Next

        For I = 1 To n - 2 : sn(I) = sn(I) Xor sn(I + 2) Xor (k4 * sn(I + 1)) Mod 256 : Next
        For I = n To 3 Step -1 : sn(I) = sn(I) Xor sn(I - 2) Xor (k3 * sn(I - 1)) Mod 256 : Next
        For I = 1 To n - 1 : sn(I) = sn(I) Xor sn(I + 1) Xor (k2 * sn(I + 1)) Mod 256 : Next
        For I = n To 2 Step -1 : sn(I) = sn(I) Xor sn(I - 1) Xor (k1 * sn(I - 1)) Mod 256 : Next

        For I = 1 To n : Mid(ss, I, 1) = Chr(sn(I)) : Next I

        If salt Then StrDecode = Mid(ss, 3, Len(ss) - 4) Else StrDecode = ss

    End Function

    Function padLR(ByVal pExpresion As String, ByVal pLongitud As Byte, ByVal pCharPad As String, ByVal pTipo As Byte) As String

        If pLongitud - Len(Trim(pExpresion)) >= 0 Then
            If pTipo = 1 Then ' Left
                Return pExpresion.Trim().PadLeft(pLongitud, pCharPad) 'padLR = String(pLongitud - Len(Trim(pExpresion)), pCharPad) & Trim(pExpresion)
            ElseIf pTipo = 2 Then ' Right
                Return pExpresion.Trim.PadRight(pLongitud, pCharPad) '            padLR = Trim(pExpresion) & String(pLongitud - Len(Trim(pExpresion)), pCharPad)
            ElseIf pTipo = 3 Then ' Center
                Return pExpresion.Trim.PadLeft(pLongitud / 2, pCharPad).PadRight(pLongitud / 2, pCharPad) '            padLR = String((pLongitud - Len(Trim(pExpresion)) / 2), pCharPad) & Trim(pExpresion) & String((pLongitud - Len(Trim(pExpresion)) / 2), pCharPad)
            End If
        End If
        Return "" 'Diego  que valor habria que colocar aca??
    End Function

    'diego     Public Sub GrabarLVColumnas2(ByRef lv As ListView, ByVal Nombre As String)
    'diego     Dim a As Long
    'diego     Dim I As Long
    'diego         For a = 1 To lv.ColumnHeaders.Count
    'diego             WriteINI(lv.Name & "-" & Nombre, lv.ColumnHeaders(a).Text, lv.ColumnHeaders(a).Width)
    'diego         Next a
    'diego     End Sub

    'diego     Public Sub LeerLVColumnas2(ByRef lv As ListView, ByVal Nombre As String)

    'diego         On Error Resume Next

    'diego     Dim a As Long
    'diego     Dim I As Long
    'diego         For a = 1 To lv.ColumnHeaders.Count
    'diego      I = Funciones.modINIs.ReadINI(lv.Name & "-" & Nombre, lv.ColumnHeaders(a).Text, "0")
    'diego             If I > 0 Then lv.ColumnHeaders(a).Width = I
    'diego  Next a

    'diego     End Sub

    Function vFecha(ByVal Fecha As String, Optional ByVal noNull As Boolean = False) As Boolean

        vFecha = False
        If InStr(1, Fecha, "_") = 0 Then
            If IsDate(Fecha) Then
                If CDate(Fecha) <= Date.Today Then
                    vFecha = True
                End If
            End If
        Else
            If Not noNull Then
                If Fecha = "__/__/____" Then
                    vFecha = True
                End If
            End If
        End If

    End Function

    'Public Sub GeneraErrLog(ByVal sRep As String, ByVal m_sMODULENAME_ As String, ByVal PROCNAME_ As String)

    '    Dim fn As String
    '    Dim Separator As String
    '    Dim xErrCompleto As String

    '    If Err.Number <> 0 And Len(Trim(sRep)) = 0 Then
    '        xErrCompleto = "Error Numero    : " & Err.Number & vbNewLine & _
    '            "Build Number    : " & Application.ProductVersion & vbNewLine & _
    '            "Module Nombre   : " & m_sMODULENAME_ & vbNewLine & _
    '            "Procedure Name  : " & PROCNAME_ & vbNewLine & _
    '            "Numero de Línea : " & Err.Source & " (" & Erl() & ")" & vbNewLine & _
    '            "Día y Hora      : " & Now & vbNewLine & _
    '            "Descripción     : " & Err.Description
    '    Else
    '        xErrCompleto = "Module Nombre   : " & m_sMODULENAME_ & vbNewLine & _
    '            "Procedure Name  : " & PROCNAME_ & vbNewLine & _
    '            "Día y Hora      : " & Now & vbNewLine & _
    '            "Descripción     : " & sRep

    '    End If

    '    Separator = New String("-", 78)
    '    fn = vg.Path & "\ErrorGuardianLog.txt"
    '    Dim f As New System.IO.StreamWriter(fn)

    '    f.WriteLine(xErrCompleto.ToCharArray)
    '    f.WriteLine("")

    '    f.WriteLine(Separator)
    '    f.Close()
    'End Sub

    Public Function PcName() As String

        Dim sBuffer As String
        Dim lSize As Long

        sBuffer = Space$(255)
        lSize = Len(sBuffer)
        Call GetComputerName(sBuffer, lSize)

        If lSize > 1 Then
            PcName = Left$(sBuffer, lSize)
            If Asc(Right$(PcName, 1)) = 0 Then
                PcName = Left$(PcName, Len(PcName) - 1)
            End If
        Else
            PcName = "Desconocido"
        End If

    End Function

    Function SinComa(ByVal Texto As String) As String

        Dim wPos As Integer

        wPos = InStr(1, Texto, ",", vbTextCompare)

        If wPos <> 0 Then Mid(Texto, wPos, 1) = "."

        SinComa = Texto

    End Function

End Module
