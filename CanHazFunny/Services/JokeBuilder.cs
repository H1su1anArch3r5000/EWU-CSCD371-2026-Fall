using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CanHazFunny.Services;
public interface JokeBuilder
{
    Task<string> GetJokeAsync();
}
