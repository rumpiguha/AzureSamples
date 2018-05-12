namespace DataStorage.Models
{
    using System;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Store class
    /// </summary>
    [Serializable]
    public class Store : TableEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public double Distance { get; set; }

        /// <summary>
        /// Gets or sets the parent group.
        /// </summary>
        /// <value>
        /// The parent group.
        /// </value>
        public string ParentGroup { get; set; }

        /// <summary>
        /// Gets or sets the email contact fo store.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the city of store.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the Country of store.
        /// </summary>
        /// <value>
        /// The Country.
        /// </value>
        public string Country { get; set; }
    }
}
