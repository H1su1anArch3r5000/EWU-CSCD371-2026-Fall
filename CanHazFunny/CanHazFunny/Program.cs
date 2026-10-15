namespace CanHazFunny;
using CanHazFunny.Output;
using CanHazFunny.Services;
class Program
{
    public static async Task Main(string[] args)
    {
        var http = new HttpClient();
        var jokeService = new JokeService(http);
        var output = new ConsoleJokingOutput();

        var jester = new Jester(jokeService, output);

        await jester.TellJokeAsync();
    }
}
