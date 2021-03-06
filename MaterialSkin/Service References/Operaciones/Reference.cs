﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaterialSkin.Operaciones {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AuthenticationType", Namespace="http://okibrasil.com/ws_webteller/types")]
    [System.SerializableAttribute()]
    public partial class AuthenticationType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string sLoginField;
        
        private string sPasswordField;
        
        private string sTerminalField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string sLogin {
            get {
                return this.sLoginField;
            }
            set {
                if ((object.ReferenceEquals(this.sLoginField, value) != true)) {
                    this.sLoginField = value;
                    this.RaisePropertyChanged("sLogin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string sPassword {
            get {
                return this.sPasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.sPasswordField, value) != true)) {
                    this.sPasswordField = value;
                    this.RaisePropertyChanged("sPassword");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string sTerminal {
            get {
                return this.sTerminalField;
            }
            set {
                if ((object.ReferenceEquals(this.sTerminalField, value) != true)) {
                    this.sTerminalField = value;
                    this.RaisePropertyChanged("sTerminal");
                }
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DataResponse", Namespace="http://schemas.datacontract.org/2004/07/WS_Webteller.OperationTypes")]
    [System.SerializableAttribute()]
    public partial class DataResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RetDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Code {
            get {
                return this.CodeField;
            }
            set {
                if ((this.CodeField.Equals(value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RetData {
            get {
                return this.RetDataField;
            }
            set {
                if ((object.ReferenceEquals(this.RetDataField, value) != true)) {
                    this.RetDataField = value;
                    this.RaisePropertyChanged("RetData");
                }
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://okibrasil.com/ws_service", ConfigurationName="Operaciones.IWebTeller")]
    public interface IWebTeller {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/InitTerminal", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/InitTerminalResponse")]
        MaterialSkin.Operaciones.DataResponse InitTerminal(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/InitTerminal", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/InitTerminalResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> InitTerminalAsync(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/CloseTerminal", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/CloseTerminalResponse")]
        MaterialSkin.Operaciones.DataResponse CloseTerminal(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/CloseTerminal", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/CloseTerminalResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> CloseTerminalAsync(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/StartDeposit", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/StartDepositResponse")]
        MaterialSkin.Operaciones.DataResponse StartDeposit(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/StartDeposit", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/StartDepositResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> StartDepositAsync(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/ContinueDeposit", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/ContinueDepositResponse")]
        MaterialSkin.Operaciones.DataResponse ContinueDeposit(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/ContinueDeposit", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/ContinueDepositResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> ContinueDepositAsync(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/EndDeposit", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/EndDepositResponse")]
        MaterialSkin.Operaciones.DataResponse EndDeposit(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/EndDeposit", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/EndDepositResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> EndDepositAsync(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/CancelDeposit", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/CancelDepositResponse")]
        MaterialSkin.Operaciones.DataResponse CancelDeposit(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/CancelDeposit", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/CancelDepositResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> CancelDepositAsync(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/Withdraw", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/WithdrawResponse")]
        MaterialSkin.Operaciones.DataResponse Withdraw(MaterialSkin.Operaciones.AuthenticationType authData, int iTotalValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/Withdraw", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/WithdrawResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> WithdrawAsync(MaterialSkin.Operaciones.AuthenticationType authData, int iTotalValue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/TransactionDayOpen", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/TransactionDayOpenResponse")]
        MaterialSkin.Operaciones.DataResponse TransactionDayOpen(MaterialSkin.Operaciones.AuthenticationType authData, int day);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/TransactionDayOpen", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/TransactionDayOpenResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> TransactionDayOpenAsync(MaterialSkin.Operaciones.AuthenticationType authData, int day);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/TransactionDayClose", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/TransactionDayCloseResponse")]
        MaterialSkin.Operaciones.DataResponse TransactionDayClose(MaterialSkin.Operaciones.AuthenticationType authData, int day);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/TransactionDayClose", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/TransactionDayCloseResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> TransactionDayCloseAsync(MaterialSkin.Operaciones.AuthenticationType authData, int day);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/TransactionDayReopen", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/TransactionDayReopenResponse")]
        MaterialSkin.Operaciones.DataResponse TransactionDayReopen(MaterialSkin.Operaciones.AuthenticationType authData, int day);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/TransactionDayReopen", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/TransactionDayReopenResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> TransactionDayReopenAsync(MaterialSkin.Operaciones.AuthenticationType authData, int day);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/TransactionDayCurrent", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/TransactionDayCurrentResponse")]
        MaterialSkin.Operaciones.DataResponse TransactionDayCurrent(MaterialSkin.Operaciones.AuthenticationType authData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://okibrasil.com/ws_service/IWebTeller/TransactionDayCurrent", ReplyAction="http://okibrasil.com/ws_service/IWebTeller/TransactionDayCurrentResponse")]
        System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> TransactionDayCurrentAsync(MaterialSkin.Operaciones.AuthenticationType authData);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWebTellerChannel : MaterialSkin.Operaciones.IWebTeller, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebTellerClient : System.ServiceModel.ClientBase<MaterialSkin.Operaciones.IWebTeller>, MaterialSkin.Operaciones.IWebTeller {
        
        public WebTellerClient() {
        }
        
        public WebTellerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebTellerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebTellerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebTellerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MaterialSkin.Operaciones.DataResponse InitTerminal(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.InitTerminal(authData);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> InitTerminalAsync(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.InitTerminalAsync(authData);
        }
        
        public MaterialSkin.Operaciones.DataResponse CloseTerminal(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.CloseTerminal(authData);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> CloseTerminalAsync(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.CloseTerminalAsync(authData);
        }
        
        public MaterialSkin.Operaciones.DataResponse StartDeposit(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.StartDeposit(authData);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> StartDepositAsync(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.StartDepositAsync(authData);
        }
        
        public MaterialSkin.Operaciones.DataResponse ContinueDeposit(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.ContinueDeposit(authData);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> ContinueDepositAsync(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.ContinueDepositAsync(authData);
        }
        
        public MaterialSkin.Operaciones.DataResponse EndDeposit(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.EndDeposit(authData);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> EndDepositAsync(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.EndDepositAsync(authData);
        }
        
        public MaterialSkin.Operaciones.DataResponse CancelDeposit(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.CancelDeposit(authData);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> CancelDepositAsync(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.CancelDepositAsync(authData);
        }
        
        public MaterialSkin.Operaciones.DataResponse Withdraw(MaterialSkin.Operaciones.AuthenticationType authData, int iTotalValue) {
            return base.Channel.Withdraw(authData, iTotalValue);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> WithdrawAsync(MaterialSkin.Operaciones.AuthenticationType authData, int iTotalValue) {
            return base.Channel.WithdrawAsync(authData, iTotalValue);
        }
        
        public MaterialSkin.Operaciones.DataResponse TransactionDayOpen(MaterialSkin.Operaciones.AuthenticationType authData, int day) {
            return base.Channel.TransactionDayOpen(authData, day);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> TransactionDayOpenAsync(MaterialSkin.Operaciones.AuthenticationType authData, int day) {
            return base.Channel.TransactionDayOpenAsync(authData, day);
        }
        
        public MaterialSkin.Operaciones.DataResponse TransactionDayClose(MaterialSkin.Operaciones.AuthenticationType authData, int day) {
            return base.Channel.TransactionDayClose(authData, day);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> TransactionDayCloseAsync(MaterialSkin.Operaciones.AuthenticationType authData, int day) {
            return base.Channel.TransactionDayCloseAsync(authData, day);
        }
        
        public MaterialSkin.Operaciones.DataResponse TransactionDayReopen(MaterialSkin.Operaciones.AuthenticationType authData, int day) {
            return base.Channel.TransactionDayReopen(authData, day);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> TransactionDayReopenAsync(MaterialSkin.Operaciones.AuthenticationType authData, int day) {
            return base.Channel.TransactionDayReopenAsync(authData, day);
        }
        
        public MaterialSkin.Operaciones.DataResponse TransactionDayCurrent(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.TransactionDayCurrent(authData);
        }
        
        public System.Threading.Tasks.Task<MaterialSkin.Operaciones.DataResponse> TransactionDayCurrentAsync(MaterialSkin.Operaciones.AuthenticationType authData) {
            return base.Channel.TransactionDayCurrentAsync(authData);
        }
    }
}
