﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfConsoleClient.ManufacturerServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ManufacturerServiceReference.IManufacturerService")]
    public interface IManufacturerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/GetAllManufacturers", ReplyAction="http://tempuri.org/IManufacturerService/GetAllManufacturersResponse")]
        System.Collections.Generic.List<WcfService.ManufacturerDto> GetAllManufacturers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/GetAllManufacturers", ReplyAction="http://tempuri.org/IManufacturerService/GetAllManufacturersResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WcfService.ManufacturerDto>> GetAllManufacturersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/AddManufacturer", ReplyAction="http://tempuri.org/IManufacturerService/AddManufacturerResponse")]
        int AddManufacturer(Entities.Manufacturer manufacturer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/AddManufacturer", ReplyAction="http://tempuri.org/IManufacturerService/AddManufacturerResponse")]
        System.Threading.Tasks.Task<int> AddManufacturerAsync(Entities.Manufacturer manufacturer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/UpdateManufacturer", ReplyAction="http://tempuri.org/IManufacturerService/UpdateManufacturerResponse")]
        string UpdateManufacturer(Entities.Manufacturer manufacturer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/UpdateManufacturer", ReplyAction="http://tempuri.org/IManufacturerService/UpdateManufacturerResponse")]
        System.Threading.Tasks.Task<string> UpdateManufacturerAsync(Entities.Manufacturer manufacturer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/DeleteManufacturer", ReplyAction="http://tempuri.org/IManufacturerService/DeleteManufacturerResponse")]
        string DeleteManufacturer(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/DeleteManufacturer", ReplyAction="http://tempuri.org/IManufacturerService/DeleteManufacturerResponse")]
        System.Threading.Tasks.Task<string> DeleteManufacturerAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/GetManufacturerById", ReplyAction="http://tempuri.org/IManufacturerService/GetManufacturerByIdResponse")]
        WcfService.ManufacturerDto GetManufacturerById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IManufacturerService/GetManufacturerById", ReplyAction="http://tempuri.org/IManufacturerService/GetManufacturerByIdResponse")]
        System.Threading.Tasks.Task<WcfService.ManufacturerDto> GetManufacturerByIdAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IManufacturerServiceChannel : WcfConsoleClient.ManufacturerServiceReference.IManufacturerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ManufacturerServiceClient : System.ServiceModel.ClientBase<WcfConsoleClient.ManufacturerServiceReference.IManufacturerService>, WcfConsoleClient.ManufacturerServiceReference.IManufacturerService {
        
        public ManufacturerServiceClient() {
        }
        
        public ManufacturerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ManufacturerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ManufacturerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ManufacturerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<WcfService.ManufacturerDto> GetAllManufacturers() {
            return base.Channel.GetAllManufacturers();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WcfService.ManufacturerDto>> GetAllManufacturersAsync() {
            return base.Channel.GetAllManufacturersAsync();
        }
        
        public int AddManufacturer(Entities.Manufacturer manufacturer) {
            return base.Channel.AddManufacturer(manufacturer);
        }
        
        public System.Threading.Tasks.Task<int> AddManufacturerAsync(Entities.Manufacturer manufacturer) {
            return base.Channel.AddManufacturerAsync(manufacturer);
        }
        
        public string UpdateManufacturer(Entities.Manufacturer manufacturer) {
            return base.Channel.UpdateManufacturer(manufacturer);
        }
        
        public System.Threading.Tasks.Task<string> UpdateManufacturerAsync(Entities.Manufacturer manufacturer) {
            return base.Channel.UpdateManufacturerAsync(manufacturer);
        }
        
        public string DeleteManufacturer(int id) {
            return base.Channel.DeleteManufacturer(id);
        }
        
        public System.Threading.Tasks.Task<string> DeleteManufacturerAsync(int id) {
            return base.Channel.DeleteManufacturerAsync(id);
        }
        
        public WcfService.ManufacturerDto GetManufacturerById(int id) {
            return base.Channel.GetManufacturerById(id);
        }
        
        public System.Threading.Tasks.Task<WcfService.ManufacturerDto> GetManufacturerByIdAsync(int id) {
            return base.Channel.GetManufacturerByIdAsync(id);
        }
    }
}
