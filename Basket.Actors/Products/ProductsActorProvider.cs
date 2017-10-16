namespace BasketService.Actors.Products
{
    using Akka.Actor;

    public sealed class ProductsActorProvider
    {
        private IActorRef ProductsActor { get; set; }

        public ProductsActorProvider(ActorSystem actorSystem)
        {
            var products = Domain.Data.Products.GetProducts();
            this.ProductsActor = actorSystem.ActorOf(Props.Create<ProductsActor>(products), "products");
        }

        public IActorRef GetInstance()
        {
            return ProductsActor;
        }
    }
}
