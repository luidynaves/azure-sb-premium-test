using luidy_bus_test.Model;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessaging
{
    public class ServiceBusTopicSender
    {
        private readonly TopicClient _topicClient;
        private readonly IConfiguration _configuration;
        private const string TOPIC_PATH = "transaction";

        public ServiceBusTopicSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _topicClient = new TopicClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                TOPIC_PATH
            );
        }
        
        public async Task SendMessage(MyPayload payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            try
            {
                await _topicClient.SendAsync(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}