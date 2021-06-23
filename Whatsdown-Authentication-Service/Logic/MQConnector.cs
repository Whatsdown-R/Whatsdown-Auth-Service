using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;



using Newtonsoft.Json;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_Authentication_Service.Logic
{
	public class MQConnector : MQConnectorBase
	{
		private readonly EventingBasicConsumer consumer;
		

		public MQConnector(IOptions<ConnectionStrings> connectionStrings, ILogger<MQConnector> logger, IOptions<Credentials> mqCredentials) : base(connectionStrings, logger, mqCredentials)
		{
			consumer = new EventingBasicConsumer(Channel);
			//consumer.Received += On_SensorData_Received;


			Channel.QueueDeclare("new-profile-queue", true, exclusive: false, autoDelete: false, arguments: null);

		
		}
		public void PublishProfile(CreateProfile profile)
        {
			var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(profile));
			Channel.BasicPublish("", routingKey: "new-profile-queue", null, body);
		}

		/*private void On_SensorData_Received(object sender, BasicDeliverEventArgs e)
		{
			var body = Encoding.UTF8.GetString(e.Body.ToArray());
			logger.LogDebug($"message received from MQ: '{body}'");

			var message = JsonConvert.DeserializeObject<SensorDataMessage>(body);
			logger.LogInformation($"message received with {message.DataType.Name} and {message.Data}");
		}*/
	}
}
