using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtoActorHelloWordSample
{
    public class HelloActor : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            //the message we received
            var msg = context.Message;
            //match message based on type
            if (msg is Hello helloMsg)
            {
                Console.WriteLine($"Hello {helloMsg.Who}");
            }
            return Task.CompletedTask;
        }
    }
}
