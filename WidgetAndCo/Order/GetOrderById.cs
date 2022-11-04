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
    public class GetOrderById
    {
        public readonly IOrderService _orderService;
        public GetOrderById(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [FunctionName("GetOrderById")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetOrderById/{orderId}")] HttpRequest req, Guid orderId,
            ILogger log)
        {
            return new OkObjectResult(await _orderService.GetOrder(orderId));
        }
    }
}
