using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CanHazFunny.Services;

public class JokeService : JokeBuilder
{
    private HttpClient HttpClient { get; } = new();
    private readonly HttpClient _http;

    public JokeService(HttpClient http)
    {
        ArgumentNullException.ThrowIfNull(http);
        _http = http;
    }

    public async Task<string> GetJokeAsync()
    {
        string joke = await _http.GetStringAsync("https://geek-jokes.sameerkumar.website/api?format=text");
        return joke;
    }
   
}
