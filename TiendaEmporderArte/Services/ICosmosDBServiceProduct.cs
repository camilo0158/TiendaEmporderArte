using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaEmporderArte.Models;

namespace TiendaEmporderArte.Services
{
    public interface ICosmosDBServiceProduct
    {
        Container _container { get; set; }

        Task AddProductAsync(Product product);
        Task DeleteProductAsync(string id);
        Task<Product> GetProductAsync(string id);
        Task<IEnumerable<Product>> GetProductsAsync(string queryString);
        Task UpdateProductAsync(string id, Product product);
    }
}