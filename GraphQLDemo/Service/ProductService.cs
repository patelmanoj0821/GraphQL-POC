using GraphQLDemo.Interface;
using GraphQLDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VWAC.Framework.Clients;

namespace GraphQLDemo.Service
{
    public class ProductService : IProduct
    {

        public IFailOverCosmosDbClient Client { get; }

        public ProductService(IFailOverCosmosDbClientFactory factory)
        {
            /// you need to update it to right DB and proper container.
           /// as of now I have used VWAC DB and Service collection to test.
            this.Client = factory.CreateCosmosDbClient("REPLACE ME", "VWAC");
        }

        public async Task<Product> CreateProduct(Product product)
        {
            ProductDocument doc = new ProductDocument(product);

            await this.Client.WriteAsync(product.Id, "ServiceCollection", doc).ConfigureAwait(false);
            return product;
           
        }

        public async Task<String> DeleteProduct(string productId)
        {
            await this.Client.DeleteAsync(productId, "ServiceCollection", productId).ConfigureAwait(false);
            return "OK"; 
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(1) { new Product() { Id = "2", Name = " sample", Price = 4 } };
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await this.Client.ReadAsync<Product>(productId, "ServiceCollection", productId).ConfigureAwait(false); 
        }

        public async Task<Product> UpdateProduct(string productId, Product product)
        {
            Product productObj = await this.Client.ReadAsync<Product>(productId, "ServiceCollection", productId).ConfigureAwait(false);
            productObj.Name = product.Name;
            productObj.Price = product.Price;

            return await CreateProduct(productObj).ConfigureAwait(false);
            
        }
    }
}
