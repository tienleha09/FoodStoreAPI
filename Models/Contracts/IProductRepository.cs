using FoodStoreAPI.Utilities;
using System.Collections.Generic;

namespace FoodStoreAPI.Models.Contracts
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll(bool trackChanges, RequestParameters parameters);
        Product GetProduct(int id);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        void CreateProduct(Product product);
        bool ProductExists(int id);
    }
}
