# EagleRock

## Introduction

This is a simple project for receiving location data from Drones capturing traffic data.
It also gives the ability see where they are at any given point.

## Assumptions


We are not putting any security around this present, but we should definitely implement that to ensure we don't get DOS etc.

We are currently using JSON for the payloads, but we could also potentially support GoogleRPC (or if necessary Protobuf directly for significantly reduced network traffic)

We need to record all the bot information, but we also need to be able to retrieve where a Bot is right now.

Not adding the RabbitMQ at this point, but would need to handle the Connection to a Queue, given that Rabbit can go off-line, we need to support potential for loss of connectivity.

If we want to do load balancing that should be build at a layer above the API, so that we can scale out the services. We definitely don't want to use MemoryCache for that or
we've have some very weird data from different servers. That would be where we should use `Redis` or `Memcached` for the storage. 
We could further alleviate this by only having the API as super light weight, and all it does is posts the messages that it receives to a AWS SQS or RabbitMQ Queue, and then we have 
a series of services sitting behind that so the processing of the data does not impact the ingestion of the data. 
This would also enable the upgrading of the service (which could have a temporary outage) because the API would handle the data in and be placing the messages on the queue for 
processing.

These all require further changes to the present API, but have been considered.

## Future

We could also add some endpoints to support arrays of data in the event that there has been a large block of messages, but that takes away from the singular nature.

This really should use Async for the services, but presently they are low call low IO, so that would be another change when needed.


