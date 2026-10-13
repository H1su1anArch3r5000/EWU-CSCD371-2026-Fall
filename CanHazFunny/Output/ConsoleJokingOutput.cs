using System;
using System.Collections.Generic;
using System.Text;

namespace CanHazFunny.Output
{
    public class ConsoleJokingOutput : JokeOutputStringer
    {
        public void Write(string message) {
            Console.WriteLine(message);
        }
    }
}
