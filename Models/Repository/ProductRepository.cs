using FoodStoreAPI.Models.Contracts;
using FoodStoreAPI.Utilities;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;

namespace FoodStoreAPI.Models.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context){}

        public void CreateProduct(Product product) => Create(product);

        public void DeleteProduct(Product product) => Delete(product);
        
        public IEnumerable<Product> GetAll(bool trackChanges, RequestParameters parameters)
        {
            return FindAll(trackChanges)
                .OrderBy(p => p.Id)
                .Skip(parameters.PageSize * (parameters.PageNumber - 1))
                .Take(parameters.PageSize);
        } 

        public Product GetProduct(int id) => FindById(id);

        public bool ProductExists(int id)
        {
            return _dataContext.Products.Any(p => p.Id == id);
        }

        public void UpdateProduct(Product product) => Update(product);
    }
}
