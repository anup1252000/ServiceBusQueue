using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace ServiceBus.QueueTopic.Service
{
    public class MessagePublisher:IMessagePublisher
    {
        private readonly IQueueClient _queueClient;

        public MessagePublisher(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public Task Publish<T>(T value)
        {
            var message = new Message
            {
                Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value))
            };
            return _queueClient.SendAsync(message);
        }

        public Task Publish(string raw)
        {
            var message = new Message(Encoding.UTF8.GetBytes(raw));
            return _queueClient.SendAsync(message);
        }
    }
}
