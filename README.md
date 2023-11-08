# README

1. Extend `IClassFixture<WebApplicationFactory<Program>>`
2. Add `public partial class Program {}` in the end of line of `Program.cs`
3. Test Web API with `HttpClient client = _fixture.CreateClient();`

```csharp
[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
```

```csharp
[Collection("Database collection")]
private readonly IOptions<AppSettings> _options;

public SimpleIntegrationTest(DatabaseFixture databaseFixture)
{
    _options = databaseFixture.options;
}
```
---

## References

1. [https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0#built-in-attributes](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0#built-in-attributes)
2. [A complete list of validation attributes](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0)
3. [Basic tests with the default WebApplicationFactory](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0#basic-tests-with-the-default-webapplicationfactory)