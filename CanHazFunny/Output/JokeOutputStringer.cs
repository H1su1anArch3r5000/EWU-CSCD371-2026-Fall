using System;
using System.Collections.Generic;
using System.Text;

namespace CanHazFunny.Output
{
    public interface JokeOutputStringer
    {
        public abstract void Write(string message);
    }
}
