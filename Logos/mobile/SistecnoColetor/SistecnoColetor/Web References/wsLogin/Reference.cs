﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.8009
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.8009.
// 
namespace SistecnoColetor.wsLogin {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="LoginSoap", Namespace="http://tempuri.org/")]
    public partial class Login : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public Login() {
            this.Url = "http://wss.sistecno.com.br/appWs/wss/login.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RetornaConexao", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string RetornaConexao(string idEmpresa, string senha) {
            object[] results = this.Invoke("RetornaConexao", new object[] {
                        idEmpresa,
                        senha});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginRetornaConexao(string idEmpresa, string senha, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("RetornaConexao", new object[] {
                        idEmpresa,
                        senha}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndRetornaConexao(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EstaAtivo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string EstaAtivo() {
            object[] results = this.Invoke("EstaAtivo", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginEstaAtivo(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("EstaAtivo", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string EndEstaAtivo(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RetornarProgramasColetor", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable RetornarProgramasColetor(string senha) {
            object[] results = this.Invoke("RetornarProgramasColetor", new object[] {
                        senha});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginRetornarProgramasColetor(string senha, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("RetornarProgramasColetor", new object[] {
                        senha}, callback, asyncState);
        }
        
        /// <remarks/>
        public System.Data.DataTable EndRetornarProgramasColetor(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((System.Data.DataTable)(results[0]));
        }
    }
}
