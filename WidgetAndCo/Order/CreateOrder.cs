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
using cloud_db.Domain.DTO;

namespace WidgetAndCo.Order
{
    public class CreateOrder
    {
        public readonly IOrderService _orderService;
        public CreateOrder(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [FunctionName("CreateOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var reqContent = await new StreamReader(req.Body).ReadToEndAsync();

            CreateOrderDTO createOrderDTO = JsonConvert.DeserializeObject<CreateOrderDTO>(reqContent);

            var order = await _orderService.CreateOrder(createOrderDTO);

            return new OkObjectResult(order);
        }
    }
}
