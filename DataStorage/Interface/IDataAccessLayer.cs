using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage
{
    using DataStorage.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// interface for IDataAccessLayer.
    /// </summary>
    public interface IDataAccessLayer
    {
         /// <summary>
        /// Get all stores from table storage
        /// </summary>
        /// <returns>
        /// <see cref="Task"/>
        /// </returns>
        List<Store> GetAllStores();
    }
}
