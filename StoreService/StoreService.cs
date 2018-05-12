namespace StoreService
{
    using System;
    using System.Collections.Generic;
    using System.Fabric;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DataStorage;
    using DataStorage.Models;
    using Microsoft.ServiceFabric.Data.Collections;
    using Microsoft.ServiceFabric.Services.Communication.Runtime;
    using Microsoft.ServiceFabric.Services.Runtime;

    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class StoreService : StatefulService
    {
        /// <summary>
        /// Gets or sets the stores.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public IReliableDictionary<string, Store> StoreDictionary { get; set; }

        /// <summary>
        /// The table storage
        /// </summary>
        private IDataAccessLayer dataStorage;


        public StoreService(StatefulServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// It will fetch stores every hour and save it in service statemanager
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await this.SaveStoresToCache(cancellationToken);
                }
                catch (OperationCanceledException exception)
                {
                    ServiceEventSource.Current.ServiceMessage(this.Context, $"Cancellation requested. Stopping Job Processor service at {DateTime.UtcNow}");
                }
                catch (Exception ex)
                {
                    ServiceEventSource.Current.ServiceMessage(this.Context, $"Exception Occured during process. Exception details - {ex.Message} --- {ex.InnerException}");
                }

                await Task.Delay(TimeSpan.FromHours(1), cancellationToken);
            }
        }

        /// <summary>
        /// Method to get stores from table storage and save in state manager
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <returns><see cref="Task"/></returns>
        private async Task SaveStoresToCache(CancellationToken cancellationToken)
        {
            var stores = this.dataStorage.GetAllStores();

            this.StoreDictionary = await this.StateManager.GetOrAddAsync<IReliableDictionary<string, Store>>("myDictionary");

            if (stores.Any())
            {
                using (var tx = StateManager.CreateTransaction())
                {
                    ////Iterate the table storage for each store and add in IReliable Dictionary
                    stores.ForEach(async s => await this.StoreDictionary.AddAsync(tx, s.Name, s, TimeSpan.FromHours(1), cancellationToken));
                    await tx.CommitAsync();
                }
            }
        }
    }
}
