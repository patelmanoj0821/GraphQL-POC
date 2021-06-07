using GraphQLDemo.Interface;
using GraphQLDemo.Model;
using GraphQLDemo.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraphQLDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private IProduct _productService;

        public ProductsController(IProduct productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productService.GetAllProducts();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Task<Product> Get(string id)
        {
            return _productService.GetProductById(id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public Task<Product> Post([FromBody] Product product)
        {
           return _productService.CreateProduct(product);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public Task<Product> Put(string id, [FromBody] Product product)
        {
           return  _productService.UpdateProduct(id, product);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _productService.DeleteProduct(id);
        }
    }
}
