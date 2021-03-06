﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using StoreService.Interface;

namespace StatelessStore.Controllers
{
    [Produces("application/json")]
    [Route("api/Store")]
    public class StoreController : Controller
    {
        /// <summary>
        /// The dealer service.
        /// </summary>
        private IStoreRemotingListener client;

        /// <summary>
        /// Initializes a new instance of the <see cref="DealerController" /> class.
        /// </summary>
        /// <param name="serviceProxyFactory">The service proxy factory.</param>
        public StoreController(IServiceProxyFactory serviceProxyFactory)
        {
            this.client = serviceProxyFactory.CreateServiceProxy<IStoreRemotingListener>(new Uri("fabric:/StorageToService/StoreService"), new ServicePartitionKey(1));
        }

        /// <summary>
        /// The GetStores method.
        /// </summary>
        /// <param name="query">The <see cref="StoreRequest"/></param>
        /// <param name="brand">The brand</param>
        /// <param name="language">The language</param>
        /// <returns><see cref="Task"/></returns>
        [Route("get/{id}")]
        [HttpPost]
        public async Task<IActionResult> GetStores([FromBody] string id)
        {
            var storeData = await this.client.GetStoresById(id);
            return this.Ok(storeData);
        }
    }
}