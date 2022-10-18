﻿namespace ServicosWEB.wsInfraStock
{


	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
	[System.SerializableAttribute()]
	//  [System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/ErrorLog")]
	public partial class error : object, System.ComponentModel.INotifyPropertyChanged
	{

		private string dateField;

		private string messageField;

		private string summaryField;

		private string detailField;

		private string codeField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 0)]
		public string date
		{
			get
			{
				return this.dateField;
			}
			set
			{
				this.dateField = value;
				this.RaisePropertyChanged("date");
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 1)]
		public string message
		{
			get
			{
				return this.messageField;
			}
			set
			{
				this.messageField = value;
				this.RaisePropertyChanged("message");
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 2)]
		public string summary
		{
			get
			{
				return this.summaryField;
			}
			set
			{
				this.summaryField = value;
				this.RaisePropertyChanged("summary");
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 3)]
		public string detail
		{
			get
			{
				return this.detailField;
			}
			set
			{
				this.detailField = value;
				this.RaisePropertyChanged("detail");
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 4)]
		public string code
		{
			get
			{
				return this.codeField;
			}
			set
			{
				this.codeField = value;
				this.RaisePropertyChanged("code");
			}
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if ((propertyChanged != null))
			{
				propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.accurate.com/acec/Stock")]
	public partial class stock : object, System.ComponentModel.INotifyPropertyChanged
	{

		private string skuIdField;

		//private int wareHouseIdField;

		//private bool wareHouseIdFieldSpecified;

		// private int warehouseCnpjField;

		// private bool warehouseCnpjFieldSpecified;

		//private int stockTypeField;

		// private int leadTimeField;

		// private bool leadTimeFieldSpecified;

		private int quantityField;

		// private bool quantityFieldSpecified;

		// private bool relativeStockFlagField;


		// private bool relativeStockFlagFieldSpecified;

		// private System.DateTime startDateField;

		// private bool startDateFieldSpecified;

		//private System.DateTime endDateField;

		private string stockTypeSourceNameField;

		//private bool endDateFieldSpecified;

		//  private bool stockTypeSourceNameFieldSpecified;



		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 0)]
		public string skuId
		{
			get
			{
				return this.skuIdField;
			}
			set
			{
				this.skuIdField = value;
				this.RaisePropertyChanged("skuId");
			}
		}

		/// <remarks/>
		//[System.Xml.Serialization.XmlElementAttribute(Order=1)]
		//public int wareHouseId {
		//    get {
		//        return this.wareHouseIdField;
		//    }
		//    set {
		//        this.wareHouseIdField = value;
		//        this.RaisePropertyChanged("wareHouseId");
		//    }
		//}

		/// <remarks/>
		//[System.Xml.Serialization.XmlIgnoreAttribute()]
		//public bool wareHouseIdSpecified {
		//    get {
		//        return this.wareHouseIdFieldSpecified;
		//    }
		//    set {
		//        this.wareHouseIdFieldSpecified = value;
		//        this.RaisePropertyChanged("wareHouseIdSpecified");
		//    }
		//}

		/// <remarks/>
		//[System.Xml.Serialization.XmlElementAttribute(Order=2)]
		//public int warehouseCnpj {
		//    get {
		//        return this.warehouseCnpjField;
		//    }
		//    set {
		//        this.warehouseCnpjField = value;
		//        this.RaisePropertyChanged("warehouseCnpj");
		//    }
		//}

		///// <remarks/>
		//[System.Xml.Serialization.XmlIgnoreAttribute()]
		//public bool warehouseCnpjSpecified {
		//    get {
		//        return this.warehouseCnpjFieldSpecified;
		//    }
		//    set {
		//        this.warehouseCnpjFieldSpecified = value;
		//        this.RaisePropertyChanged("warehouseCnpjSpecified");
		//    }
		//}

		/// <remarks/>
		//[System.Xml.Serialization.XmlElementAttribute(Order=3)]
		//public int stockType {
		//    get {
		//        return this.stockTypeField;
		//    }
		//    set {
		//        this.stockTypeField = value;
		//        this.RaisePropertyChanged("stockType");
		//    }
		//}

		/// <remarks/>
		//[System.Xml.Serialization.XmlElementAttribute(Order=4)]
		//public int leadTime {
		//    get {
		//        return this.leadTimeField;
		//    }
		//    set {
		//        this.leadTimeField = value;
		//        this.RaisePropertyChanged("leadTime");
		//    }
		//}

		/// <remarks/>
		//[System.Xml.Serialization.XmlIgnoreAttribute()]
		//public bool leadTimeSpecified {
		//    get {
		//        return this.leadTimeFieldSpecified;
		//    }
		//    set {
		//        this.leadTimeFieldSpecified = value;
		//        this.RaisePropertyChanged("leadTimeSpecified");
		//    }
		//}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 1)]
		public int quantity
		{
			get
			{
				return this.quantityField;
			}
			set
			{
				this.quantityField = value;
				this.RaisePropertyChanged("quantity");
			}
		}

		/// <remarks/>
		//[System.Xml.Serialization.XmlIgnoreAttribute()]
		//public bool quantitySpecified {
		//    get {
		//        return this.quantityFieldSpecified;
		//    }
		//    set {
		//        this.quantityFieldSpecified = value;
		//        this.RaisePropertyChanged("quantitySpecified");
		//    }
		//}

		/// <remarks/>
		//[System.Xml.Serialization.XmlElementAttribute(Order=6)]
		//public bool relativeStockFlag {
		//    get {
		//        return this.relativeStockFlagField;
		//    }
		//    set {
		//        this.relativeStockFlagField = value;
		//        this.RaisePropertyChanged("relativeStockFlag");
		//    }
		//}

		/// <remarks/>
		//[System.Xml.Serialization.XmlIgnoreAttribute()]
		//public bool relativeStockFlagSpecified {
		//    get {
		//        return this.relativeStockFlagFieldSpecified;
		//    }
		//    set {
		//        this.relativeStockFlagFieldSpecified = value;
		//        this.RaisePropertyChanged("relativeStockFlagSpecified");
		//    }
		//}

		/// <remarks/>
		//[System.Xml.Serialization.XmlElementAttribute(Order=7)]
		//public System.DateTime startDate {
		//    get {
		//        return this.startDateField;
		//    }
		//    set {
		//        this.startDateField = value;
		//        this.RaisePropertyChanged("startDate");
		//    }
		//}

		/// <remarks/>
		//[System.Xml.Serialization.XmlIgnoreAttribute()]
		//public bool startDateSpecified {
		//    get {
		//        return this.startDateFieldSpecified;
		//    }
		//    set {
		//        this.startDateFieldSpecified = value;
		//        this.RaisePropertyChanged("startDateSpecified");
		//    }
		//}

		///// <remarks/>
		//[System.Xml.Serialization.XmlElementAttribute(Order=8)]
		//public System.DateTime endDate {
		//    get {
		//        return this.endDateField;
		//    }
		//    set {
		//        this.endDateField = value;
		//        this.RaisePropertyChanged("endDate");
		//    }
		//}

		///// <remarks/>
		//[System.Xml.Serialization.XmlIgnoreAttribute()]
		//public bool endDateSpecified {
		//    get {
		//        return this.endDateFieldSpecified;
		//    }
		//    set {
		//        this.endDateFieldSpecified = value;
		//        this.RaisePropertyChanged("endDateSpecified");
		//    }
		//}


		#region Moises
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 2)]
		public System.String stockTypeSourceName
		{
			get
			{
				return this.stockTypeSourceNameField;
			}
			set
			{
				this.stockTypeSourceNameField = value;
				this.RaisePropertyChanged("stockTypeSourceName");
			}
		}

		///// <remarks/>
		//[System.Xml.Serialization.XmlIgnoreAttribute()]
		//public bool stockTypeSourceNameSpecified
		//{
		//    get
		//    {
		//        return this.stockTypeSourceNameFieldSpecified;
		//    }
		//    set
		//    {
		//        this.stockTypeSourceNameFieldSpecified = value;
		//        this.RaisePropertyChanged("stockTypeSourceNameSpecified");
		//    }
		//}

		#endregion



		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;


		protected void RaisePropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if ((propertyChanged != null))
			{
				propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		//public string stockTypeSourceName { get; set; }
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	[System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.accurate.com/acec/StockServices", ConfigurationName = "wsInfraStock.StockServices")]
	public interface StockServices
	{

		// CODEGEN: Gerando contrato de mensagem porque a operação setStock não é RPC nem documento codificado.
		[System.ServiceModel.OperationContractAttribute(Action = "setStock", ReplyAction = "*")]
		[System.ServiceModel.FaultContractAttribute(typeof(ServicosWEB.wsInfraStock.error), Action = "setStock", Name = "error", Namespace = "http://www.accurate.com/acec/ErrorLog")]
		[System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults = true)]
		ServicosWEB.wsInfraStock.setStockResponse1 setStock(ServicosWEB.wsInfraStock.setStockRequest1 request);
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/StockServices")]
	public partial class setStockRequest : object, System.ComponentModel.INotifyPropertyChanged
	{

		private stock[] stockListField;

		private bool orderAccountingFlagField;

		private string storeIdField;

		public setStockRequest()
		{
			this.orderAccountingFlagField = false;
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayAttribute(Order = 0)]
		[System.Xml.Serialization.XmlArrayItemAttribute("Stock", IsNullable = false)]
		public stock[] stockList
		{
			get
			{
				return this.stockListField;
			}
			set
			{
				this.stockListField = value;
				this.RaisePropertyChanged("stockList");
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 1)]
		[System.ComponentModel.DefaultValueAttribute(false)]
		public bool orderAccountingFlag
		{
			get
			{
				return this.orderAccountingFlagField;
			}
			set
			{
				this.orderAccountingFlagField = value;
				this.RaisePropertyChanged("orderAccountingFlag");
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 2)]
		public string storeId
		{
			get
			{
				return this.storeIdField;
			}
			set
			{
				this.storeIdField = value;
				this.RaisePropertyChanged("storeId");
			}
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if ((propertyChanged != null))
			{
				propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accurate.com/acec/StockServices")]
	public partial class setStockResponse : object, System.ComponentModel.INotifyPropertyChanged
	{

		private string resultField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Order = 0)]
		public string result
		{
			get
			{
				return this.resultField;
			}
			set
			{
				this.resultField = value;
				this.RaisePropertyChanged("result");
			}
		}

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged(string propertyName)
		{
			System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if ((propertyChanged != null))
			{
				propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
	}

	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
	[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
	public partial class setStockRequest1
	{

		[System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.accurate.com/acec/StockServices", Order = 0)]
		public ServicosWEB.wsInfraStock.setStockRequest setStockRequest;

		public setStockRequest1()
		{
		}

		public setStockRequest1(ServicosWEB.wsInfraStock.setStockRequest setStockRequest)
		{
			this.setStockRequest = setStockRequest;
		}
	}

	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
	[System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
	public partial class setStockResponse1
	{

		[System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.accurate.com/acec/StockServices", Order = 0)]
		public ServicosWEB.wsInfraStock.setStockResponse setStockResponse;

		public setStockResponse1()
		{
		}

		public setStockResponse1(ServicosWEB.wsInfraStock.setStockResponse setStockResponse)
		{
			this.setStockResponse = setStockResponse;
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public interface StockServicesChannel : ServicosWEB.wsInfraStock.StockServices, System.ServiceModel.IClientChannel
	{
	}

	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public partial class StockServicesClient : System.ServiceModel.ClientBase<ServicosWEB.wsInfraStock.StockServices>, ServicosWEB.wsInfraStock.StockServices
	{

		public StockServicesClient()
		{
		}

		public StockServicesClient(string endpointConfigurationName) :
				base(endpointConfigurationName)
		{
		}

		public StockServicesClient(string endpointConfigurationName, string remoteAddress) :
				base(endpointConfigurationName, remoteAddress)
		{
		}

		public StockServicesClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
				base(endpointConfigurationName, remoteAddress)
		{
		}

		public StockServicesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
				base(binding, remoteAddress)
		{
		}

		[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
		ServicosWEB.wsInfraStock.setStockResponse1 ServicosWEB.wsInfraStock.StockServices.setStock(ServicosWEB.wsInfraStock.setStockRequest1 request)
		{
			return base.Channel.setStock(request);
		}

		public ServicosWEB.wsInfraStock.setStockResponse setStock(ServicosWEB.wsInfraStock.setStockRequest setStockRequest)
		{
			ServicosWEB.wsInfraStock.setStockRequest1 inValue = new ServicosWEB.wsInfraStock.setStockRequest1();
			inValue.setStockRequest = setStockRequest;
			ServicosWEB.wsInfraStock.setStockResponse1 retVal = ((ServicosWEB.wsInfraStock.StockServices)(this)).setStock(inValue);
			return retVal.setStockResponse;
		}
	}
}