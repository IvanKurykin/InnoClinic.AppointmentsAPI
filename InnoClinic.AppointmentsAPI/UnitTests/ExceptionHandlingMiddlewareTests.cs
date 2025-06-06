using API.Middleware;
using BLL.Exceptions;
using DAL.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using UnitTests.TestCases;

namespace UnitTests;

public class ExceptionHandlingMiddlewareTests
{
    [Fact]
    public async Task InvokeAsyncNoExceptionContinuesPipeline()
    {
        var httpContext = new DefaultHttpContext();
        var wasNextCalled = false;
        var middleware = new ExceptionHandlingMiddleware(_ =>
        {
            wasNextCalled = true;
            return Task.CompletedTask;
        });

        await middleware.InvokeAsync(httpContext);

        wasNextCalled.Should().BeTrue();
        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task InvokeAsyncValidationExceptionReturns400WithDetails()
    {
        var httpContext = new DefaultHttpContext();
        await using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;

        var validationFailures = new List<ValidationFailure>
        {
            new(MiddlewareTestCases.NameField, MiddlewareTestCases.NameError),
            new(MiddlewareTestCases.EmailField, MiddlewareTestCases.EmailError)
        };
        var validationException = new ValidationException(validationFailures);

        var middleware = new ExceptionHandlingMiddleware(_ => throw validationException);

        await middleware.InvokeAsync(httpContext);

        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        httpContext.Response.ContentType.Should().Be("application/json");

        memoryStream.Seek(0, SeekOrigin.Begin);
        var json = await JsonSerializer.DeserializeAsync<JsonElement>(memoryStream);

        json.GetProperty(MiddlewareTestCases.Error).GetString().Should().Be(MiddlewareTestCases.ValidationErrorMessage);

        var details = json.GetProperty(MiddlewareTestCases.Details).EnumerateArray().ToList();
        details.Should().HaveCount(2);
        details[0].GetProperty(MiddlewareTestCases.PropertyName).GetString().Should().Be(MiddlewareTestCases.NameField);
        details[0].GetProperty(MiddlewareTestCases.ErrorMessage).GetString().Should().Be(MiddlewareTestCases.NameError);
    }

    [Theory]
    [InlineData(typeof(NotFoundException<Appointment>), StatusCodes.Status404NotFound)]
    [InlineData(typeof(NotFoundException<AppointmentResult>), StatusCodes.Status404NotFound)]
    public async Task InvokeAsyncCustomExceptionReturnsCorrectStatusCode(Type exceptionType, int expectedStatusCode)
    {
        var httpContext = new DefaultHttpContext();
        await using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;

        var exception = (Exception)Activator.CreateInstance(exceptionType, "Test message")!;
        var middleware = new ExceptionHandlingMiddleware(_ => throw exception);

        await middleware.InvokeAsync(httpContext);

        httpContext.Response.StatusCode.Should().Be(expectedStatusCode);
        httpContext.Response.ContentType.Should().Be("application/json");

        memoryStream.Seek(0, SeekOrigin.Begin);
        var json = await JsonSerializer.DeserializeAsync<JsonElement>(memoryStream);

        json.GetProperty(MiddlewareTestCases.Error).GetString().Should().Be("Test message");
        json.GetProperty(MiddlewareTestCases.Details).ValueKind.Should().Be(JsonValueKind.Null);
    }

    [Fact]
    public async Task InvokeAsyncUnhandledExceptionReturns500()
    {
        var httpContext = new DefaultHttpContext();
        await using var memoryStream = new MemoryStream();
        httpContext.Response.Body = memoryStream;

        var exception = new Exception(MiddlewareTestCases.GenericExceptionMessage);
        var middleware = new ExceptionHandlingMiddleware(_ => throw exception);

        await middleware.InvokeAsync(httpContext);

        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        httpContext.Response.ContentType.Should().Be("application/json");

        memoryStream.Seek(0, SeekOrigin.Begin);
        var json = await JsonSerializer.DeserializeAsync<JsonElement>(memoryStream);

        json.GetProperty(MiddlewareTestCases.Error).GetString().Should().Be(MiddlewareTestCases.GenericExceptionMessage);
        json.GetProperty(MiddlewareTestCases.Details).ValueKind.Should().Be(JsonValueKind.Null);
    }
}