using Akka.Actor;
using Basket.Actors.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Actors.Baskets
{
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
