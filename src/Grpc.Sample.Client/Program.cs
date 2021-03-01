using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Sample.Server;
using System;

using var channel = GrpcChannel.ForAddress("https://localhost:5001");

var greeterClient = new Greeter.GreeterClient(channel);
var sayHelloReply = await greeterClient.SayHelloAsync(new()
{
    Name = "Lucimarck"
});

Console.WriteLine(sayHelloReply.Message);

var counterClient = new Counter.CounterClient(channel);
var getCountCall = counterClient.GetCount(new()
{
    InitialValue = 0,
    DelayDuration = TimeSpan.FromSeconds(1).ToDuration(),
    MaximumValue = 100,
});

await foreach (var reply in getCountCall.ResponseStream.ReadAllAsync())
{
    Console.WriteLine(reply.CurrentValue);
}