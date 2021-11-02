using gRPC.Client;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace gRPC.Server
{
    public class CountService : Counter.CounterBase
    {
        private readonly ILogger<CountService> _logger;
        private static Contador _CONTADOR = new Contador();
        private static string _FRAMEWORK;
        private static string _LOCAL;

        static CountService()
        {
            _FRAMEWORK = Assembly
                        .GetEntryAssembly()?
                        .GetCustomAttribute<TargetFrameworkAttribute>()?
                        .FrameworkName;
            _LOCAL = Environment.MachineName;
        }
        
        public CountService(ILogger<CountService> logger)
        {
            _logger = logger;
        }

        public override Task<CountReply> Count(CountRequest request, ServerCallContext context)
        {
            _CONTADOR.Incrementar();
            int valorAtual = _CONTADOR.CurrentValue;

            _logger.LogInformation($"Valor atual: {valorAtual}");

            return Task.FromResult(new CountReply
            {
                Message = "Olá " + request.Name,
                CurrentValue = valorAtual,
                LocalSvc = _LOCAL,
                TargetFramework = _FRAMEWORK
            });
        }
    }
}
