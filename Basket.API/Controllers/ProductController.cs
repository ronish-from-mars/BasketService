namespace BasketService.API.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Akka.Actor;

    using BasketService.Actors.Baskets;
    using BasketService.Actors.Messaging;
    using BasketService.Actors.Products;
    using BasketService.Domain.Models;

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
            if (id <= 0)
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