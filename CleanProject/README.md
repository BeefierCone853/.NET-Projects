# CleanProject

This is a .NET 8 project following the [**Clean Architecture**](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html).
The project is for learning purposes and testing new things.
For a more up-to date project, check out the **minimal-apis** branch.

## Endpoints

The API endpoints are built using controllers.
There is a second branch where the API endpoints are built using [**Minimal APIs**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-8.0).

## Technology Stack

This section contains a quick overview on the technology stack of the application.

### Entity Framework Core

[**Entity Framework Core**](https://github.com/dotnet/efcore) is the used database ORM on this project.

### PostgreSQL

The database system used on the project is [**PostgreSQL**](https://www.postgresql.org/).

### Docker

[**Docker**](https://www.docker.com/) is used for containerization of the whole application.

### Seq

[**Seq**](https://datalust.co/seq) is used for For centralized logging.

## Patterns

Design patterns help write better code and have numerous advantages.
Some of those are:

  1. Simplifying the coding process
  2. Better maintainability
  3. Code reuse

### CQRS

CQRS seperates read and write operations for a database. It maximizes performance, scalabilty and security.
[**MediatR**](https://github.com/jbogard/MediatR) is used to achieve this.
On this project, the Read/Write database is not seperated.

### Result

The result pattern is used for handling errors. It encapsulates the *result* of an operation into a result object. It can represent a success or failure. By implementing the result pattern, there are also fewer
thrown exceptions throughout the application.

### Repository

The repository pattern provides an abstraction layer between the application's data logic and
the data source. It promotes easier testing and maintainability.

## Testing

The project also contains tests. There are unit tests, functional tests and integration tests.
Tests are written using [**xunit**](https://xunit.net/) and [**FluentAssertions**](https://fluentassertions.com/).

### Unit Tests

Unit tests are used for testing small pieces of code. Mocking is used to replace different services, like an external database. [**NSubstitute**](https://nsubstitute.github.io/) is used for mocking.

### Functional Tests

Functional tests are used for testing the application as a whole. Real services are used for these tests.
[**Testcontainers**](https://dotnet.testcontainers.org/) are used to build real services.

### Integration Tests

Integration tests check if multiple modules of the application work together as expected.
These type of tests also use real services. [**Testcontainers**](https://dotnet.testcontainers.org/) are also used to achieve this.
