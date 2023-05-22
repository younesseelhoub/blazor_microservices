﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductOwner.MIcroservices.Data;
using ProductOwner.MIcroservices.Model;
using ProductOwner.MIcroservices.Utility;
using RabbitMQ.Client;
using System.Text;

namespace ProductOwner.MIcroservices.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextClass _dbContext;

        public ProductService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductDetails>> GetProductListAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

         public async Task<IEnumerable<Product>> GetProductListAsyncc()
        {
            return await _dbContext.Product.ToListAsync();
        }

        public async Task<ProductDetails> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ProductDetails> AddProductAsync(ProductDetails productDetails)
        {
            var result = _dbContext.Products.Add(productDetails);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

         public async Task<Product> AddProductAsyncc(Product productDetails)
        {
            var result = _dbContext.Product.Add(productDetails);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }




        public bool SendProductOffer(ProductOfferDetail productOfferDetails)
        {
            var RabbitMQServer = "";
            var RabbitMQUserName = "";
            var RabbutMQPassword = "";

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                RabbitMQServer = Environment.GetEnvironmentVariable("RABBIT_MQ_SERVER");
                RabbitMQUserName = Environment.GetEnvironmentVariable("RABBIT_MQ_USERNAME");
                RabbutMQPassword = Environment.GetEnvironmentVariable("RABBIT_MQ_PASSWORD");
            }
            else
            {
                RabbitMQServer = StaticConfigurationManager.AppSetting["RabbitMQ:RabbitURL"];
                RabbitMQUserName = StaticConfigurationManager.AppSetting["RabbitMQ:Username"];
                RabbutMQPassword = StaticConfigurationManager.AppSetting["RabbitMQ:Password"];
            }

            try
            {
                var factory = new ConnectionFactory()
                { HostName = RabbitMQServer, UserName = RabbitMQUserName, Password = RabbutMQPassword };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    //Direct Exchange Details like name and type of exchange
                    channel.ExchangeDeclare(StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchangeName"], StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchhangeType"]);

                    //Declare Queue with Name and a few property related to Queue like durabality of msg, auto delete and many more
                    channel.QueueDeclare(queue: StaticConfigurationManager.AppSetting["RabbitMqSettings:QueueName"],
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    //Bind Queue with Exhange and routing details
                    channel.QueueBind(queue: StaticConfigurationManager.AppSetting["RabbitMqSettings:QueueName"], exchange: StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchangeName"], routingKey: StaticConfigurationManager.AppSetting["RabbitMqSettings:RouteKey"]);

                    //Seriliaze object using Newtonsoft library
                    string productDetail = JsonConvert.SerializeObject(productOfferDetails);
                    var body = Encoding.UTF8.GetBytes(productDetail);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    //publish msg
                    channel.BasicPublish(exchange: StaticConfigurationManager.AppSetting["RabbitMqSettings:ExchangeName"],
                                         routingKey: StaticConfigurationManager.AppSetting["RabbitMqSettings:RouteKey"],
                                         basicProperties: properties,
                                         body: body);

                    return true;
                }
            }

            catch (Exception)
            {
            }
            return false;



        }

      
    }
}
