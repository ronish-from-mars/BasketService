namespace BasketService.Actors.Baskets
{
    using Akka.Actor;
    using BasketService.Actors.Messaging;

    public class BasketsActor : ReceiveActor
    {
        private IActorRef ProductActor { get; }

        public BasketsActor(IActorRef productActor)
        {
            this.ProductActor = productActor;

            // register handler for incoming msg
            ReceiveAny(m => {

                if (m is BasketOperationMsg)
                {
                    var msg = m as BasketOperationMsg;

                    // create new child actor if nobody, else retrieve child actor
                    var basketActor = Context.Child(msg.CustomerId.ToString()) is Nobody ?
                        Context.ActorOf(BasketActor.Props(this.ProductActor), msg.CustomerId.ToString()) :
                        Context.Child(msg.CustomerId.ToString());

                    basketActor.Forward(m);
                }

            });
        }

        public static Props Props(IActorRef productsActor)
        {
            return Akka.Actor.Props.Create(() => new BasketsActor(productsActor));
        }
    }
}
