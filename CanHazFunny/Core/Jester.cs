using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using CanHazFunny.Output;
using CanHazFunny.Services;




namespace CanHazFunny.Core
{
    
    public class Jester{        
        private readonly JokeService _jokeService;
        private readonly JokeOutputStringer _output;

        public Jester(JokeService jokeService, JokeOutputStringer output)
        {
            ArgumentNullException.ThrowIfNull(jokeService);
            ArgumentNullException.ThrowIfNull(output);

            _jokeService = jokeService;
            _output = output;
        }

        public async Task TellJoke()
        {
            string joke;

            do
            {
                joke = await _jokeService.GetJokeAsync();
            }
            while (joke.Contains("Chuck Norris", StringComparison.OrdinalIgnoreCase));

            _output.Write(joke);
        }
    }
}
}
