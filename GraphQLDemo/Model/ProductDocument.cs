// Copyright (c) Volkswagen Group. All rights reserved.

namespace GraphQLDemo.Model
{
    using Newtonsoft.Json;

    public class ProductDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }


        [JsonConstructor]
        public ProductDocument(Product product)
        {
            this.Id = product.Id.ToLowerInvariant();
            this.PartitionKey = product.Id.ToLowerInvariant();
            this.Name = product.Name;
            this.Price = product.Price;
        }


        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

    }
}
