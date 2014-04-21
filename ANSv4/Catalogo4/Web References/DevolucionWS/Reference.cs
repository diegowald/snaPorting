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

namespace Catalogo.DevolucionWS {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="DevolucionSoap", Namespace="http://tempuri.org/")]
    public partial class Devolucion : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback EnviarDevolucionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Devolucion() {
            this.Url = global::Catalogo.Properties.Settings.Default.Catalogo_DevolucionWS_Devolucion;
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
        public event EnviarDevolucionCompletedEventHandler EnviarDevolucionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnviarDevolucion", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long EnviarDevolucion(string MacAddress, string NroDevolucion, string CodCliente, string Fecha, string Observaciones, string Detalle) {
            object[] results = this.Invoke("EnviarDevolucion", new object[] {
                        MacAddress,
                        NroDevolucion,
                        CodCliente,
                        Fecha,
                        Observaciones,
                        Detalle});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarDevolucionAsync(string MacAddress, string NroDevolucion, string CodCliente, string Fecha, string Observaciones, string Detalle) {
            this.EnviarDevolucionAsync(MacAddress, NroDevolucion, CodCliente, Fecha, Observaciones, Detalle, null);
        }
        
        /// <remarks/>
        public void EnviarDevolucionAsync(string MacAddress, string NroDevolucion, string CodCliente, string Fecha, string Observaciones, string Detalle, object userState) {
            if ((this.EnviarDevolucionOperationCompleted == null)) {
                this.EnviarDevolucionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarDevolucionOperationCompleted);
            }
            this.InvokeAsync("EnviarDevolucion", new object[] {
                        MacAddress,
                        NroDevolucion,
                        CodCliente,
                        Fecha,
                        Observaciones,
                        Detalle}, this.EnviarDevolucionOperationCompleted, userState);
        }
        
        private void OnEnviarDevolucionOperationCompleted(object arg) {
            if ((this.EnviarDevolucionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarDevolucionCompleted(this, new EnviarDevolucionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void EnviarDevolucionCompletedEventHandler(object sender, EnviarDevolucionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarDevolucionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarDevolucionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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