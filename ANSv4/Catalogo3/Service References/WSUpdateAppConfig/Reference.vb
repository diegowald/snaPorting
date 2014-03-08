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

Imports System.Data

Namespace WSUpdateAppConfig
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="WSUpdateAppConfig.appConfigSoap")>  _
    Public Interface appConfigSoap
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ObtenerListaPrecioV2", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute()>  _
        Function ObtenerListaPrecioV2(ByVal MacAddress As String, ByVal Cuit As String, ByVal IDAns As String) As Byte
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/TenerQueEnviarInfo", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute()>  _
        Function TenerQueEnviarInfo(ByVal MacAddress As String) As Long
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/EnviarInfo", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute()>  _
        Function EnviarInfo( _
                    ByVal MacAddress As String,  _
                    ByVal Cuit As String,  _
                    ByVal RazonSocial As String,  _
                    ByVal ApellidoNombre As String,  _
                    ByVal Domicilio As String,  _
                    ByVal Telefono As String,  _
                    ByVal Ciudad As String,  _
                    ByVal Email As String,  _
                    ByVal IDAns As String,  _
                    ByVal AppCaduca As String,  _
                    ByVal dbCaduca As String,  _
                    ByVal Pin As String,  _
                    ByVal FechaUltimoAcceso As String,  _
                    ByVal Mensaje As String,  _
                    ByVal EnviarAuditoria As String,  _
                    ByVal Url As String,  _
                    ByVal Modem As String,  _
                    ByVal Version As String,  _
                    ByVal Build As String,  _
                    ByVal ListaPrecio As String,  _
                    ByVal Auditor As String) As Long
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ObtenerInfoDS", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute()>  _
        Function ObtenerInfoDS(ByVal MacAddress As String) As System.Data.DataSet
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ObtenerInfo", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute()>  _
        Function ObtenerInfo(ByVal MacAddress As String) As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ObtenerComandosDS", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute()>  _
        Function ObtenerComandosDS(ByVal MacAddress As String) As System.Data.DataSet
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ObtenerComandos", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute()>  _
        Function ObtenerComandos(ByVal MacAddress As String) As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/SincronizacionAppConfigCompletada", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute()>  _
        Sub SincronizacionAppConfigCompletada(ByVal MacAddress As String)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface appConfigSoapChannel
        Inherits WSUpdateAppConfig.appConfigSoap, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class appConfigSoapClient
        Inherits System.ServiceModel.ClientBase(Of WSUpdateAppConfig.appConfigSoap)
        Implements WSUpdateAppConfig.appConfigSoap
        
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
        
        Public Function ObtenerListaPrecioV2(ByVal MacAddress As String, ByVal Cuit As String, ByVal IDAns As String) As Byte Implements WSUpdateAppConfig.appConfigSoap.ObtenerListaPrecioV2
            Return MyBase.Channel.ObtenerListaPrecioV2(MacAddress, Cuit, IDAns)
        End Function
        
        Public Function TenerQueEnviarInfo(ByVal MacAddress As String) As Long Implements WSUpdateAppConfig.appConfigSoap.TenerQueEnviarInfo
            Return MyBase.Channel.TenerQueEnviarInfo(MacAddress)
        End Function
        
        Public Function EnviarInfo( _
                    ByVal MacAddress As String,  _
                    ByVal Cuit As String,  _
                    ByVal RazonSocial As String,  _
                    ByVal ApellidoNombre As String,  _
                    ByVal Domicilio As String,  _
                    ByVal Telefono As String,  _
                    ByVal Ciudad As String,  _
                    ByVal Email As String,  _
                    ByVal IDAns As String,  _
                    ByVal AppCaduca As String,  _
                    ByVal dbCaduca As String,  _
                    ByVal Pin As String,  _
                    ByVal FechaUltimoAcceso As String,  _
                    ByVal Mensaje As String,  _
                    ByVal EnviarAuditoria As String,  _
                    ByVal Url As String,  _
                    ByVal Modem As String,  _
                    ByVal Version As String,  _
                    ByVal Build As String,  _
                    ByVal ListaPrecio As String,  _
                    ByVal Auditor As String) As Long Implements WSUpdateAppConfig.appConfigSoap.EnviarInfo
            Return MyBase.Channel.EnviarInfo(MacAddress, Cuit, RazonSocial, ApellidoNombre, Domicilio, Telefono, Ciudad, Email, IDAns, AppCaduca, dbCaduca, Pin, FechaUltimoAcceso, Mensaje, EnviarAuditoria, Url, Modem, Version, Build, ListaPrecio, Auditor)
        End Function
        
        Public Function ObtenerInfoDS(ByVal MacAddress As String) As System.Data.DataSet Implements WSUpdateAppConfig.appConfigSoap.ObtenerInfoDS
            Return MyBase.Channel.ObtenerInfoDS(MacAddress)
        End Function
        
        Public Function ObtenerInfo(ByVal MacAddress As String) As String Implements WSUpdateAppConfig.appConfigSoap.ObtenerInfo
            Return MyBase.Channel.ObtenerInfo(MacAddress)
        End Function
        
        Public Function ObtenerComandosDS(ByVal MacAddress As String) As System.Data.DataSet Implements WSUpdateAppConfig.appConfigSoap.ObtenerComandosDS
            Return MyBase.Channel.ObtenerComandosDS(MacAddress)
        End Function
        
        Public Function ObtenerComandos(ByVal MacAddress As String) As String Implements WSUpdateAppConfig.appConfigSoap.ObtenerComandos
            Return MyBase.Channel.ObtenerComandos(MacAddress)
        End Function
        
        Public Sub SincronizacionAppConfigCompletada(ByVal MacAddress As String) Implements WSUpdateAppConfig.appConfigSoap.SincronizacionAppConfigCompletada
            MyBase.Channel.SincronizacionAppConfigCompletada(MacAddress)
        End Sub
    End Class
End Namespace
