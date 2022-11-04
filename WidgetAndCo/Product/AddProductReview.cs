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

namespace WidgetAndCo.Product
{
    public  class AddProductReview
    {
        private readonly IProductService _productService;
        public AddProductReview(IProductService productService)
        {
            _productService = productService;
        }
        [FunctionName("AddProductReview")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var reqContent = await new StreamReader(req.Body).ReadToEndAsync();

            AddProductReviewDTO addProductReviewDTO = JsonConvert.DeserializeObject<AddProductReviewDTO>(reqContent);
            var product = await _productService.AddProductReview(addProductReviewDTO);

            return new OkObjectResult(product);
        }
    }
}
