﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.34014
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
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
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.10.4;Initial Catalog=GrupoLogos;User ID=sa;Password=@logos090" +
            "22005$;")]
        public string BDNovoLogos {
            get {
                return ((string)(this["BDNovoLogos"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.0.5;Initial Catalog=stnnovo;User ID=sa;Password=@oncetsis1212" +
            "2014;")]
        public string BDStnNovo {
            get {
                return ((string)(this["BDStnNovo"]));
            }
        }
    }
}