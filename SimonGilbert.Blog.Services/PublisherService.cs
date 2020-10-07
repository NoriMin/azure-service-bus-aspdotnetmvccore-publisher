using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SimonGilbert.Blog.ViewModels;

namespace SimonGilbert.Blog.Services
{
    public class PublisherService : IPublisherService
    {
        private const string ServiceBusPrimaryConnectionString = "Endpoint=sb://taminaholprep.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=3mWFoAN6DSbitTYB7zgqGGK3/eGClWYrea5dORyGYCg=";
        private const string TopicName = "restaurant-orders";
        private static ITopicClient _topicClient;

        public PublisherService()
        {
            _topicClient = new TopicClient(
                ServiceBusPrimaryConnectionString, 
                TopicName);
        }

        public async Task Send(RestaurantOrderViewModel viewModel)
        {
            var message = ToMessage(viewModel);

            try
            {
                await _topicClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            finally
            {
                await _topicClient.CloseAsync();
            }
        }

        private static Message ToMessage(RestaurantOrderViewModel model)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            var message = new Message
            {
                Body = body,
                ContentType = "text/plain",
            };

            return message;
        }
    }
}
