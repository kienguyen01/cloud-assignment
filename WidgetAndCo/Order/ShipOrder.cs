using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using cloud_db.DAL.Service;

namespace WidgetAndCo.Order
{
    public class ShipOrder
    {
        public readonly IOrderService _orderService;
        public ShipOrder(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [FunctionName("ShipOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "ShipOrder/{orderId}")] HttpRequest req, Guid orderId,
            ILogger log)
        {
            var product = await _orderService.ShipOrder(orderId);

            return new OkObjectResult(product);
        }
    }
}
