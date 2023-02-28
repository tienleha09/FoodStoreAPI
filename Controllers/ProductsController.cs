using FoodStoreAPI.Models;
using FoodStoreAPI.Models.Contracts;
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
    public class ProductsController : ControllerBase
    {
        private readonly IRepoManager _repo;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IRepoManager repo, ILogger<ProductsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery]RequestParameters requestParameters)
        {
            try
            {
                _logger.LogInformation("Get Products");
                var products = _repo.Product.GetAll(false, requestParameters);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to retrieve products: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            _logger.LogInformation($"Get Product id: {id}");
            var product = _repo.Product.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult Post([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _repo.Product.CreateProduct(product);
                _repo.Save();

                return CreatedAtAction(nameof(GetProduct),
                    new { id = product.Id }, product);
            }
            else
            {
                _logger.LogError("Bad data sent from client");
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateProduct(int id, [FromBody]Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _repo.Product.UpdateProduct(product);
            try
            {
                _repo.Save();
                return Ok(product);
            }
            catch(DBConcurrencyException)
            {
                if (!_repo.Product.ProductExists(id))
                {
                    return NotFound();
                }
                return BadRequest();
            }        
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = _repo.Product.GetProduct(id);
            if (product == null) return NotFound();
            _repo.Product.DeleteProduct(product);
            _repo.Save();
            return Ok(new {success = true});
        }
    }
}
