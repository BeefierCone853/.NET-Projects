using System.Net.Http.Headers;
using MVC.Abstractions;

namespace MVC.Services.Base;

public class BaseHttpService(IClient client, ILocalStorageService localStorage)
{
    protected readonly ILocalStorageService _localStorage = localStorage;
    protected readonly IClient _client = client;

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        return ex.StatusCode switch
        {
            400 => new Response<Guid>()
            {
                Message = "Validation Errors have occured.", ValidationErrors = ex.Response, Success = false
            },
            404 => new Response<Guid>() { Message = "The requested item could not be found.", Success = false },
            _ => new Response<Guid>() { Message = "Something went wrong, please try again.", Success = false }
        };
    }

    protected void AddBearerToken()
    {
        if (_localStorage.Exists("token"))
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                _localStorage.GetStorageValue<string>("token"));
        }
    }
}