using GraphQL;
using GraphQL.Types;
using GraphQLDemo.Interface;
using GraphQLDemo.Model;
using GraphQLDemo.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDemo.Mutation
{
    public class ProductMutation : ObjectGraphType
    {
        HttpClient httpClient = new HttpClient();

        private const string baseUrl = "https://localhost:44387/api/products";

        public ProductMutation(IProduct productService)
        {
           /* Field<ProductType>("createProduct", arguments: new QueryArguments(new QueryArgument<ProductInputType> { Name = "product" }),
                resolve: context =>
                {
                    return productService.CreateProduct(context.GetArgument<Product>("product"));
                });

            Field<ProductType>("updateProduct", arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" },
                new QueryArgument<ProductInputType> { Name = "product" }),
                resolve: context =>
                {
                    return productService.UpdateProduct(context.GetArgument<string>("id"),context.GetArgument<Product>("product"));
                });

            Field<StringGraphType>("deleteProduct", arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                resolve: context =>
                {
                    productService.DeleteProduct(context.GetArgument<string>("id"));
                    return "Delete success";
                });*/



            Field<ProductType>("CreateProduct", arguments: new QueryArguments(new QueryArgument<ProductInputType> { Name = "product" }),
                resolve: a =>
                {
                    var data = Newtonsoft.Json.JsonConvert.SerializeObject(a.GetArgument<Product>("product"));
                    System.Net.Http.HttpContent reqBody = new StringContent(data, UTF8Encoding.UTF8, "application/json");
                    return this.httpClient.PostAsync(new Uri(baseUrl), reqBody);
                });

            Field<ProductType>("updateProduct", arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id" }, new QueryArgument<ProductInputType> { Name = "product" }),
                resolve: a =>
                {
                    var data = Newtonsoft.Json.JsonConvert.SerializeObject(a.GetArgument<Product>("product"));
                    System.Net.Http.HttpContent reqBody = new StringContent(data, UTF8Encoding.UTF8, "application/json");
                    return this.httpClient.PutAsync(new Uri(baseUrl + "\'" + a.GetArgument<string>("Id")), reqBody);
                });

            Field<StringGraphType>("deleteProduct", arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id" }),
                resolve: a =>
                {
                    this.httpClient.DeleteAsync(new Uri(baseUrl + "\'" + a.GetArgument<string>("Id")));
                    return "delete success";
                });
        }

        public Product postProduct(Uri uri)
        {
            var response = httpClient.GetAsync(uri).Result;
            var responseText = response.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(responseText);
        }

        public Product getProductCall(Uri uri)
        {
            var response = httpClient.GetAsync(uri).Result;
            var responseText = response.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(responseText);
        }
    }
}
