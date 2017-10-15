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
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private BasketsActorProvider basketsActorProvider;

        private ProductsActorProvider productsActorProvider;

        public ProductController(BasketsActorProvider basketsActorProvider, ProductsActorProvider productsActorProvider)
        {
            this.basketsActorProvider = basketsActorProvider;
            this.productsActorProvider = productsActorProvider;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProductsCatalog()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await this.productsActorProvider.GetInstance()
                .Ask<IEnumerable<Product>>(new GetProductsCatalogMsg());

            if (products is IEnumerable<Product> && products != null && products.Any())
            {
                return Ok(products);
            }

            return NotFound();
        }

        // GET: api/Product/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await this.productsActorProvider.GetInstance()
                .Ask<Product>(new GetProductMsg(id));

            if (product != null && product is Product)
            {
                return Ok(product);
            }

            return NotFound();
        }
    }
}