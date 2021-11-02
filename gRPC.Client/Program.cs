using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace gRPC.Client
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:49157");

            await GreeterAsync(channel);
            await CounterAsync(channel);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }

        private static async Task GreeterAsync(GrpcChannel channel)
        {
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });

            Console.WriteLine("Greeting: " + reply.Message);                       
        }

        private static async Task CounterAsync(GrpcChannel channel)
        {

            var client = new Counter.CounterClient(channel);
            var reply = await client.CountAsync(
                              new CountRequest { Name = "Guilherme" });

            Console.WriteLine(
                  $"Mensagem: {reply.Message} " +
                  $"| Valor Atual: {reply.CurrentValue} " +
                  $"| Local: {reply.LocalSvc} " +
                  $"| Target Framework: {reply.TargetFramework} |");

        }


    }
}
