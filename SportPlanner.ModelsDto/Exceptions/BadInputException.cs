using System;

namespace SportPlanner.ModelsDto.Exceptions
{
    public class BadInputException : Exception
    {
        public BadInputException(string message) : base(message)
        {
        }
    }
}

