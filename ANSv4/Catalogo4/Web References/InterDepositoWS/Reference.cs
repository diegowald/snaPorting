﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.17929.
// 
#pragma warning disable 1591

namespace Catalogo.InterDepositoWS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="InterDepositoSoap", Namespace="http://tempuri.org/")]
    public partial class InterDeposito : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback EnviarInterDepositoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public InterDeposito() {
            this.Url = global::Catalogo.Properties.Settings.Default.Catalogo_InterDepositoWS_InterDeposito;
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
        public event EnviarInterDepositoCompletedEventHandler EnviarInterDepositoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnviarInterDeposito", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long EnviarInterDeposito(string MacAddress, string NroInterDeposito, string CodCliente, string Bco_Dep_Tipo, string Bco_Dep_Fecha, string Bco_Dep_Numero, string Bco_Dep_Monto, string Bco_Dep_Ch_Cantidad, string Bco_Dep_IdCta, string Observaciones, string Detalle) {
            object[] results = this.Invoke("EnviarInterDeposito", new object[] {
                        MacAddress,
                        NroInterDeposito,
                        CodCliente,
                        Bco_Dep_Tipo,
                        Bco_Dep_Fecha,
                        Bco_Dep_Numero,
                        Bco_Dep_Monto,
                        Bco_Dep_Ch_Cantidad,
                        Bco_Dep_IdCta,
                        Observaciones,
                        Detalle});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarInterDepositoAsync(string MacAddress, string NroInterDeposito, string CodCliente, string Bco_Dep_Tipo, string Bco_Dep_Fecha, string Bco_Dep_Numero, string Bco_Dep_Monto, string Bco_Dep_Ch_Cantidad, string Bco_Dep_IdCta, string Observaciones, string Detalle) {
            this.EnviarInterDepositoAsync(MacAddress, NroInterDeposito, CodCliente, Bco_Dep_Tipo, Bco_Dep_Fecha, Bco_Dep_Numero, Bco_Dep_Monto, Bco_Dep_Ch_Cantidad, Bco_Dep_IdCta, Observaciones, Detalle, null);
        }
        
        /// <remarks/>
        public void EnviarInterDepositoAsync(string MacAddress, string NroInterDeposito, string CodCliente, string Bco_Dep_Tipo, string Bco_Dep_Fecha, string Bco_Dep_Numero, string Bco_Dep_Monto, string Bco_Dep_Ch_Cantidad, string Bco_Dep_IdCta, string Observaciones, string Detalle, object userState) {
            if ((this.EnviarInterDepositoOperationCompleted == null)) {
                this.EnviarInterDepositoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarInterDepositoOperationCompleted);
            }
            this.InvokeAsync("EnviarInterDeposito", new object[] {
                        MacAddress,
                        NroInterDeposito,
                        CodCliente,
                        Bco_Dep_Tipo,
                        Bco_Dep_Fecha,
                        Bco_Dep_Numero,
                        Bco_Dep_Monto,
                        Bco_Dep_Ch_Cantidad,
                        Bco_Dep_IdCta,
                        Observaciones,
                        Detalle}, this.EnviarInterDepositoOperationCompleted, userState);
        }
        
        private void OnEnviarInterDepositoOperationCompleted(object arg) {
            if ((this.EnviarInterDepositoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarInterDepositoCompleted(this, new EnviarInterDepositoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void EnviarInterDepositoCompletedEventHandler(object sender, EnviarInterDepositoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarInterDepositoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarInterDepositoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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