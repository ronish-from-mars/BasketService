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
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerBasket([FromRoute] int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerBasket = await this.basketsActorProvider.GetInstance()
                .Ask<CustomerBasket>(new GetCustomerBasketMsg(customerId));

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
                return CreatedAtAction("GetCustomerBasket", new { customerId = customerBasket.CustomerId }, customerBasket);
            }

            return NotFound();
        }

        // DELETE: api/basket/remove
        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteItemFromBasket([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // send message to remove item from basket
            var itemRemoved = await this.basketsActorProvider.GetInstance()
                .Ask<Task<bool>>(new RemoveItemFromBasketMsg(productDto.CustomerId, productDto.ProductId));

            if (itemRemoved.Result)
            {
                return Ok(itemRemoved.Result);
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
                return CreatedAtAction("GetCustomerBasket", new { customerId = updatedBasket.CustomerId }, updatedBasket);
            }

            return NotFound();
        }


        // DELETE: api/basket/clear
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearBasket([FromBody] int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // send message to clear basket
            var basketEmptied = await this.basketsActorProvider.GetInstance()
                .Ask<Task<CustomerBasket>>(new ClearCustomerBasketMsg(customerId));

            if (basketEmptied.Result != null && basketEmptied.Result is CustomerBasket)
            {
                return Ok(basketEmptied.Result);
            }

            return NotFound();
        }
    }
}