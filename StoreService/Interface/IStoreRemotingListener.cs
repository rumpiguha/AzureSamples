using DataStorage.Models;
using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreService.Interface
{
    /// <summary>
    /// Store Service Interface
    /// </summary>
    public interface IStoreRemotingListener : IService
    {
        /// <summary>
        /// Method to get stores from state manager
        /// </summary>
        /// <param name="request">The request </param>
        /// <returns>Asynchronous Task</returns>
        Task<List<Store>> GetStoresById(string request);
    }
}
