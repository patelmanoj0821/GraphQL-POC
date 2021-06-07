using GraphQLDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.Interface
{
    public interface IProduct
    {
        public List<Product> GetAllProducts();

        public Task<Product> GetProductById(string productId);

        public Task<Product> CreateProduct(Product product);

        public Task<Product> UpdateProduct(string productId, Product product);

        public Task<String> DeleteProduct(string productId);
    }
}
