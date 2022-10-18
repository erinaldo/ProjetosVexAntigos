﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace ServicosWEB.br.com.roge.wslogos02 {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WSLogosSoap", Namespace="urn:rogeXlogos")]
    public partial class WSLogos : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GravarConferenciaOperationCompleted;
        
        private System.Threading.SendOrPostCallback RetornarDadosCDEANOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WSLogos() {
            this.Url = global::ServicosWEB.Properties.Settings.Default.ServicosWEB_br_com_roge_wslogos02_WSLogos;
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
        public event GravarConferenciaCompletedEventHandler GravarConferenciaCompleted;
        
        /// <remarks/>
        public event RetornarDadosCDEANCompletedEventHandler RetornarDadosCDEANCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:rogeXlogos/WSRoge.WSLogos.GravarConferencia", RequestNamespace="urn:rogeXlogos", ResponseNamespace="urn:rogeXlogos", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GravarConferencia(ConferenciaNotaFiscal objConferencia) {
            object[] results = this.Invoke("GravarConferencia", new object[] {
                        objConferencia});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GravarConferenciaAsync(ConferenciaNotaFiscal objConferencia) {
            this.GravarConferenciaAsync(objConferencia, null);
        }
        
        /// <remarks/>
        public void GravarConferenciaAsync(ConferenciaNotaFiscal objConferencia, object userState) {
            if ((this.GravarConferenciaOperationCompleted == null)) {
                this.GravarConferenciaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGravarConferenciaOperationCompleted);
            }
            this.InvokeAsync("GravarConferencia", new object[] {
                        objConferencia}, this.GravarConferenciaOperationCompleted, userState);
        }
        
        private void OnGravarConferenciaOperationCompleted(object arg) {
            if ((this.GravarConferenciaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GravarConferenciaCompleted(this, new GravarConferenciaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:rogeXlogos/WSRoge.WSLogos.RetornarDadosCDEAN", RequestNamespace="urn:rogeXlogos", ResponseNamespace="urn:rogeXlogos", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet RetornarDadosCDEAN() {
            object[] results = this.Invoke("RetornarDadosCDEAN", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void RetornarDadosCDEANAsync() {
            this.RetornarDadosCDEANAsync(null);
        }
        
        /// <remarks/>
        public void RetornarDadosCDEANAsync(object userState) {
            if ((this.RetornarDadosCDEANOperationCompleted == null)) {
                this.RetornarDadosCDEANOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRetornarDadosCDEANOperationCompleted);
            }
            this.InvokeAsync("RetornarDadosCDEAN", new object[0], this.RetornarDadosCDEANOperationCompleted, userState);
        }
        
        private void OnRetornarDadosCDEANOperationCompleted(object arg) {
            if ((this.RetornarDadosCDEANCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RetornarDadosCDEANCompleted(this, new RetornarDadosCDEANCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:rogeXlogos")]
    public partial class ConferenciaNotaFiscal {
        
        private ConferenciaNotaFiscalProduto[] produtosField;
        
        private ConferenciaNotaFiscalEanVolume[] eanvolumesField;
        
        private string chaveeletronicaField;
        
        private System.DateTime datahorainsercaoField;
        
        private bool datahorainsercaoFieldSpecified;
        
        private string datahoraField;
        
        private string usuarioField;
        
        private byte[] statusField;
        
        /// <remarks/>
        public ConferenciaNotaFiscalProduto[] produtos {
            get {
                return this.produtosField;
            }
            set {
                this.produtosField = value;
            }
        }
        
        /// <remarks/>
        public ConferenciaNotaFiscalEanVolume[] eanvolumes {
            get {
                return this.eanvolumesField;
            }
            set {
                this.eanvolumesField = value;
            }
        }
        
        /// <remarks/>
        public string chaveeletronica {
            get {
                return this.chaveeletronicaField;
            }
            set {
                this.chaveeletronicaField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime datahorainsercao {
            get {
                return this.datahorainsercaoField;
            }
            set {
                this.datahorainsercaoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool datahorainsercaoSpecified {
            get {
                return this.datahorainsercaoFieldSpecified;
            }
            set {
                this.datahorainsercaoFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string datahora {
            get {
                return this.datahoraField;
            }
            set {
                this.datahoraField = value;
            }
        }
        
        /// <remarks/>
        public string usuario {
            get {
                return this.usuarioField;
            }
            set {
                this.usuarioField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:rogeXlogos")]
    public partial class ConferenciaNotaFiscalProduto {
        
        private string chaveeletronicaField;
        
        private string codigoprodutoField;
        
        private string quantidadeField;
        
        private bool pertenceanotaField;
        
        /// <remarks/>
        public string chaveeletronica {
            get {
                return this.chaveeletronicaField;
            }
            set {
                this.chaveeletronicaField = value;
            }
        }
        
        /// <remarks/>
        public string codigoproduto {
            get {
                return this.codigoprodutoField;
            }
            set {
                this.codigoprodutoField = value;
            }
        }
        
        /// <remarks/>
        public string quantidade {
            get {
                return this.quantidadeField;
            }
            set {
                this.quantidadeField = value;
            }
        }
        
        /// <remarks/>
        public bool pertenceanota {
            get {
                return this.pertenceanotaField;
            }
            set {
                this.pertenceanotaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:rogeXlogos")]
    public partial class ConferenciaNotaFiscalEanVolume {
        
        private string chaveeletronicaField;
        
        private string codigobarrasvolumeField;
        
        /// <remarks/>
        public string chaveeletronica {
            get {
                return this.chaveeletronicaField;
            }
            set {
                this.chaveeletronicaField = value;
            }
        }
        
        /// <remarks/>
        public string codigobarrasvolume {
            get {
                return this.codigobarrasvolumeField;
            }
            set {
                this.codigobarrasvolumeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    public delegate void GravarConferenciaCompletedEventHandler(object sender, GravarConferenciaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GravarConferenciaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GravarConferenciaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    public delegate void RetornarDadosCDEANCompletedEventHandler(object sender, RetornarDadosCDEANCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RetornarDadosCDEANCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RetornarDadosCDEANCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591