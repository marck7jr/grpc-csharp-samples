using Grpc.Core;
using System.Threading.Tasks;

namespace Grpc.Sample.Server
{
    public class CounterService : Counter.CounterBase
    {
        public override async Task GetCount(CounterRequest request, IServerStreamWriter<CounterReply> responseStream, ServerCallContext context)
        {
            int value = request.InitialValue;

            while ((request.MaximumValue == 0 || value <= request.MaximumValue) & !context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new()
                {
                    CurrentValue = value
                });

                await Task.Delay(request.DelayDuration.ToTimeSpan());

                value++;
            }
        }
    }
}
