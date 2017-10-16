namespace BasketService.UnitTest
{
    using System;
    using System.Net;

    using Microsoft.AspNetCore.Hosting;
    
    using Xunit;
    

    public class BasketTest
    {
        [Fact]
        public void AddItemIntoBasket()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls("http://localhost:54578")
                   .UseKestrel()
                   .UseStartup<API.Startup>()
                   .Build())
            {
                host.Start();

                var productDto = new ClientSdk.Models.ProductDto(1, 2, 5);
                    
                var client = new ClientSdk.BasketApi(new Uri("http://localhost:54578"));
                var basket = client.ApiBasketPostWithHttpMessagesAsync(productDto);

                Equals(HttpStatusCode.Created, basket.Result.Response.StatusCode);
            }
        }

        [Fact]
        public void GetItemFromBasket()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls("http://localhost:54578")
                   .UseKestrel()
                   .UseStartup<API.Startup>()
                   .Build())
            {
                host.Start();

                var client = new ClientSdk.BasketApi(new Uri("http://localhost:54578"));
                var basket = client.ApiBasketByCustomerIdGetWithHttpMessagesAsync(1);

                Equals(HttpStatusCode.OK, basket.Result.Response.StatusCode);
            }
        }

        [Fact]
        public void UpdateItemQuantityFromBasket()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls("http://localhost:54578")
                   .UseKestrel()
                   .UseStartup<API.Startup>()
                   .Build())
            {
                host.Start();

                var productDto = new ClientSdk.Models.ProductDto(1, 2, 10);

                var client = new ClientSdk.BasketApi(new Uri("http://localhost:54578"));
                var result = client.ApiBasketUpdatePutWithHttpMessagesAsync(productDto);

                Equals(HttpStatusCode.Created, result.Result.Response.StatusCode);
            }
        }

        [Fact]
        public void ClearBasket()
        {
            using (var host = new WebHostBuilder()
                   .UseUrls("http://localhost:54578")
                   .UseKestrel()
                   .UseStartup<API.Startup>()
                   .Build())
            {
                host.Start();

                var client = new ClientSdk.BasketApi(new Uri("http://localhost:54578"));
                var result = client.ApiBasketClearDeleteWithHttpMessagesAsync(1);

                Equals(HttpStatusCode.OK, result.Result.Response.StatusCode);
            }
        }
    }
}
