using System;
using System.Runtime.Serialization;

namespace Curso.CSharp.Api.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() { }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, Exception exception) : base(message, exception) { }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
