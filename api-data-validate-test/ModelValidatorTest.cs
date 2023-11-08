using System.ComponentModel.DataAnnotations;
using api.Controllers;
using api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api_data_validate_test;

public class ModelValidatorTest
{
    [Fact]
    public void ItShouldNotHaveAnyErrorMessageWhenModelIsValid()
    {
        var model = new Foo
        {
            Name = "foo",
            Amount = 1000,
            Items = new List<Item>
            {
                new() { Name = "first", Value = 1 },
                new() { Name = "second", Value = 2 },
            }
        };

        VerifyModel(model, new List<string>());
    }

    [Fact]
    public void ItShouldHaveAmountErrorMessageWhenModelHaveNoAmountProperty()
    {
        var model = new Foo
        {
            Name = "foo",
            Items = new List<Item>
            {
                new() { Name = "first", Value = 1 },
                new() { Name = "second", Value = 2 },
            }
        };

        var validationContext = new ValidationContext(model, null, null);
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(model, validationContext, validationResults, true);

        Assert.Equal("The Amount field is required.", validationResults.First().ErrorMessage);
    }

    [Fact]
    public void ItShouldHaveErrorMessageWhenModelHaveNoRequiredProperty()
    {
        var model = new Foo
        {
            Items = new List<Item>
            {
                new() { Name = "first", Value = 1 },
                new() { Name = "second", Value = 2 },
            }
        };
        var expectedErrorMessages = new List<string>
        {
            "The Name field is required.",
            "The Amount field is required.",
        };

        VerifyModel(model, expectedErrorMessages);
    }

    [Fact]
    public void ItShouldHaveItemNameErrorMessageWhenModelHaveNoItemNameProperty()
    {
        var model = new Foo
        {
            Amount = 1000,
            Name = "foo",
            Items = new List<Item>
            {
                new() { Name = "first", Value = 1 },
                new() { Value = 2 },
            }
        };

        var expectedErrorMessages = new List<List<string>>
        {
            new(),
            new()
            {
                "The Name field is required.",
            }
            
        };

        VerifyModel(model, new List<string>());
        VerifyCollectionModel(model.Items, expectedErrorMessages);
    }

    private void VerifyCollectionModel(List<Item> modelItems, List<List<string>> expectedErrorMessages)
    {
        for (int i = 0; i < modelItems.Count; i++)
        {
            VerifyModel(modelItems[i], expectedErrorMessages[i]);
        }
    }

    protected void VerifyModel<TModel>(TModel model, List<string> expectedErrorMessages)
    {
        List<string> errorMessages = new List<string>();
        var validationContext = new ValidationContext(model, null, null);
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        foreach (var validationResult in validationResults)
        {
            errorMessages.Add(validationResult.ErrorMessage);
        }

        // Assert.Equal(expectedErrorMessages.Count, errorMessages.Count);
        expectedErrorMessages.Sort();
        errorMessages.Sort();
        Assert.Equal(expectedErrorMessages, errorMessages);
    }
}