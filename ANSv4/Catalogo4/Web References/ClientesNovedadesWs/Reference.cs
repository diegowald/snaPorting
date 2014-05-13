﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.18444.
// 
#pragma warning disable 1591

namespace Catalogo.ClientesNovedadesWs {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ClientesNovedadesSoap", Namespace="http://tempuri.org/")]
    public partial class ClientesNovedades : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback CallClientesNovedadesOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ClientesNovedades() {
            this.Url = global::Catalogo.Properties.Settings.Default.Catalogo_ClientesNovedadesWs_ClientesNovedades;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event CallClientesNovedadesCompletedEventHandler CallClientesNovedadesCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ClientesNovedades", RequestElementName="ClientesNovedades", RequestNamespace="http://tempuri.org/", ResponseElementName="ClientesNovedadesResponse", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("ClientesNovedadesResult")]
        public long CallClientesNovedades(string MacAddress, string Fechas, string Novedades, string Clientes, string Viajantes, string Tipos) {
            object[] results = this.Invoke("CallClientesNovedades", new object[] {
                        MacAddress,
                        Fechas,
                        Novedades,
                        Clientes,
                        Viajantes,
                        Tipos});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        internal void CallClientesNovedadesAsync(string MacAddress, string Fechas, string Novedades, string Clientes, string Viajantes, string Tipos) {
            this.CallClientesNovedadesAsync(MacAddress, Fechas, Novedades, Clientes, Viajantes, Tipos, null);
        }
        
        /// <remarks/>
        internal void CallClientesNovedadesAsync(string MacAddress, string Fechas, string Novedades, string Clientes, string Viajantes, string Tipos, object userState) {
            if ((this.CallClientesNovedadesOperationCompleted == null)) {
                this.CallClientesNovedadesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCallClientesNovedadesOperationCompleted);
            }
            this.InvokeAsync("CallClientesNovedades", new object[] {
                        MacAddress,
                        Fechas,
                        Novedades,
                        Clientes,
                        Viajantes,
                        Tipos}, this.CallClientesNovedadesOperationCompleted, userState);
        }
        
        private void OnCallClientesNovedadesOperationCompleted(object arg) {
            if ((this.CallClientesNovedadesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CallClientesNovedadesCompleted(this, new CallClientesNovedadesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void CallClientesNovedadesCompletedEventHandler(object sender, CallClientesNovedadesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CallClientesNovedadesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CallClientesNovedadesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591