﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión de runtime:4.0.30319.18444
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace WSVerExistencia
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="WSVerExistencia.VerExistenciaSoap")>  _
    Public Interface VerExistenciaSoap
        
        'CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento MacAddress del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ObtenerExistencia", ReplyAction:="*")>  _
        Function ObtenerExistencia(ByVal request As WSVerExistencia.ObtenerExistenciaRequest) As WSVerExistencia.ObtenerExistenciaResponse
        
        'CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento MacAddress del espacio de nombres http://tempuri.org/ no está marcado para aceptar valores nil.
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ObtenerZonaCliente", ReplyAction:="*")>  _
        Function ObtenerZonaCliente(ByVal request As WSVerExistencia.ObtenerZonaClienteRequest) As WSVerExistencia.ObtenerZonaClienteResponse
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ObtenerExistenciaRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ObtenerExistencia", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As WSVerExistencia.ObtenerExistenciaRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As WSVerExistencia.ObtenerExistenciaRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class ObtenerExistenciaRequestBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public MacAddress As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=1)>  _
        Public IDAns As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=2)>  _
        Public IdProducto As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal MacAddress As String, ByVal IDAns As String, ByVal IdProducto As String)
            MyBase.New
            Me.MacAddress = MacAddress
            Me.IDAns = IDAns
            Me.IdProducto = IdProducto
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ObtenerExistenciaResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ObtenerExistenciaResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As WSVerExistencia.ObtenerExistenciaResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As WSVerExistencia.ObtenerExistenciaResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class ObtenerExistenciaResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public ObtenerExistenciaResult As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal ObtenerExistenciaResult As String)
            MyBase.New
            Me.ObtenerExistenciaResult = ObtenerExistenciaResult
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ObtenerZonaClienteRequest
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ObtenerZonaCliente", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As WSVerExistencia.ObtenerZonaClienteRequestBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As WSVerExistencia.ObtenerZonaClienteRequestBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class ObtenerZonaClienteRequestBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public MacAddress As String
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=1)>  _
        Public IDAns As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal MacAddress As String, ByVal IDAns As String)
            MyBase.New
            Me.MacAddress = MacAddress
            Me.IDAns = IDAns
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.ServiceModel.MessageContractAttribute(IsWrapped:=false)>  _
    Partial Public Class ObtenerZonaClienteResponse
        
        <System.ServiceModel.MessageBodyMemberAttribute(Name:="ObtenerZonaClienteResponse", [Namespace]:="http://tempuri.org/", Order:=0)>  _
        Public Body As WSVerExistencia.ObtenerZonaClienteResponseBody
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal Body As WSVerExistencia.ObtenerZonaClienteResponseBody)
            MyBase.New
            Me.Body = Body
        End Sub
    End Class
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced),  _
     System.Runtime.Serialization.DataContractAttribute([Namespace]:="http://tempuri.org/")>  _
    Partial Public Class ObtenerZonaClienteResponseBody
        
        <System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue:=false, Order:=0)>  _
        Public ObtenerZonaClienteResult As String
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal ObtenerZonaClienteResult As String)
            MyBase.New
            Me.ObtenerZonaClienteResult = ObtenerZonaClienteResult
        End Sub
    End Class
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface VerExistenciaSoapChannel
        Inherits WSVerExistencia.VerExistenciaSoap, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class VerExistenciaSoapClient
        Inherits System.ServiceModel.ClientBase(Of WSVerExistencia.VerExistenciaSoap)
        Implements WSVerExistencia.VerExistenciaSoap
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function WSVerExistencia_VerExistenciaSoap_ObtenerExistencia(ByVal request As WSVerExistencia.ObtenerExistenciaRequest) As WSVerExistencia.ObtenerExistenciaResponse Implements WSVerExistencia.VerExistenciaSoap.ObtenerExistencia
            Return MyBase.Channel.ObtenerExistencia(request)
        End Function
        
        Public Function ObtenerExistencia(ByVal MacAddress As String, ByVal IDAns As String, ByVal IdProducto As String) As String
            Dim inValue As WSVerExistencia.ObtenerExistenciaRequest = New WSVerExistencia.ObtenerExistenciaRequest()
            inValue.Body = New WSVerExistencia.ObtenerExistenciaRequestBody()
            inValue.Body.MacAddress = MacAddress
            inValue.Body.IDAns = IDAns
            inValue.Body.IdProducto = IdProducto
            Dim retVal As WSVerExistencia.ObtenerExistenciaResponse = CType(Me,WSVerExistencia.VerExistenciaSoap).ObtenerExistencia(inValue)
            Return retVal.Body.ObtenerExistenciaResult
        End Function
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Function WSVerExistencia_VerExistenciaSoap_ObtenerZonaCliente(ByVal request As WSVerExistencia.ObtenerZonaClienteRequest) As WSVerExistencia.ObtenerZonaClienteResponse Implements WSVerExistencia.VerExistenciaSoap.ObtenerZonaCliente
            Return MyBase.Channel.ObtenerZonaCliente(request)
        End Function
        
        Public Function ObtenerZonaCliente(ByVal MacAddress As String, ByVal IDAns As String) As String
            Dim inValue As WSVerExistencia.ObtenerZonaClienteRequest = New WSVerExistencia.ObtenerZonaClienteRequest()
            inValue.Body = New WSVerExistencia.ObtenerZonaClienteRequestBody()
            inValue.Body.MacAddress = MacAddress
            inValue.Body.IDAns = IDAns
            Dim retVal As WSVerExistencia.ObtenerZonaClienteResponse = CType(Me,WSVerExistencia.VerExistenciaSoap).ObtenerZonaCliente(inValue)
            Return retVal.Body.ObtenerZonaClienteResult
        End Function
    End Class
End Namespace
