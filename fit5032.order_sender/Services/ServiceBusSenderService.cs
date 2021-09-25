using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using fit5032.order_sender.ConfigOptions;
using fit5032.order_sender.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace fit5032.order_sender.Services
{
    public class ServiceBusSenderService: IServiceBusSenderService
    {
        private readonly ServiceBusSender _serviceBusSender;

        public ServiceBusSenderService(ServiceBusClient serviceBusClient, IOptions<ServiceBusConfig> serviceBusConfig)
        {
            var topic = serviceBusConfig.Value.Topic;
            _serviceBusSender = serviceBusClient.CreateSender(topic);
        }

        public async Task SendMessageAsync(OrderMessage orderMessage, CancellationToken cancellationToken = default)
        {
            var serviceBusMessage = CreateServiceBusMessage(orderMessage);

            // Send message to Service Bus
            await _serviceBusSender.SendMessageAsync(serviceBusMessage, cancellationToken);
        }

        private ServiceBusMessage CreateServiceBusMessage(OrderMessage orderMessage)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            // Convert OrderMessage object into json
            var jsonSerializedMessage = JsonConvert.SerializeObject(orderMessage);
            // Convert json into Byte array (Byte[])
            // Service Bus Messages need to be sent as Byte array
            var jsonSerializedMessageInBytes = new MemoryStream(Encoding.UTF8.GetBytes(jsonSerializedMessage)).ToArray();
            var message = new ServiceBusMessage(jsonSerializedMessageInBytes)
            {
                ContentType = "application/json"
            };

            return message;
        }
    }
}
