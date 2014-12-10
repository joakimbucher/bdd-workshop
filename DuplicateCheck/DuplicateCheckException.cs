using System;

namespace DuplicateCheck
{
    public class DuplicateCheckException : Exception
    {
        public DuplicateCheckException(string message)
            : base(message)
        {
        }
    }
}
