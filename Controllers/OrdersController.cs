using FoodStoreAPI.Models;
using FoodStoreAPI.Models.Contracts;
using FoodStoreAPI.Models.Repository;
using FoodStoreAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace FoodStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepoManager _repo;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(IRepoManager repo, ILogger<OrdersController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetOrders([FromQuery]RequestParameters requestParameters)
        {
            try
            {
                _logger.LogInformation("Get Orders");
                var orders = _repo.Order.GetAll(false, requestParameters);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve orders: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            _logger.LogInformation($"Get Order id: {id}");
            var order = _repo.Order.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                _repo.Order.CreateOrder(order);
                _repo.Save();

                return CreatedAtAction(nameof(GetOrder),
                    new { id = order.Id }, order);
            }
            else
            {
                _logger.LogError("Bad data sent from client");
                return BadRequest();
            }

        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateOrder(int id, [FromBody]Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return BadRequest();
        //    }
        //    _repo.Order.UpdateOrder(order);
        //    try
        //    {
        //        _repo.Save();
        //        return Ok(order);
        //    }
        //    catch(DBConcurrencyException)
        //    {
        //        if (!_repo.Order.OrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        return BadRequest();
        //    }        
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var order = _repo.Order.GetOrder(id);
        //    if (order == null) return NotFound();
        //    _repo.Order.DeleteOrder(order);
        //    _repo.Save();
        //    return Ok(new {success = true});
        //}
    }
}
