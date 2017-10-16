namespace BasketService.Actors.Baskets
{
    using Akka.Actor;

    using BasketService.Actors.Products;

    public class BasketsActorProvider
    {
        private IActorRef BasketsActorInstance { get; set; }

        public BasketsActorProvider(ActorSystem actorSystem, ProductsActorProvider provider)
        {
            var productsActor = provider.GetInstance();
            this.BasketsActorInstance = actorSystem.ActorOf(BasketsActor.Props(productsActor), "baskets");
        }

        public IActorRef GetInstance()
        {
            return this.BasketsActorInstance;
        }
    }
}
