namespace BasketService.API.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Akka.Actor;

    using BasketService.Actors.Baskets;
    using BasketService.Actors.Messages;
    using BasketService.Actors.Messaging;
    using BasketService.Actors.Products;
    using BasketService.Domain.Dtos;
    using BasketService.Domain.Models;
    
    [Produces("application/json")]
    [Route("api/[controller]")]
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

        // POST: api/Basket
        [HttpPost]
        public async Task<IActionResult> AddItemToBasket([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // add item to existing basket else create new basket & add item
            var customerBasket = await this.basketsActorProvider.GetInstance()
                .Ask<CustomerBasket>(new AddItemToBasketMsg(productDto.CustomerId, productDto.ProductId, productDto.Quantity));

            if (customerBasket is CustomerBasket && customerBasket != null)
            {
                return CreatedAtAction("GetCustomerBasket", new { id = customerBasket.Id }, customerBasket);
            }

            return NotFound();
        }

        // DELETE: api/basket/item/5
        [HttpDelete("item")]
        public async Task<IActionResult> DeleteItemFromBasket([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // send message to remove item from basket
            var itemRemoved = await this.basketsActorProvider.GetInstance()
                .Ask<bool>(new RemoveItemFromBasketMsg(productDto.CustomerId, productDto.ProductId));

            if (itemRemoved)
            {
                return Ok(itemRemoved);
            }

            return NotFound();
        }

        // PUT: api/basket/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateItemQuantity([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // send message to remove item from basket
            var updatedBasket = await this.basketsActorProvider.GetInstance()
                .Ask<CustomerBasket>(new UpdateItemQuantityMsg(productDto.CustomerId, productDto.ProductId, productDto.Quantity));

            if (updatedBasket is CustomerBasket && updatedBasket != null)
            {
                return CreatedAtAction("GetCustomerBasket", new { id = updatedBasket.Id }, updatedBasket);
            }

            return NotFound();
        }


        //// DELETE: api/basket/clear/5
        //[HttpDelete("clear")]
        //public async Task<IActionResult> DeleteItemFromBasket([FromBody] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // send message to remove item from basket
        //    var itemRemoved = await this.basketsActorProvider.GetInstance()
        //        .Ask<bool>(new RemoveItemFromBasketMsg(productDto.CustomerId, productDto.ProductId));

        //    if (itemRemoved)
        //    {
        //        return Ok(itemRemoved);
        //    }

        //    return NotFound();
        //}
    }
}