using FoodStoreAPI.Models.Contracts;

namespace FoodStoreAPI.Models.Repository
{
    public class RepoManager: IRepoManager
    {
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;
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

        public IOrderRepository Order {
            get
            {
                _orderRepository ??= new OrderRepository(_dataContext);
                return _orderRepository;
            }
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }
    }
}
