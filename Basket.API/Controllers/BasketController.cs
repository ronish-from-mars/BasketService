using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Basket.Actors.Baskets;
using Basket.Actors.Products;
using Basket.Actors.Messaging;
using Akka.Actor;
using Basket.Domain.Models;

namespace Basket.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Basket")]
    public class BasketController : Controller
    {
        private BasketsActorProvider basketsActorProvider;

        private ProductsActorProvider productsActorProvider;

        public BasketController(BasketsActorProvider basketsActorProvider, ProductsActorProvider productsActorProvider)
        {
            this.basketsActorProvider = basketsActorProvider;
            this.productsActorProvider = productsActorProvider;
        }

        // GET: api/Basket/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerBasket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerBasket = await this.basketsActorProvider.GetInstance()
                .Ask<CustomerBasket>(new GetCustomerBasketMsg(id));

            if (customerBasket is CustomerBasket && customerBasket != null)
            {
                return Ok(customerBasket);
            }

            return NotFound();
        }
    }
}