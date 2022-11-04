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

namespace WidgetAndCo.OrderDetail
{
    public class RemoveOrderDetail
    {
        private readonly IOrderDetailService _orderDetailService;
        public RemoveOrderDetail(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [FunctionName("RemoveOrderDetail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "RemoveOrderDetail/{orderDetailId}")] HttpRequest req, Guid orderDetailId,
            ILogger log)
        {
            await _orderDetailService.RemoveOrderDetail(orderDetailId);

            return new OkResult();
        }
    }
}
