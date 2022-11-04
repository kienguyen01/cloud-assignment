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
    public class EditOrderDetail
    {
        private readonly IOrderDetailService _orderDetailService;
        public EditOrderDetail(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [FunctionName("EditOrderDetail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            var reqContent = await new StreamReader(req.Body).ReadToEndAsync();

            EditOrderDetailDTO editOrderDetailDTO = JsonConvert.DeserializeObject<EditOrderDetailDTO>(reqContent);

            var orderDetail = await _orderDetailService.EditOrderDetail(editOrderDetailDTO);

            return new OkObjectResult(orderDetail);
        }
    }
}
