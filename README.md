## Instructions

Main application is Bjss.PriceBasket which is the console that should be run with products on the command line.

You can either run the exe from a command prompt with product args or run from visual studio as products are configured in project properties.

When you first run it may be slow while it setups database and applies migrations to localdb.

## Architecture

The application architecture follows a domain centric design in this instance the Onion Architecture, which is well
suited to medium size applications that are not overly complex as it aids seperation of concerns and makes unit
testing really easy as all the dependencies are externalised.

In a more complex application with a rich domain it may be worth applying DDD, which is the next step on from this architecture.

### Bjss.PriceBasket Layers

 * Bjss.PriceBasket.Core - contains the main business logic and most of the code that would normally be unit tested fully in a production app.

 * Bjss.PriceBasket.Infrastructure - contains the code that is subject to change most frequently IO, data access, web service...

 * Bjss.PriceBasket.Application - contains a layer between the presentation and the core domain.  Provides code for interacting between the user and the interface

 * Bjss.PriceBasket - outer most layer which interacts directly with the console.  Also boostraps the application.

 ## Unit Testing
  
  In a production app I would have much more coverage but focused mainly on the Core as I was developing, as that contains the most testable code.
  
  Normally I'd also cover the over layers with both unit and integration tests.
  
  I would also include tests to cover the specification.

  


