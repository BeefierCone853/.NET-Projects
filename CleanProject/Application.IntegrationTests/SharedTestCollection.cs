using Application.IntegrationTests.Abstractions;

namespace Application.IntegrationTests;

[CollectionDefinition(nameof(SharedTestCollection))]
public class SharedTestCollection : ICollectionFixture<IntegrationTestWebAppFactory>;