# dapr-samples

- pub-sub-routing-http sample
Shows pub sub routing using the dapr .NET SDK with declarative subscriptions to http endpoints.
Uses polymorphic products (Gadget, Widget, Thingamajig inheriting from abstract Product), requiring polymorphic serialisation and deserlisation in .NET 7.0 STJ of the CloudEvent data.
Also shows a NotAProduct that goes to the dead letter topic, but subscription of dead letter topic not yet working.

- pub-sub-routing-grpc sample
Shows pub sub routing using the dapr .NET SDK with declarative subscriptions to the dapr grpc AppCallback service. 
Uses polymorphic products (Gadget, Widget, Thingamajig inheriting from abstract Product), requiring polymorphic serialisation and deserlisation in .NET 7.0 STJ of the CloudEvent data.
The TopicEventRequest in the call to AppCallback OnTopicEvent does contain the Path from the matched rule in the subscription.yaml, however I have not found a way to use this to route the event to a corresponding endpoint. 
Instead I use the MassTransit Mediator which uses polymorphic dispatch, which works differently from the routing rules. For instance a Gadget will be consumed by both the Gadget consumer and the base Product consumer, whereas the pubsub routing rules should send the Gadget only to the Gadget endpoint.         
Also shows a NotAProduct that goes to the dead letter topic, but need to think of a way to make the dead letter topic subscription work with MassTransit Mediator.
