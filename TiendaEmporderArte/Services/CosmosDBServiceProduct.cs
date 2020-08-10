namespace TiendaEmporderArte.Services
{
    using Microsoft.Azure.Cosmos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TiendaEmporderArte.Models;
    public class CosmosDBServiceProduct : ICosmosDBServiceProduct
    {
        public Container _container { get; set; }

        public CosmosDBServiceProduct(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddProductAsync(Product product)
        {
            await this._container.CreateItemAsync<Product>(product, new PartitionKey(product.Id));
        }

        public async Task DeleteProductAsync(string id)
        {
            await this._container.DeleteItemAsync<Product>(id, new PartitionKey(id));
        }

        public async Task<Product> GetProductAsync(string id)
        {
            try
            {
                ItemResponse<Product> response = await this._container.ReadItemAsync<Product>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));
            List<Product> products = new List<Product>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                products.AddRange(response.ToList());
            }
            return products;
        }

        public async Task UpdateProductAsync(string id, Product product)
        {
            await this._container.UpsertItemAsync<Product>(product, new PartitionKey(id));
        }
    }
}
