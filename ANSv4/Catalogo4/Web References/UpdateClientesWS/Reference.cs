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

namespace Catalogo.UpdateClientesWS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="UpdateClientesSoap", Namespace="http://tempuri.org/")]
    public partial class UpdateClientes : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetTodosLosClientes_CantidadOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTodosLosClientes_DatosOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTodosLosClientes_Datos_RegistrosOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTodasLasCtasCtes_CantidadOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTodasLasCtasCtes_DatosOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTodasLasCtasCtes_Datos_RegistrosOperationCompleted;
        
        private System.Threading.SendOrPostCallback SincronizacionClientesCompletadaOperationCompleted;
        
        private System.Threading.SendOrPostCallback SincronizacionCtasCtesCompletadaOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public UpdateClientes() {
            this.Url = global::Catalogo.Properties.Settings.Default.Catalogo_UpdateClientesWS_UpdateClientes;
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
        public event GetTodosLosClientes_CantidadCompletedEventHandler GetTodosLosClientes_CantidadCompleted;
        
        /// <remarks/>
        public event GetTodosLosClientes_DatosCompletedEventHandler GetTodosLosClientes_DatosCompleted;
        
        /// <remarks/>
        public event GetTodosLosClientes_Datos_RegistrosCompletedEventHandler GetTodosLosClientes_Datos_RegistrosCompleted;
        
        /// <remarks/>
        public event GetTodasLasCtasCtes_CantidadCompletedEventHandler GetTodasLasCtasCtes_CantidadCompleted;
        
        /// <remarks/>
        public event GetTodasLasCtasCtes_DatosCompletedEventHandler GetTodasLasCtasCtes_DatosCompleted;
        
        /// <remarks/>
        public event GetTodasLasCtasCtes_Datos_RegistrosCompletedEventHandler GetTodasLasCtasCtes_Datos_RegistrosCompleted;
        
        /// <remarks/>
        public event SincronizacionClientesCompletadaCompletedEventHandler SincronizacionClientesCompletadaCompleted;
        
        /// <remarks/>
        public event SincronizacionCtasCtesCompletadaCompletedEventHandler SincronizacionCtasCtesCompletadaCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTodosLosClientes_Cantidad", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long GetTodosLosClientes_Cantidad(string MacAddress) {
            object[] results = this.Invoke("GetTodosLosClientes_Cantidad", new object[] {
                        MacAddress});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void GetTodosLosClientes_CantidadAsync(string MacAddress) {
            this.GetTodosLosClientes_CantidadAsync(MacAddress, null);
        }
        
        /// <remarks/>
        public void GetTodosLosClientes_CantidadAsync(string MacAddress, object userState) {
            if ((this.GetTodosLosClientes_CantidadOperationCompleted == null)) {
                this.GetTodosLosClientes_CantidadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTodosLosClientes_CantidadOperationCompleted);
            }
            this.InvokeAsync("GetTodosLosClientes_Cantidad", new object[] {
                        MacAddress}, this.GetTodosLosClientes_CantidadOperationCompleted, userState);
        }
        
        private void OnGetTodosLosClientes_CantidadOperationCompleted(object arg) {
            if ((this.GetTodosLosClientes_CantidadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTodosLosClientes_CantidadCompleted(this, new GetTodosLosClientes_CantidadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTodosLosClientes_Datos", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetTodosLosClientes_Datos(string MacAddress, long LastID) {
            object[] results = this.Invoke("GetTodosLosClientes_Datos", new object[] {
                        MacAddress,
                        LastID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetTodosLosClientes_DatosAsync(string MacAddress, long LastID) {
            this.GetTodosLosClientes_DatosAsync(MacAddress, LastID, null);
        }
        
        /// <remarks/>
        public void GetTodosLosClientes_DatosAsync(string MacAddress, long LastID, object userState) {
            if ((this.GetTodosLosClientes_DatosOperationCompleted == null)) {
                this.GetTodosLosClientes_DatosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTodosLosClientes_DatosOperationCompleted);
            }
            this.InvokeAsync("GetTodosLosClientes_Datos", new object[] {
                        MacAddress,
                        LastID}, this.GetTodosLosClientes_DatosOperationCompleted, userState);
        }
        
        private void OnGetTodosLosClientes_DatosOperationCompleted(object arg) {
            if ((this.GetTodosLosClientes_DatosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTodosLosClientes_DatosCompleted(this, new GetTodosLosClientes_DatosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTodosLosClientes_Datos_Registros", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetTodosLosClientes_Datos_Registros(string MacAddress, long LastID) {
            object[] results = this.Invoke("GetTodosLosClientes_Datos_Registros", new object[] {
                        MacAddress,
                        LastID});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetTodosLosClientes_Datos_RegistrosAsync(string MacAddress, long LastID) {
            this.GetTodosLosClientes_Datos_RegistrosAsync(MacAddress, LastID, null);
        }
        
        /// <remarks/>
        public void GetTodosLosClientes_Datos_RegistrosAsync(string MacAddress, long LastID, object userState) {
            if ((this.GetTodosLosClientes_Datos_RegistrosOperationCompleted == null)) {
                this.GetTodosLosClientes_Datos_RegistrosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTodosLosClientes_Datos_RegistrosOperationCompleted);
            }
            this.InvokeAsync("GetTodosLosClientes_Datos_Registros", new object[] {
                        MacAddress,
                        LastID}, this.GetTodosLosClientes_Datos_RegistrosOperationCompleted, userState);
        }
        
        private void OnGetTodosLosClientes_Datos_RegistrosOperationCompleted(object arg) {
            if ((this.GetTodosLosClientes_Datos_RegistrosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTodosLosClientes_Datos_RegistrosCompleted(this, new GetTodosLosClientes_Datos_RegistrosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTodasLasCtasCtes_Cantidad", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long GetTodasLasCtasCtes_Cantidad(string MacAddress) {
            object[] results = this.Invoke("GetTodasLasCtasCtes_Cantidad", new object[] {
                        MacAddress});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void GetTodasLasCtasCtes_CantidadAsync(string MacAddress) {
            this.GetTodasLasCtasCtes_CantidadAsync(MacAddress, null);
        }
        
        /// <remarks/>
        public void GetTodasLasCtasCtes_CantidadAsync(string MacAddress, object userState) {
            if ((this.GetTodasLasCtasCtes_CantidadOperationCompleted == null)) {
                this.GetTodasLasCtasCtes_CantidadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTodasLasCtasCtes_CantidadOperationCompleted);
            }
            this.InvokeAsync("GetTodasLasCtasCtes_Cantidad", new object[] {
                        MacAddress}, this.GetTodasLasCtasCtes_CantidadOperationCompleted, userState);
        }
        
        private void OnGetTodasLasCtasCtes_CantidadOperationCompleted(object arg) {
            if ((this.GetTodasLasCtasCtes_CantidadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTodasLasCtasCtes_CantidadCompleted(this, new GetTodasLasCtasCtes_CantidadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTodasLasCtasCtes_Datos", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetTodasLasCtasCtes_Datos(string MacAddress, long LastID) {
            object[] results = this.Invoke("GetTodasLasCtasCtes_Datos", new object[] {
                        MacAddress,
                        LastID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetTodasLasCtasCtes_DatosAsync(string MacAddress, long LastID) {
            this.GetTodasLasCtasCtes_DatosAsync(MacAddress, LastID, null);
        }
        
        /// <remarks/>
        public void GetTodasLasCtasCtes_DatosAsync(string MacAddress, long LastID, object userState) {
            if ((this.GetTodasLasCtasCtes_DatosOperationCompleted == null)) {
                this.GetTodasLasCtasCtes_DatosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTodasLasCtasCtes_DatosOperationCompleted);
            }
            this.InvokeAsync("GetTodasLasCtasCtes_Datos", new object[] {
                        MacAddress,
                        LastID}, this.GetTodasLasCtasCtes_DatosOperationCompleted, userState);
        }
        
        private void OnGetTodasLasCtasCtes_DatosOperationCompleted(object arg) {
            if ((this.GetTodasLasCtasCtes_DatosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTodasLasCtasCtes_DatosCompleted(this, new GetTodasLasCtasCtes_DatosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTodasLasCtasCtes_Datos_Registros", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetTodasLasCtasCtes_Datos_Registros(string MacAddress, long LastID) {
            object[] results = this.Invoke("GetTodasLasCtasCtes_Datos_Registros", new object[] {
                        MacAddress,
                        LastID});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetTodasLasCtasCtes_Datos_RegistrosAsync(string MacAddress, long LastID) {
            this.GetTodasLasCtasCtes_Datos_RegistrosAsync(MacAddress, LastID, null);
        }
        
        /// <remarks/>
        public void GetTodasLasCtasCtes_Datos_RegistrosAsync(string MacAddress, long LastID, object userState) {
            if ((this.GetTodasLasCtasCtes_Datos_RegistrosOperationCompleted == null)) {
                this.GetTodasLasCtasCtes_Datos_RegistrosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTodasLasCtasCtes_Datos_RegistrosOperationCompleted);
            }
            this.InvokeAsync("GetTodasLasCtasCtes_Datos_Registros", new object[] {
                        MacAddress,
                        LastID}, this.GetTodasLasCtasCtes_Datos_RegistrosOperationCompleted, userState);
        }
        
        private void OnGetTodasLasCtasCtes_Datos_RegistrosOperationCompleted(object arg) {
            if ((this.GetTodasLasCtasCtes_Datos_RegistrosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTodasLasCtasCtes_Datos_RegistrosCompleted(this, new GetTodasLasCtasCtes_Datos_RegistrosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SincronizacionClientesCompletada", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SincronizacionClientesCompletada(string MacAddress) {
            this.Invoke("SincronizacionClientesCompletada", new object[] {
                        MacAddress});
        }
        
        /// <remarks/>
        public void SincronizacionClientesCompletadaAsync(string MacAddress) {
            this.SincronizacionClientesCompletadaAsync(MacAddress, null);
        }
        
        /// <remarks/>
        public void SincronizacionClientesCompletadaAsync(string MacAddress, object userState) {
            if ((this.SincronizacionClientesCompletadaOperationCompleted == null)) {
                this.SincronizacionClientesCompletadaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSincronizacionClientesCompletadaOperationCompleted);
            }
            this.InvokeAsync("SincronizacionClientesCompletada", new object[] {
                        MacAddress}, this.SincronizacionClientesCompletadaOperationCompleted, userState);
        }
        
        private void OnSincronizacionClientesCompletadaOperationCompleted(object arg) {
            if ((this.SincronizacionClientesCompletadaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SincronizacionClientesCompletadaCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SincronizacionCtasCtesCompletada", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SincronizacionCtasCtesCompletada(string MacAddress) {
            this.Invoke("SincronizacionCtasCtesCompletada", new object[] {
                        MacAddress});
        }
        
        /// <remarks/>
        public void SincronizacionCtasCtesCompletadaAsync(string MacAddress) {
            this.SincronizacionCtasCtesCompletadaAsync(MacAddress, null);
        }
        
        /// <remarks/>
        public void SincronizacionCtasCtesCompletadaAsync(string MacAddress, object userState) {
            if ((this.SincronizacionCtasCtesCompletadaOperationCompleted == null)) {
                this.SincronizacionCtasCtesCompletadaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSincronizacionCtasCtesCompletadaOperationCompleted);
            }
            this.InvokeAsync("SincronizacionCtasCtesCompletada", new object[] {
                        MacAddress}, this.SincronizacionCtasCtesCompletadaOperationCompleted, userState);
        }
        
        private void OnSincronizacionCtasCtesCompletadaOperationCompleted(object arg) {
            if ((this.SincronizacionCtasCtesCompletadaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SincronizacionCtasCtesCompletadaCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void GetTodosLosClientes_CantidadCompletedEventHandler(object sender, GetTodosLosClientes_CantidadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTodosLosClientes_CantidadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTodosLosClientes_CantidadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetTodosLosClientes_DatosCompletedEventHandler(object sender, GetTodosLosClientes_DatosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTodosLosClientes_DatosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTodosLosClientes_DatosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetTodosLosClientes_Datos_RegistrosCompletedEventHandler(object sender, GetTodosLosClientes_Datos_RegistrosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTodosLosClientes_Datos_RegistrosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTodosLosClientes_Datos_RegistrosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetTodasLasCtasCtes_CantidadCompletedEventHandler(object sender, GetTodasLasCtasCtes_CantidadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTodasLasCtasCtes_CantidadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTodasLasCtasCtes_CantidadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetTodasLasCtasCtes_DatosCompletedEventHandler(object sender, GetTodasLasCtasCtes_DatosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTodasLasCtasCtes_DatosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTodasLasCtasCtes_DatosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetTodasLasCtasCtes_Datos_RegistrosCompletedEventHandler(object sender, GetTodasLasCtasCtes_Datos_RegistrosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTodasLasCtasCtes_Datos_RegistrosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTodasLasCtasCtes_Datos_RegistrosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void SincronizacionClientesCompletadaCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void SincronizacionCtasCtesCompletadaCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591