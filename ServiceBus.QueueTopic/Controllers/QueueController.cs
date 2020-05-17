using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.QueueTopic.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceBus.QueueTopic.Controllers
{
    [Route("api/[controller]")]
    public class QueueController : Controller
    {
        private readonly IMessagePublisher _messagePublisher;

        public QueueController(IMessagePublisher messagePublisher)
        {
            this._messagePublisher = messagePublisher;
        }

        // POST api/Queue
        [HttpPost]
        public async Task<ActionResult> PublishToQueue([FromBody]Employee employee)
        {
            await _messagePublisher.Publish(employee);
            return Ok();
        }
    }
}
