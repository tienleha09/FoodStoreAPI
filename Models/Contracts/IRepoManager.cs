namespace FoodStoreAPI.Models.Contracts
{
    public interface IRepoManager
    {
        IProductRepository Product { get; }
        void Save();
    }
}
