﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicosWEB.InfraTracking {
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.accurate.com/acec/ErrorLog")]
    public partial class error : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string dateField;
        
        private string messageField;
        
        private string summaryField;
        
        private string detailField;
        
        private string codeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string date {
            get {
                return this.dateField;
            }
            set {
                this.dateField = value;
                this.RaisePropertyChanged("date");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("message");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string summary {
            get {
                return this.summaryField;
            }
            set {
                this.summaryField = value;
                this.RaisePropertyChanged("summary");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string detail {
            get {
                return this.detailField;
            }
            set {
                this.detailField = value;
                this.RaisePropertyChanged("detail");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
                this.RaisePropertyChanged("code");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.accurate.com/acec/Tracking")]
    public partial class SkuDeliveryTracking : object, System.ComponentModel.INotifyPropertyChanged {
        
        private long orderIdField;
        
        private long deliveryIdField;
        
        private bool deliveryIdFieldSpecified;
        
        private string skuIdField;
        
        private string skuNameField;
        
        private long preparedQtField;
        
        private bool preparedQtFieldSpecified;
        
        private decimal unitPriceField;
        
        private bool unitPriceFieldSpecified;
        
        private decimal totalPriceField;
        
        private bool totalPriceFieldSpecified;
        
        private string kitSkuIdField;
        
        private System.DateTime occurrenceDtField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public long orderId {
            get {
                return this.orderIdField;
            }
            set {
                this.orderIdField = value;
                this.RaisePropertyChanged("orderId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public long deliveryId {
            get {
                return this.deliveryIdField;
            }
            set {
                this.deliveryIdField = value;
                this.RaisePropertyChanged("deliveryId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deliveryIdSpecified {
            get {
                return this.deliveryIdFieldSpecified;
            }
            set {
                this.deliveryIdFieldSpecified = value;
                this.RaisePropertyChanged("deliveryIdSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string skuId {
            get {
                return this.skuIdField;
            }
            set {
                this.skuIdField = value;
                this.RaisePropertyChanged("skuId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string skuName {
            get {
                return this.skuNameField;
            }
            set {
                this.skuNameField = value;
                this.RaisePropertyChanged("skuName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public long preparedQt {
            get {
                return this.preparedQtField;
            }
            set {
                this.preparedQtField = value;
                this.RaisePropertyChanged("preparedQt");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool preparedQtSpecified {
            get {
                return this.preparedQtFieldSpecified;
            }
            set {
                this.preparedQtFieldSpecified = value;
                this.RaisePropertyChanged("preparedQtSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public decimal unitPrice {
            get {
                return this.unitPriceField;
            }
            set {
                this.unitPriceField = value;
                this.RaisePropertyChanged("unitPrice");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool unitPriceSpecified {
            get {
                return this.unitPriceFieldSpecified;
            }
            set {
                this.unitPriceFieldSpecified = value;
                this.RaisePropertyChanged("unitPriceSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public decimal totalPrice {
            get {
                return this.totalPriceField;
            }
            set {
                this.totalPriceField = value;
                this.RaisePropertyChanged("totalPrice");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalPriceSpecified {
            get {
                return this.totalPriceFieldSpecified;
            }
            set {
                this.totalPriceFieldSpecified = value;
                this.RaisePropertyChanged("totalPriceSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string kitSkuId {
            get {
                return this.kitSkuIdField;
            }
            set {
                this.kitSkuIdField = value;
                this.RaisePropertyChanged("kitSkuId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public System.DateTime occurrenceDt {
            get {
                return this.occurrenceDtField;
            }
            set {
                this.occurrenceDtField = value;
                this.RaisePropertyChanged("occurrenceDt");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.accurate.com/acec/Tracking")]
    public partial class trackingProperty : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string trackingPropertyIdField;
        
        private string trackingPropertyValueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string trackingPropertyId {
            get {
                return this.trackingPropertyIdField;
            }
            set {
                this.trackingPropertyIdField = value;
                this.RaisePropertyChanged("trackingPropertyId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string trackingPropertyValue {
            get {
                return this.trackingPropertyValueField;
            }
            set {
                this.trackingPropertyValueField = value;
                this.RaisePropertyChanged("trackingPropertyValue");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.accurate.com/acec/InvoiceInfo")]
    public partial class ObjectData : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string objectIdField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string objectId {
            get {
                return this.objectIdField;
            }
            set {
                this.objectIdField = value;
                this.RaisePropertyChanged("objectId");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.accurate.com/acec/InvoiceInfo")]
    public partial class InvoiceInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private long issuerDocumentNrField;
        
        private bool issuerDocumentNrFieldSpecified;
        
        private long invoiceNumberField;
        
        private bool invoiceNumberFieldSpecified;
        
        private string invoiceSerialNumberField;
        
        private System.DateTime invoiceEmissionDateField;
        
        private bool invoiceEmissionDateFieldSpecified;
        
        private string invoiceEletronicKeyField;
        
        private ObjectData[] objectDataListField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public long issuerDocumentNr {
            get {
                return this.issuerDocumentNrField;
            }
            set {
                this.issuerDocumentNrField = value;
                this.RaisePropertyChanged("issuerDocumentNr");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool issuerDocumentNrSpecified {
            get {
                return this.issuerDocumentNrFieldSpecified;
            }
            set {
                this.issuerDocumentNrFieldSpecified = value;
                this.RaisePropertyChanged("issuerDocumentNrSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public long invoiceNumber {
            get {
                return this.invoiceNumberField;
            }
            set {
                this.invoiceNumberField = value;
                this.RaisePropertyChanged("invoiceNumber");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool invoiceNumberSpecified {
            get {
                return this.invoiceNumberFieldSpecified;
            }
            set {
                this.invoiceNumberFieldSpecified = value;
                this.RaisePropertyChanged("invoiceNumberSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string invoiceSerialNumber {
            get {
                return this.invoiceSerialNumberField;
            }
            set {
                this.invoiceSerialNumberField = value;
                this.RaisePropertyChanged("invoiceSerialNumber");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public System.DateTime invoiceEmissionDate {
            get {
                return this.invoiceEmissionDateField;
            }
            set {
                this.invoiceEmissionDateField = value;
                this.RaisePropertyChanged("invoiceEmissionDate");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool invoiceEmissionDateSpecified {
            get {
                return this.invoiceEmissionDateFieldSpecified;
            }
            set {
                this.invoiceEmissionDateFieldSpecified = value;
                this.RaisePropertyChanged("invoiceEmissionDateSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string invoiceEletronicKey {
            get {
                return this.invoiceEletronicKeyField;
            }
            set {
                this.invoiceEletronicKeyField = value;
                this.RaisePropertyChanged("invoiceEletronicKey");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=5)]
        [System.Xml.Serialization.XmlArrayItemAttribute("objectData", IsNullable=false)]
        public ObjectData[] objectDataList {
            get {
                return this.objectDataListField;
            }
            set {
                this.objectDataListField = value;
                this.RaisePropertyChanged("objectDataList");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.accurate.com/acec/Tracking")]
    public partial class Tracking : object, System.ComponentModel.INotifyPropertyChanged {
        
        private long orderIdField;
        
        private long deliveryIdField;
        
        private bool deliveryIdFieldSpecified;
        
        private string controlPointIdField;
        
        private string controlPointNmField;
        
        private System.DateTime occurrenceDtField;
        
        private long carrierIdField;
        
        private bool carrierIdFieldSpecified;
        
        private string carrierNmField;
        
        private string contractIdField;
        
        private string receiverNmField;
        
        private string carrierMessageField;
        
        private System.DateTime adjustedDeliveryDtField;
        
        private bool adjustedDeliveryDtFieldSpecified;
        
        private string cTRCNumberField;
        
        private string carrierURLField;
        
        private string invoiceURLField;
        
        private InvoiceInfo invoiceInfoField;
        
        private trackingProperty[] trackingPropertyListField;
        
        private SkuDeliveryTracking[] skuDeliveryTrackingListField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public long orderId {
            get {
                return this.orderIdField;
            }
            set {
                this.orderIdField = value;
                this.RaisePropertyChanged("orderId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public long deliveryId {
            get {
                return this.deliveryIdField;
            }
            set {
                this.deliveryIdField = value;
                this.RaisePropertyChanged("deliveryId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deliveryIdSpecified {
            get {
                return this.deliveryIdFieldSpecified;
            }
            set {
                this.deliveryIdFieldSpecified = value;
                this.RaisePropertyChanged("deliveryIdSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string controlPointId {
            get {
                return this.controlPointIdField;
            }
            set {
                this.controlPointIdField = value;
                this.RaisePropertyChanged("controlPointId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string controlPointNm {
            get {
                return this.controlPointNmField;
            }
            set {
                this.controlPointNmField = value;
                this.RaisePropertyChanged("controlPointNm");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public System.DateTime occurrenceDt {
            get {
                return this.occurrenceDtField;
            }
            set {
                this.occurrenceDtField = value;
                this.RaisePropertyChanged("occurrenceDt");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public long carrierId {
            get {
                return this.carrierIdField;
            }
            set {
                this.carrierIdField = value;
                this.RaisePropertyChanged("carrierId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool carrierIdSpecified {
            get {
                return this.carrierIdFieldSpecified;
            }
            set {
                this.carrierIdFieldSpecified = value;
                this.RaisePropertyChanged("carrierIdSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string carrierNm {
            get {
                return this.carrierNmField;
            }
            set {
                this.carrierNmField = value;
                this.RaisePropertyChanged("carrierNm");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string contractId {
            get {
                return this.contractIdField;
            }
            set {
                this.contractIdField = value;
                this.RaisePropertyChanged("contractId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string receiverNm {
            get {
                return this.receiverNmField;
            }
            set {
                this.receiverNmField = value;
                this.RaisePropertyChanged("receiverNm");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string carrierMessage {
            get {
                return this.carrierMessageField;
            }
            set {
                this.carrierMessageField = value;
                this.RaisePropertyChanged("carrierMessage");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=10)]
        public System.DateTime adjustedDeliveryDt {
            get {
                return this.adjustedDeliveryDtField;
            }
            set {
                this.adjustedDeliveryDtField = value;
                this.RaisePropertyChanged("adjustedDeliveryDt");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool adjustedDeliveryDtSpecified {
            get {
                return this.adjustedDeliveryDtFieldSpecified;
            }
            set {
                this.adjustedDeliveryDtFieldSpecified = value;
                this.RaisePropertyChanged("adjustedDeliveryDtSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string CTRCNumber {
            get {
                return this.cTRCNumberField;
            }
            set {
                this.cTRCNumberField = value;
                this.RaisePropertyChanged("CTRCNumber");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public string carrierURL {
            get {
                return this.carrierURLField;
            }
            set {
                this.carrierURLField = value;
                this.RaisePropertyChanged("carrierURL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string invoiceURL {
            get {
                return this.invoiceURLField;
            }
            set {
                this.invoiceURLField = value;
                this.RaisePropertyChanged("invoiceURL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.accurate.com/acec/InvoiceInfo", Order=14)]
        public InvoiceInfo invoiceInfo {
            get {
                return this.invoiceInfoField;
            }
            set {
                this.invoiceInfoField = value;
                this.RaisePropertyChanged("invoiceInfo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=15)]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public trackingProperty[] trackingPropertyList {
            get {
                return this.trackingPropertyListField;
            }
            set {
                this.trackingPropertyListField = value;
                this.RaisePropertyChanged("trackingPropertyList");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=16)]
        [System.Xml.Serialization.XmlArrayItemAttribute("skuDeliveryTracking", IsNullable=false)]
        public SkuDeliveryTracking[] skuDeliveryTrackingList {
            get {
                return this.skuDeliveryTrackingListField;
            }
            set {
                this.skuDeliveryTrackingListField = value;
                this.RaisePropertyChanged("skuDeliveryTrackingList");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.accurate.com/acec/TrackingServices", ConfigurationName="InfraTracking.TrackingServices")]
    public interface TrackingServices {
        
        // CODEGEN: Generating message contract since the operation captureTracking is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="captureTracking", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServicosWEB.InfraTracking.error), Action="captureTracking", Name="error", Namespace="http://www.accurate.com/acec/ErrorLog")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(trackingProperty[]))]
        ServicosWEB.InfraTracking.captureTrackingResponse1 captureTracking(ServicosWEB.InfraTracking.captureTrackingRequest1 request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.accurate.com/acec/TrackingServices")]
    public partial class captureTrackingRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Tracking[] trackingListField;
        
        private string storeIdField;
        
        private bool generateEventsInSameTransactionField;
        
        private bool generateEventsInSameTransactionFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace="http://www.accurate.com/acec/Tracking", Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("tracking", IsNullable=false)]
        public Tracking[] trackingList {
            get {
                return this.trackingListField;
            }
            set {
                this.trackingListField = value;
                this.RaisePropertyChanged("trackingList");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string storeId {
            get {
                return this.storeIdField;
            }
            set {
                this.storeIdField = value;
                this.RaisePropertyChanged("storeId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public bool generateEventsInSameTransaction {
            get {
                return this.generateEventsInSameTransactionField;
            }
            set {
                this.generateEventsInSameTransactionField = value;
                this.RaisePropertyChanged("generateEventsInSameTransaction");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool generateEventsInSameTransactionSpecified {
            get {
                return this.generateEventsInSameTransactionFieldSpecified;
            }
            set {
                this.generateEventsInSameTransactionFieldSpecified = value;
                this.RaisePropertyChanged("generateEventsInSameTransactionSpecified");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.accurate.com/acec/TrackingServices")]
    public partial class captureTrackingResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool successField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public bool success {
            get {
                return this.successField;
            }
            set {
                this.successField = value;
                this.RaisePropertyChanged("success");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class captureTrackingRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.accurate.com/acec/TrackingServices", Order=0)]
        public ServicosWEB.InfraTracking.captureTrackingRequest captureTrackingRequest;
        
        public captureTrackingRequest1() {
        }
        
        public captureTrackingRequest1(ServicosWEB.InfraTracking.captureTrackingRequest captureTrackingRequest) {
            this.captureTrackingRequest = captureTrackingRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class captureTrackingResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.accurate.com/acec/TrackingServices", Order=0)]
        public ServicosWEB.InfraTracking.captureTrackingResponse captureTrackingResponse;
        
        public captureTrackingResponse1() {
        }
        
        public captureTrackingResponse1(ServicosWEB.InfraTracking.captureTrackingResponse captureTrackingResponse) {
            this.captureTrackingResponse = captureTrackingResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface TrackingServicesChannel : ServicosWEB.InfraTracking.TrackingServices, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TrackingServicesClient : System.ServiceModel.ClientBase<ServicosWEB.InfraTracking.TrackingServices>, ServicosWEB.InfraTracking.TrackingServices {
        
        public TrackingServicesClient() {
        }
        
        public TrackingServicesClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TrackingServicesClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TrackingServicesClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TrackingServicesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ServicosWEB.InfraTracking.captureTrackingResponse1 ServicosWEB.InfraTracking.TrackingServices.captureTracking(ServicosWEB.InfraTracking.captureTrackingRequest1 request) {
            return base.Channel.captureTracking(request);
        }
        
        public ServicosWEB.InfraTracking.captureTrackingResponse captureTracking(ServicosWEB.InfraTracking.captureTrackingRequest captureTrackingRequest) {
            ServicosWEB.InfraTracking.captureTrackingRequest1 inValue = new ServicosWEB.InfraTracking.captureTrackingRequest1();
            inValue.captureTrackingRequest = captureTrackingRequest;
            ServicosWEB.InfraTracking.captureTrackingResponse1 retVal = ((ServicosWEB.InfraTracking.TrackingServices)(this)).captureTracking(inValue);
            return retVal.captureTrackingResponse;
        }
    }
}