using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace solution
{
    public interface ISimpleServer
    {
        int MaxConcurrentRequests { get; }
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
    internal class RequestMetadata
    {
        internal int RequestId { get; set; }
        internal int ProcessTimeMs { get; set; }
    }
    public class SimpleServer : ISimpleServer
    {
        private Random rng = new Random();
        private int requestId = 1;
        private readonly object root = new object();
        private int GetRequestId()
        {
            lock (root)
            {
                return requestId++;
            }
        }
        public int MaxConcurrentRequests { get; private set; }
        protected readonly int msProcessTime;
        protected readonly int msJitter;
        private readonly int logicThreshold;
        private int concurrentRequests;
        public SimpleServer(int msProcessTime = 500, int msJitter = 200, int maxConcurrentRequests = -1, int logicThreshold = 90) : base()
        {
            this.msProcessTime = msProcessTime;
            this.msJitter = msJitter;
            MaxConcurrentRequests = maxConcurrentRequests;
            this.logicThreshold = logicThreshold;
        }
        public async Task<SimpleResponse> Get(SimpleRequest request)
        {
            try
            {
                await Task.Yield();
                var sw = new Stopwatch();
                sw.Start();
                var requestMetadata = new RequestMetadata
                {
                    ProcessTimeMs = rng.Next(msJitter) + msProcessTime,
                    RequestId = GetRequestId()
                };
                int requestsCount = Interlocked.Increment(ref concurrentRequests);

                Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.ffff}] Starting processing request {string.Format("{0,2}", requestMetadata.RequestId)}");
                if (MaxConcurrentRequests > 0 && requestsCount > MaxConcurrentRequests)
                {
                    throw new InvalidOperationException("Too many concurrent requests. Please try again later");
                }

                int output = await ServerLogic(request.FunctionName, request.InputValue, requestMetadata.ProcessTimeMs);

                sw.Stop();
                Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.ffff}] Finished processing request {string.Format("{0,2}", requestMetadata.RequestId)}. Process time: {string.Format("{0,4}", sw.ElapsedMilliseconds)} [ms]");
                return new SimpleResponse
                {
                    OutputValue = output
                };
            }
            finally
            {
                Interlocked.Decrement(ref concurrentRequests);
            }
        }
        protected async Task<int> ServerLogic(string functionName, int input, int? calculationTime = null)
        {
            if (calculationTime == null)
            {
                await Task.Delay(rng.Next(msJitter) + msProcessTime);
            }
            else
            {
                await Task.Delay(calculationTime.Value);
            }
            if (-logicThreshold <= input && input <= logicThreshold)
            {
                switch (functionName)
                {
                    case "square": return input * input;
                    case "abs": return Math.Abs(input);
                    case "identity": return input;
                    default: throw new ArgumentException(nameof(functionName));
                }
            }
            throw new SimpleException($"Server couldn't compute {functionName} of {input}");
        }
    }
}
