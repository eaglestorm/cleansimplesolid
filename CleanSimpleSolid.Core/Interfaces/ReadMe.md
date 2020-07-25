# Core Interfaces

In clean architecture one of the major uses of interfaces is to implement the Inversion of Control
pattern between the Core project and those that reference it.

The reason for this is so that the core classes can access classes 
in projects that reference it while allowing the core project to have no
references to any of these projects.

This allows the business logic that contains most of your intellectual property and a 
significant amount of the risk (it's what most people will care about if you get it wrong)
to be easily unit testable as you are not required to mock any dependencies.

I've started to think that one measure of code quality is the level of
mocking in your unit tests.  The more mocking the worse your code quality. 