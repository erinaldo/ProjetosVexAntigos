﻿
namespace Rdl {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IsNullable=false)]
    public partial class Report {
        
        private object[] itemsField;
        
        private ItemsChoiceType37[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Author", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("AutoRefresh", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("Body", typeof(BodyType))]
        [System.Xml.Serialization.XmlElementAttribute("BottomMargin", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Classes", typeof(ClassesType))]
        [System.Xml.Serialization.XmlElementAttribute("Code", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CodeModules", typeof(CodeModulesType))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementStyle", typeof(ReportDataElementStyle))]
        [System.Xml.Serialization.XmlElementAttribute("DataSchema", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataSets", typeof(DataSetsType))]
        [System.Xml.Serialization.XmlElementAttribute("DataSources", typeof(DataSourcesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataTransform", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Description", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("EmbeddedImages", typeof(EmbeddedImagesType))]
        [System.Xml.Serialization.XmlElementAttribute("InteractiveHeight", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("InteractiveWidth", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Language", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("LeftMargin", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("PageFooter", typeof(PageHeaderFooterType))]
        [System.Xml.Serialization.XmlElementAttribute("PageHeader", typeof(PageHeaderFooterType))]
        [System.Xml.Serialization.XmlElementAttribute("PageHeight", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("PageWidth", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ReportParameters", typeof(ReportParametersType))]
        [System.Xml.Serialization.XmlElementAttribute("RightMargin", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("TopMargin", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType37[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class BodyType {
        
        private object[] itemsField;
        
        private ItemsChoiceType30[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ColumnSpacing", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Columns", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType30[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ReportItemsType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Chart", typeof(ChartType))]
        [System.Xml.Serialization.XmlElementAttribute("CustomReportItem", typeof(CustomReportItemType))]
        [System.Xml.Serialization.XmlElementAttribute("Image", typeof(ImageType))]
        [System.Xml.Serialization.XmlElementAttribute("Line", typeof(LineType))]
        [System.Xml.Serialization.XmlElementAttribute("List", typeof(ListType))]
        [System.Xml.Serialization.XmlElementAttribute("Matrix", typeof(MatrixType))]
        [System.Xml.Serialization.XmlElementAttribute("Rectangle", typeof(RectangleType))]
        [System.Xml.Serialization.XmlElementAttribute("Subreport", typeof(SubreportType))]
        [System.Xml.Serialization.XmlElementAttribute("Table", typeof(TableType))]
        [System.Xml.Serialization.XmlElementAttribute("Textbox", typeof(TextboxType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ChartType {
        
        private object[] itemsField;
        
        private ItemsChoiceType27[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CategoryAxis", typeof(CategoryAxisType))]
        [System.Xml.Serialization.XmlElementAttribute("CategoryGroupings", typeof(CategoryGroupingsType))]
        [System.Xml.Serialization.XmlElementAttribute("ChartData", typeof(ChartDataType))]
        [System.Xml.Serialization.XmlElementAttribute("ChartElementOutput", typeof(ChartTypeChartElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(ChartTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("DataSetName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Filters", typeof(FiltersType))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("KeepTogether", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Legend", typeof(LegendType))]
        [System.Xml.Serialization.XmlElementAttribute("LinkToChild", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("NoRows", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtEnd", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtStart", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Palette", typeof(ChartTypePalette))]
        [System.Xml.Serialization.XmlElementAttribute("PlotArea", typeof(PlotAreaType))]
        [System.Xml.Serialization.XmlElementAttribute("PointWidth", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("SeriesGroupings", typeof(SeriesGroupingsType))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("Subtype", typeof(ChartTypeSubtype))]
        [System.Xml.Serialization.XmlElementAttribute("ThreeDProperties", typeof(ThreeDPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("Title", typeof(TitleType))]
        [System.Xml.Serialization.XmlElementAttribute("ToolTip", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Type", typeof(ChartTypeType))]
        [System.Xml.Serialization.XmlElementAttribute("ValueAxis", typeof(ValueAxisType))]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType27[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ActionType {
        
        private object[] itemsField;
        
        private ItemsChoiceType8[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("BookmarkLink", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Drillthrough", typeof(DrillthroughType))]
        [System.Xml.Serialization.XmlElementAttribute("Hyperlink", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType8[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DrillthroughType {
        
        private object[] itemsField;
        
        private ItemsChoiceType7[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("BookmarkLink", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Parameters", typeof(ParametersType))]
        [System.Xml.Serialization.XmlElementAttribute("ReportName", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType7[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ParametersType {
        
        private ParameterType[] parameterField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Parameter")]
        public ParameterType[] Parameter {
            get {
                return this.parameterField;
            }
            set {
                this.parameterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ParameterType {
        
        private object[] itemsField;
        
        private ItemsChoiceType6[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Omit", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType6[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType6 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Omit,
        
        /// <remarks/>
        Value,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ClassType {
        
        private object[] itemsField;
        
        private ItemsChoiceType36[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ClassName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("InstanceName", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType36[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType36 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        ClassName,
        
        /// <remarks/>
        InstanceName,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ClassesType {
        
        private ClassType[] classField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Class")]
        public ClassType[] Class {
            get {
                return this.classField;
            }
            set {
                this.classField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class CodeModulesType {
        
        private string[] codeModuleField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CodeModule")]
        public string[] CodeModule {
            get {
                return this.codeModuleField;
            }
            set {
                this.codeModuleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class EmbeddedImageType {
        
        private object[] itemsField;
        
        private ItemsChoiceType35[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ImageData", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("MIMEType", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType35[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType35 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        ImageData,
        
        /// <remarks/>
        MIMEType,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class EmbeddedImagesType {
        
        private EmbeddedImageType[] embeddedImageField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EmbeddedImage")]
        public EmbeddedImageType[] EmbeddedImage {
            get {
                return this.embeddedImageField;
            }
            set {
                this.embeddedImageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class PageHeaderFooterType {
        
        private object[] itemsField;
        
        private ItemsChoiceType34[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("PrintOnFirstPage", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("PrintOnLastPage", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType34[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class StyleType {
        
        private object[] itemsField;
        
        private ItemsChoiceType5[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("BackgroundColor", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("BackgroundGradientEndColor", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("BackgroundGradientType", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("BackgroundImage", typeof(BackgroundImageType))]
        [System.Xml.Serialization.XmlElementAttribute("BorderColor", typeof(BorderColorStyleWidthType))]
        [System.Xml.Serialization.XmlElementAttribute("BorderStyle", typeof(BorderColorStyleWidthType))]
        [System.Xml.Serialization.XmlElementAttribute("BorderWidth", typeof(BorderColorStyleWidthType))]
        [System.Xml.Serialization.XmlElementAttribute("Calendar", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Color", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Direction", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("FontFamily", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("FontSize", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("FontStyle", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("FontWeight", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Format", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Language", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("LineHeight", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("NumeralLanguage", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("NumeralVariant", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PaddingBottom", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PaddingLeft", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PaddingRight", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PaddingTop", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("TextAlign", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("TextDecoration", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("UnicodeBiDi", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("VerticalAlign", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("WritingMode", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType5[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class BackgroundImageType {
        
        private object[] itemsField;
        
        private ItemsChoiceType4[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("BackgroundRepeat", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("MIMEType", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Source", typeof(BackgroundImageTypeSource))]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType4[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum BackgroundImageTypeSource {
        
        /// <remarks/>
        External,
        
        /// <remarks/>
        Embedded,
        
        /// <remarks/>
        Database,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType4 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        BackgroundRepeat,
        
        /// <remarks/>
        MIMEType,
        
        /// <remarks/>
        Source,
        
        /// <remarks/>
        Value,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class BorderColorStyleWidthType {
        
        private object[] itemsField;
        
        private ItemsChoiceType3[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Bottom", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Default", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Right", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType3[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType3 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Bottom,
        
        /// <remarks/>
        Default,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        Right,
        
        /// <remarks/>
        Top,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType5 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        BackgroundColor,
        
        /// <remarks/>
        BackgroundGradientEndColor,
        
        /// <remarks/>
        BackgroundGradientType,
        
        /// <remarks/>
        BackgroundImage,
        
        /// <remarks/>
        BorderColor,
        
        /// <remarks/>
        BorderStyle,
        
        /// <remarks/>
        BorderWidth,
        
        /// <remarks/>
        Calendar,
        
        /// <remarks/>
        Color,
        
        /// <remarks/>
        Direction,
        
        /// <remarks/>
        FontFamily,
        
        /// <remarks/>
        FontSize,
        
        /// <remarks/>
        FontStyle,
        
        /// <remarks/>
        FontWeight,
        
        /// <remarks/>
        Format,
        
        /// <remarks/>
        Language,
        
        /// <remarks/>
        LineHeight,
        
        /// <remarks/>
        NumeralLanguage,
        
        /// <remarks/>
        NumeralVariant,
        
        /// <remarks/>
        PaddingBottom,
        
        /// <remarks/>
        PaddingLeft,
        
        /// <remarks/>
        PaddingRight,
        
        /// <remarks/>
        PaddingTop,
        
        /// <remarks/>
        TextAlign,
        
        /// <remarks/>
        TextDecoration,
        
        /// <remarks/>
        UnicodeBiDi,
        
        /// <remarks/>
        VerticalAlign,
        
        /// <remarks/>
        WritingMode,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType34 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        PrintOnFirstPage,
        
        /// <remarks/>
        PrintOnLastPage,
        
        /// <remarks/>
        ReportItems,
        
        /// <remarks/>
        Style,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ParameterValueType {
        
        private object[] itemsField;
        
        private ItemsChoiceType32[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType32[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType32 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Value,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ParameterValuesType {
        
        private ParameterValueType[] parameterValueField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ParameterValue")]
        public ParameterValueType[] ParameterValue {
            get {
                return this.parameterValueField;
            }
            set {
                this.parameterValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ValidValuesType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DataSetReference", typeof(DataSetReferenceType))]
        [System.Xml.Serialization.XmlElementAttribute("ParameterValues", typeof(ParameterValuesType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataSetReferenceType {
        
        private object[] itemsField;
        
        private ItemsChoiceType31[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DataSetName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("LabelField", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("ValueField", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType31[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType31 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        DataSetName,
        
        /// <remarks/>
        LabelField,
        
        /// <remarks/>
        ValueField,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ValuesType {
        
        private string[] valueField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Value")]
        public string[] Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DefaultValueType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DataSetReference", typeof(DataSetReferenceType))]
        [System.Xml.Serialization.XmlElementAttribute("Values", typeof(ValuesType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ReportParameterType {
        
        private object[] itemsField;
        
        private ItemsChoiceType33[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("AllowBlank", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("DataType", typeof(ReportParameterTypeDataType))]
        [System.Xml.Serialization.XmlElementAttribute("DefaultValue", typeof(DefaultValueType))]
        [System.Xml.Serialization.XmlElementAttribute("Hidden", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("MultiValue", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Nullable", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Prompt", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("UsedInQuery", typeof(ReportParameterTypeUsedInQuery))]
        [System.Xml.Serialization.XmlElementAttribute("ValidValues", typeof(ValidValuesType))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType33[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ReportParameterTypeDataType {
        
        /// <remarks/>
        Boolean,
        
        /// <remarks/>
        DateTime,
        
        /// <remarks/>
        Integer,
        
        /// <remarks/>
        Float,
        
        /// <remarks/>
        String,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ReportParameterTypeUsedInQuery {
        
        /// <remarks/>
        False,
        
        /// <remarks/>
        True,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType33 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        AllowBlank,
        
        /// <remarks/>
        DataType,
        
        /// <remarks/>
        DefaultValue,
        
        /// <remarks/>
        Hidden,
        
        /// <remarks/>
        MultiValue,
        
        /// <remarks/>
        Nullable,
        
        /// <remarks/>
        Prompt,
        
        /// <remarks/>
        UsedInQuery,
        
        /// <remarks/>
        ValidValues,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ReportParametersType {
        
        private ReportParameterType[] reportParameterField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReportParameter")]
        public ReportParameterType[] ReportParameter {
            get {
                return this.reportParameterField;
            }
            set {
                this.reportParameterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataCellType {
        
        private DataValueType[] dataValueField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataValue")]
        public DataValueType[] DataValue {
            get {
                return this.dataValueField;
            }
            set {
                this.dataValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataValueType {
        
        private object[] itemsField;
        
        private ItemsChoiceType22[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Name", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType22[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType22 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Name,
        
        /// <remarks/>
        Value,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataRowType {
        
        private DataCellType[] dataCellField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataCell")]
        public DataCellType[] DataCell {
            get {
                return this.dataCellField;
            }
            set {
                this.dataCellField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataRowsType {
        
        private DataRowType[] dataRowField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataRow")]
        public DataRowType[] DataRow {
            get {
                return this.dataRowField;
            }
            set {
                this.dataRowField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataRowGroupingsType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DataGroupings", typeof(DataGroupingsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataGroupingsType {
        
        private DataGroupingType[] dataGroupingField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataGrouping")]
        public DataGroupingType[] DataGrouping {
            get {
                return this.dataGroupingField;
            }
            set {
                this.dataGroupingField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataGroupingType {
        
        private object[] itemsField;
        
        private ItemsChoiceType28[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataGroupings", typeof(DataGroupingsType))]
        [System.Xml.Serialization.XmlElementAttribute("Grouping", typeof(GroupingType))]
        [System.Xml.Serialization.XmlElementAttribute("Sorting", typeof(SortingType))]
        [System.Xml.Serialization.XmlElementAttribute("Static", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Subtotal", typeof(bool))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType28[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class CustomPropertiesType {
        
        private CustomPropertyType[] customPropertyField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CustomProperty")]
        public CustomPropertyType[] CustomProperty {
            get {
                return this.customPropertyField;
            }
            set {
                this.customPropertyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class CustomPropertyType {
        
        private object[] itemsField;
        
        private ItemsChoiceType10[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Name", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType10[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType10 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Name,
        
        /// <remarks/>
        Value,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class GroupingType {
        
        private object[] itemsField;
        
        private ItemsChoiceType17[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataCollectionName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(GroupingTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("Filters", typeof(FiltersType))]
        [System.Xml.Serialization.XmlElementAttribute("GroupExpressions", typeof(GroupExpressionsType))]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtEnd", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtStart", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Parent", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType17[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum GroupingTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class FiltersType {
        
        private FilterType[] filterField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Filter")]
        public FilterType[] Filter {
            get {
                return this.filterField;
            }
            set {
                this.filterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class FilterType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("FilterExpression", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("FilterValues", typeof(FilterValuesType))]
        [System.Xml.Serialization.XmlElementAttribute("Operator", typeof(FilterTypeOperator))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class FilterValuesType {
        
        private string[] filterValueField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FilterValue")]
        public string[] FilterValue {
            get {
                return this.filterValueField;
            }
            set {
                this.filterValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum FilterTypeOperator {
        
        /// <remarks/>
        Equal,
        
        /// <remarks/>
        Like,
        
        /// <remarks/>
        NotEqual,
        
        /// <remarks/>
        GreaterThan,
        
        /// <remarks/>
        GreaterThanOrEqual,
        
        /// <remarks/>
        LessThan,
        
        /// <remarks/>
        LessThanOrEqual,
        
        /// <remarks/>
        TopN,
        
        /// <remarks/>
        BottomN,
        
        /// <remarks/>
        TopPercent,
        
        /// <remarks/>
        BottomPercent,
        
        /// <remarks/>
        In,
        
        /// <remarks/>
        Between,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class GroupExpressionsType {
        
        private string[] groupExpressionField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GroupExpression")]
        public string[] GroupExpression {
            get {
                return this.groupExpressionField;
            }
            set {
                this.groupExpressionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType17 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataCollectionName,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        Filters,
        
        /// <remarks/>
        GroupExpressions,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        PageBreakAtEnd,
        
        /// <remarks/>
        PageBreakAtStart,
        
        /// <remarks/>
        Parent,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class SortingType {
        
        private SortByType[] sortByField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SortBy")]
        public SortByType[] SortBy {
            get {
                return this.sortByField;
            }
            set {
                this.sortByField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class SortByType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Direction", typeof(SortByTypeDirection))]
        [System.Xml.Serialization.XmlElementAttribute("SortExpression", typeof(string))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum SortByTypeDirection {
        
        /// <remarks/>
        Ascending,
        
        /// <remarks/>
        Descending,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType28 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataGroupings,
        
        /// <remarks/>
        Grouping,
        
        /// <remarks/>
        Sorting,
        
        /// <remarks/>
        Static,
        
        /// <remarks/>
        Subtotal,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataColumnGroupingsType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DataGroupings", typeof(DataGroupingsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class CustomDataType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DataColumnGroupings", typeof(DataColumnGroupingsType))]
        [System.Xml.Serialization.XmlElementAttribute("DataRowGroupings", typeof(DataRowGroupingsType))]
        [System.Xml.Serialization.XmlElementAttribute("DataRows", typeof(DataRowsType))]
        [System.Xml.Serialization.XmlElementAttribute("DataSetName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Filters", typeof(FiltersType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class CustomReportItemType {
        
        private object[] itemsField;
        
        private ItemsChoiceType29[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("AltReportItem", typeof(ReportItemsType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CustomData", typeof(CustomDataType))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(CustomReportItemTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("RepeatWith", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Type", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType29[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum CustomReportItemTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class VisibilityType {
        
        private object[] itemsField;
        
        private ItemsChoiceType9[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Hidden", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("ToggleItem", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType9[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType9 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Hidden,
        
        /// <remarks/>
        ToggleItem,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType29 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        AltReportItem,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CustomData,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        RepeatWith,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        Type,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class PlotAreaType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ThreeDPropertiesType {
        
        private object[] itemsField;
        
        private ItemsChoiceType26[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Clustered", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("DepthRatio", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("DrawingStyle", typeof(ThreeDPropertiesTypeDrawingStyle))]
        [System.Xml.Serialization.XmlElementAttribute("Enabled", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("GapDepth", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("HeightRatio", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("Inclination", typeof(string), DataType="integer")]
        [System.Xml.Serialization.XmlElementAttribute("Perspective", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("ProjectionMode", typeof(ThreeDPropertiesTypeProjectionMode))]
        [System.Xml.Serialization.XmlElementAttribute("Rotation", typeof(string), DataType="integer")]
        [System.Xml.Serialization.XmlElementAttribute("Shading", typeof(ThreeDPropertiesTypeShading))]
        [System.Xml.Serialization.XmlElementAttribute("WallThickness", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType26[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ThreeDPropertiesTypeDrawingStyle {
        
        /// <remarks/>
        Cube,
        
        /// <remarks/>
        Cylinder,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ThreeDPropertiesTypeProjectionMode {
        
        /// <remarks/>
        Perspective,
        
        /// <remarks/>
        Orthographic,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ThreeDPropertiesTypeShading {
        
        /// <remarks/>
        None,
        
        /// <remarks/>
        Simple,
        
        /// <remarks/>
        Real,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType26 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Clustered,
        
        /// <remarks/>
        DepthRatio,
        
        /// <remarks/>
        DrawingStyle,
        
        /// <remarks/>
        Enabled,
        
        /// <remarks/>
        GapDepth,
        
        /// <remarks/>
        HeightRatio,
        
        /// <remarks/>
        Inclination,
        
        /// <remarks/>
        Perspective,
        
        /// <remarks/>
        ProjectionMode,
        
        /// <remarks/>
        Rotation,
        
        /// <remarks/>
        Shading,
        
        /// <remarks/>
        WallThickness,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ValueAxisType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Axis", typeof(AxisType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class AxisType {
        
        private object[] itemsField;
        
        private ItemsChoiceType25[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("CrossAt", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Interlaced", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("LogScale", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("MajorGridLines", typeof(MajorGridLinesType))]
        [System.Xml.Serialization.XmlElementAttribute("MajorInterval", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("MajorTickMarks", typeof(AxisTypeMajorTickMarks))]
        [System.Xml.Serialization.XmlElementAttribute("Margin", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Max", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Min", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("MinorGridLines", typeof(MinorGridLinesType))]
        [System.Xml.Serialization.XmlElementAttribute("MinorInterval", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("MinorTickMarks", typeof(AxisTypeMinorTickMarks))]
        [System.Xml.Serialization.XmlElementAttribute("Reverse", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Scalar", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("Title", typeof(TitleType))]
        [System.Xml.Serialization.XmlElementAttribute("Visible", typeof(bool))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType25[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MajorGridLinesType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ShowGridLines", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum AxisTypeMajorTickMarks {
        
        /// <remarks/>
        None,
        
        /// <remarks/>
        Inside,
        
        /// <remarks/>
        Outside,
        
        /// <remarks/>
        Cross,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MinorGridLinesType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ShowGridLines", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum AxisTypeMinorTickMarks {
        
        /// <remarks/>
        None,
        
        /// <remarks/>
        Inside,
        
        /// <remarks/>
        Outside,
        
        /// <remarks/>
        Cross,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TitleType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Caption", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Position", typeof(TitleTypePosition))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum TitleTypePosition {
        
        /// <remarks/>
        Center,
        
        /// <remarks/>
        Near,
        
        /// <remarks/>
        Far,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType25 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        CrossAt,
        
        /// <remarks/>
        Interlaced,
        
        /// <remarks/>
        LogScale,
        
        /// <remarks/>
        MajorGridLines,
        
        /// <remarks/>
        MajorInterval,
        
        /// <remarks/>
        MajorTickMarks,
        
        /// <remarks/>
        Margin,
        
        /// <remarks/>
        Max,
        
        /// <remarks/>
        Min,
        
        /// <remarks/>
        MinorGridLines,
        
        /// <remarks/>
        MinorInterval,
        
        /// <remarks/>
        MinorTickMarks,
        
        /// <remarks/>
        Reverse,
        
        /// <remarks/>
        Scalar,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        Title,
        
        /// <remarks/>
        Visible,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class CategoryAxisType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Axis", typeof(AxisType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class LegendType {
        
        private object[] itemsField;
        
        private ItemsChoiceType24[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("InsidePlotArea", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Layout", typeof(LegendTypeLayout))]
        [System.Xml.Serialization.XmlElementAttribute("Position", typeof(LegendTypePosition))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("Visible", typeof(bool))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType24[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum LegendTypeLayout {
        
        /// <remarks/>
        Column,
        
        /// <remarks/>
        Row,
        
        /// <remarks/>
        Table,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum LegendTypePosition {
        
        /// <remarks/>
        TopLeft,
        
        /// <remarks/>
        TopCenter,
        
        /// <remarks/>
        TopRight,
        
        /// <remarks/>
        LeftTop,
        
        /// <remarks/>
        LeftCenter,
        
        /// <remarks/>
        LeftBottom,
        
        /// <remarks/>
        RightTop,
        
        /// <remarks/>
        RightCenter,
        
        /// <remarks/>
        RightBottom,
        
        /// <remarks/>
        BottomLeft,
        
        /// <remarks/>
        BottomCenter,
        
        /// <remarks/>
        BottomRight,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType24 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        InsidePlotArea,
        
        /// <remarks/>
        Layout,
        
        /// <remarks/>
        Position,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        Visible,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MarkerType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Size", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("Type", typeof(MarkerTypeType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum MarkerTypeType {
        
        /// <remarks/>
        None,
        
        /// <remarks/>
        Square,
        
        /// <remarks/>
        Circle,
        
        /// <remarks/>
        Diamond,
        
        /// <remarks/>
        Triangle,
        
        /// <remarks/>
        Cross,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataLabelType {
        
        private object[] itemsField;
        
        private ItemsChoiceType23[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Position", typeof(DataLabelTypePosition))]
        [System.Xml.Serialization.XmlElementAttribute("Rotation", typeof(string), DataType="integer")]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Visible", typeof(bool))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType23[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum DataLabelTypePosition {
        
        /// <remarks/>
        Auto,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        TopLeft,
        
        /// <remarks/>
        TopRight,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        Center,
        
        /// <remarks/>
        Right,
        
        /// <remarks/>
        BottomLeft,
        
        /// <remarks/>
        Bottom,
        
        /// <remarks/>
        BottomRight,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType23 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Position,
        
        /// <remarks/>
        Rotation,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        Value,
        
        /// <remarks/>
        Visible,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataValuesType {
        
        private DataValueType[] dataValueField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataValue")]
        public DataValueType[] DataValue {
            get {
                return this.dataValueField;
            }
            set {
                this.dataValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataPointType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(DataPointTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("DataLabel", typeof(DataLabelType))]
        [System.Xml.Serialization.XmlElementAttribute("DataValues", typeof(DataValuesType))]
        [System.Xml.Serialization.XmlElementAttribute("Marker", typeof(MarkerType))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum DataPointTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataPointsType {
        
        private DataPointType[] dataPointField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataPoint")]
        public DataPointType[] DataPoint {
            get {
                return this.dataPointField;
            }
            set {
                this.dataPointField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ChartSeriesType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DataPoints", typeof(DataPointsType))]
        [System.Xml.Serialization.XmlElementAttribute("PlotType", typeof(ChartSeriesTypePlotType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ChartSeriesTypePlotType {
        
        /// <remarks/>
        Auto,
        
        /// <remarks/>
        Line,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ChartDataType {
        
        private ChartSeriesType[] chartSeriesField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ChartSeries")]
        public ChartSeriesType[] ChartSeries {
            get {
                return this.chartSeriesField;
            }
            set {
                this.chartSeriesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class StaticCategoriesType {
        
        private StaticMemberType[] staticMemberField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StaticMember")]
        public StaticMemberType[] StaticMember {
            get {
                return this.staticMemberField;
            }
            set {
                this.staticMemberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class StaticMemberType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DynamicCategoriesType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Grouping", typeof(GroupingType))]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Sorting", typeof(SortingType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class CategoryGroupingType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DynamicCategories", typeof(DynamicCategoriesType))]
        [System.Xml.Serialization.XmlElementAttribute("StaticCategories", typeof(StaticCategoriesType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class CategoryGroupingsType {
        
        private CategoryGroupingType[] categoryGroupingField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CategoryGrouping")]
        public CategoryGroupingType[] CategoryGrouping {
            get {
                return this.categoryGroupingField;
            }
            set {
                this.categoryGroupingField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class StaticSeriesType {
        
        private StaticMemberType[] staticMemberField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StaticMember")]
        public StaticMemberType[] StaticMember {
            get {
                return this.staticMemberField;
            }
            set {
                this.staticMemberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DynamicSeriesType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Grouping", typeof(GroupingType))]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Sorting", typeof(SortingType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class SeriesGroupingType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DynamicSeries", typeof(DynamicSeriesType))]
        [System.Xml.Serialization.XmlElementAttribute("StaticSeries", typeof(StaticSeriesType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class SeriesGroupingsType {
        
        private SeriesGroupingType[] seriesGroupingField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SeriesGrouping")]
        public SeriesGroupingType[] SeriesGrouping {
            get {
                return this.seriesGroupingField;
            }
            set {
                this.seriesGroupingField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DetailsType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Grouping", typeof(GroupingType))]
        [System.Xml.Serialization.XmlElementAttribute("Sorting", typeof(SortingType))]
        [System.Xml.Serialization.XmlElementAttribute("TableRows", typeof(TableRowsType))]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TableRowsType {
        
        private TableRowType[] tableRowField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TableRow")]
        public TableRowType[] TableRow {
            get {
                return this.tableRowField;
            }
            set {
                this.tableRowField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TableRowType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("TableCells", typeof(TableCellsType))]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TableCellsType {
        
        private TableCellType[] tableCellField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TableCell")]
        public TableCellType[] TableCell {
            get {
                return this.tableCellField;
            }
            set {
                this.tableCellField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TableCellType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ColSpan", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class FooterType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("RepeatOnNewPage", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("TableRows", typeof(TableRowsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TableGroupType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Footer", typeof(FooterType))]
        [System.Xml.Serialization.XmlElementAttribute("Grouping", typeof(GroupingType))]
        [System.Xml.Serialization.XmlElementAttribute("Header", typeof(HeaderType))]
        [System.Xml.Serialization.XmlElementAttribute("Sorting", typeof(SortingType))]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class HeaderType {
        
        private object[] itemsField;
        
        private ItemsChoiceType20[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("FixedHeader", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("RepeatOnNewPage", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("TableRows", typeof(TableRowsType))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType20[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType20 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        FixedHeader,
        
        /// <remarks/>
        RepeatOnNewPage,
        
        /// <remarks/>
        TableRows,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TableGroupsType {
        
        private TableGroupType[] tableGroupField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TableGroup")]
        public TableGroupType[] TableGroup {
            get {
                return this.tableGroupField;
            }
            set {
                this.tableGroupField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TableColumnType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("FixedHeader", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TableColumnsType {
        
        private TableColumnType[] tableColumnField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TableColumn")]
        public TableColumnType[] TableColumn {
            get {
                return this.tableColumnField;
            }
            set {
                this.tableColumnField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TableType {
        
        private object[] itemsField;
        
        private ItemsChoiceType21[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(TableTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("DataSetName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DetailDataCollectionName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DetailDataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DetailDataElementOutput", typeof(TableTypeDetailDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("Details", typeof(DetailsType))]
        [System.Xml.Serialization.XmlElementAttribute("FillPage", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Filters", typeof(FiltersType))]
        [System.Xml.Serialization.XmlElementAttribute("Footer", typeof(FooterType))]
        [System.Xml.Serialization.XmlElementAttribute("Header", typeof(HeaderType))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("KeepTogether", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("LinkToChild", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("NoRows", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtEnd", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtStart", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("RepeatWith", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("TableColumns", typeof(TableColumnsType))]
        [System.Xml.Serialization.XmlElementAttribute("TableGroups", typeof(TableGroupsType))]
        [System.Xml.Serialization.XmlElementAttribute("ToolTip", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType21[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum TableTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum TableTypeDetailDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType21 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Action,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        DataSetName,
        
        /// <remarks/>
        DetailDataCollectionName,
        
        /// <remarks/>
        DetailDataElementName,
        
        /// <remarks/>
        DetailDataElementOutput,
        
        /// <remarks/>
        Details,
        
        /// <remarks/>
        FillPage,
        
        /// <remarks/>
        Filters,
        
        /// <remarks/>
        Footer,
        
        /// <remarks/>
        Header,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        KeepTogether,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        LinkToChild,
        
        /// <remarks/>
        NoRows,
        
        /// <remarks/>
        PageBreakAtEnd,
        
        /// <remarks/>
        PageBreakAtStart,
        
        /// <remarks/>
        RepeatWith,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        TableColumns,
        
        /// <remarks/>
        TableGroups,
        
        /// <remarks/>
        ToolTip,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MatrixColumnType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MatrixColumnsType {
        
        private MatrixColumnType[] matrixColumnField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MatrixColumn")]
        public MatrixColumnType[] MatrixColumn {
            get {
                return this.matrixColumnField;
            }
            set {
                this.matrixColumnField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MatrixCellType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MatrixCellsType {
        
        private MatrixCellType[] matrixCellField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MatrixCell")]
        public MatrixCellType[] MatrixCell {
            get {
                return this.matrixCellField;
            }
            set {
                this.matrixCellField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MatrixRowType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("MatrixCells", typeof(MatrixCellsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MatrixRowsType {
        
        private MatrixRowType[] matrixRowField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MatrixRow")]
        public MatrixRowType[] MatrixRow {
            get {
                return this.matrixRowField;
            }
            set {
                this.matrixRowField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class StaticRowType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class StaticRowsType {
        
        private StaticRowType[] staticRowField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StaticRow")]
        public StaticRowType[] StaticRow {
            get {
                return this.staticRowField;
            }
            set {
                this.staticRowField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class RowGroupingType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DynamicRows", typeof(DynamicColumnsRowsType))]
        [System.Xml.Serialization.XmlElementAttribute("FixedHeader", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("StaticRows", typeof(StaticRowsType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DynamicColumnsRowsType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Grouping", typeof(GroupingType))]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        [System.Xml.Serialization.XmlElementAttribute("Sorting", typeof(SortingType))]
        [System.Xml.Serialization.XmlElementAttribute("Subtotal", typeof(SubtotalType))]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class SubtotalType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(SubtotalTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("Position", typeof(SubtotalTypePosition))]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum SubtotalTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum SubtotalTypePosition {
        
        /// <remarks/>
        Before,
        
        /// <remarks/>
        After,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class RowGroupingsType {
        
        private RowGroupingType[] rowGroupingField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RowGrouping")]
        public RowGroupingType[] RowGrouping {
            get {
                return this.rowGroupingField;
            }
            set {
                this.rowGroupingField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class StaticColumnType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class StaticColumnsType {
        
        private StaticColumnType[] staticColumnField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("StaticColumn")]
        public StaticColumnType[] StaticColumn {
            get {
                return this.staticColumnField;
            }
            set {
                this.staticColumnField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ColumnGroupingType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DynamicColumns", typeof(DynamicColumnsRowsType))]
        [System.Xml.Serialization.XmlElementAttribute("FixedHeader", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("StaticColumns", typeof(StaticColumnsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ColumnGroupingsType {
        
        private ColumnGroupingType[] columnGroupingField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ColumnGrouping")]
        public ColumnGroupingType[] ColumnGrouping {
            get {
                return this.columnGroupingField;
            }
            set {
                this.columnGroupingField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class CornerType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class MatrixType {
        
        private object[] itemsField;
        
        private ItemsChoiceType19[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CellDataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CellDataElementOutput", typeof(MatrixTypeCellDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("ColumnGroupings", typeof(ColumnGroupingsType))]
        [System.Xml.Serialization.XmlElementAttribute("Corner", typeof(CornerType))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(MatrixTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("DataSetName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Filters", typeof(FiltersType))]
        [System.Xml.Serialization.XmlElementAttribute("GroupsBeforeRowHeaders", typeof(uint))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("KeepTogether", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("LayoutDirection", typeof(MatrixTypeLayoutDirection))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("LinkToChild", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("MatrixColumns", typeof(MatrixColumnsType))]
        [System.Xml.Serialization.XmlElementAttribute("MatrixRows", typeof(MatrixRowsType))]
        [System.Xml.Serialization.XmlElementAttribute("NoRows", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtEnd", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtStart", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("RepeatWith", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("RowGroupings", typeof(RowGroupingsType))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("ToolTip", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType19[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum MatrixTypeCellDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum MatrixTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum MatrixTypeLayoutDirection {
        
        /// <remarks/>
        LTR,
        
        /// <remarks/>
        RTL,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType19 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Action,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CellDataElementName,
        
        /// <remarks/>
        CellDataElementOutput,
        
        /// <remarks/>
        ColumnGroupings,
        
        /// <remarks/>
        Corner,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        DataSetName,
        
        /// <remarks/>
        Filters,
        
        /// <remarks/>
        GroupsBeforeRowHeaders,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        KeepTogether,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        LayoutDirection,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        LinkToChild,
        
        /// <remarks/>
        MatrixColumns,
        
        /// <remarks/>
        MatrixRows,
        
        /// <remarks/>
        NoRows,
        
        /// <remarks/>
        PageBreakAtEnd,
        
        /// <remarks/>
        PageBreakAtStart,
        
        /// <remarks/>
        RepeatWith,
        
        /// <remarks/>
        RowGroupings,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        ToolTip,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ListType {
        
        private object[] itemsField;
        
        private ItemsChoiceType18[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(ListTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("DataInstanceElementOutput", typeof(ListTypeDataInstanceElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("DataInstanceName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataSetName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("FillPage", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Filters", typeof(FiltersType))]
        [System.Xml.Serialization.XmlElementAttribute("Grouping", typeof(GroupingType))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("KeepTogether", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("LinkToChild", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("NoRows", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtEnd", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtStart", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("RepeatWith", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        [System.Xml.Serialization.XmlElementAttribute("Sorting", typeof(SortingType))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("ToolTip", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType18[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ListTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ListTypeDataInstanceElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType18 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Action,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        DataInstanceElementOutput,
        
        /// <remarks/>
        DataInstanceName,
        
        /// <remarks/>
        DataSetName,
        
        /// <remarks/>
        FillPage,
        
        /// <remarks/>
        Filters,
        
        /// <remarks/>
        Grouping,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        KeepTogether,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        LinkToChild,
        
        /// <remarks/>
        NoRows,
        
        /// <remarks/>
        PageBreakAtEnd,
        
        /// <remarks/>
        PageBreakAtStart,
        
        /// <remarks/>
        RepeatWith,
        
        /// <remarks/>
        ReportItems,
        
        /// <remarks/>
        Sorting,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        ToolTip,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class SubreportType {
        
        private object[] itemsField;
        
        private ItemsChoiceType16[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(SubreportTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("LinkToChild", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("MergeTransactions", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("NoRows", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Parameters", typeof(ParametersType))]
        [System.Xml.Serialization.XmlElementAttribute("RepeatWith", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("ReportName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("ToolTip", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType16[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum SubreportTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType16 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Action,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        LinkToChild,
        
        /// <remarks/>
        MergeTransactions,
        
        /// <remarks/>
        NoRows,
        
        /// <remarks/>
        Parameters,
        
        /// <remarks/>
        RepeatWith,
        
        /// <remarks/>
        ReportName,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        ToolTip,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ImageType {
        
        private object[] itemsField;
        
        private ItemsChoiceType15[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(ImageTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("LinkToChild", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("MIMEType", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("RepeatWith", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Sizing", typeof(ImageTypeSizing))]
        [System.Xml.Serialization.XmlElementAttribute("Source", typeof(ImageTypeSource))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("ToolTip", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType15[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ImageTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ImageTypeSizing {
        
        /// <remarks/>
        AutoSize,
        
        /// <remarks/>
        Fit,
        
        /// <remarks/>
        FitProportional,
        
        /// <remarks/>
        Clip,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ImageTypeSource {
        
        /// <remarks/>
        External,
        
        /// <remarks/>
        Embedded,
        
        /// <remarks/>
        Database,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType15 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Action,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        LinkToChild,
        
        /// <remarks/>
        MIMEType,
        
        /// <remarks/>
        RepeatWith,
        
        /// <remarks/>
        Sizing,
        
        /// <remarks/>
        Source,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        ToolTip,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        Value,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class UserSortType {
        
        private object[] itemsField;
        
        private ItemsChoiceType13[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("SortExpression", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("SortExpressionScope", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("SortTarget", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType13[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType13 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        SortExpression,
        
        /// <remarks/>
        SortExpressionScope,
        
        /// <remarks/>
        SortTarget,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ToggleImageType {
        
        private object[] itemsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("InitialState", typeof(string))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class TextboxType {
        
        private object[] itemsField;
        
        private ItemsChoiceType14[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CanGrow", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("CanShrink", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(TextboxTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementStyle", typeof(TextboxTypeDataElementStyle))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("HideDuplicates", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("LinkToChild", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("RepeatWith", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("ToggleImage", typeof(ToggleImageType))]
        [System.Xml.Serialization.XmlElementAttribute("ToolTip", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("UserSort", typeof(UserSortType))]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType14[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum TextboxTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum TextboxTypeDataElementStyle {
        
        /// <remarks/>
        Auto,
        
        /// <remarks/>
        AttributeNormal,
        
        /// <remarks/>
        ElementNormal,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType14 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Action,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CanGrow,
        
        /// <remarks/>
        CanShrink,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        DataElementStyle,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        HideDuplicates,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        LinkToChild,
        
        /// <remarks/>
        RepeatWith,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        ToggleImage,
        
        /// <remarks/>
        ToolTip,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        UserSort,
        
        /// <remarks/>
        Value,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class RectangleType {
        
        private object[] itemsField;
        
        private ItemsChoiceType12[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(RectangleTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("LinkToChild", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtEnd", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("PageBreakAtStart", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("RepeatWith", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("ReportItems", typeof(ReportItemsType))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("ToolTip", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType12[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum RectangleTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType12 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Action,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        LinkToChild,
        
        /// <remarks/>
        PageBreakAtEnd,
        
        /// <remarks/>
        PageBreakAtStart,
        
        /// <remarks/>
        RepeatWith,
        
        /// <remarks/>
        ReportItems,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        ToolTip,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class LineType {
        
        private object[] itemsField;
        
        private ItemsChoiceType11[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Action", typeof(ActionType))]
        [System.Xml.Serialization.XmlElementAttribute("Bookmark", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CustomProperties", typeof(CustomPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataElementOutput", typeof(LineTypeDataElementOutput))]
        [System.Xml.Serialization.XmlElementAttribute("Height", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Label", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Left", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("LinkToChild", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("RepeatWith", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Style", typeof(StyleType))]
        [System.Xml.Serialization.XmlElementAttribute("ToolTip", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Top", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("Visibility", typeof(VisibilityType))]
        [System.Xml.Serialization.XmlElementAttribute("Width", typeof(string), DataType="normalizedString")]
        [System.Xml.Serialization.XmlElementAttribute("ZIndex", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType11[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum LineTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType11 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Action,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        LinkToChild,
        
        /// <remarks/>
        RepeatWith,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        ToolTip,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class QueryParameterType {
        
        private object[] itemsField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class QueryParametersType {
        
        private QueryParameterType[] queryParameterField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("QueryParameter")]
        public QueryParameterType[] QueryParameter {
            get {
                return this.queryParameterField;
            }
            set {
                this.queryParameterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class QueryType {
        
        private object[] itemsField;
        
        private ItemsChoiceType2[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("CommandText", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("CommandType", typeof(QueryTypeCommandType))]
        [System.Xml.Serialization.XmlElementAttribute("DataSourceName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("QueryParameters", typeof(QueryParametersType))]
        [System.Xml.Serialization.XmlElementAttribute("Timeout", typeof(uint))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType2[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum QueryTypeCommandType {
        
        /// <remarks/>
        Text,
        
        /// <remarks/>
        StoredProcedure,
        
        /// <remarks/>
        TableDirect,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType2 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        CommandText,
        
        /// <remarks/>
        CommandType,
        
        /// <remarks/>
        DataSourceName,
        
        /// <remarks/>
        QueryParameters,
        
        /// <remarks/>
        Timeout,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class FieldType {
        
        private object[] itemsField;
        
        private ItemsChoiceType1[] itemsElementNameField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("DataField", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Value", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType1[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType1 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        DataField,
        
        /// <remarks/>
        Value,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class FieldsType {
        
        private FieldType[] fieldField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Field")]
        public FieldType[] Field {
            get {
                return this.fieldField;
            }
            set {
                this.fieldField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataSetType {
        
        private object[] itemsField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("AccentSensitivity", typeof(DataSetTypeAccentSensitivity))]
        [System.Xml.Serialization.XmlElementAttribute("CaseSensitivity", typeof(DataSetTypeCaseSensitivity))]
        [System.Xml.Serialization.XmlElementAttribute("Collation", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Fields", typeof(FieldsType))]
        [System.Xml.Serialization.XmlElementAttribute("Filters", typeof(FiltersType))]
        [System.Xml.Serialization.XmlElementAttribute("KanatypeSensitivity", typeof(DataSetTypeKanatypeSensitivity))]
        [System.Xml.Serialization.XmlElementAttribute("Query", typeof(QueryType))]
        [System.Xml.Serialization.XmlElementAttribute("WidthSensitivity", typeof(DataSetTypeWidthSensitivity))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum DataSetTypeAccentSensitivity {
        
        /// <remarks/>
        True,
        
        /// <remarks/>
        False,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum DataSetTypeCaseSensitivity {
        
        /// <remarks/>
        True,
        
        /// <remarks/>
        False,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum DataSetTypeKanatypeSensitivity {
        
        /// <remarks/>
        True,
        
        /// <remarks/>
        False,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum DataSetTypeWidthSensitivity {
        
        /// <remarks/>
        True,
        
        /// <remarks/>
        False,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataSetsType {
        
        private DataSetType[] dataSetField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataSet")]
        public DataSetType[] DataSet {
            get {
                return this.dataSetField;
            }
            set {
                this.dataSetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class ConnectionPropertiesType {
        
        private object[] itemsField;
        
        private ItemsChoiceType[] itemsElementNameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ConnectString", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DataProvider", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("IntegratedSecurity", typeof(bool))]
        [System.Xml.Serialization.XmlElementAttribute("Prompt", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        ConnectString,
        
        /// <remarks/>
        DataProvider,
        
        /// <remarks/>
        IntegratedSecurity,
        
        /// <remarks/>
        Prompt,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataSourceType {
        
        private object[] itemsField;
        
        private string nameField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ConnectionProperties", typeof(ConnectionPropertiesType))]
        [System.Xml.Serialization.XmlElementAttribute("DataSourceReference", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Transaction", typeof(bool))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public partial class DataSourcesType {
        
        private DataSourceType[] dataSourceField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataSource")]
        public DataSourceType[] DataSource {
            get {
                return this.dataSourceField;
            }
            set {
                this.dataSourceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType7 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        BookmarkLink,
        
        /// <remarks/>
        Parameters,
        
        /// <remarks/>
        ReportName,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType8 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        BookmarkLink,
        
        /// <remarks/>
        Drillthrough,
        
        /// <remarks/>
        Hyperlink,
        
        /// <remarks/>
        Label,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ChartTypeChartElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ChartTypeDataElementOutput {
        
        /// <remarks/>
        Output,
        
        /// <remarks/>
        NoOutput,
        
        /// <remarks/>
        ContentsOnly,
        
        /// <remarks/>
        Auto,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ChartTypePalette {
        
        /// <remarks/>
        Default,
        
        /// <remarks/>
        EarthTones,
        
        /// <remarks/>
        Excel,
        
        /// <remarks/>
        GrayScale,
        
        /// <remarks/>
        Light,
        
        /// <remarks/>
        Pastel,
        
        /// <remarks/>
        SemiTransparent,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ChartTypeSubtype {
        
        /// <remarks/>
        Stacked,
        
        /// <remarks/>
        PercentStacked,
        
        /// <remarks/>
        Plain,
        
        /// <remarks/>
        Smooth,
        
        /// <remarks/>
        Exploded,
        
        /// <remarks/>
        Line,
        
        /// <remarks/>
        SmoothLine,
        
        /// <remarks/>
        HighLowClose,
        
        /// <remarks/>
        OpenHighLowClose,
        
        /// <remarks/>
        Candlestick,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ChartTypeType {
        
        /// <remarks/>
        Column,
        
        /// <remarks/>
        Bar,
        
        /// <remarks/>
        Line,
        
        /// <remarks/>
        Pie,
        
        /// <remarks/>
        Scatter,
        
        /// <remarks/>
        Bubble,
        
        /// <remarks/>
        Area,
        
        /// <remarks/>
        Doughnut,
        
        /// <remarks/>
        Stock,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType27 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Action,
        
        /// <remarks/>
        Bookmark,
        
        /// <remarks/>
        CategoryAxis,
        
        /// <remarks/>
        CategoryGroupings,
        
        /// <remarks/>
        ChartData,
        
        /// <remarks/>
        ChartElementOutput,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementOutput,
        
        /// <remarks/>
        DataSetName,
        
        /// <remarks/>
        Filters,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        KeepTogether,
        
        /// <remarks/>
        Label,
        
        /// <remarks/>
        Left,
        
        /// <remarks/>
        Legend,
        
        /// <remarks/>
        LinkToChild,
        
        /// <remarks/>
        NoRows,
        
        /// <remarks/>
        PageBreakAtEnd,
        
        /// <remarks/>
        PageBreakAtStart,
        
        /// <remarks/>
        Palette,
        
        /// <remarks/>
        PlotArea,
        
        /// <remarks/>
        PointWidth,
        
        /// <remarks/>
        SeriesGroupings,
        
        /// <remarks/>
        Style,
        
        /// <remarks/>
        Subtype,
        
        /// <remarks/>
        ThreeDProperties,
        
        /// <remarks/>
        Title,
        
        /// <remarks/>
        ToolTip,
        
        /// <remarks/>
        Top,
        
        /// <remarks/>
        Type,
        
        /// <remarks/>
        ValueAxis,
        
        /// <remarks/>
        Visibility,
        
        /// <remarks/>
        Width,
        
        /// <remarks/>
        ZIndex,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType30 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        ColumnSpacing,
        
        /// <remarks/>
        Columns,
        
        /// <remarks/>
        Height,
        
        /// <remarks/>
        ReportItems,
        
        /// <remarks/>
        Style,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ReportDataElementStyle {
        
        /// <remarks/>
        AttributeNormal,
        
        /// <remarks/>
        ElementNormal,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition", IncludeInSchema=false)]
    public enum ItemsChoiceType37 {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        
        /// <remarks/>
        Author,
        
        /// <remarks/>
        AutoRefresh,
        
        /// <remarks/>
        Body,
        
        /// <remarks/>
        BottomMargin,
        
        /// <remarks/>
        Classes,
        
        /// <remarks/>
        Code,
        
        /// <remarks/>
        CodeModules,
        
        /// <remarks/>
        CustomProperties,
        
        /// <remarks/>
        DataElementName,
        
        /// <remarks/>
        DataElementStyle,
        
        /// <remarks/>
        DataSchema,
        
        /// <remarks/>
        DataSets,
        
        /// <remarks/>
        DataSources,
        
        /// <remarks/>
        DataTransform,
        
        /// <remarks/>
        Description,
        
        /// <remarks/>
        EmbeddedImages,
        
        /// <remarks/>
        InteractiveHeight,
        
        /// <remarks/>
        InteractiveWidth,
        
        /// <remarks/>
        Language,
        
        /// <remarks/>
        LeftMargin,
        
        /// <remarks/>
        PageFooter,
        
        /// <remarks/>
        PageHeader,
        
        /// <remarks/>
        PageHeight,
        
        /// <remarks/>
        PageWidth,
        
        /// <remarks/>
        ReportParameters,
        
        /// <remarks/>
        RightMargin,
        
        /// <remarks/>
        TopMargin,
        
        /// <remarks/>
        Width,
    }
}
