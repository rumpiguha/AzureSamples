namespace DataStorage
{
    using DataStorage.Models;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using System.Collections.Generic;
    using System.Linq;

    public class DataAccessLayer : IDataAccessLayer
    {
        /// <summary>
        /// The cloud table
        /// </summary>
        private readonly CloudTable cloudTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessLayer" /> class.
        /// </summary>
        /// <returns>
        /// <see cref="Task"/>
        /// </returns>
        public DataAccessLayer()
        {
            var cloudAccount = CloudStorageAccount.Parse("_storageConnection");
            this.cloudTable = cloudAccount.CreateCloudTableClient().GetTableReference("stores");
            this.cloudTable.CreateIfNotExists();
        }

        /// <summary>
        /// Gets all tokens for user.
        /// </summary>
        /// <returns>
        ///   <see cref="IEnumerable{T}" />
        /// </returns>
        public List<Store> GetAllStores()
        {
            TableQuery<Store> query = new TableQuery<Store>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "sales"));
           
            return this.cloudTable.ExecuteQuery(query).ToList();
        }
    }
}
