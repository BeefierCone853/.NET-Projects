using WebApi.FunctionalTests.Abstractions;

namespace WebApi.FunctionalTests;

[CollectionDefinition(nameof(SharedTestCollection))]
public class SharedTestCollection : ICollectionFixture<FunctionalTestWebAppFactory>;