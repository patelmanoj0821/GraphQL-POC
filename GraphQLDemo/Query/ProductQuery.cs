using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Interface;
using GraphQLDemo.Model;
using GraphQLDemo.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GraphQLDemo.Query
{
    public class ProductQuery : ObjectGraphType
    {
        HttpClient httpClient = new HttpClient();

        private const string baseUrl = "https://localhost:44387/api/products";

        public ProductQuery(IProduct productService)
        {
            /*Field<ListGraphType<ProductType>>("products", resolve: a => {return productService.GetAllProducts(); });

            Field<ProductType>("product",arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                resolve: context => 
                {
                    return productService.GetProductById(context.GetArgument<string>( "id"));
                });*/

            // Below is a way where we can call existing REST APIs using resolvers.

            Field<ListGraphType<ProductType>>("products",
                resolve: a =>
                {
                    return this.getCall(new Uri(baseUrl));

                });

            Field<ProductType>("product", arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                resolve: context =>
                {
                    return this.getProductCall(new Uri(baseUrl + "\\" + context.GetArgument<string>("id")));
                });
            
        }
        public List<Product> getCall(Uri uri)
        {
            var response = httpClient.GetAsync(uri).Result;
            var responseText = response.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(responseText); 
        }

        public Product getProductCall(Uri uri)
        {
            var response = httpClient.GetAsync(uri).Result;
            var responseText = response.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(responseText);
        }
    }
}
