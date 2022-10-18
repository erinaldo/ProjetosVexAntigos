﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Robo.Email.Notas.Solutions.Windows.Testes.br.com.vexlogistica.www2 {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsJosaparSoap", Namespace="http://tempuri.org/")]
    public partial class wsJosapar : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback EstoqueOperationCompleted;
        
        private System.Threading.SendOrPostCallback EstoqueValidadeDiasOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsJosapar() {
            this.Url = global::Robo.Email.Notas.Solutions.Windows.Testes.Properties.Settings.Default.Robo_Email_Notas_br_com_vexlogistica_www2_wsJosapar;
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
        public event EstoqueCompletedEventHandler EstoqueCompleted;
        
        /// <remarks/>
        public event EstoqueValidadeDiasCompletedEventHandler EstoqueValidadeDiasCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Estoque", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Estoque(string login, string senha) {
            object[] results = this.Invoke("Estoque", new object[] {
                        login,
                        senha});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void EstoqueAsync(string login, string senha) {
            this.EstoqueAsync(login, senha, null);
        }
        
        /// <remarks/>
        public void EstoqueAsync(string login, string senha, object userState) {
            if ((this.EstoqueOperationCompleted == null)) {
                this.EstoqueOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEstoqueOperationCompleted);
            }
            this.InvokeAsync("Estoque", new object[] {
                        login,
                        senha}, this.EstoqueOperationCompleted, userState);
        }
        
        private void OnEstoqueOperationCompleted(object arg) {
            if ((this.EstoqueCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EstoqueCompleted(this, new EstoqueCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EstoqueValidadeDias", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable EstoqueValidadeDias(string login, string senha, int QuantidadeDias) {
            object[] results = this.Invoke("EstoqueValidadeDias", new object[] {
                        login,
                        senha,
                        QuantidadeDias});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void EstoqueValidadeDiasAsync(string login, string senha, int QuantidadeDias) {
            this.EstoqueValidadeDiasAsync(login, senha, QuantidadeDias, null);
        }
        
        /// <remarks/>
        public void EstoqueValidadeDiasAsync(string login, string senha, int QuantidadeDias, object userState) {
            if ((this.EstoqueValidadeDiasOperationCompleted == null)) {
                this.EstoqueValidadeDiasOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEstoqueValidadeDiasOperationCompleted);
            }
            this.InvokeAsync("EstoqueValidadeDias", new object[] {
                        login,
                        senha,
                        QuantidadeDias}, this.EstoqueValidadeDiasOperationCompleted, userState);
        }
        
        private void OnEstoqueValidadeDiasOperationCompleted(object arg) {
            if ((this.EstoqueValidadeDiasCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EstoqueValidadeDiasCompleted(this, new EstoqueValidadeDiasCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void EstoqueCompletedEventHandler(object sender, EstoqueCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EstoqueCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EstoqueCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void EstoqueValidadeDiasCompletedEventHandler(object sender, EstoqueValidadeDiasCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EstoqueValidadeDiasCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EstoqueValidadeDiasCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591