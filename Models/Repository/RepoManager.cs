using FoodStoreAPI.Models.Contracts;

namespace FoodStoreAPI.Models.Repository
{
    public class RepoManager: IRepoManager
    {
        private IProductRepository _productRepository;
        private DataContext _dataContext;
        public RepoManager(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IProductRepository Product{
            get{
                if( _productRepository == null)
                {
                    _productRepository = new ProductRepository(_dataContext);
                }
                return _productRepository;
            }
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}
