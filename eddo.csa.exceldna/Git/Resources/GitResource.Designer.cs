﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eddo.csa.exceldna.Git.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class GitResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal GitResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("eddo.csa.exceldna.Git.Resources.GitResource", typeof(GitResource).Assembly);
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
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] alert_error {
            get {
                object obj = ResourceManager.GetObject("alert_error", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] alert_warn {
            get {
                object obj = ResourceManager.GetObject("alert_warn", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;customUI xmlns=&apos;http://schemas.microsoft.com/office/2009/07/customui&apos; loadImage=&apos;Image1&apos;&gt;
        /// &lt;ribbon&gt;
        ///  &lt;tabs&gt;
        ///   &lt;tab id=&apos;tab1&apos; label=&apos;FiBa CSA&apos;&gt;
        ///    &lt;group id=&apos;git1&apos; label=&apos;Git&apos;&gt;
        ///     &lt;button id=&apos;btnLoadLocalChanges&apos; label=&apos;Load local changes&apos; screentip=&apos;Load Local Changes from Git&apos; supertip=&apos;Click to load all local changes from selected branch&apos; onAction=&apos;OnbtnLoadLocalChangesPressed&apos; image=&apos;LoadLocalChanges&apos;/&gt;
        ///    &lt;/group&gt;
        ///    &lt;group id=&apos;git2&apos; label=&apos;Git Set [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string eddoRibbon {
            get {
                return ResourceManager.GetString("eddoRibbon", resourceCulture);
            }
        }
    }
}
