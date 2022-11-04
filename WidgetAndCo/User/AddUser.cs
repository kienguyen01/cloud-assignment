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

namespace WidgetAndCo.User
{
    public class AddUser
    {
        private readonly IUserService _userService;
        private readonly ILogger<AddUser> _logger;

        public AddUser(IUserService userService, ILogger<AddUser> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [FunctionName("AddUser")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, 
            ILogger log)
        {

            var reqContent = await new StreamReader(req.Body).ReadToEndAsync();

            AddUserDTO addUserDTO = JsonConvert.DeserializeObject<AddUserDTO>(reqContent);

            var user = await _userService.AddUser(addUserDTO);

            return new OkObjectResult(user);
        }
    }
}
