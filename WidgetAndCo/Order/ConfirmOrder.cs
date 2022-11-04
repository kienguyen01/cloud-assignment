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
   
    public class ConfirmOrder
    {
        public readonly IOrderService _orderService;
        public ConfirmOrder(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [FunctionName("ConfirmOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "ConfirmOrder/{OrderId}")] HttpRequest req, Guid OrderId,
            ILogger log)
        {
            var order = await _orderService.ConfirmOrder(OrderId);

            return new OkObjectResult(order);
        }
    }
}
