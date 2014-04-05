﻿Private Sub cmdCascara_Click()
    
    Dim wDias As Integer
    Dim wTipoCascara As String
    Dim xI As Byte
        
    txtDeduConcepto.Text = ""
    
    Set m.adoREC = xGetRSMDB(vg.Conexion, "tblClientes", "ID=" & CStr(cboCliente.ItemData(cboCliente.ListIndex)), "NONE")
    If Not m.adoREC.EOF Then
        wTipoCascara = UCase(Trim(IIf(IsNull(m.adoREC!Cascara), "XX", m.adoREC!Cascara)))
    Else
        wTipoCascara = "XX"
    End If
        
    m.I = -1001
    
    cmdReciboVer_Click

    wDias = CalculaDiasCascara()
    Set m.adoREC = adoComando(vg.Conexion, "SELECT * FROM ansCascara WHERE Codigo='" & wTipoCascara & "' and Desde<=" & wDias & " and Hasta>=" & wDias)
    
    If Not (m.adoREC.BOF And m.adoREC.EOF) Then
       ' MsgBox m.adoREC!ID & " " & m.adoREC!Codigo & " " & m.adoREC!d1 & " " & m.adoREC!d2 & " " & m.adoREC!d3 & " " & m.adoREC!d4 & " " & m.adoREC!d5 & " "
        For xI = 1 To 5
            If m.adoREC("d" & xI) > 0 Then
                txtDeduConcepto.Text = txtDeduConcepto.Text + Trim(CStr(m.adoREC("d" & xI))) & "+"
                'txtDeduConcepto.Text = "cascara: " & m.adoREC("codigo") & " " & m.adoREC("id")
                'txtDeduImporte.Text = Format(CSng(m.adoREC("d" & xI)), "fixed")
            End If
        Next xI
        txtDeduImporte.Text = m.adoREC("dTotal") 'Format(m.adoREC("dTotal"), "fixed")
        txtDeduConcepto.Text = "cascara: " & Left(txtDeduConcepto.Text, Len(Trim(txtDeduConcepto.Text)) - 1) & "%"
        chkDeduPorcentaje.value = 1
        chkDeduAlResto.value = 1
        'If xI > 1 Then Me.chkDeduAlResto.value = 1 Else Me.chkDeduAlResto.value = 0
        cmdAgregarDedu_Click
    Else
        MsgBox "Los plazos NO son válidos para aplicar cascara", vbCritical + vbOKOnly, "ATENCIÓN!"
    End If
    Set m.adoREC = Nothing
    
    m.I = 0
    
End Sub

Private Function CalculaDiasCascara() As Integer

    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "CalculaDiasCascara"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------

    Dim X As Byte
    Dim wFecha1 As Date
    Dim wFechaFactPromedio As Date
    Dim wFechaValoresPromedio As Date
    Dim wDias As Integer
    Dim wSumaImp As Single
    Dim wSumaImpDias As Single
    Dim wEntro1 As Boolean
    Dim wEntro2 As Boolean
    
    wEntro1 = False
    wEntro2 = False
    wFechaFactPromedio = Date
    wFechaValoresPromedio = Date
    wDias = 32000
    
    Set m.adoREC = adoComando(vg.Conexion, "SELECT * FROM v_Cascara_Facturas")
    If Not (m.adoREC.BOF And m.adoREC.EOF) Then
              
        X = 1
        While Not m.adoREC.EOF
            If X = 1 Then
                wFecha1 = m.adoREC!Fecha
                wSumaImp = IIf(m.adoREC!Importe <> m.adoREC!Saldo, m.adoREC!Saldo, m.adoREC!Importe - m.adoREC!ImpOferta - m.adoREC!ImpPercep)
                wSumaImpDias = IIf(m.adoREC!Importe <> m.adoREC!Saldo, m.adoREC!Saldo, m.adoREC!Importe - m.adoREC!ImpOferta - m.adoREC!ImpPercep)
            Else
                wSumaImp = wSumaImp + IIf(m.adoREC!Importe <> m.adoREC!Saldo, m.adoREC!Saldo, m.adoREC!Importe - m.adoREC!ImpOferta - m.adoREC!ImpPercep)
                wSumaImpDias = wSumaImpDias + _
                                (IIf(m.adoREC!Importe <> m.adoREC!Saldo, m.adoREC!Saldo, m.adoREC!Importe - m.adoREC!ImpOferta - m.adoREC!ImpPercep) * _
                                DateDiff("d", wFecha1, m.adoREC!Fecha))
            End If
            wEntro1 = True
            X = X + 1
            m.adoREC.MoveNext
        Wend
        wDias = Round((wSumaImpDias / wSumaImp), 0) - 1
        wFechaFactPromedio = wFecha1 + wDias
        
    End If
    Set m.adoREC = Nothing

    Set m.adoREC = adoComando(vg.Conexion, "SELECT * FROM v_Cascara_Valores")
    If Not (m.adoREC.BOF And m.adoREC.EOF) Then
              
        X = 1
        While Not m.adoREC.EOF
            If X = 1 Then
                wFecha1 = m.adoREC!Fecha
                wSumaImp = m.adoREC!ImpValor
                wSumaImpDias = m.adoREC!ImpValor
            Else
                wSumaImp = wSumaImp + m.adoREC!ImpValor
                wSumaImpDias = wSumaImpDias + (m.adoREC!ImpValor * DateDiff("d", wFecha1, m.adoREC!Fecha))
            End If
            wEntro2 = True
            X = X + 1
            m.adoREC.MoveNext
        Wend
        wDias = Round((wSumaImpDias / wSumaImp), 0) - 1
        wFechaValoresPromedio = wFecha1 + wDias
        
    End If
    Set m.adoREC = Nothing

    If wEntro1 And wEntro2 Then
        wDias = DateDiff("d", wFechaFactPromedio, wFechaValoresPromedio)
    End If
    
    CalculaDiasCascara = wDias
    
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


Private Sub dtF_EmiCheque_LostFocus()
    
    If cboTipoValor.ListIndex >= 5 Then
        dtF_CobroCheque.value = dtF_EmiCheque.value
    End If
    
End Sub

Private Sub lvCliNovedad_KeyDown(KeyCode As Integer, Shift As Integer)
  
  On Error Resume Next
  
  If Not (lvCliNovedad.SelectedItem Is Nothing) Then
    Select Case KeyCode
      Case 46 ' DEL
        lvCliNovedad.ListItems.Remove lvCliNovedad.SelectedItem.Index
        KeyCode = 0
    End Select
  End If

End Sub

Private Sub cmdAgregarNovedad_Click()
  
'-------- ErrorGuardian Begin --------
Const PROCNAME_ As String = "cmdAgregarNovedad_Click"
On Error GoTo ErrorGuardianLocalHandler
'-------- ErrorGuardian End ----------
    
    Dim wIdNovedad As Long
    
    wIdNovedad = 0
    
    If DatosValidos("novedad") Then
 
        Set m.ItemX = lvCliNovedad.ListItems.Add(, , CliF_Novedad.value)
                                        
        miModulo.ClientesNovedades_add m.IdCliente, CDate(CliF_Novedad.value), CStr(Trim(CliNovedad.Text)), wIdNovedad
       
        m.ItemX.SubItems(1) = CliNovedad.Text
        m.ItemX.SubItems(2) = wIdNovedad
                                
        CliF_Novedad.value = Date
        CliNovedad.Text = ""
        
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

Private Sub lvCliNovedad_DblClick()
    
  On Error Resume Next
  
    If Not lvCliNovedad.SelectedItem Is Nothing Then
              
        CliF_Novedad.value = lvCliNovedad.ListItems(lvCliNovedad.SelectedItem.Index).Text
        CliNovedad.Text = lvCliNovedad.ListItems(lvCliNovedad.SelectedItem.Index).SubItems(1)
                        
        adoComandoIU vg.Conexion, "DELETE FROM tblClientesNovedades WHERE ID=" & CStr(lvCliNovedad.ListItems(lvCliNovedad.SelectedItem.Index).SubItems(2))
                
        lvCliNovedad.ListItems.Remove lvCliNovedad.SelectedItem.Index
                                
        Set EtiquetasClientes.SelectedItem = EtiquetasClientes.Tabs(2)
        
    End If
    
End Sub

Private Sub CliNovedad_Validate(Cancel As Boolean)
   
   Cancel = Not DatosValidos("CliNovedad")
    
End Sub

Private Sub Form_KeyPress(KeyAscii As Integer)
  
  On Error Resume Next
  
  Dim wPorcentajeLinea As String
  
  Select Case KeyAscii
    Case 20   'CTRL + T
         If cmdVenta.Tag = "CANCELAR" Then
            If Me.txtTotal.Visible = True Then
               txtTotal.Visible = False
               lbtTotal.Visible = False
            Else
               txtTotal.Visible = True
               lbtTotal.Visible = True
            End If
         End If
    Case 16   'CTRL + P
         
         If vg.miSABOR >= A2_Clientes Then
            
            If Not lvBuscar.SelectedItem Is Nothing Then
denuevo3:
                wPorcentajeLinea = InputBox("Porcentaje:" & vbCrLf & "    (Presione Cancelar para Porcentaje=0)", Trim(lvBuscar.SelectedItem.ListSubItems(CCol.cLinea).Text) & " (" & lvBuscar.SelectedItem.ListSubItems(CCol.cPorclinea).Text & "%)", lvBuscar.SelectedItem.ListSubItems(CCol.cPorclinea).Text)
                
                If Len(Trim(wPorcentajeLinea)) = 0 Then ' Apreto Cancelar
                   wPorcentajeLinea = "0"
                   adoModulo.adoComandoIU vg.Conexion, "DELETE FROM tblLineasPorcentaje WHERE ucase(trim(Linea))='" & UCase(Trim(lvBuscar.SelectedItem.ListSubItems(CCol.cLinea).Text)) & "'"
                   MsgBox "El Porcentaje se borró con Exito!", vbExclamation, "Atención"
                Else
                    
                    If Not IsNumeric(wPorcentajeLinea) Then
                        MsgBox "Ingrese sólo valores numericos", vbExclamation, "Atención"
                        GoTo denuevo3
                    End If
                    
                    wPorcentajeLinea = Replace(wPorcentajeLinea, ",", ".")
                                       
                    adoModulo.adoComandoIU vg.Conexion, "DELETE FROM tblLineasPorcentaje WHERE ucase(trim(Linea))='" & UCase(Trim(lvBuscar.SelectedItem.ListSubItems(CCol.cLinea).Text)) & "'"
                        
                    If CSng(wPorcentajeLinea) <> 0 Then
                       adoModulo.adoComandoIU vg.Conexion, "INSERT INTO tblLineasPorcentaje (Linea, Porcentaje) VALUES ('" & UCase(Trim(lvBuscar.SelectedItem.ListSubItems(CCol.cLinea).Text)) & "', " & wPorcentajeLinea & " )"
                    End If
                    
                End If
                
                If wPorcentajeLinea <> 0 Then
                    For m.I = 1 To lvBuscar.ListItems.Count
                        lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cPorclinea) = wPorcentajeLinea
                        lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cPrecio) = Format(lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cPlista) + (lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cPlista) * wPorcentajeLinea) / 100, "##,##0.00")
                        lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cLinea).Bold = True
                    Next m.I
                Else
                    For m.I = 1 To lvBuscar.ListItems.Count
                        lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cPorclinea) = wPorcentajeLinea
                        lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cPrecio) = Format(lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cPlista) + (lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cPlista) * wPorcentajeLinea) / 100, "##,##0.00")
                        lvBuscar.ListItems.Item(m.I).ListSubItems.Item(CCol.cLinea).Bold = False
                    Next m.I
                End If
                lvBuscar.Refresh
            End If
         End If
         
    Case 2   'CTRL + B
         txtBuscar.SetFocus
    Case 4   'CTRL + D
         If cmdRecibo.Tag = "CANCELAR" Then
            VerDetalleRecibo
            KeyAscii = 0
         End If
    Case 6 'CTRL + F
            If vg.miSABOR > A2_Clientes Then
                If UCase(cmdVISITA.Tag) = "INICIAR VISITA" Then
                    xFindCliente "ID", "RazonSocial", "Lista de Clientes - Buscar por ..."
                End If
            End If
    Case 26  'CTRL + Z
    
        Dim dlg As New frmConexionUpdate

        dlg.modoUpdate = EstadoActual
        dlg.Show vbModal
        DoEvents
        
        Set dlg = Nothing
        
  End Select
      
End Sub

Private Sub lvEstado_DblClick()

    On Error Resume Next
    
  If cmdRecibo.Tag = "CANCELAR" Then
      
        If lvEstado.SelectedItem.ListSubItems(11) = "N" Then ' no fue aplicada todavia
                           
            If Mid(lvEstado.SelectedItem.SubItems(1), 1, 3) = "FAC" Or _
               Mid(lvEstado.SelectedItem.SubItems(1), 1, 3) = "DEB" Then
                
                If lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbRed Then
                    'ya esta agregada
        
                    lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbDefault
                    lvEstado.SelectedItem.ListSubItems.Item(1).Bold = False
                    Set m.ItemX = lvAplicacion.FindItem(lvEstado.SelectedItem.ListSubItems.Item(1).Text, lvwText, , lvwPartial)
                    
                    If Not m.ItemX Is Nothing Then
                       lvAplicacion.ListItems(m.ItemX.Index).Selected = True
                       lvAplicacion.ListItems.Remove lvAplicacion.SelectedItem.Index
                               
                        If lvAplicacion.ListItems.Count <= 0 Then
                            lvEstado.Tag = "-1"
                        End If
                        
                    End If
                    Set m.ItemX = Nothing
                    
                    TotalApli
                  
                Else
                    If lvEstado.Tag = "-1" Then
                       lvEstado.Tag = lvEstado.SelectedItem.SubItems(12)
                    End If
                    
                    If lvEstado.SelectedItem.SubItems(12) <> lvEstado.Tag Then
                         MsgBox "Debe elegir todas las facturas de " & IIf(lvEstado.Tag = "1", "contado", "cta.cte"), vbInformation + vbOKOnly, "atención"
                         txtApliConcepto.Text = ""
                         txtApliImporte.Text = ""
                         txtPercepImporte.Text = ""
                    Else
                         cmdAgregarAplicacion_Click
                         lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbRed
                         lvEstado.SelectedItem.ListSubItems.Item(1).Bold = True
                    End If
                    
                End If
                
            ElseIf Mid(lvEstado.SelectedItem.SubItems(1), 1, 3) = "CRE" Or _
                   Mid(lvEstado.SelectedItem.SubItems(1), 1, 3) = "REC" Then
               
                If lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbRed Then
                    'ya esta agregada
                  
                    lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbDefault
                    lvEstado.SelectedItem.ListSubItems.Item(1).Bold = False
                    Set m.ItemX = lvADeducir.FindItem(lvEstado.SelectedItem.ListSubItems.Item(1).Text, lvwText, , lvwPartial)
                    If Not m.ItemX Is Nothing Then
                       lvADeducir.ListItems(m.ItemX.Index).Selected = True
                       lvADeducir.ListItems.Remove lvADeducir.SelectedItem.Index
                    End If
                    Set m.ItemX = Nothing
                    
                    TotalADeducir
                
                Else
                    cmdAgregarDedu_Click
                    lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbRed
                    lvEstado.SelectedItem.ListSubItems.Item(1).Bold = True
                End If
                
            End If
            
        Else
            MsgBox "El comprobante ya fue aplicado", vbInformation + vbOKOnly, "atención"
        End If
    
  End If

End Sub

Private Sub lvEstado_ItemClick(ByVal Item As MSComctlLib.ListItem)
    
    On Error Resume Next
    
  If cmdRecibo.Tag = "CANCELAR" Then
  
      If Mid(Item.SubItems(1), 1, 3) = "FAC" Then
         
          txtApliConcepto.Text = Item.SubItems(1)
          txtApliImporte.Text = Item.SubItems(3)
          txtPercepImporte.Text = Item.SubItems(9)
          
      ElseIf Mid(Item.SubItems(1), 1, 3) = "DEB" Then
          
          txtApliConcepto.Text = Item.SubItems(1)
          txtApliImporte.Text = Format(0, "fixed")
          txtPercepImporte.Text = CDbl(Item.SubItems(3))
          
      ElseIf Mid(Item.SubItems(1), 1, 3) = "CRE" Or _
             Mid(Item.SubItems(1), 1, 3) = "REC" Then
          
          txtDeduConcepto.Text = Item.SubItems(1)
          txtDeduImporte.Text = Abs(CDbl(Item.SubItems(3)))
          
      End If
  
  End If
  
End Sub

Private Sub lvEstado_KeyDown(KeyCode As Integer, Shift As Integer)
    
    If vg.miSABOR > A2_Clientes Then
        If (KeyCode = vbKeyI) And (Shift = vbCtrlMask) Then
            If ReadINI("DATOS", "ICC", "0") = "1" Then
                'Imprimir la cta. cte.
                CtaCte_Imprimir m.IdCliente
            End If
        End If
    End If
    
End Sub
                                                                                                             
Private Sub lvValores_KeyDown(KeyCode As Integer, Shift As Integer)
  
      On Error Resume Next
      
  If Not (lvValores.SelectedItem Is Nothing) Then
    Select Case KeyCode
      Case 46 ' DEL
        lvValores.ListItems.Remove lvValores.SelectedItem.Index
        TotalRecibo
        KeyCode = 0
    End Select
  End If
  
End Sub

Private Sub lvADeducir_KeyDown(KeyCode As Integer, Shift As Integer)
  
      On Error Resume Next
      
  If Not (lvADeducir.SelectedItem Is Nothing) Then
    Select Case KeyCode
      Case 46 ' DEL
        
        Set m.ItemX = lvEstado.FindItem(lvADeducir.SelectedItem.Text, lvwSubItem, , lvwPartial)
        If Not m.ItemX Is Nothing Then
           lvEstado.ListItems(m.ItemX.Index).Selected = True
           lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbDefault
           lvEstado.SelectedItem.ListSubItems.Item(1).Bold = False
        End If
        Set m.ItemX = Nothing
        
        lvADeducir.ListItems.Remove lvADeducir.SelectedItem.Index
        TotalADeducir
        KeyCode = 0
    End Select
  End If
  
End Sub

Private Sub lvAplicacion_KeyDown(KeyCode As Integer, Shift As Integer)
  
      On Error Resume Next
      
  If Not (lvAplicacion.SelectedItem Is Nothing) Then
    Select Case KeyCode
      Case 46 ' DEL
        
        Set m.ItemX = lvEstado.FindItem(lvAplicacion.SelectedItem.Text, lvwSubItem, , lvwPartial)
        If Not m.ItemX Is Nothing Then
           lvEstado.ListItems(m.ItemX.Index).Selected = True
           lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbDefault
           lvEstado.SelectedItem.ListSubItems.Item(1).Bold = False
        End If
        Set m.ItemX = Nothing
        
        lvAplicacion.ListItems.Remove lvAplicacion.SelectedItem.Index
        TotalApli
        KeyCode = 0
    End Select
  End If

End Sub

Private Sub cboTipoValor_Click()
    
    On Error Resume Next
    
  ' Para el caso del cheque
  If cboTipoValor.ItemData(cboTipoValor.ListIndex) = 1 Then 'Cheque
    lblReciboNroCheque.Caption = "Nº Cheque"
    lblReciboFechaEmision.Caption = "F. Emisión"
      dtF_EmiCheque.Enabled = True
    dtF_CobroCheque.Enabled = True
    txtN_Cheque.Enabled = True
    chkDeTercero.Enabled = True
    txtNroDeCuenta.Enabled = True
    chkRecBahia.Enabled = True
    cboBanco.Enabled = True
    txtCPA.Enabled = True
    txtCambio.Enabled = False
  ElseIf cboTipoValor.ItemData(cboTipoValor.ListIndex) = 2 Then 'Efectivo
    dtF_EmiCheque.Enabled = False
    dtF_CobroCheque.Enabled = False
    txtN_Cheque.Enabled = False
    chkDeTercero.Enabled = False
    txtNroDeCuenta.Enabled = False
    chkRecBahia.Enabled = False
    cboBanco.Enabled = False
    txtCPA.Enabled = False
    txtCambio.Enabled = False
  ElseIf cboTipoValor.ItemData(cboTipoValor.ListIndex) = 3 Or cboTipoValor.ItemData(cboTipoValor.ListIndex) = 4 Then 'Divisas
    dtF_EmiCheque.Enabled = False
    dtF_CobroCheque.Enabled = False
    txtN_Cheque.Enabled = False
    chkDeTercero.Enabled = False
    txtNroDeCuenta.Enabled = False
    chkRecBahia.Enabled = False
    cboBanco.Enabled = False
    txtCPA.Enabled = False
    txtCambio.Enabled = True
  ElseIf cboTipoValor.ItemData(cboTipoValor.ListIndex) >= 5 Then 'Retenciones
    lblReciboNroCheque.Caption = "Nº Ret."
    lblReciboFechaEmision.Caption = "F. Ret."
    dtF_EmiCheque.Enabled = True
    dtF_CobroCheque.Enabled = False
    txtN_Cheque.Enabled = True
    chkDeTercero.Enabled = False
    txtNroDeCuenta.Enabled = False
    chkRecBahia.Enabled = False
    cboBanco.Enabled = False
    txtCPA.Enabled = False
    txtCambio.Enabled = False
  End If
  
    txtCambio.Text = ""
    lbldivisas.Caption = ""
  
  If InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "dólar") > 0 Then
    txtCambio.Text = vg.Dolar
  ElseIf InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "euro") > 0 Then
    txtCambio.Text = vg.Euro
  End If
  
End Sub

Private Sub cmdADeducir_Click()
    Set EtiquetasRecibos.SelectedItem = EtiquetasRecibos.Tabs("ADeducir")
End Sub

Private Sub cmdAgregarAplicacion_Click()
  
'-------- ErrorGuardian Begin --------
Const PROCNAME_ As String = "cmdAgregarAplicacion_Click"
On Error GoTo ErrorGuardianLocalHandler
'-------- ErrorGuardian End ----------
    
    If DatosValidos("aplicacion") Then
        
        If Not lvAplicacion.SelectedItem Is Nothing Then
            Select Case lvAplicacion.SelectedItem.Tag
            Case "add"
                Set m.ItemX = lvAplicacion.ListItems.Add(, , txtApliConcepto.Text)
            Case "upd"
                Set m.ItemX = lvAplicacion.SelectedItem
            Case Else
                'MsgBox "view", vbInformation + vbOKOnly, "cmdAgregarAplicacion_Click"
                Exit Sub
            End Select
        Else 'NOT NOT...
            Set m.ItemX = lvAplicacion.ListItems.Add(, , txtApliConcepto.Text)
        End If
        
        m.ItemX.SubItems(1) = Format(txtApliImporte.Text, "fixed")
        m.ItemX.SubItems(2) = IIf(Len(Trim(txtPercepImporte.Text)) = 0, Format("0", "fixed"), Format(txtPercepImporte.Text, "fixed"))
        m.ItemX.Tag = "add"
        
        lvAplicacion.ListItems(m.ItemX.Index).Selected = True
        
        Set m.ItemX = lvEstado.FindItem(lvAplicacion.SelectedItem.Text, lvwSubItem, , lvwPartial)
        If Not m.ItemX Is Nothing Then
           lvEstado.ListItems(m.ItemX.Index).Selected = True
           lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbRed
           lvEstado.SelectedItem.ListSubItems.Item(1).Bold = True
        End If
        Set m.ItemX = Nothing
                
        TotalApli
        
        txtApliConcepto.Text = ""
        txtApliImporte.Text = ""
        txtPercepImporte.Text = ""
            
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

Private Sub cmdAgregarDedu_Click()
    
'-------- ErrorGuardian Begin --------
Const PROCNAME_ As String = "cmdAgregarDedu_Click"
On Error GoTo ErrorGuardianLocalHandler
'-------- ErrorGuardian End ----------
    
    If DatosValidos("adeducir") Then
        
        If Not lvADeducir.SelectedItem Is Nothing Then
            Select Case lvADeducir.SelectedItem.Tag
            Case "add"
                Set m.ItemX = lvADeducir.ListItems.Add(, , txtDeduConcepto.Text)
            Case "upd"
                Set m.ItemX = lvADeducir.SelectedItem
            Case Else
                'MsgBox "view", vbInformation + vbOKOnly, "cmdAgregarDedu_Click"
                Exit Sub
            End Select
        Else 'NOT NOT...
            Set m.ItemX = lvADeducir.ListItems.Add(, , txtDeduConcepto.Text)
        End If
        
        With m
            .ItemX.SubItems(1) = txtDeduImporte.Text
            .ItemX.SubItems(2) = chkDeduPorcentaje.value
            .ItemX.SubItems(3) = chkDeduAlResto.value
            .ItemX.Tag = "add"
        End With 'm
        lvADeducir.ListItems(m.ItemX.Index).Selected = True
        
        Set m.ItemX = lvEstado.FindItem(lvADeducir.SelectedItem.Text, lvwSubItem, , lvwPartial)
        If Not m.ItemX Is Nothing Then
           lvEstado.ListItems(m.ItemX.Index).Selected = True
           lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbRed
           lvEstado.SelectedItem.ListSubItems.Item(1).Bold = True
        End If
        Set m.ItemX = Nothing
        
        TotalADeducir
        
        txtDeduConcepto.Text = ""
        txtDeduImporte.Text = ""
        chkDeduPorcentaje.value = 0
        chkDeduAlResto.value = 0
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

Private Sub cmdAplicacion_Click()
      Set EtiquetasRecibos.SelectedItem = EtiquetasRecibos.Tabs("Aplicacion")
End Sub

Private Sub cmdEnviar_Click()

  '-------- ErrorGuardian Begin --------
  Const PROCNAME_ As String = "cmdEnviar_Click"
  On Error GoTo ErrorGuardianLocalHandler
  '-------- ErrorGuardian End ----------

  
  ' Aca se realiza el envio de todos los pedidos y recibos
  ' voy recorriendo cada movimiento y si esta chequeado realizo el envio
  
  ' Verifico si existen elementos MARCADOS para transmitir
  Dim RealizarTransferencia As Boolean
  Dim li As ListItem
  
  RealizarTransferencia = False
  For Each li In lvMovimientos.ListItems
    If li.Checked Then
      RealizarTransferencia = True
      Exit For
    End If
  Next
  
  If RealizarTransferencia Then
    If ProbarConexion Then
        Dim dlg As frmConexionEnvio
        Set dlg = New frmConexionEnvio
        dlg.ModoTransmision = TRANSMITIR_LISTVIEW
        Set dlg.lvMovimientos = lvMovimientos
        dlg.Show vbModal, Me
        Set dlg = Nothing
        cboEnvios_Click
    Else
       vg.auditor.Guardar Comunicaciones, FALLO, "Probar Conexion, TRANSMITIR_LISTVIEW " & cboCliente.List(cboCliente.ListIndex)
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

Private Sub Form_QueryUnload(Cancel As Integer, _
                             UnloadMode As Integer)
      
    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "Form_QueryUnload"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------
    
    If ReadINI("DATOS", "ConfirmaSalida", "1") = "1" Then
        If MsgBox("Saliendo del Catálogo... ¿Está seguro?", vbYesNo Or vbQuestion Or vbSystemModal Or vbDefaultButton2, "Cerrando la Aplicación") = vbNo Then
            Cancel = 1
            Exit Sub
        End If
    End If
                    
  #If Sabor = 3 Or Sabor = 4 Then
    
      If cmdVISITA.Tag <> "INICIAR Visita" Then
        Call MsgBox("Antes de salir debe cerrar VISITA", vbExclamation, "Atención")
        Cancel = True
        Exit Sub
      End If
    
      If LosMovimientos.PreguntoAlSalir Then
          ' Si hay movimientos pendientes pregunto si quiere enviarlos
                   
        Select Case MsgBox("Tiene movimientos que aun no ha enviado. ¿QUIERE ENVIARLOS AHORA?", vbYesNo Or vbQuestion Or vbSystemModal Or vbDefaultButton1, "ENVIO DE MOVIMIENTOS")
            Case vbYes
              If ProbarConexion Then
                  Dim dlg As frmConexionEnvio
                  Set dlg = New frmConexionEnvio
                  dlg.ModoTransmision = TRANSMITIR_RECORDSET
                  dlg.IdCliente = 0
                  dlg.Show vbModal, Me
                  Set dlg = Nothing
              Else
                  vg.auditor.Guardar Comunicaciones, FALLO, "Probar Conexion, TRANSMITIR_RECORDSET" & cboCliente.List(cboCliente.ListIndex)
              End If
            Case vbNo
                If vg.miSABOR > 2 And ReadINI("DATOS", "EEA", "1") = 1 Then
                    If Len(Trim(Dir(vg.Path & "\monitorE.exe"))) > 0 Then
                        Shell vg.Path & "\monitorE.exe", vbHide
                    End If
                End If
        End Select
         
      End If ' Not (m.adoREC = EOF) And Not (m.adoREC = BOF)
      
  #End If
  
  With Me
    If .WindowState <> vbMinimized Then
      GuardarFormatoForm Me
      WriteINI "Preferencias", "picHorizontal", picHorizontal.Top
                    
      GrabarLVColumnas Me.lvBuscar
      GrabarLVColumnas Me.lvEstado
      GrabarLVColumnas Me.lvMovimientos
      GrabarLVColumnas Me.lvPedido
      GrabarLVColumnas Me.lvDevolucion1
      GrabarLVColumnas Me.lvDevolucion2
      GrabarLVColumnas Me.lvValores
      GrabarLVColumnas Me.lvAplicacion
      GrabarLVColumnas Me.lvADeducir
    End If
  End With 'Me
    
  Set fExistencia = Nothing
  
  Select Case UnloadMode
    Case vbFormControlMenu
        '0 El usuario eligió el comando Cerrar del menú Control del formulario.
        vg.auditor.Guardar Programa, TERMINA, "eligió el comando Cerrar"
    Case vbFormCode
        '1 Se invocó la instrucción Unload desde el código.
        vg.auditor.Guardar Programa, TERMINA, "instrucción Unload"
    Case vbAppWindows
        '2 La sesión actual del entorno operativo Microsoft Windows está finalizando.
        vg.auditor.Guardar Programa, TERMINA, "Microsoft Windows está finalizando"
    Case vbAppTaskManager
        '3 El Administrador de tareas de Microsoft Windows está cerrando la aplicación.
        vg.auditor.Guardar Programa, TERMINA, "vbAppTaskManager"
    Case vbFormMDIForm
        '4 Un formulario MDI secundario se está cerrando porque el formulario MDI también se está cerrando.
        vg.auditor.Guardar Programa, TERMINA, "vbFormMDIForm"
    Case vbFormOwner
        '5 Un formulario se está cerrando por que su formulario propietario se está cerrando
        vg.auditor.Guardar Programa, TERMINA, "vbFormOwner"
  End Select
                        
'-------- ErrorGuardian Begin --------
Exit Sub

ErrorGuardianLocalHandler:
    If Err.Number = 53 Then ' el archivo de VersionAnterior NO EXISTE
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

Private Sub cboEnvios_Click()
    
    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "cboEnvios_Click"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------
    
    If cboEnvios.ListIndex <= -1 Then Exit Sub
    
    ' Para que aparezca o no el boton ENVIAR
    If UCase(cboEnvios.Text) = "NO ENVIADOS" Then
      
      If Not vg.NoConn Then
        cmdEnviar.Visible = True
      Else
        cmdEnviar.Visible = False
      End If
      
      lvMovimientos.Checkboxes = True
      SetListViewColor lvMovimientos, Picture1, vbWhite, &HC0FFFF
    Else
      cmdEnviar.Visible = False
      lvMovimientos.Checkboxes = False
      SetListViewColor lvMovimientos, Picture1, vbWhite, &HC0FFFF
    End If
    
    ' Siempre limpio el Listado
    lvMovimientos.ListItems.Clear
   
#If Sabor = 2 Then        ' Autopartes Lucho
    ' Para el cliente elegido
    Set m.adoREC = LosMovimientos.Leer(CByte( _
                                     cboEnvios.ItemData(cboEnvios.ListIndex)), _
                                     CLng(vg.NroUsuario))
#End If
   
   
#If Sabor = 3 Or Sabor = 4 Then       ' Viajante
    ' Para el cliente elegido
    Set m.adoREC = LosMovimientos.Leer(CByte( _
                                     cboEnvios.ItemData(cboEnvios.ListIndex)), _
                                     cboCliente.ItemData(cboCliente.ListIndex))
#End If

    With m.adoREC
        If Not .EOF Then
            Do While Not .EOF

                Set m.ItemX = lvMovimientos.ListItems.Add(, , IIf(IsNull(!Nro), " ", !Nro))
                m.ItemX.SubItems(1) = IIf(IsNull(!Fecha), " ", Format(!Fecha, "yyyy-MM-dd"))
                m.ItemX.SubItems(2) = IIf(IsNull(!RazonSocial), " ", !RazonSocial)
                m.ItemX.SubItems(3) = IIf(IsNull(!NroImpresion), " ", !NroImpresion)
                m.ItemX.SubItems(4) = IIf(IsNull(!F_Transmicion), " ", !F_Transmicion)
                m.ItemX.SubItems(5) = IIf(IsNull(!Origen), " ", !Origen)
                m.ItemX.SubItems(6) = IIf(IsNull(!IdCliente), " ", !IdCliente)
                m.ItemX.SubItems(7) = IIf(IsNull(!Observaciones), " ", !Observaciones)
                
                If Len(Trim(m.ItemX.SubItems(4))) = 0 Then
                    m.ItemX.ForeColor = vbRed
                    m.ItemX.Bold = True
                    m.ItemX.ListSubItems.Item(1).ForeColor = vbRed
                    m.ItemX.ListSubItems.Item(1).Bold = True
                    m.ItemX.ListSubItems.Item(2).ForeColor = vbRed
                    m.ItemX.ListSubItems.Item(2).Bold = True
                    m.ItemX.ListSubItems.Item(3).ForeColor = vbRed
                    m.ItemX.ListSubItems.Item(3).Bold = True
                    m.ItemX.ListSubItems.Item(4).ForeColor = vbRed
                    m.ItemX.ListSubItems.Item(4).Bold = True
                    m.ItemX.ListSubItems.Item(5).ForeColor = vbRed
                    m.ItemX.ListSubItems.Item(5).Bold = True
                    m.ItemX.ListSubItems.Item(6).ForeColor = vbRed
                    m.ItemX.ListSubItems.Item(6).Bold = True
                    m.ItemX.ListSubItems.Item(7).ForeColor = vbRed
                    m.ItemX.ListSubItems.Item(7).Bold = True
                End If

                .MoveNext
            Loop
        End If
    End With 'adoREC
    Set m.adoREC = Nothing

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

Private Sub cmdReciboBorrar_Click()
      
  '-------- ErrorGuardian Begin --------
  Const PROCNAME_ As String = "cmdReciboBorrar_Click"
  On Error GoTo ErrorGuardianLocalHandler
  '-------- ErrorGuardian End ----------
      
  If Not (lvValores.SelectedItem Is Nothing) Then
      If MsgBox("¿Está seguro?", vbExclamation Or vbYesNo, "BORRAR ITEM DE RECIBO") = vbYes Then
        lvValores.ListItems.Remove lvValores.SelectedItem.Index
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

Private Sub cmdReciboVer_Click()
  
  '-------- ErrorGuardian Begin --------
  Const PROCNAME_ As String = "cmdReciboVer_Click"
  On Error GoTo ErrorGuardianLocalHandler
  '-------- ErrorGuardian End ----------
   
  ' iNABILITO nUEVO iNGRESO
  Screen.MousePointer = vbHourglass
  
  Dim verReporte As Boolean
  Dim tImporte As Single
  
  tImporte = 0
  
  If m.I = -1001 Then
    verReporte = False
  Else
    verReporte = True
  End If
  
   ' IMPRIMO si hay valores
   If lvValores.ListItems.Count > 0 Then
   
      ' GUARDO si es EXITOSO
      elrecibo.Nuevo m.IdCliente
      elrecibo.NroImpresion = 0
      
      For m.I = 1 To lvValores.ListItems.Count
       elrecibo.ADDItem CByte(lvValores.ListItems(m.I).ListSubItems(10)), _
                        CSng(lvValores.ListItems(m.I).ListSubItems(1)), _
                        CDate(lvValores.ListItems(m.I).ListSubItems(2)), _
                        CDate(lvValores.ListItems(m.I).ListSubItems(3)), _
                        CStr(lvValores.ListItems(m.I).ListSubItems(4)), _
                        CStr(lvValores.ListItems(m.I).ListSubItems(5)), _
                        CInt(lvValores.ListItems(m.I).ListSubItems(11)), _
                        CStr(lvValores.ListItems(m.I).ListSubItems(7)), _
                        Not CBool(lvValores.ListItems(m.I).ListSubItems(8)), _
                        CSng("0" & lvValores.ListItems(m.I).ListSubItems(12)) 'tipo cambio
         
         'idvalor, importe, femision, fcobro, ncheque, ncuenta, idbanco, cpa, detercero
        If lvValores.ListItems(m.I).ListSubItems(10) = 3 Or _
            lvValores.ListItems(m.I).ListSubItems(10) = 4 Then
            tImporte = tImporte + (CSng(lvValores.ListItems(m.I).ListSubItems(1)) * CSng(lvValores.ListItems(m.I).ListSubItems(12)))
        Else
            tImporte = tImporte + CSng(lvValores.ListItems(m.I).ListSubItems(1))
        End If
        
      Next m.I
      
      elrecibo.Bahia = CBool(lvValores.ListItems(1).ListSubItems(9))
      elrecibo.Observaciones = txtObservaciones.Text
      
      elrecibo.Total = tImporte
      elrecibo.Percepciones = lblTotPercep.Caption
      
      For m.I = 1 To lvAplicacion.ListItems.Count
       elrecibo.ADDItemApli CStr(lvAplicacion.ListItems(m.I).Text), _
                            CSng(lvAplicacion.ListItems(m.I).ListSubItems(1))
                            
         'concepto, importe, observaciones
      Next m.I
      
      For m.I = 1 To lvADeducir.ListItems.Count
       elrecibo.ADDItemDedu CStr(lvADeducir.ListItems(m.I).Text), _
                            CSng(lvADeducir.ListItems(m.I).ListSubItems(1)), _
                            CBool(lvADeducir.ListItems(m.I).ListSubItems(2)), _
                            CBool(lvADeducir.ListItems(m.I).ListSubItems(3))
                            
         'concepto, importe, observaciones
      Next m.I
       
      elrecibo.Guardar "VER"
      
      If verReporte Then
        Recibo_Imprimir vg.NroImprimir
      End If
      vg.NroImprimir = ""
              
   Else
   
      VerDetalleRecibo
      
   End If
   
   ' Cierro el Recibo
   Screen.MousePointer = vbDefault

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

Private Sub cmdReciboImprimir_Click()
  
  '-------- ErrorGuardian Begin --------
  Const PROCNAME_ As String = "cmdReciboImprimir_Click"
  On Error GoTo ErrorGuardianLocalHandler
  '-------- ErrorGuardian End ----------
    
  Dim Dif As Single
  Dim tImporte As Single
  Dim tOk As Boolean
  
  tOk = True
  tImporte = 0
    
  ' iNABILITO nUEVO iNGRESO
  Screen.MousePointer = vbHourglass
  
   If lvValores.ListItems.Count = 0 Or lvAplicacion.ListItems.Count = 0 Then
        Screen.MousePointer = vbDefault
        MsgBox "Faltan Valores o Aplicación", vbExclamation & vbOKOnly, "atención"
        tOk = False
   Else
       Dif = ((CSng(Replace(lblTotApli.Caption, "$", "")) - CSng(Replace(lblTotaDedu.Caption, "$", "")) - CSng(Replace(lblTotaDeduAlResto.Caption, "$", ""))) + CSng(Replace(lblTotPercep.Caption, "$", ""))) - (CSng(Replace(lblTotRecibo.Caption, "$", "")))
       
       If Dif > 0 Then
            If Dif > CSng(lvAplicacion.ListItems(lvAplicacion.ListItems.Count).ListSubItems(1)) Then
                Screen.MousePointer = vbDefault
                MsgBox "Agregar valores ó quitar última Factura en Aplicación", vbExclamation & vbOKOnly, "atención"
                tOk = False
            End If
       End If
   End If
   
  If tOk Then
  
     InhabilitarRecibo
  
   ' IMPRIMO si hay valores
       
      ' GUARDO si es EXITOSO
      elrecibo.Nuevo m.IdCliente
      elrecibo.NroImpresion = 0
      
      For m.I = 1 To lvValores.ListItems.Count
       elrecibo.ADDItem CByte(lvValores.ListItems(m.I).ListSubItems(10)), _
                        CSng(lvValores.ListItems(m.I).ListSubItems(1)), _
                        CDate(lvValores.ListItems(m.I).ListSubItems(2)), _
                        CDate(lvValores.ListItems(m.I).ListSubItems(3)), _
                        CStr(lvValores.ListItems(m.I).ListSubItems(4)), _
                        CStr(lvValores.ListItems(m.I).ListSubItems(5)), _
                        CInt(lvValores.ListItems(m.I).ListSubItems(11)), _
                        CStr(lvValores.ListItems(m.I).ListSubItems(7)), _
                        Not CBool(lvValores.ListItems(m.I).ListSubItems(8)), _
                        CSng("0" & lvValores.ListItems(m.I).ListSubItems(12))
                        
         'idvalor, importe, femision, fcobro, ncheque, ncuenta, idbanco, cpa, detercero
         If lvValores.ListItems(m.I).ListSubItems(10) = 3 Or _
            lvValores.ListItems(m.I).ListSubItems(10) = 4 Then
            tImporte = tImporte + (CSng(lvValores.ListItems(m.I).ListSubItems(1)) * CSng(lvValores.ListItems(m.I).ListSubItems(12)))
         Else
            tImporte = tImporte + CSng(lvValores.ListItems(m.I).ListSubItems(1))
         End If
         
      Next m.I
      
      elrecibo.Bahia = CBool(lvValores.ListItems(1).ListSubItems(9))
      elrecibo.Observaciones = txtObservaciones.Text
      
      elrecibo.Total = tImporte
      elrecibo.Percepciones = lblTotPercep.Caption
    
      For m.I = 1 To lvAplicacion.ListItems.Count
       elrecibo.ADDItemApli CStr(lvAplicacion.ListItems(m.I).Text), _
                            CSng(lvAplicacion.ListItems(m.I).ListSubItems(1))
                            
         'concepto, importe, observaciones
      Next m.I
      
      For m.I = 1 To lvADeducir.ListItems.Count
       elrecibo.ADDItemDedu CStr(lvADeducir.ListItems(m.I).Text), _
                            CSng(lvADeducir.ListItems(m.I).ListSubItems(1)), _
                            CBool(lvADeducir.ListItems(m.I).ListSubItems(2)), _
                             CBool(lvADeducir.ListItems(m.I).ListSubItems(3))
                            
         'concepto, importe, observaciones
      Next m.I
       
      vg.auditor.Guardar Recibo, GUARDA
      elrecibo.Guardar
      vg.auditor.Guardar Recibo, TERMINA
      Recibo_Imprimir vg.NroImprimir
      vg.NroImprimir = ""
   
      CerrarRecibo
      
   End If
   
   ' Cierro el Recibo
   Screen.MousePointer = vbDefault
  

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


Private Sub ElRecibo_GuardoOK(nroRecibo As String)
  
  txtObservaciones.Text = ""
  lvValores.ListItems.Clear
  lvAplicacion.ListItems.Clear
  lvADeducir.ListItems.Clear
  
  MsgBox "Recibo Grabado con nº " & nroRecibo, vbInformation, "Recibo Grabado"
  
End Sub


' *****************************
' COMBOS
' *****************************
Private Sub cboCliente_Click()
    
    On Error Resume Next
    
    If cboCliente.Text = "- Todos -" Then
      cmdVISITA.Visible = False
    Else
      cmdVISITA.Visible = True
    End If
    cboEnvios_Click

End Sub


Private Sub cmdNuevoValor_Click()

    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "cmdNuevoValor_Click"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------

    lvValores.Tag = "add"
    LimpiarIngresosRecibo
    Set EtiquetasRecibos.SelectedItem = EtiquetasRecibos.Tabs(2)
    cboTipoValor.SetFocus

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

Private Sub cmdRecibo_Click()
   
    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "cmdRecibo_Click"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------
       
    With cmdRecibo
        If .Tag = "INICIAR" Then
          vg.auditor.Guardar Recibo, INICIA
          ' Limpio Listados
          TotalRecibo
          TotalADeducir
          TotalApli
          txtObservaciones.Text = ""
          lvValores.ListItems.Clear
          lvAplicacion.ListItems.Clear
          lvADeducir.ListItems.Clear
          
          AbrirRecibo
          HabilitarRecibo
        Else
          If MsgBox("¿Esta Seguro que quiere CANCELAR el Recibo?", vbQuestion + vbYesNo, "ATENCIÓN") = vbYes Then
            vg.auditor.Guardar Recibo, CANCELA
            ' Limpio Listados
            txtObservaciones.Text = ""
            lvValores.ListItems.Clear
            lvAplicacion.ListItems.Clear
            lvADeducir.ListItems.Clear
            
            CerrarRecibo
            Set EtiquetasAdminis.SelectedItem = EtiquetasAdminis.Tabs(2)
            InhabilitarRecibo
          End If
        End If
    End With 'cmdRecibo
    
    lvEstado.Tag = "-1"
    
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

Private Sub CerrarRecibo()
  
    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "CerrarRecibo"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------
  
  cmdRecibo.Caption = "Ini&ciar Recibo"
  cmdRecibo.Tag = "INICIAR"
  cmdRecibo.ToolTipText = "INICIAR Recibo Nuevo"
  cboEnvios_Click

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

Private Sub AbrirRecibo()
    On Error Resume Next
    
  cmdRecibo.Caption = "CANCELAR"
  cmdRecibo.Tag = "CANCELAR"
  cmdRecibo.ToolTipText = "CANCELAR este Recibo"
  
End Sub

Private Sub HabilitarRecibo()
    
    On Error Resume Next
  
  cmdNuevoValor.Enabled = True
  cmdADeducir.Enabled = True
  cmdAplicacion.Enabled = True
  cmdReciboImprimir.Enabled = True
  cmdReciboVer.Enabled = True
  lvValores.Enabled = True
  lvAplicacion.Enabled = True
  lvADeducir.Enabled = True
  EtiquetasRecibos.Enabled = True
  txtObservaciones.Enabled = True

End Sub

Private Sub InhabilitarRecibo()
  
    On Error Resume Next
    
  lvValores.Enabled = False
  lvAplicacion.Enabled = False
  lvADeducir.Enabled = False
  cmdNuevoValor.Enabled = False
  cmdADeducir.Enabled = False
  cmdAplicacion.Enabled = False
  cmdReciboImprimir.Enabled = False
  cmdReciboVer.Enabled = False
  EtiquetasRecibos.Enabled = False
  txtObservaciones.Enabled = False

End Sub

Private Sub cmdValorAceptar_Click()

    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "cmdValorAceptar_Click"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------
    
    If DatosValidos("recibo") Then
                
        
'        If Not lvValores.SelectedItem Is Nothing Then
'            Select Case lvValores.SelectedItem.Tag
'            Case "add"
'                Set m.itemX = lvValores.ListItems.Add(, , cboTipoValor.Text)
'            Case "upd"
'                Set m.itemX = lvValores.SelectedItem
'            Case Else
'                'MsgBox "view"
'                Exit Sub
'            End Select
'        Else 'NOT NOT...
'            Set m.itemX = lvValores.ListItems.Add(, , cboTipoValor.Text)
'        End If
                                        
        If lvValores.Tag = "upd" Then
            If Not lvValores.SelectedItem Is Nothing Then
               Set m.ItemX = lvValores.SelectedItem
            Else
               Set m.ItemX = lvValores.ListItems.Add(, , cboTipoValor.Text)
            End If
        Else
            Set m.ItemX = lvValores.ListItems.Add(, , cboTipoValor.Text)
        End If
                
        With m
            .ItemX.SubItems(1) = txtValor.Text
            .ItemX.SubItems(2) = dtF_EmiCheque.value
            .ItemX.SubItems(3) = dtF_CobroCheque.value
            .ItemX.SubItems(4) = txtN_Cheque.Text
            .ItemX.SubItems(5) = txtNroDeCuenta.Text
            .ItemX.SubItems(7) = txtCPA.Text
            .ItemX.SubItems(8) = chkDeTercero.value
            .ItemX.SubItems(9) = chkRecBahia.value
            .ItemX.SubItems(10) = cboTipoValor.ItemData(cboTipoValor.ListIndex)
            .ItemX.SubItems(12) = txtCambio.Text
            
            If cboBanco.ListIndex < 0 Then
              .ItemX.SubItems(6) = " "
              .ItemX.SubItems(11) = 0
            Else
              .ItemX.SubItems(6) = cboBanco.List(cboBanco.ListIndex)
              .ItemX.SubItems(11) = cboBanco.ItemData(cboBanco.ListIndex)
            End If
            .ItemX.Tag = vbNullString
        End With 'm
        
        lvValores.Tag = "add"
        
        TotalRecibo
        
        Set EtiquetasRecibos.SelectedItem = EtiquetasRecibos.Tabs(1)
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

Private Sub cmdValorCancelar_Click()
    
    On Error Resume Next
    
    LimpiarIngresosRecibo
    Set EtiquetasRecibos.SelectedItem = EtiquetasRecibos.Tabs(1)

End Sub

Private Sub cmdVISITA_Click()
    
    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "cmdVISITA_Click"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------
        
If vg.AppActiva Then
    ' Dependiendo del cliente
    If Not cboCliente.ListIndex < 1 Then
        With cmdVISITA
            If .Tag = "INICIAR Visita" Then
                            
              Dim wSaldo As Single
              wSaldo = 0
              
              ' Limpio las listas por cambio de cliente
              lvDevolucion1.ListItems.Clear
              lvDevolucion2.ListItems.Clear
              lvPedido.ListItems.Clear
              lvValores.ListItems.Clear
              lvAplicacion.ListItems.Clear
              lvADeducir.ListItems.Clear
              lvEstado.ListItems.Clear
              lvCliNovedad.ListItems.Clear
              
              txtObservaciones.Text = ""
              DoEvents
              ' Coloca en 0 el totalizador de Pedido
              TotalPedido
              TotalRecibo
              TotalADeducir
              TotalApli
              DoEvents
              ' INICIA la VISITA
              m.IdCliente = cboCliente.ItemData(cboCliente.ListIndex)
              
              vg.auditor.Guardar Visita, INICIA, cboCliente.List(cboCliente.ListIndex)
              
              .Caption = "Cerrar Visita"
              .Tag = "CERRAR Visita"
              .ToolTipText = "CERRAR Sesion de Venta"
                            
              EtiquetasAdminis.Enabled = True
              cboCliente.Enabled = False
              For m.I = 1 To EtiquetasAdminis.Tabs.Count
                ' La que se borra
                EtiquetasAdminis.Tabs.Remove 1
              Next m.I
              DoEvents
              EtiquetasAdminis.Tabs.Add 1, "Pedido", "Nota de &Venta"
              EtiquetasAdminis.Tabs.Add 2, "Recibo", "&Recibo"
              EtiquetasAdminis.Tabs.Add 3, "Estado", "&Cta. Cte."
              EtiquetasAdminis.Tabs.Add 4, "Devoluciones", "&Devoluciones"
              EtiquetasAdminis.Tabs.Add 5, "DatosClientes", "Clien&te"
              EtiquetasAdminis.Tabs.Add 6, "Movimientos", "&Bandeja de Enviados"
              EtiquetasAdminis.Tabs.Add 7, "InterDepositos", "Inter Depósitos"
              DoEvents
              Set m.adoREC = xGetRSMDB(vg.Conexion, "tblClientes", "ID=" & CStr(cboCliente.ItemData(cboCliente.ListIndex)), "NONE")
              If Not m.adoREC.EOF Then
                 If m.adoREC!Activo = "2" Then
                    'EtiquetasAdminis.Tabs.Remove EtiquetasAdminis.Tabs("Pedido").Index
                    MsgBox "Cliente suspendido para la VENTA", vbCritical + vbOKOnly, "ATENCIÓN!"
                 Else
                    'todo OK
                 End If
              End If
              
              Set EtiquetasAdminis.SelectedItem = EtiquetasAdminis.Tabs(1)
              DoEvents
              ' Cargo la Cta Cte. del cliente seleccionado
              Set m.adoREC = xGetRSMDB(vg.Conexion, "v_ctacte", "IDCliente=" & CStr(cboCliente.ItemData(cboCliente.ListIndex)), "Orden, Fecha")
              If Not m.adoREC.EOF Then
                  wSaldo = 0
                  While Not m.adoREC.EOF
                    Set m.ItemX = lvEstado.ListItems.Add(, , m.adoREC!Fecha)
                    m.ItemX.SubItems(1) = m.adoREC!Comprobante
                    m.ItemX.SubItems(2) = m.adoREC!Importe
                    m.ItemX.SubItems(3) = m.adoREC!Saldo
                    
                    wSaldo = wSaldo + CSng(m.adoREC!SaldoS)
                    m.ItemX.SubItems(4) = wSaldo
                    
                    m.ItemX.SubItems(5) = m.adoREC!Det_Comprobante
                    m.ItemX.SubItems(6) = m.adoREC!ImpOferta
                    m.ItemX.SubItems(7) = m.adoREC!TextoOferta
                    m.ItemX.SubItems(8) = m.adoREC!Vencida
                    m.ItemX.SubItems(9) = m.adoREC!ImpPercep       'XJXJXJ
                    m.ItemX.SubItems(10) = m.adoREC!IdCliente
                    m.ItemX.SubItems(11) = IIf(IsNull(m.adoREC!EstaAplicada), "N", "S")
                    m.ItemX.SubItems(12) = IIf(IsNull(m.adoREC!EsContado), "0", m.adoREC!EsContado)
                                        
                    If Mid(m.ItemX.SubItems(1), 1, 3) = "DEB" Then
                        m.ItemX.ListSubItems(1).ForeColor = vbBlue
                        m.ItemX.ListSubItems(1).Bold = vbBlue
                    End If
                    
                    m.adoREC.MoveNext
                  Wend
                  
                  'lvEstado.ColumnHeaders(3).Alignment = lvwColumnRight
                  'lvEstado.ColumnHeaders(4).Alignment = lvwColumnRight
                  'lvEstado.ColumnHeaders(5).Alignment = lvwColumnRight

              End If
              Set m.adoREC = Nothing
              DoEvents
              ' Cargo los Datos Particulares del Cliente
              CliF_Novedad.value = Date
              CliNovedad.Text = ""
              lvCliNovedad.ListItems.Clear
              
              Set elcliente = New clsClientes
              elcliente.Leer CLng(m.IdCliente)
              DoEvents
              
              lblCliNroCuenta.Caption = "N° de Cuenta: " & Format(m.IdCliente, "00000")
              cliRazonSocial.Text = elcliente.RazonSocial
              CliCuit.Text = elcliente.Cuit
              CliDomicilio.Text = elcliente.Domicilio
              CliTelefono.Text = elcliente.Telefono
              CliEmail.Text = elcliente.Email
              CliLocalidad.Text = elcliente.Ciudad
              CliObservaciones.Caption = "Observaciones: " & elcliente.Observaciones
              DoEvents
              For m.I = 1 To elcliente.Novedades.Count
                 Set m.ItemX = lvCliNovedad.ListItems.Add(, , elcliente.Novedades.Item(m.I).F_Carga)
                 m.ItemX.SubItems(1) = elcliente.Novedades.Item(m.I).Novedad
                 m.ItemX.SubItems(2) = elcliente.Novedades.Item(m.I).ID
              Next m.I
              Set elcliente = Nothing
              
            Else 'NOT .CAPTION...
              ' CIERRA la VISITA
              ' Me fijo si se cerro las sesiones de Pedido Y Recibo
              
                lblCliNroCuenta.Caption = ""
                cliRazonSocial.Text = ""
                CliCuit.Text = ""
                CliDomicilio.Text = ""
                CliTelefono.Text = ""
                CliEmail.Text = ""
                CliLocalidad.Text = ""
                CliObservaciones.Caption = "Observaciones: "

              If cmdVenta.Tag <> "CANCELAR" And cmdRecibo.Tag <> "CANCELAR" And cmdDevolucion.Tag <> "CANCELAR" Then
                
                vg.auditor.Guardar Visita, TERMINA, cboCliente.List(cboCliente.ListIndex)
                
                .Tag = "INICIAR Visita"
                .Caption = "Iniciar Visita"
                .ToolTipText = "INICAR Sesion de Venta"
                'Set cmdVISITA.PictureNormal = imlBotonesVicita.ListImages("INICIAR").ExtractIcon
                EtiquetasAdminis.Enabled = False
                cboCliente.Enabled = True
                
                'If EtiquetasAdminis.Tabs.Item(1).Caption = "Nota de &Venta" Then
                '
                'End If
                
                EtiquetasAdminis.Tabs.Remove EtiquetasAdminis.Tabs("Pedido").Index
                EtiquetasAdminis.Tabs.Remove EtiquetasAdminis.Tabs("Recibo").Index
                EtiquetasAdminis.Tabs.Remove EtiquetasAdminis.Tabs("Estado").Index
                EtiquetasAdminis.Tabs.Remove EtiquetasAdminis.Tabs("Devoluciones").Index
                EtiquetasAdminis.Tabs.Remove EtiquetasAdminis.Tabs("DatosClientes").Index
                EtiquetasAdminis.Tabs.Remove EtiquetasAdminis.Tabs("InterDepositos").Index
                
                Set EtiquetasAdminis.SelectedItem = EtiquetasAdminis.Tabs(1)
                
                ' Pregunto si tiene PENDIENTES DE ENVIO
                If LosMovimientos.PreguntoAlCerrarVisita(m.IdCliente) Then
                    ' Si hay movimientos pendientes pregunto si quiere enviarlos
                    
                    Select Case MsgBox("Tiene movimientos que aun no ha enviado de este cliente." & vbCrLf & _
                                       "¿QUIERE ENVIARLOS AHORA?", vbYesNo Or vbQuestion Or vbSystemModal Or vbDefaultButton1, "ENVIO DE MOVIMIENTOS")
                     Case vbYes
                     
                        If ProbarConexion Then
                            Dim dlg As frmConexionEnvio
                            Set dlg = New frmConexionEnvio
                            dlg.ModoTransmision = TRANSMITIR_RECORDSET
                            dlg.IdCliente = 0
                            dlg.Show vbModal, Me
                            Set dlg = Nothing
                        Else
                            vg.auditor.Guardar Comunicaciones, FALLO, "Probar Conexion, desde Cierre de Visita " & cboCliente.List(cboCliente.ListIndex)
                        End If
                     
                     Case vbNo
                     
                        If vg.miSABOR > 2 And ReadINI("DATOS", "EEA", "1") = 1 Then
                            If Len(Trim(Dir(vg.Path & "\monitorE.exe"))) > 0 Then
                                Shell vg.Path & "\monitorE.exe", vbHide
                            End If
                        End If
                    End Select
                   
                  End If
                  m.IdCliente = 0
              Else
                  If cmdVenta.Tag = "CANCELAR" Then
                      MsgBox "Antes de cerrar la visita DEBE IMPRIMIR ó CANCELAR el PEDIDO", vbCritical + vbOKOnly, "ATENCIÓN!"
                      Set EtiquetasAdminis.SelectedItem = EtiquetasAdminis.Tabs("Pedido")
                      If lvPedido.ListItems.Count > 0 Then
                        lvPedido.SetFocus
                      Else
                        cmdVenta.SetFocus
                      End If ' lvPedido.ListItems.Count > 0
                  End If
                  
                  If cmdRecibo.Tag = "CANCELAR" Then
                      MsgBox "Antes de cerrar la visita DEBE IMPRIMIR ó CANCELAR el RECIBO", vbCritical + vbOKOnly, "ATENCIÓN!"
                      Set EtiquetasAdminis.SelectedItem = EtiquetasAdminis.Tabs("Recibo")
                      
                      Set EtiquetasRecibos.SelectedItem = EtiquetasRecibos.Tabs(1)
                      cmdRecibo.SetFocus
                  End If ' cmdRecibo.Tag = "CANCELAR"
              
                  If cmdDevolucion.Tag = "CANCELAR" Then
                      MsgBox "Antes de cerrar la visita DEBE IMPRIMIR ó CANCELAR la DEVOLUCION", vbCritical + vbOKOnly, "ATENCIÓN!"
                      Set EtiquetasAdminis.SelectedItem = EtiquetasAdminis.Tabs("Devoluciones")
                      If lvDevolucion1.ListItems.Count > 0 Then
                        lvDevolucion1.SetFocus
                      Else
                        cmdDevolucion.SetFocus
                      End If ' lvDevolucion1.ListItems.Count > 0
                  End If
              
                  If cmdIntDepNuevo.Tag = "CANCELAR" Then
                      MsgBox "Antes de cerrar la visita DEBE IMPRIMIR ó CANCELAR el Inter Depósito", vbCritical + vbOKOnly, "ATENCIÓN!"
                      Set EtiquetasAdminis.SelectedItem = EtiquetasAdminis.Tabs("InterDepositos")
                      dtBd_Fecha.SetFocus
                  End If
                  
              End If
              
            End If ' .Tag = "INICIAR Visita"
        End With 'cmdVISITA
    End If

Else
    MsgBox "CUIDADO!!, Debe Activar la Aplicación", vbExclamation, "ATENCION!!"
End If

'-------- ErrorGuardian Begin --------
Exit Sub

ErrorGuardianLocalHandler:
    If Err.Number = 53 Then ' el archivo de VersionAnterior NO EXISTE
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


Private Sub EtiquetasDevolucion_Click()

    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "EtiquetasDevolucion_Click"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------

    Select Case EtiquetasDevolucion.SelectedItem.Key
      Case "MercaderiaN"
          
          frmDevolucionT1.Visible = True
          frmDevolucionT2.Visible = False
          If lvDevolucion1.Tag = "upd" Then
             'estoy editando de la lista lvValores
          Else
             lvDevolucion1.Tag = "add"
             LimpiarIngresoDevolucion
          End If
          
          'cboTipoValor.SetFocus

     Case "MercaderiaF"
     
          frmDevolucionT1.Visible = False
          frmDevolucionT2.Visible = True
          
          If lvDevolucion2.Tag = "upd" Then
             'estoy editando de la lista lvValores
          Else
             lvDevolucion2.Tag = "add"
             LimpiarIngresoDevolucion
          End If
          
          'cboTipoValor.SetFocus
    End Select
    
    RedimencionaEtiaAdminX
    RedimencionaEtiaAdminY

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

Private Sub EtiquetasRecibos_Click()

    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "EtiquetasRecibos_Click"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------

    Select Case EtiquetasRecibos.SelectedItem.Key
      Case "Listado"
          frmReciboListado.Visible = True
          frmReciboDetalle.Visible = False
          frmAplicacion.Visible = False
          frmADeducir.Visible = False
      Case "Detalle"

          frmReciboListado.Visible = False
          frmReciboDetalle.Visible = True
          frmAplicacion.Visible = False
          frmADeducir.Visible = False
          
          If lvValores.Tag = "upd" Then
             'estoy editando de la lista lvValores
          Else
             lvValores.Tag = "add"
             LimpiarIngresosRecibo
          End If
          
          cboTipoValor.SetFocus
          
      Case "Aplicacion"
          frmReciboListado.Visible = False
          frmReciboDetalle.Visible = False
          frmAplicacion.Visible = True
          frmADeducir.Visible = False
          'txtApliConcepto.SetFocus
      Case "ADeducir"
          frmReciboListado.Visible = False
          frmReciboDetalle.Visible = False
          frmAplicacion.Visible = False
          frmADeducir.Visible = True
          'txtDeduConcepto.SetFocus
    End Select
    
    RedimencionaEtiaAdminX
    RedimencionaEtiaAdminY

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

Private Sub LimpiarIngresosRecibo()
    
    On Error Resume Next
    
    cboTipoValor.ListIndex = 0
    txtValor.Text = "0.00"
    txtCambio.Text = ""
    lbldivisas.Caption = ""
    dtF_EmiCheque.value = Date
    dtF_CobroCheque.value = Date
    txtN_Cheque.Text = ""
    txtNroDeCuenta.Text = ""
    cboBanco.ListIndex = 0
    txtCPA.Text = ""
    chkDeTercero.value = 0
    chkRecBahia.value = 0

End Sub

Private Sub lvAdeducir_DblClick()
    
    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "lvAdeducir_DblClick"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------
    
    If Not lvADeducir.SelectedItem Is Nothing Then
              
        txtDeduConcepto.Text = lvADeducir.ListItems(lvADeducir.SelectedItem.Index).Text
        txtDeduImporte.Text = lvADeducir.ListItems(lvADeducir.SelectedItem.Index).SubItems(1)
        chkDeduPorcentaje.value = lvADeducir.ListItems(lvADeducir.SelectedItem.Index).SubItems(2)
        chkDeduAlResto.value = lvADeducir.ListItems(lvADeducir.SelectedItem.Index).SubItems(3)
        
        Set m.ItemX = lvEstado.FindItem(lvADeducir.SelectedItem.Text, lvwSubItem, , lvwPartial)
        If Not m.ItemX Is Nothing Then
           lvEstado.ListItems(m.ItemX.Index).Selected = True
           lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbDefault
           lvEstado.SelectedItem.ListSubItems.Item(1).Bold = False
        End If
        Set m.ItemX = Nothing
        
        lvADeducir.ListItems.Remove lvADeducir.SelectedItem.Index
        
        TotalADeducir
                
        Set EtiquetasRecibos.SelectedItem = EtiquetasRecibos.Tabs(4)
        
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

Private Sub lvAplicacion_DblClick()
    
    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "lvAplicacion_DblClick"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------
    
    If Not lvAplicacion.SelectedItem Is Nothing Then
              
        txtApliConcepto.Text = lvAplicacion.ListItems(lvAplicacion.SelectedItem.Index).Text
        txtApliImporte.Text = lvAplicacion.ListItems(lvAplicacion.SelectedItem.Index).SubItems(1)
        txtPercepImporte.Text = lvAplicacion.ListItems(lvAplicacion.SelectedItem.Index).SubItems(2)
        
        Set m.ItemX = lvEstado.FindItem(lvAplicacion.SelectedItem.Text, lvwSubItem, , lvwPartial)
        If Not m.ItemX Is Nothing Then
           lvEstado.ListItems(m.ItemX.Index).Selected = True
           lvEstado.SelectedItem.ListSubItems.Item(1).ForeColor = vbDefault
           lvEstado.SelectedItem.ListSubItems.Item(1).Bold = False
        End If
        Set m.ItemX = Nothing
        
        lvAplicacion.ListItems.Remove lvAplicacion.SelectedItem.Index
        
        If lvAplicacion.ListItems.Count <= 0 Then
            lvEstado.Tag = "-1"
        End If
        
        TotalApli
        
        TotalADeducir
        
        Set EtiquetasRecibos.SelectedItem = EtiquetasRecibos.Tabs(3)
        
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

Private Sub lvValores_DblClick()

    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "lvValores_DblClick"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------

    If Not lvValores.SelectedItem Is Nothing Then
        
        cboTipoValor.ListIndex = BuscarIndiceEnCombo(cboTipoValor, lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(10), False)
        txtValor.Text = lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(1)
        dtF_EmiCheque.value = lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(2)
        dtF_CobroCheque.value = lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(3)
        txtN_Cheque.Text = lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(4)
        txtNroDeCuenta.Text = lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(5)
        cboBanco.ListIndex = BuscarIndiceEnCombo(cboBanco, lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(11), False)
        txtCPA.Text = lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(7)
        chkDeTercero.value = lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(8)
        chkRecBahia.value = lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(9)
        
        lvValores.Tag = "upd"
        Set EtiquetasRecibos.SelectedItem = EtiquetasRecibos.Tabs(2)
        
        If InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "dólar") > 0 Or _
           InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "euro") > 0 Then
            
            txtCambio.Text = lvValores.ListItems(lvValores.SelectedItem.Index).SubItems(12)
            lbldivisas.Caption = Format(CSng(txtValor.Text) * CSng(txtCambio.Text), "fixed")
        Else
            txtCambio.Text = ""
            lbldivisas.Caption = ""
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

Private Sub lvValores_KeyPress(KeyAscii As Integer)
  
    '-------- ErrorGuardian Begin --------
    Const PROCNAME_ As String = "lvValores_KeyPress"
    On Error GoTo ErrorGuardianLocalHandler
    '-------- ErrorGuardian End ----------
  
  Select Case KeyAscii
    Case 13 '[ENTER]
      cmdReciboBorrar_Click
  End Select

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


Private Sub tbBoton_Click(Index As Integer)
    
    On Error Resume Next
    
    Select Case Index
        Case 0
            Dim f1 As New frmPorcentajeLinea
            f1.Show vbModal, Me
            Set f1 = Nothing
        Case 1
            Link_http "http://www.autonauticasur.com.ar"
        Case 2
            If vg.miSABOR >= A3_Viajantes Then
                Link_http "http://www.autonauticasur-r.com.ar/portals/0/w3/acc.html"
            Else
                Link_http "http://www.autonauticasur-r.com.ar/revista/vigente/index2.html"
            End If
            
        Case 3
            Dim f2 As New frmSetting
            f2.Show vbModal, Me
            Set f2 = Nothing
        Case 4
            Dim f3 As New frmTip
            f3.Show vbModal, Me
            Set f3 = Nothing
        Case 5
            Dim X As Double
            X = Shell(vg.Path & "\Calc.exe", vbNormalFocus)
        Case 6
            Dim f4 As New frmAyuda
            f4.Show vbModal, Me
            Set f4 = Nothing
        Case 7
            Dim f5 As New frmRendicion
            'Dim f5 As New frmAperturaCuenta2
            f5.Show vbModal, Me
            Set f5 = Nothing
    End Select
        
    
End Sub

Private Sub txtApliConcepto_KeyPress(KeyAscii As Integer)
  aMayuscula KeyAscii
End Sub

Private Sub txtApliConcepto_Validate(Cancel As Boolean)
   
    On Error Resume Next
    
   If Len(Trim(txtApliConcepto.Text)) = 0 Then
        MsgBox "Ingrese Concepto", vbExclamation, "atención"
        Cancel = True
    Else
        Cancel = False
    End If
    
End Sub

Private Sub txtApliImporte_KeyPress(KeyAscii As Integer)
        
    On Error Resume Next
    
  If KeyAscii = 44 Or KeyAscii = 46 Then
      If InStr(1, txtApliImporte.Text, ",", vbTextCompare) > 0 Or InStr(1, txtApliImporte.Text, ".", vbTextCompare) > 0 Then
        KeyAscii = 0
      Else
        SoloDigitos KeyAscii
      End If
  Else
      SoloDigitos KeyAscii
  End If
   
End Sub

Private Sub txtDeduConcepto_KeyPress(KeyAscii As Integer)
  aMayuscula KeyAscii
End Sub

Private Sub txtDeduConcepto_Validate(Cancel As Boolean)
  
    On Error Resume Next
    
    If Len(Trim(txtDeduConcepto.Text)) = 0 Then
        MsgBox "Ingrese Concepto", vbExclamation, "atención"
        Cancel = True
    Else
        Cancel = False
    End If
    
End Sub

Private Sub txtDeduImporte_KeyPress(KeyAscii As Integer)
        
    On Error Resume Next
    
  If KeyAscii = 44 Or KeyAscii = 46 Then
      If InStr(1, txtDeduImporte.Text, ",", vbTextCompare) > 0 Or InStr(1, txtDeduImporte.Text, ".", vbTextCompare) > 0 Then
        KeyAscii = 0
      Else
        SoloDigitos KeyAscii
      End If
  Else
      SoloDigitos KeyAscii
  End If
   
End Sub
Private Sub txtCambio_KeyPress(KeyAscii As Integer)
        
    On Error Resume Next
    
    If KeyAscii = 44 Or KeyAscii = 46 Then
        If InStr(1, txtCambio.Text, ",", vbTextCompare) > 0 Or InStr(1, txtCambio.Text, ".", vbTextCompare) > 0 Then
          KeyAscii = 0
        Else
          SoloDigitos KeyAscii
        End If
    Else
        SoloDigitos KeyAscii
    End If

End Sub

Private Sub txtValor_GotFocus()
    txtValor.SelStart = 0
    txtValor.SelLength = Len(txtValor.Text)
End Sub

Private Sub txtValor_KeyPress(KeyAscii As Integer)
        
    On Error Resume Next
    
    If KeyAscii = 44 Or KeyAscii = 46 Then
        If InStr(1, txtValor.Text, ",", vbTextCompare) > 0 Or InStr(1, txtValor.Text, ".", vbTextCompare) > 0 Then
          KeyAscii = 0
        Else
          SoloDigitos KeyAscii
        End If
    Else
        SoloDigitos KeyAscii
    End If

End Sub

Private Function DatosValidos(ByVal pCampo As String) As Boolean
       
'-------- ErrorGuardian Begin --------
Const PROCNAME_ As String = "DatosValidos"
On Error GoTo ErrorGuardianLocalHandler
'-------- ErrorGuardian End ----------
    
    Dim StringToFind As String
    Dim FoundAt As Long
    Dim wImporte As Single
  
    DatosValidos = True
                                  
    If LCase(pCampo) = "txtvalor" Or LCase(pCampo) = "all" Or LCase(pCampo) = "recibo" Then
        If Len(Trim(txtValor.Text)) = 0 Then
            MsgBox "Ingrese Importe", vbExclamation, "Atención"
            DatosValidos = False
            txtValor.SelStart = 0
            txtValor.SelLength = Len(txtValor.Text)
            txtValor.SetFocus
        Else
            If Len(Trim(txtValor.Text)) > 0 Then
                If CDbl(txtValor.Text) <= 0 Then
                  DatosValidos = False
                  MsgBox "Ingrese Importe", vbExclamation, "Atención"
                  txtValor.SetFocus
                Else
                    If InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "dólar") > 0 Or _
                       InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "euro") > 0 Then
                        lbldivisas.Caption = Format(CSng(txtValor.Text) * CSng(txtCambio.Text), "fixed")
                    Else
                        lbldivisas.Caption = ""
                    End If
                End If
            End If
        End If
    End If

    If LCase(pCampo) = "txtcambio" Or LCase(pCampo) = "all" Or LCase(pCampo) = "recibo" Then
        If InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "dólar") > 0 Or _
           InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "euro") > 0 Then
                If Len(Trim(txtCambio.Text)) = 0 Then
                    MsgBox "Ingrese T. Cambio", vbExclamation, "Atención"
                    DatosValidos = False
                    txtCambio.SelStart = 0
                    txtCambio.SelLength = Len(txtCambio.Text)
                    txtCambio.SetFocus
                Else
                    If Len(Trim(txtValor.Text)) > 0 Then
                        If CDbl(txtValor.Text) <= 0 Then
                          DatosValidos = False
                          MsgBox "Ingrese Importe", vbExclamation, "Atención"
                          txtValor.SetFocus
                        Else
                            If InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "dólar") > 0 Or _
                               InStr(1, LCase(cboTipoValor.List(cboTipoValor.ListIndex)), "euro") > 0 Then
                                lbldivisas.Caption = Format(CSng(txtValor.Text) * CSng(txtCambio.Text), "fixed")
                            Else
                                lbldivisas.Caption = ""
                            End If
                        End If
                    End If
                End If
        End If
    End If
    
    If LCase(pCampo) = "cbotipovalor" Or LCase(pCampo) = "all" Or LCase(pCampo) = "recibo" Then
      If cboTipoValor.ListIndex <= 0 Then
          DatosValidos = False
          MsgBox "Ingrese tipo de valor", vbExclamation, "Atención"
          cboTipoValor.SetFocus
       Else
          If cboTipoValor.ListIndex = 1 Then ' cheque
              
              If LCase(pCampo) = "cbobanco" Or LCase(pCampo) = "all" Or LCase(pCampo) = "recibo" Then
                  If cboBanco.ListIndex <= 0 Then
                      MsgBox "Seleccione un Banco", vbExclamation, "Atención"
                      DatosValidos = False
                  Else
                      StringToFind = cboBanco.Text
                      FoundAt = SendMessage(cboBanco.Hwnd, CB_FINDSTRINGEXACT, ByVal -1, ByVal StringToFind)
                      If FoundAt = CB_ERR Then
                          MsgBox "Banco, INEXISTENTE", vbExclamation, "Atención"
                          DatosValidos = False
                      Else
                          cboBanco.ListIndex = FoundAt
                      End If
                      cboBanco.Refresh
                  End If
              End If
              
              'If cboBanco.ListIndex <= 0 Or Len(Trim$(txtNroDeCuenta.Text)) = 0 Or Len(Trim$(txtN_Cheque.Text)) = 0 Then
               If Len(Trim$(txtN_Cheque.Text)) = 0 Then
                  DatosValidos = False
                  MsgBox "Ingrese datos del cheque", vbExclamation, "Atención"
                  txtN_Cheque.SetFocus
               End If
          
          ElseIf cboTipoValor.ListIndex >= 5 Then ' retención
               
               If Len(Trim$(txtN_Cheque.Text)) = 0 Then
                  DatosValidos = False
                  MsgBox "Ingrese nro. de Retención", vbExclamation, "Atención"
                  txtN_Cheque.SetFocus
               End If

          End If
      End If
    End If
    
    If LCase(pCampo) = "clinovedad" Or LCase(pCampo) = "#all" Or LCase(pCampo) = "novedad" Then
        If Len(Trim(CliNovedad.Text)) = 0 Then
            MsgBox "Ingrese Novedad", vbExclamation, "Atención"
            DatosValidos = False
            CliNovedad.SetFocus
        End If
    End If
        
    If LCase(pCampo) = "all" Or LCase(pCampo) = "aplicacion" Or LCase(pCampo) = "adeducir" Then
        If BuscarIndiceEnListView(lvADeducir, "cascara:", 0) = -1 Or Left(Me.txtDeduConcepto.Text, 8) = "cascara:" Then
           DatosValidos = True
        Else
            DatosValidos = False
            MsgBox "NO puede agregar aplicación, ni descuentos," & vbCrLf & "para ello debe quitar la cascara", vbOKOnly + vbInformation, "Datos Válidos"
        End If
    End If
    
    If LCase(pCampo) = "txtapliimporte" Or LCase(pCampo) = "all" Or LCase(pCampo) = "aplicacion" Then
        If Len(Trim(txtApliImporte.Text)) = 0 Then
            MsgBox "Ingrese Importe", vbExclamation, "Atención"
            DatosValidos = False
            txtApliImporte.SelStart = 0
            txtApliImporte.SelLength = Len(txtApliImporte.Text)
            txtApliImporte.SetFocus
        End If
    End If
    
    If LCase(pCampo) = "txtdeduimporte" Or LCase(pCampo) = "all" Or LCase(pCampo) = "adeducir" Then
        If Len(Trim(txtDeduImporte.Text)) = 0 Then
            MsgBox "Ingrese Importe", vbExclamation, "Atención"
            DatosValidos = False
            txtDeduImporte.SelStart = 0
            txtDeduImporte.SelLength = Len(txtDeduImporte.Text)
            txtDeduImporte.SetFocus
        End If
    End If
        
    If LCase(pCampo) = "interdeposito" Or LCase(pCampo) = "all" Then
        
        If CSng(txtBd_Monto.Text) <= 0 Then
            MsgBox "El monto de la B.D. debe ser mayor que cero", vbExclamation, "Atención"
            DatosValidos = False
        End If
        
        If opIntDepTipoDeposito(1).value And txtBdCh_Cantidad.Text <= 0 Then
            MsgBox "ingrese la cantidad de cheques", vbExclamation, "Atención"
            DatosValidos = False
        End If
        
        If CLng("0" & txtBd_Nro.Text) <= 0 Then
            MsgBox "El n° de la B.D. debe ser mayor que cero", vbExclamation, "Atención"
            DatosValidos = False
        End If
        
        If cboCtaIntDep.ListIndex < 1 Then
            MsgBox "Seleccione Bco. ó CBU de Depósito", vbExclamation, "Atención"
            DatosValidos = False
        End If
        
        If lvIntDepFacturas.ListItems.Count <= 0 Then
            MsgBox "Debe Ingresar las Facturas de Cancela", vbExclamation, "Atención"
            DatosValidos = False
        End If
      
'        wImporte = 0
'        For m.I = 1 To lvIntDepFacturas.ListItems.Count
'         wImporte = wImporte + CSng(lvIntDepFacturas.ListItems(m.I).ListSubItems(2))
'        Next m.I
        
'        If CSng(wImporte) <> CSng(txtBd_Monto.Text) Then
'            MsgBox "El monto de la B.D. no coincide con las facturas Ingresadas", vbExclamation, "Atención"
'            DatosValidos = False
'        End If
                
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

Private Sub txtValor_LostFocus()
    txtValor.Text = Format("0" & txtValor.Text, "fixed")
End Sub

Private Sub txtValor_Validate(Cancel As Boolean)
  If LenB(Trim(txtValor.Text)) > 0 Then
    Cancel = Not DatosValidos("txtValor")
  End If
End Sub

Private Sub txtCambio_Validate(Cancel As Boolean)
  If LenB(Trim(txtCambio.Text)) > 0 Then
    Cancel = Not DatosValidos("txtCambio")
  End If
End Sub

Public Sub Recibo_Imprimir(ByVal pNroRecibo As String)
    
'-------- ErrorGuardian Begin --------
Const PROCNAME_ As String = "Recibo_Imprimir"
On Error GoTo ErrorGuardianLocalHandler
'-------- ErrorGuardian End ----------
    
    Dim f As frmReporte
    Set f = New frmReporte
       
    Dim crApp As New CRAXDRT.Application
    Dim crCONN As CRAXDRT.ConnectionProperties
    Dim Report As CRAXDRT.Report
    Dim wRptPdf As Boolean
    
    If ReadINI("DATOS", "RptPdf", "0") = 1 Then
        wRptPdf = True
    Else
        wRptPdf = False
    End If
    
    Set Report = crApp.OpenReport(vg.Path & "\Reportes\Recibo_Enc3.rpt")
    
    If wRptPdf = True Then
        Report.ExportOptions.DestinationType = crEDTDiskFile
        Report.ExportOptions.FormatType = crEFTPortableDocFormat
        Report.ExportOptions.PDFExportAllPages = True
        Report.ExportOptions.DiskFileName = vg.Path & "\pdf\R" & pNroRecibo & ".pdf"
    End If
    
    Set crCONN = Report.Database.Tables(1).ConnectionProperties
    Report.Database.Tables(1).DllName = "crdb_dao.dll"
    crCONN.DeleteAll
    crCONN.Add "Database Name", pgp(dstring, 2)
    crCONN.Add "Session UserID", pgp(u2String, 2)
    crCONN.Add "Session Password", pgp(c2String, 2)
    crCONN.Add "System Database Path", pgp(sstring, 2)
                                
    Report.EnableParameterPrompting = False
    Report.ParameterFields.Item(1).ClearCurrentValueAndRange
    Report.ParameterFields.Item(1).AddCurrentValue pNroRecibo
    Report.ReportTitle = "R - " & pNroRecibo

    If wRptPdf = True Then
        Report.Export (False)
    Else
        Set f.miReport = Report
    End If
   
    crApp.CanClose
    Set crApp = Nothing
    Set Report = Nothing
    Set crCONN = Nothing
                
    If wRptPdf = True Then
       ShellExecute 0&, "open", LCase(vg.Path & "\pdf\R" & pNroRecibo & ".pdf"), "", "", vbNormalFocus
    Else
        f.Show vbModal
    End If
    
    Set f = Nothing
            
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

Public Sub CtaCte_Imprimir(ByVal pIdCliente As String)
    
'-------- ErrorGuardian Begin --------
Const PROCNAME_ As String = "CtaCte_Imprimir"
On Error GoTo ErrorGuardianLocalHandler
'-------- ErrorGuardian End ----------
    
    Dim f As frmReporte
    Set f = New frmReporte
       
    Dim crApp As New CRAXDRT.Application
    Dim crCONN As CRAXDRT.ConnectionProperties
    Dim Report As CRAXDRT.Report
    Dim wRptPdf As Boolean
    
    If ReadINI("DATOS", "RptPdf", "0") = 1 Then
        wRptPdf = True
    Else
        wRptPdf = False
    End If
    
    Set Report = crApp.OpenReport(vg.Path & "\Reportes\CtaCte3.rpt")
    
    If wRptPdf = True Then
        Report.ExportOptions.DestinationType = crEDTDiskFile
        Report.ExportOptions.FormatType = crEFTPortableDocFormat
        Report.ExportOptions.PDFExportAllPages = True
        Report.ExportOptions.DiskFileName = vg.Path & LCase(vg.Path & "\pdf\CC" & Format(pIdCliente, "000000") & ".pdf")
    End If
    
    Set crCONN = Report.Database.Tables(1).ConnectionProperties
    Report.Database.Tables(1).DllName = "crdb_dao.dll"
    crCONN.DeleteAll
    crCONN.Add "Database Name", pgp(dstring, 2)
    crCONN.Add "Session UserID", pgp(u2String, 2)
    crCONN.Add "Session Password", pgp(c2String, 2)
    crCONN.Add "System Database Path", pgp(sstring, 2)
                                
    Report.EnableParameterPrompting = False
    Report.ParameterFields.Item(1).ClearCurrentValueAndRange
    Report.ParameterFields.Item(1).AddCurrentValue pIdCliente
    Report.ReportTitle = "Cuenta Corriente n° " & Format(pIdCliente, "000000")

    Set f.miReport = Report
    f.Show vbModal
    
    crApp.CanClose
    Set crApp = Nothing
    Set Report = Nothing
    Set crCONN = Nothing
            
    If wRptPdf = True Then
       ShellExecute 0&, "open", LCase(vg.Path & "\pdf\CC" & Format(pIdCliente, "000000") & ".pdf"), "", "", vbNormalFocus
    Else
        f.Show vbModal
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

Private Sub cboBanco_Change()
    
    On Error Resume Next
    
    Dim LengthofText As Integer
    Dim StrToFind As String
    Dim Pos As Long
    Dim SelStart As Integer
    
    If BackSpacedPressed Then Exit Sub ' If backspace was pressed let the normal thing happen
    
    StrToFind = cboBanco.Text
    SelStart = cboBanco.SelStart ' Save the starting point
    Pos = SendMessage(cboBanco.Hwnd, CB_FINDSTRING, -1, ByVal StrToFind)
    If Pos <> CB_ERR Then ' If item found
        cboBanco.Text = cboBanco.List(Pos) ' Place the auto-completed text in the text area
        cboBanco.SelStart = SelStart ' Place the caret at the correct position
        LengthofText = Len(cboBanco.Text)
        If LengthofText > SelStart Then
            cboBanco.SelLength = LengthofText - SelStart 'Highlight the auto-completed text so it can be deleted quickly if wanted
        End If
    End If
    SendMessage cboBanco.Hwnd, CB_SHOWDROPDOWN, True, 0

End Sub

Private Sub cboBanco_KeyPress(KeyAscii As Integer)
    BackSpacedPressed = (KeyAscii = vbKeyBack)
End Sub

Private Sub cboBanco_Validate(Cancel As Boolean)
    Cancel = Not DatosValidos("cboBanco")
End Sub

Private Sub TotalRecibo()
  
  '-------- ErrorGuardian Begin --------
  Const PROCNAME_ As String = "TotalRecibo"
  On Error GoTo ErrorGuardianLocalHandler
  '-------- ErrorGuardian End ----------
    
  Dim Aux As Single
  
  If lvValores.ListItems.Count < 1 Then
    lblTotRecibo.Caption = Format(0, "fixed")
    Exit Sub
  End If
  
  Aux = 0
  For m.I = 1 To lvValores.ListItems.Count
    
    If lvValores.ListItems(m.I).SubItems(10) = 3 Or lvValores.ListItems(m.I).SubItems(10) = 4 Then
        Aux = Aux + (CSng(lvValores.ListItems(m.I).SubItems(1)) * CSng(lvValores.ListItems(m.I).SubItems(12)))
    Else
        Aux = Aux + CSng(lvValores.ListItems(m.I).SubItems(1)) ' simatoria de subtotal
    End If
    
  Next m.I
  
  lblTotRecibo.Caption = Format(Aux, "fixed")
  'lblTotRecibo.Left = frmrecibolista.Width - lblTotRecibo.Width - lblTotRecibo.Width - 300
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

Private Sub TotalADeducir()
  
  '-------- ErrorGuardian Begin --------
  Const PROCNAME_ As String = "TotalADeducir"
  On Error GoTo ErrorGuardianLocalHandler
  '-------- ErrorGuardian End ----------
    
  Dim AuxNotaCredito As Single
  Dim Aux3 As Single
  Dim Aux2 As Single
  Dim Aux As Single
  
  If lvADeducir.ListItems.Count < 1 Then
    lblTotaDedu.Caption = Format(0, "fixed")
    lblTotaDeduAlResto.Caption = Format(0, "fixed")
    Exit Sub
  End If
  
  AuxNotaCredito = 0
  Aux3 = 0
  Aux2 = 0
  Aux = 0
  
  lvADeducir.Sorted = True
  lvADeducir.SortKey = 3
  lvADeducir.SortOrder = lvwAscending
  
  For m.I = 1 To lvADeducir.ListItems.Count
    If lvADeducir.ListItems(m.I).SubItems(2) = 1 And lvADeducir.ListItems(m.I).SubItems(3) = 0 Then 'es un porcentaje yNO al resto
    
      Aux = Aux + (CSng(lvADeducir.ListItems(m.I).SubItems(1)) * CSng(Replace(lblTotApli.Caption, "$", ""))) / 100
      
    ElseIf lvADeducir.ListItems(m.I).SubItems(2) = 0 And lvADeducir.ListItems(m.I).SubItems(3) = 0 Then 'es un importe yNO al resto
    
      Aux = Aux + CSng(lvADeducir.ListItems(m.I).SubItems(1)) ' sumatoria de subtotal
      
      If Left(lvADeducir.ListItems(m.I).Text, 4) = "CRE-" Then AuxNotaCredito = AuxNotaCredito + CSng(lvADeducir.ListItems(m.I).SubItems(1)) ' sumatoria de subtotal
      
    End If
    
    If lvADeducir.ListItems(m.I).SubItems(2) = 1 And lvADeducir.ListItems(m.I).SubItems(3) = 1 Then 'es un porcentaje ySI al resto
        
        If CSng(lvADeducir.ListItems(m.I).SubItems(1)) > 0 Then
            Aux2 = ((CSng(Replace(lblTotApli.Caption, "$", "")) - CSng(Aux) - CSng(Aux2) + CSng(AuxNotaCredito)) * CSng(lvADeducir.ListItems(m.I).SubItems(1))) / 100
            Aux3 = Aux3 + Aux2
        End If
    
    End If
    
  Next m.I
    
  lblTotaDedu.Caption = Format(Aux, "fixed")
  lblTotaDeduAlResto.Caption = Format(Aux3, "fixed")
  
  'lblTotRecibo.Left = frmrecibolista.Width - lblTotRecibo.Width - lblTotRecibo.Width - 300
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

Private Sub TotalApli()
  
  '-------- ErrorGuardian Begin --------
  Const PROCNAME_ As String = "TotalApli"
  On Error GoTo ErrorGuardianLocalHandler
  '-------- ErrorGuardian End ----------
    
  Dim Aux As Single
  Dim AuxP As Single
  
  If lvAplicacion.ListItems.Count < 1 Then
    lblTotApli.Caption = Format(0, "fixed")
    lblTotPercep.Caption = Format(0, "fixed")
    Exit Sub
  End If
  
  Aux = 0
  AuxP = 0
  For m.I = 1 To lvAplicacion.ListItems.Count
    Aux = Aux + CSng(lvAplicacion.ListItems(m.I).SubItems(1)) ' sumatoria de subtotal
    AuxP = AuxP + CSng(lvAplicacion.ListItems(m.I).SubItems(2)) ' sumatoria de subtotal de Percepciones
  Next m.I
  
  lblTotApli.Caption = Format(Aux, "fixed")
  lblTotPercep.Caption = Format(AuxP, "fixed")
  
  'lblTotRecibo.Left = frmrecibolista.Width - lblTotRecibo.Width - lblTotRecibo.Width - 300
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

Private Sub VerDetalleRecibo()
  
  '-------- ErrorGuardian Begin --------
  Const PROCNAME_ As String = "VerDetalleRecibo"
  On Error GoTo ErrorGuardianLocalHandler
  '-------- ErrorGuardian End ----------
    
  Screen.MousePointer = vbDefault
  
  Dim s As String
  Dim Dif As Single
  
  Dif = ((CSng(Replace(lblTotApli.Caption, "$", "")) - CSng(Replace(lblTotaDedu.Caption, "$", "")) - CSng(Replace(lblTotaDeduAlResto.Caption, "$", ""))) + CSng(Replace(lblTotPercep.Caption, "$", ""))) - (CSng(Replace(lblTotRecibo.Caption, "$", "")))
  
  s = vbNullString
  
  s = s & "$ " & Format(lblTotApli.Caption, "###,##0.00") & "   Aplicación" & vbNewLine & vbNewLine
  s = s & "$ " & Format(lblTotaDedu.Caption, "###,##0.00") & "   A Deducir" & vbNewLine & vbNewLine
  s = s & "$ " & Format(lblTotaDeduAlResto.Caption, "###,##0.00") & "   A Deducir al Resto" & vbNewLine & vbNewLine
  s = s & "$ " & Format(lblTotPercep.Caption, "###,##0.00") & "   Percepciones/Débitos" & vbNewLine & vbNewLine
  s = s & "$ " & Format(lblTotRecibo.Caption, "###,##0.00") & "   Valores" & vbNewLine & vbNewLine & vbNewLine
  s = s & "$ " & Format(Dif, "###,##0.00") & "   Diferencia" & vbNewLine
  
'  s = s & "Aplicado ____________: " & Trim(lblTotApli.Caption) & vbNewLine & vbNewLine
'  s = s & "A Deducir ___________: " & Trim(lblTotaDedu.Caption) & vbNewLine & vbNewLine
'  s = s & "A Deducir al Resto ____: " & Trim(lblTotaDeduAlResto.Caption) & vbNewLine & vbNewLine
'  s = s & "Percepciones ________: " & Trim(lblTotPercep.Caption) & vbNewLine & vbNewLine
'  s = s & "Valores _____________: " & Trim(lblTotRecibo.Caption) & vbNewLine & vbNewLine & vbNewLine
'  s = s & "Diferencia ___________: " & "$ " & Trim(dif) & vbNewLine
  
'  s = s & "Aplicado ______: " & Format(lblTotApli.Caption, "     0.00") & vbCrLf & vbCrLf
'  s = s & "A Deducir _____: " & Format(lblTotaDedu.Caption, "     0.00") & vbCrLf & vbCrLf
'  s = s & "Percepciones __: " & Format(lblTotPercep.Caption, "     0.00") & vbCrLf & vbCrLf
'  s = s & "Valores _______: " & Format(lblTotRecibo.Caption, "     0.00") & vbCrLf & vbCrLf & vbCrLf
'  s = s & "Diferencia ____: " & Format(dif, "     0.00") & " $" & vbCrLf
  
'  s = s & "Aplicado _____: " & padLR(Format(lblTotApli.Caption, "     0.00"), 9, Space(1), 2) & vbCrLf & vbCrLf
'  s = s & "A Deducir ____: " & padLR(Format(lblTotaDedu.Caption, "     0.00"), 9, Space(1), 2) & vbCrLf & vbCrLf
'  s = s & "Percepciones _: " & padLR(Format(lblTotPercep.Caption, "     0.00"), 9, Space(1), 2) & vbCrLf & vbCrLf
'  s = s & "Valores ______: " & padLR(Format(lblTotRecibo.Caption, "     0.00"), 9, Space(1), 2) & vbCrLf & vbCrLf & vbCrLf
'  s = s & "Diferencia ___: " & Format(dif, "      0.00") & " $" & vbCrLf

             
  MsgBox s, vbInformation + vbMsgBoxRight, "Detalle del Recibo"
    
  'Dim f As frmMsgBox
  'Set f = New frmMsgBox
  
  'f.MiMsgBox temsgboxInf, "Detalle del Recibo", s
  
  'Set f = Nothing
  
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



Private Sub xFindCliente(ByVal pCampo1 As String, ByVal pCampo2 As String, ByVal pTitulo As String)
    
    Dim f As New frmFindAll
    
    selec.Campo1 = pCampo1
    selec.Campo2 = pCampo2
    selec.Titulo = pTitulo

    f.Show vbModal
    
    
    If CInt(selec.variable1) > 0 Then
        cboCliente.ListIndex = BuscarIndiceEnCombo(cboCliente, selec.variable1, False)
    Else
        cboCliente.ListIndex = 0
    End If
    cboCliente.SetFocus
    
    Set f = Nothing
    
End Sub

Private Function xFindTransporte() As String
    
    Dim f As New frmSelecTransporte
    
    selec.Campo1 = "id"
    selec.Campo2 = "nombre"
    selec.Titulo = "Lista de Transportes - Seleccione -"

    f.Show vbModal, Me
    DoEvents
        
    If CInt(selec.variable1) > 0 Then
        xFindTransporte = selec.variable2
    Else
        xFindTransporte = ""
    End If
       
    Set f = Nothing
    
'    Dim f As New frmSelecTransporte
'    f.Show vbModal, Me
'    DoEvents
'    xFindTransporte = f.Transporte
'    Set f = Nothing
        
End Function

Private Function fFindDeposito() As Byte
    
    Dim f As New frmSelecDeposito

    f.Show vbModal, Me
    DoEvents
    fFindDeposito = f.Deposito
    
    Set f = Nothing
        
End Function


'- eof --------------------------------------------------------------------------
