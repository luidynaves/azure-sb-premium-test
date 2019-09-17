using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace luidy_bus_test.Services
{
    public class EventHubSender
    {
        private readonly EventHubClient _eventHubClient;

        public EventHubSender(EventHubClient eventHubClient)
        {
            _eventHubClient = eventHubClient;
        }

        public async Task PublishAsync<T>(T message)
        {
            try
            {
                var eventData = new EventData(GetMessageBytes(message));

                await _eventHubClient.SendAsync(eventData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] GetMessageBytes<T>(T message)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var jsonMessage = JsonConvert.SerializeObject(message, serializerSettings);
            return Encoding.UTF8.GetBytes(jsonMessage);
        }
    }
}