using System;

namespace DDDSouthWest.Domain
{
    public class DuplicateRecordException : Exception
    {
        public DuplicateRecordException()
        {
        }

        public DuplicateRecordException(string message) : base(message)
        {
        }

        public DuplicateRecordException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}