using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using cloud_db.Repository;
using cloud_db.DAL.Service;

namespace WidgetAndCo.Product
{
    public class GetProduct
    {
        private readonly IProductService _productService;

        public GetProduct(IProductService productService)
        {
            _productService = productService;
        }
        [FunctionName("GetProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "RemoveProduct/{productId}")] HttpRequest req, Guid productId,
            ILogger log)
        {
            //var product = _productService.GetProduct(productId);

            return new OkObjectResult(await _productService.GetProduct(productId));
        }
    }
}
