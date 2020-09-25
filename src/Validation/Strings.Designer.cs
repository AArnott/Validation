﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Validation {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Validation.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; must contain at least one element..
        /// </summary>
        internal static string Argument_EmptyArray {
            get {
                return ResourceManager.GetString("Argument_EmptyArray", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; cannot be an empty string (&quot;&quot;) or start with the null character..
        /// </summary>
        internal static string Argument_EmptyString {
            get {
                return ResourceManager.GetString("Argument_EmptyString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; must be set to a value defined by the enum &apos;{1}&apos;..
        /// </summary>
        internal static string Argument_EnumNotDefined {
            get {
                return ResourceManager.GetString("Argument_EnumNotDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value of argument &apos;{0}&apos; ({1}) is invalid for Enum type &apos;{2}&apos;..
        /// </summary>
        internal static string Argument_NotEnum {
            get {
                return ResourceManager.GetString("Argument_NotEnum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; cannot contain a null (Nothing in Visual Basic) element..
        /// </summary>
        internal static string Argument_NullElement {
            get {
                return ResourceManager.GetString("Argument_NullElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; cannot be the default value defined by &apos;{1}&apos;..
        /// </summary>
        internal static string Argument_StructIsDefault {
            get {
                return ResourceManager.GetString("Argument_StructIsDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The argument cannot consist entirely of white space characters..
        /// </summary>
        internal static string Argument_Whitespace {
            get {
                return ResourceManager.GetString("Argument_Whitespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An internal error occurred. Please contact customer support..
        /// </summary>
        internal static string InternalExceptionMessage {
            get {
                return ResourceManager.GetString("InternalExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value of argument &apos;{0}&apos; ({1}) is invalid for Enum type &apos;{2}&apos;..
        /// </summary>
        internal static string InvalidEnumArgument {
            get {
                return ResourceManager.GetString("InvalidEnumArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot find an instance of the {0} service..
        /// </summary>
        internal static string ServiceMissing {
            get {
                return ResourceManager.GetString("ServiceMissing", resourceCulture);
            }
        }
    }
}
