using System;
using System.Collections.Generic;

namespace DuplicateCheck
{
    public class DuplicateCheckException : Exception
    {
        public DuplicateCheckException(string message)
            : base(message)
        {
        }

        public IEnumerable<DuplicateDetails> DuplicateDetails { get; set; }
    }
}
