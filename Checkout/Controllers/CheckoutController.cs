using System.Text.Json;
using System.Text.Json.Serialization;
using Dapr.Client;
using Grpc.Net.Client.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(DaprClient daprClient, ILogger<CheckoutController> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        [HttpPost]
        public async Task<CheckoutData> Post([FromBody] TestParameters testParameters)
        {
            var httpRequest = _daprClient.CreateInvokeMethodRequest("order-processor", "order", testParameters);

            var httpResponse = await _daprClient.InvokeMethodWithResponseAsync(httpRequest);

            OrderData? orderData;
            try
            {
                orderData = await httpResponse.EnsureSuccessStatusCode()
                    .Content
                    .ReadFromJsonAsync<OrderData>();

                _logger.LogInformation("Checkout - Succeeded!" + orderData!.OrderId);
            }
            catch (JsonException e)
            {
                var orderDataPayLoad = await httpResponse.Content.ReadAsStringAsync();

                _logger.LogError(e, "Checkout - failed! - Deserialization failed -  Payload: " + orderDataPayLoad);

                throw;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Checkout - failed! - {e.Message}");

                throw;
            }

            return new CheckoutData(orderData);
        }
    }

    public record TestParameters(int Size, int Delay);

    public record OrderData(Guid OrderId, string Data);

    public record CheckoutData(OrderData OrderData);
}
