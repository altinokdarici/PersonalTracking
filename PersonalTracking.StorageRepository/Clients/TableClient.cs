using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTracking.StorageRepository.Clients
{
    public abstract class TableClient<T>
        where T : TableEntity, new()
    {
        const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=altinok;AccountKey=ACCOUNT_KEY";
        protected CloudTable Table { get; private set; }
        public TableClient()
        {

            PluralizationService plService = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-US"));
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            string tableName = plService.Pluralize(typeof(T).Name);
            Table = tableClient.GetTableReference(tableName);
            Table.CreateIfNotExists();
        }
        public void InsertOrReplace(T log)
        {
            // Create the InsertOrReplace TableOperation
            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(log);
            // Execute the operation.
            Table.Execute(insertOrReplaceOperation);
        }
        public T Get(string rowKey, string partitionKey)
        {
            T t = new T();
            t.PartitionKey = partitionKey;
            t.RowKey = rowKey;
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);

            // Execute the operation.
            TableResult retrievedResult = Table.Execute(retrieveOperation);

            if (retrievedResult.Result == null)
                return t;
            else
                return (T)retrievedResult.Result;
        }
    }
}
