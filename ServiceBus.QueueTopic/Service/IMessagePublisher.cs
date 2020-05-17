using System;
using System.Threading.Tasks;

namespace ServiceBus.QueueTopic.Service
{
    public interface IMessagePublisher
    {
        Task Publish<T>(T value);

        Task Publish(string raw);
    }
}
