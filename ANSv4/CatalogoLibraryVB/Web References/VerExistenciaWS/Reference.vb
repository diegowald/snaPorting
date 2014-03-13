﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.17929
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.17929.
'
Namespace VerExistenciaWS
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="VerExistenciaSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class VerExistencia
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private ObtenerExistenciaOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ObtenerZonaClienteOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.CatalogoLibraryVB.My.MySettings.Default.CatalogoLibraryVB_VerExistencia_VerExistencia
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event ObtenerExistenciaCompleted As ObtenerExistenciaCompletedEventHandler
        
        '''<remarks/>
        Public Event ObtenerZonaClienteCompleted As ObtenerZonaClienteCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObtenerExistencia", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ObtenerExistencia(ByVal MacAddress As String, ByVal IDAns As String, ByVal IdProducto As String) As String
            Dim results() As Object = Me.Invoke("ObtenerExistencia", New Object() {MacAddress, IDAns, IdProducto})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ObtenerExistenciaAsync(ByVal MacAddress As String, ByVal IDAns As String, ByVal IdProducto As String)
            Me.ObtenerExistenciaAsync(MacAddress, IDAns, IdProducto, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ObtenerExistenciaAsync(ByVal MacAddress As String, ByVal IDAns As String, ByVal IdProducto As String, ByVal userState As Object)
            If (Me.ObtenerExistenciaOperationCompleted Is Nothing) Then
                Me.ObtenerExistenciaOperationCompleted = AddressOf Me.OnObtenerExistenciaOperationCompleted
            End If
            Me.InvokeAsync("ObtenerExistencia", New Object() {MacAddress, IDAns, IdProducto}, Me.ObtenerExistenciaOperationCompleted, userState)
        End Sub
        
        Private Sub OnObtenerExistenciaOperationCompleted(ByVal arg As Object)
            If (Not (Me.ObtenerExistenciaCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObtenerExistenciaCompleted(Me, New ObtenerExistenciaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObtenerZonaCliente", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ObtenerZonaCliente(ByVal MacAddress As String, ByVal IDAns As String) As String
            Dim results() As Object = Me.Invoke("ObtenerZonaCliente", New Object() {MacAddress, IDAns})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ObtenerZonaClienteAsync(ByVal MacAddress As String, ByVal IDAns As String)
            Me.ObtenerZonaClienteAsync(MacAddress, IDAns, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ObtenerZonaClienteAsync(ByVal MacAddress As String, ByVal IDAns As String, ByVal userState As Object)
            If (Me.ObtenerZonaClienteOperationCompleted Is Nothing) Then
                Me.ObtenerZonaClienteOperationCompleted = AddressOf Me.OnObtenerZonaClienteOperationCompleted
            End If
            Me.InvokeAsync("ObtenerZonaCliente", New Object() {MacAddress, IDAns}, Me.ObtenerZonaClienteOperationCompleted, userState)
        End Sub
        
        Private Sub OnObtenerZonaClienteOperationCompleted(ByVal arg As Object)
            If (Not (Me.ObtenerZonaClienteCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObtenerZonaClienteCompleted(Me, New ObtenerZonaClienteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")>  _
    Public Delegate Sub ObtenerExistenciaCompletedEventHandler(ByVal sender As Object, ByVal e As ObtenerExistenciaCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ObtenerExistenciaCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")>  _
    Public Delegate Sub ObtenerZonaClienteCompletedEventHandler(ByVal sender As Object, ByVal e As ObtenerZonaClienteCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ObtenerZonaClienteCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
End Namespace
