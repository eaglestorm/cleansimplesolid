# Use Case Messages

The purpose of these message objects is to isolate the core project from the outside world.
i.e. The messages are how we pass data to and get data from the domain model that contains the 
core business logic.

It also makes unit testing the use cases easier was we are defining the valid input and output.

The downside is that you have a lot of mapping of objects, i.e. these messages are likely mapped twice
once from the Dto in the API, then again to the Core Domain Objects.