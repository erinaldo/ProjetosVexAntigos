﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.586
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistran.Library {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class BaseAntigaConfig : global::System.Configuration.ApplicationSettingsBase {
        
        private static BaseAntigaConfig defaultInstance = ((BaseAntigaConfig)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new BaseAntigaConfig())));
        
        public static BaseAntigaConfig Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.10.9;Initial Catalog=Logos;User ID=site_ASP;Password=asp7998;" +
            "")]
        public string BDAntigo {
            get {
                return ((string)(this["BDAntigo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.10.4;Initial Catalog=GRUPOLOGOS;User ID=site_ASP;Password=asp" +
            "7998;")]
        public string BDNovoLogos {
            get {
                return ((string)(this["BDNovoLogos"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.10.5;Initial Catalog=stnnovo;User ID=site_aspTeste;Password=m" +
            "o139854;")]
        public string BDStnNovo {
            get {
                return ((string)(this["BDStnNovo"]));
            }
        }
    }
}
