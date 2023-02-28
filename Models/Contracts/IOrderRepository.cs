using FoodStoreAPI.Utilities;
using System.Collections.Generic;

namespace FoodStoreAPI.Models.Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll(bool trackChanges, RequestParameters parameters);
        Order GetOrder(int id);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        void CreateOrder(Order order);
        Order FindOrderByPhoneNumber(string phoneNumber);
    }
}
