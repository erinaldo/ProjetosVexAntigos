﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:WebServiceComprovei", ConfigurationName="ServiceReference1.WebServiceComproveiPortType")]
    public interface WebServiceComproveiPortType {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:WebServiceComprovei#sendDocsToPOD", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="status")]
        string sendDocsToPOD(string conteudoArquivo, string nomeArquivo);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:WebServiceComprovei#getDocFromPOD", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Pausa))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Ocorrencia))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Item))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Parada))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="Rota")]
        Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.Rota getDocFromPOD(string numeroRota, string dataRota);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:WebServiceComprovei#importDocsToPOD", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Pausa))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Ocorrencia))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Item))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Parada))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="status")]
        string importDocsToPOD(string conteudoArquivo, string nomeArquivo);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:WebServiceComprovei#sendDocsOSToPOD", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Pausa))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Ocorrencia))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Item))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Parada))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="status")]
        string sendDocsOSToPOD(string conteudoArquivo, string nomeArquivo);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:WebServiceComprovei#getDocsOSToPOD", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Pausa))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Ocorrencia))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Item))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Parada))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="Retorno")]
        Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.Documentos getDocsOSToPOD();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:WebServiceComprovei#getDocumentsFromPOD", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Pausa))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Ocorrencia))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Item))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Parada))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="Retorno")]
        Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.Documentos getDocumentsFromPOD([System.Xml.Serialization.SoapElementAttribute(DataType="integer")] string qtdDocumentos);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:WebServiceComprovei#getADocumentFromPOD", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Pausa))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Ocorrencia))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Item))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Parada))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="Retorno")]
        Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.Documento getADocumentFromPOD(string key);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:WebServiceComprovei#cancelDocuments", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Pausa))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Ocorrencia))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Item))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Parada))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="status")]
        string cancelDocuments(Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.ArrayOfCancelDocs batchOfCancelDocs);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Rota : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string erroField;
        
        private string numeroField;
        
        private string dataField;
        
        private Parada[] paradasField;
        
        private Pausa[] pausasField;
        
        /// <remarks/>
        public string Erro {
            get {
                return this.erroField;
            }
            set {
                this.erroField = value;
                this.RaisePropertyChanged("Erro");
            }
        }
        
        /// <remarks/>
        public string Numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
                this.RaisePropertyChanged("Numero");
            }
        }
        
        /// <remarks/>
        public string Data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
                this.RaisePropertyChanged("Data");
            }
        }
        
        /// <remarks/>
        public Parada[] Paradas {
            get {
                return this.paradasField;
            }
            set {
                this.paradasField = value;
                this.RaisePropertyChanged("Paradas");
            }
        }
        
        /// <remarks/>
        public Pausa[] Pausas {
            get {
                return this.pausasField;
            }
            set {
                this.pausasField = value;
                this.RaisePropertyChanged("Pausas");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Parada : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string numeroField;
        
        private Item[] itensField;
        
        private Ocorrencia[] ocorrenciasField;
        
        /// <remarks/>
        public string Numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
                this.RaisePropertyChanged("Numero");
            }
        }
        
        /// <remarks/>
        public Item[] Itens {
            get {
                return this.itensField;
            }
            set {
                this.itensField = value;
                this.RaisePropertyChanged("Itens");
            }
        }
        
        /// <remarks/>
        public Ocorrencia[] Ocorrencias {
            get {
                return this.ocorrenciasField;
            }
            set {
                this.ocorrenciasField = value;
                this.RaisePropertyChanged("Ocorrencias");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Item : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string numeroField;
        
        private string descricaoField;
        
        private string barcodeField;
        
        private string quantidadeField;
        
        private Foto fotoField;
        
        /// <remarks/>
        public string Numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
                this.RaisePropertyChanged("Numero");
            }
        }
        
        /// <remarks/>
        public string Descricao {
            get {
                return this.descricaoField;
            }
            set {
                this.descricaoField = value;
                this.RaisePropertyChanged("Descricao");
            }
        }
        
        /// <remarks/>
        public string Barcode {
            get {
                return this.barcodeField;
            }
            set {
                this.barcodeField = value;
                this.RaisePropertyChanged("Barcode");
            }
        }
        
        /// <remarks/>
        public string Quantidade {
            get {
                return this.quantidadeField;
            }
            set {
                this.quantidadeField = value;
                this.RaisePropertyChanged("Quantidade");
            }
        }
        
        /// <remarks/>
        public Foto Foto {
            get {
                return this.fotoField;
            }
            set {
                this.fotoField = value;
                this.RaisePropertyChanged("Foto");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Foto : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string extensaoField;
        
        private string tipoField;
        
        private string comentarioField;
        
        private string dadoField;
        
        /// <remarks/>
        public string Extensao {
            get {
                return this.extensaoField;
            }
            set {
                this.extensaoField = value;
                this.RaisePropertyChanged("Extensao");
            }
        }
        
        /// <remarks/>
        public string Tipo {
            get {
                return this.tipoField;
            }
            set {
                this.tipoField = value;
                this.RaisePropertyChanged("Tipo");
            }
        }
        
        /// <remarks/>
        public string Comentario {
            get {
                return this.comentarioField;
            }
            set {
                this.comentarioField = value;
                this.RaisePropertyChanged("Comentario");
            }
        }
        
        /// <remarks/>
        public string Dado {
            get {
                return this.dadoField;
            }
            set {
                this.dadoField = value;
                this.RaisePropertyChanged("Dado");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class CancelDoc : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string documentKeyField;
        
        /// <remarks/>
        public string DocumentKey {
            get {
                return this.documentKeyField;
            }
            set {
                this.documentKeyField = value;
                this.RaisePropertyChanged("DocumentKey");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class ArrayOfCancelDocs : object, System.ComponentModel.INotifyPropertyChanged {
        
        private CancelDoc[] cancelDocField;
        
        /// <remarks/>
        public CancelDoc[] CancelDoc {
            get {
                return this.cancelDocField;
            }
            set {
                this.cancelDocField = value;
                this.RaisePropertyChanged("CancelDoc");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Documento : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string erroField;
        
        private string tipoField;
        
        private string modeloField;
        
        private string numeroField;
        
        private string serieField;
        
        private string emissaoField;
        
        private string cnpjField;
        
        private string chaveField;
        
        private Ocorrencia[] ocorrenciasField;
        
        /// <remarks/>
        public string Erro {
            get {
                return this.erroField;
            }
            set {
                this.erroField = value;
                this.RaisePropertyChanged("Erro");
            }
        }
        
        /// <remarks/>
        public string Tipo {
            get {
                return this.tipoField;
            }
            set {
                this.tipoField = value;
                this.RaisePropertyChanged("Tipo");
            }
        }
        
        /// <remarks/>
        public string Modelo {
            get {
                return this.modeloField;
            }
            set {
                this.modeloField = value;
                this.RaisePropertyChanged("Modelo");
            }
        }
        
        /// <remarks/>
        public string Numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
                this.RaisePropertyChanged("Numero");
            }
        }
        
        /// <remarks/>
        public string Serie {
            get {
                return this.serieField;
            }
            set {
                this.serieField = value;
                this.RaisePropertyChanged("Serie");
            }
        }
        
        /// <remarks/>
        public string Emissao {
            get {
                return this.emissaoField;
            }
            set {
                this.emissaoField = value;
                this.RaisePropertyChanged("Emissao");
            }
        }
        
        /// <remarks/>
        public string cnpj {
            get {
                return this.cnpjField;
            }
            set {
                this.cnpjField = value;
                this.RaisePropertyChanged("cnpj");
            }
        }
        
        /// <remarks/>
        public string Chave {
            get {
                return this.chaveField;
            }
            set {
                this.chaveField = value;
                this.RaisePropertyChanged("Chave");
            }
        }
        
        /// <remarks/>
        public Ocorrencia[] Ocorrencias {
            get {
                return this.ocorrenciasField;
            }
            set {
                this.ocorrenciasField = value;
                this.RaisePropertyChanged("Ocorrencias");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Ocorrencia : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string numeroField;
        
        private string motivoField;
        
        private string dataField;
        
        private Assinatura assinaturaField;
        
        private Anotacao anotacaoField;
        
        private Foto fotoField;
        
        /// <remarks/>
        public string Numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
                this.RaisePropertyChanged("Numero");
            }
        }
        
        /// <remarks/>
        public string Motivo {
            get {
                return this.motivoField;
            }
            set {
                this.motivoField = value;
                this.RaisePropertyChanged("Motivo");
            }
        }
        
        /// <remarks/>
        public string Data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
                this.RaisePropertyChanged("Data");
            }
        }
        
        /// <remarks/>
        public Assinatura Assinatura {
            get {
                return this.assinaturaField;
            }
            set {
                this.assinaturaField = value;
                this.RaisePropertyChanged("Assinatura");
            }
        }
        
        /// <remarks/>
        public Anotacao Anotacao {
            get {
                return this.anotacaoField;
            }
            set {
                this.anotacaoField = value;
                this.RaisePropertyChanged("Anotacao");
            }
        }
        
        /// <remarks/>
        public Foto Foto {
            get {
                return this.fotoField;
            }
            set {
                this.fotoField = value;
                this.RaisePropertyChanged("Foto");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Assinatura : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string extensaoField;
        
        private string nomeField;
        
        private string dadoField;
        
        /// <remarks/>
        public string Extensao {
            get {
                return this.extensaoField;
            }
            set {
                this.extensaoField = value;
                this.RaisePropertyChanged("Extensao");
            }
        }
        
        /// <remarks/>
        public string Nome {
            get {
                return this.nomeField;
            }
            set {
                this.nomeField = value;
                this.RaisePropertyChanged("Nome");
            }
        }
        
        /// <remarks/>
        public string Dado {
            get {
                return this.dadoField;
            }
            set {
                this.dadoField = value;
                this.RaisePropertyChanged("Dado");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Anotacao : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string anotacao1Field;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute("Anotacao")]
        public string Anotacao1 {
            get {
                return this.anotacao1Field;
            }
            set {
                this.anotacao1Field = value;
                this.RaisePropertyChanged("Anotacao1");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Documentos : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string erroField;
        
        private Documento[] documentos1Field;
        
        /// <remarks/>
        public string Erro {
            get {
                return this.erroField;
            }
            set {
                this.erroField = value;
                this.RaisePropertyChanged("Erro");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute("Documentos")]
        public Documento[] Documentos1 {
            get {
                return this.documentos1Field;
            }
            set {
                this.documentos1Field = value;
                this.RaisePropertyChanged("Documentos1");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:WebServiceComprovei")]
    public partial class Pausa : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string numeroField;
        
        private string dataHoraIniField;
        
        private string dataHoraFimField;
        
        private string motivoField;
        
        private Foto fotoField;
        
        /// <remarks/>
        public string Numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
                this.RaisePropertyChanged("Numero");
            }
        }
        
        /// <remarks/>
        public string DataHoraIni {
            get {
                return this.dataHoraIniField;
            }
            set {
                this.dataHoraIniField = value;
                this.RaisePropertyChanged("DataHoraIni");
            }
        }
        
        /// <remarks/>
        public string DataHoraFim {
            get {
                return this.dataHoraFimField;
            }
            set {
                this.dataHoraFimField = value;
                this.RaisePropertyChanged("DataHoraFim");
            }
        }
        
        /// <remarks/>
        public string Motivo {
            get {
                return this.motivoField;
            }
            set {
                this.motivoField = value;
                this.RaisePropertyChanged("Motivo");
            }
        }
        
        /// <remarks/>
        public Foto Foto {
            get {
                return this.fotoField;
            }
            set {
                this.fotoField = value;
                this.RaisePropertyChanged("Foto");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebServiceComproveiPortTypeChannel : Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.WebServiceComproveiPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebServiceComproveiPortTypeClient : System.ServiceModel.ClientBase<Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.WebServiceComproveiPortType>, Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.WebServiceComproveiPortType {
        
        public WebServiceComproveiPortTypeClient() {
        }
        
        public WebServiceComproveiPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebServiceComproveiPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceComproveiPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServiceComproveiPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string sendDocsToPOD(string conteudoArquivo, string nomeArquivo) {
            return base.Channel.sendDocsToPOD(conteudoArquivo, nomeArquivo);
        }
        
        public Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.Rota getDocFromPOD(string numeroRota, string dataRota) {
            return base.Channel.getDocFromPOD(numeroRota, dataRota);
        }
        
        public string importDocsToPOD(string conteudoArquivo, string nomeArquivo) {
            return base.Channel.importDocsToPOD(conteudoArquivo, nomeArquivo);
        }
        
        public string sendDocsOSToPOD(string conteudoArquivo, string nomeArquivo) {
            return base.Channel.sendDocsOSToPOD(conteudoArquivo, nomeArquivo);
        }
        
        public Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.Documentos getDocsOSToPOD() {
            return base.Channel.getDocsOSToPOD();
        }
        
        public Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.Documentos getDocumentsFromPOD(string qtdDocumentos) {
            return base.Channel.getDocumentsFromPOD(qtdDocumentos);
        }
        
        public Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.Documento getADocumentFromPOD(string key) {
            return base.Channel.getADocumentFromPOD(key);
        }
        
        public string cancelDocuments(Robo.Email.Notas.Solutions.Windows.Testes.ServiceReference1.ArrayOfCancelDocs batchOfCancelDocs) {
            return base.Channel.cancelDocuments(batchOfCancelDocs);
        }
    }
}
