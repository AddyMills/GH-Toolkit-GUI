﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GH_Toolkit_GUI {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.9.0.0")]
    internal sealed partial class UserPreferences : global::System.Configuration.ApplicationSettingsBase {
        
        private static UserPreferences defaultInstance = ((UserPreferences)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new UserPreferences())));
        
        public static UserPreferences Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public decimal PreviewFadeIn {
            get {
                return ((decimal)(this["PreviewFadeIn"]));
            }
            set {
                this["PreviewFadeIn"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public decimal PreviewFadeOut {
            get {
                return ((decimal)(this["PreviewFadeOut"]));
            }
            set {
                this["PreviewFadeOut"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool Gh3BackupNag {
            get {
                return ((bool)(this["Gh3BackupNag"]));
            }
            set {
                this["Gh3BackupNag"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool GhaBackupNag {
            get {
                return ((bool)(this["GhaBackupNag"]));
            }
            set {
                this["GhaBackupNag"] = value;
            }
        }
    }
}