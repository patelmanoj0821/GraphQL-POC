using GraphiQl;
using GraphQL.Server;
using GraphQL.Types;
using GraphQLDemo.Interface;
using GraphQLDemo.Mutation;
using GraphQLDemo.Query;
using GraphQLDemo.Schema;
using GraphQLDemo.Service;
using GraphQLDemo.Type;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VWAC.Framework.Clients;

namespace GraphQLDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IProduct, ProductService>();
            services.AddTransient<ProductType>();
            services.AddTransient<ProductQuery>();
            services.AddTransient<ProductInputType>();
            services.AddTransient<ProductMutation>();
            services.AddTransient<ISchema, ProductSchema>();
            FailOverCosmosDbClientFactory failOverCosmosDbClientFactory = new FailOverCosmosDbClientFactory();
            services.AddSingleton<IFailOverCosmosDbClientFactory>(failOverCosmosDbClientFactory);
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = false;
            }).AddSystemTextJson();

            //services.AddDbContext<GraphQLDbContext>(options => options.UseSqlServer(@"Data Source= (localdb)\MSSQLLocalDB ; Initial catalog=GraphQLDb; Integrated Security = True"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //dbContext.Database.EnsureCreated();
            app.UseGraphiQl("/graphql");
            app.UseGraphQL<ISchema>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
