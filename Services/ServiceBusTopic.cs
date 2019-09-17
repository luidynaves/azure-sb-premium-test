using System;
using System.Text;
using System.Threading.Tasks;
using luidy_bus_test.Model;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace luidy_bus_test.Services
{
    public class ServiceBusTopic
    {
        private readonly TopicClient _topicClient;

        public ServiceBusTopic(TopicClient topicClient)
        {
            _topicClient = topicClient;
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