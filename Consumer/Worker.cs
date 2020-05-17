using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Consumer
{
    public class Employee
    {
        public Employee()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IQueueClient queueClient;

        public Worker(ILogger<Worker> logger, IQueueClient queueClient)
        {
            _logger = logger;
            this.queueClient = queueClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                queueClient.RegisterMessageHandler((Message message, CancellationToken cancellationToken) =>
                {
                    var employee = JsonConvert.DeserializeObject<Employee>(Encoding.UTF8.GetString(message.Body));
                    _logger.LogInformation(employee.Id+""+employee.Name);
                    return queueClient.CompleteAsync(message.SystemProperties.LockToken);
                },x=>Task.CompletedTask);
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
