using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using cloud_db.Domain.DTO;
using cloud_db.DAL.Service;

namespace WidgetAndCo.Product
{
    public class AddProduct
    {
        private readonly IProductService _productService;

        public AddProduct(IProductService productService)
        {
            _productService = productService;
        }

        [FunctionName("AddProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string Connection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            string containerName = Environment.GetEnvironmentVariable("Container");
            Stream myBlob = new MemoryStream();
            var file = req.Form.Files["File"];
            myBlob = file.OpenReadStream();
            var blobClient = new BlobContainerClient(Connection, containerName);
            var blob = blobClient.GetBlobClient(file.FileName);
            await blob.UploadAsync(myBlob);

            AddProductDTO addProductDTO = new AddProductDTO()
            {
                Price = double.Parse(req.Form["Price"]),
                ProductName = req.Form["ProductName"],
                ProductPictureName = file.FileName
            };

            var Product = await _productService.AddProduct(addProductDTO);

            return new CreatedResult("Products", Product);
        }
    }
}
