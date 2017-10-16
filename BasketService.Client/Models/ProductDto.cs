namespace BasketService.ClientSdk.Models
{
    using Newtonsoft.Json;

    public partial class ProductDto
    {
        /// <summary>
        /// Initializes a new instance of the ProductDto class.
        /// </summary>
        public ProductDto()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ProductDto class.
        /// </summary>
        public ProductDto(int? customerId = default(int?), int? productId = default(int?), int? quantity = default(int?))
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "customerId")]
        public int? CustomerId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productId")]
        public int? ProductId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int? Quantity { get; set; }

    }
}
