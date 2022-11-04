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

namespace WidgetAndCo.OrderDetail
{
    public class CreateOrderDetail
    {
        private readonly IOrderDetailService _orderDetailService;
        public CreateOrderDetail(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [FunctionName("CreateOrderDetail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var reqContent = await new StreamReader(req.Body).ReadToEndAsync();

            CreateOrderDetailDTO createOrderDetailDTO = JsonConvert.DeserializeObject<CreateOrderDetailDTO>(reqContent);

            var orderDetail = await _orderDetailService.CreateOrderDetail(createOrderDetailDTO);

            return new OkObjectResult(orderDetail);
        }
    }
}
