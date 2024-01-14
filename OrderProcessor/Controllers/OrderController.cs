using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Text.Json;

namespace OrderProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<OrderData> Post([FromBody] TestParameters testParameters)
        {
            var data = CreateData(testParameters.Size);

            await Task.Delay(testParameters.Delay);

            var orderData = new OrderData(Guid.NewGuid(), JsonSerializer.Serialize(data));

            _logger.LogInformation($"Order placed: {orderData.OrderId}");

            return orderData;
        }

        private object CreateData(int size)
        {
            var dynamicObjects = new List<dynamic>();

            // Generate dynamic objects with properties
            for (var i = 0; i < size; i++)
            {
                dynamic dynamicObject = new ExpandoObject();
                dynamicObject.Property1 = $"Value1_{i}";
                dynamicObject.Property2 = i;
                // Add more properties as needed

                // Add the dynamic object to the list
                dynamicObjects.Add(dynamicObject);
            }

            return dynamicObjects;
        }

        public record TestParameters(int Size, int Delay);

        public record OrderData(Guid OrderId, string Data);
    }
}
