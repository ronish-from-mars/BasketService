# Checkout Basket Service API


## Prerequisites
- Visual Studio 2017
- ASP.NET Core 2.0

	### Steps to run
	- Build solution
	- Start w/ or w/o debugging with IIS Express on Visual Studio
	- Navigate to http://localhost:54578/help/documentation to test the APIs using Swagger UI


## Optional

- Docker - Untested on Docker client & Server 17.04.0-ce 



## Architecture
- Onion Architecture

![](https://raw.githubusercontent.com/ronish-from-mars/BasketService/master/public/DDD-OnionArch.png)



- Actor Model using <a href="http://getakka.net/">Akka.Net</a> 

  Akka.Net is great about building high speed, scalable and resilient micro-services platforms, by using actor model and passing messages between actors.
  ![](https://raw.githubusercontent.com/ronish-from-mars/BasketService/master/public/ActorModel.png)

## Features
- .NET Core Dependency Injection
- Used Autorest to generate clients for the APIs
- Used Swashbuckle which combines ApiExplorer and Swagger/swagger-ui to provide a rich discovery, documentation and playground experience to your API consumers (https://github.com/domaindrivendev/Swashbuckle).

Swagger UI path: [localhost]/help/documentation

![](https://raw.githubusercontent.com/ronish-from-mars/BasketService/master/public/SwaggerUI.PNG)

## Basket APIs Endpoints

### Add Item Into Basket

Method: POST

URI: api/basket

### Get Customer Basket

Method: GET

URI: api/basket/{customerId}

### Update Item Quantity

Method: PUT

URI: api/basket/update

### Delete Item From Basket

Method: DELETE

URI: api/basket/remove


## Product APIs Endpoints

### Get Sample Products

Method: GET

URI: api/Product

### Get Product By Id

Method: GET

URI: api/Product/{id}

## Notes

-  Product stocks is updated when items in basket, quantity are updated/deleted.

- Unit tests include testing of the client sdk only.

- APIs testing can be done using the Swagger UI: http://localhost:54578/help/documentation


## Assumptions

- Customers already have an account on the system.

- The system uses customer id to update basket rather than cookies when browsing anonymously.

- The system uses a hard-corded sample product catalog. All is in-memory and is lost after the application stops.
