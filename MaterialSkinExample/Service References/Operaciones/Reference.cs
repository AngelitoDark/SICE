﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaterialSkinExample.Operaciones {
    
    
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
    public interface IWebTellerChannel : MaterialSkinExample.Operaciones.IWebTeller, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebTellerClient : System.ServiceModel.ClientBase<MaterialSkinExample.Operaciones.IWebTeller>, MaterialSkinExample.Operaciones.IWebTeller {
        
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
