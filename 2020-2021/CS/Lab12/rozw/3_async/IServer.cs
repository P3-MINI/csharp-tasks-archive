using System;
using System.Threading.Tasks;

namespace _3_async
{
    public interface IServer
    {
        Task<SimpleResponse> Get(SimpleRequest request);
    }

    public class SimpleRequest
    {
        public string FunctionName { get; set; }
        public int InputValue { get; set; }
    }

    public class SimpleResponse
    {
        public int OutputValue { get; set; }
    }

    public class SimpleException : Exception
    {
        public SimpleException(string message) : base(message) { }
    }
}
