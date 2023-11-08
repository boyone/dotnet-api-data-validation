using System.Net;
using System.Net.Http.Json;
using System.Text;
using api.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace api_data_validate_test;

public class ModelValidatorIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _fixture;

    public ModelValidatorIntegrationTest(
        WebApplicationFactory<Program> fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ItShouldHaveItemNameErrorMessageWhenModelHaveNoItemNameProperty()
    {
        HttpClient client = _fixture.CreateClient();
        var invalidModel = new Foo
        {
            Amount = 1000,
            Name = "foo",
            Items = new List<Item>
            {
                new() { Name = "first", Value = 1 },
                new() { },
            }
        };
        var json = JsonConvert.SerializeObject(invalidModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/FOO", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
        var httpValidationProblemDetails = await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();

        Assert.Equal("The Name field is required.", httpValidationProblemDetails.Errors["Items[1].Name"][0]);
        Assert.Equal("The Value field is required.", httpValidationProblemDetails.Errors["Items[1].Value"][0]);
        // var content = await response.Content.ReadAsStringAsync();
        // Assert.Equal("pong", content);
    }
    
    [Fact]
    public async Task ItShouldHaveItemNameErrorMessageWhenModelValueOutOfRange()
    {
        HttpClient client = _fixture.CreateClient();
        var invalidModel = new Foo
        {
            Amount = 1000,
            Name = "foo",
            Items = new List<Item>
            {
                new() { Name = "first", Value = 11 },
            }
        };
        var json = JsonConvert.SerializeObject(invalidModel);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/FOO", content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
        var httpValidationProblemDetails = await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();

        Assert.Equal("The field Value must be between 1 and 10.", httpValidationProblemDetails.Errors["Items[0].Value"][0]);
    }
}


/*
*  Microsoft.AspNetCore.Http.HttpValidationProblemDetails
* {
*      "type":"https://tools.ietf.org/html/rfc7231#section-6.5.1",
*      "title":"One or more validation errors occurred.",
*      "status":400,
*      "traceId":"00-295e6fe0f96b5ca1cb538dbe7dd5bc52-c848772053997126-00",
*      "errors":{
*          "Items[1].Name":["The Name field is required."],
*          "Items[1].Value":["The Value field is required."],
*      }
* }
*/