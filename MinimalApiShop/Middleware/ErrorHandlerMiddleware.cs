using MinimalApiShop.Extensions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace MinimalApiShop.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext content)
    {
        try
        {
            await _next(content);
        }
        catch (InvalidDataException ex)
        {
            await content.Response
                .WithStatusCode(Status405MethodNotAllowed)
                .WithJsonContent(ex.Message);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            await content.Response
                .WithStatusCode(Status416RangeNotSatisfiable)
                .WithJsonContent(ex.Message);
        }
        catch (ArgumentException ex)
        {
            await content.Response
                .WithStatusCode(Status400BadRequest)
                .WithJsonContent(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            await content.Response
                .WithStatusCode(Status203NonAuthoritative)
                .WithJsonContent(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await content.Response
                .WithStatusCode(Status403Forbidden)
                .WithJsonContent(ex.Message);
        }
        catch (Exception ex)
        {
            await content.Response
                .WithStatusCode(Status400BadRequest)
                .WithJsonContent(ex.Message);
        }
    }
}