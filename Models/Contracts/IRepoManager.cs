namespace FoodStoreAPI.Models.Contracts
{
    public interface IRepoManager
    {
        IProductRepository Product { get; }
        IOrderRepository Order { get; }
        void Save();
    }
}
