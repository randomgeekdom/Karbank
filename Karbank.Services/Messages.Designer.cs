﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Karbank.Services {
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
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Karbank.Services.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Your account has insufficient funds for this transaction..
        /// </summary>
        internal static string InsufficientFunds {
            get {
                return ResourceManager.GetString("InsufficientFunds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The account number you have entered is invalid..
        /// </summary>
        internal static string InvalidAccountNumber {
            get {
                return ResourceManager.GetString("InvalidAccountNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot withdraw or transfer funds from this account since you are not the owner..
        /// </summary>
        internal static string InvalidOwner {
            get {
                return ResourceManager.GetString("InvalidOwner", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Transaction amount must be greater than 0..
        /// </summary>
        internal static string TransactionGreaterThanZero {
            get {
                return ResourceManager.GetString("TransactionGreaterThanZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your transaction has completed successfully..
        /// </summary>
        internal static string TransactionSuccessful {
            get {
                return ResourceManager.GetString("TransactionSuccessful", resourceCulture);
            }
        }
    }
}
