using FoodStoreAPI.Models.Contracts;
using FoodStoreAPI.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FoodStoreAPI.Models.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context) : base(context){
            _context = context;
        }

        public void CreateOrder(Order order)
        {
            //make dbcontext aware of these products, only modify when they're changed.
            //attachRange makes ctx set these products to Unchanged state and tracked
            //https://stackoverflow.com/questions/57139771/how-dbcontext-attachrange-works-in-this-scenario
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if(order.Id == 0)
            {
                Create(order);
            }
            
        } 

        //todo: change delete
        public void DeleteOrder(Order order) => Delete(order);
        
        public IEnumerable<Order> GetAll(bool trackChanges, RequestParameters parameters)
        {
            return FindAll(trackChanges)
                .Include(o => o.Lines)
                .ThenInclude(l => l.Product)
                .OrderBy(p => p.Id)
                .Skip(parameters.PageSize * (parameters.PageNumber - 1))
                .Take(parameters.PageSize);
        } 

        //to be changed
        public Order GetOrder(int id) => FindById(id);

        //to be changed
        public void UpdateOrder(Order order) => Update(order);

        Order IOrderRepository.FindOrderByPhoneNumber(string phoneNumber)
        {
            return _context.Orders.Include(o => o.Lines)
                .ThenInclude(l => l.Product)
                .FirstOrDefault(o => o.PhoneNumber == phoneNumber)
                ;
        }
    }
}
