using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace StringManipulations.API
{
    public static class Manipulations
    {
        [FunctionName("Reverse")]
        public static async Task<IActionResult> Reverse(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "reverse")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            string returnVal = string.Concat(requestBody.Reverse());

            return (ActionResult)new OkObjectResult($"Reversed Body is: {returnVal}");
        }

        [FunctionName("SwaggerJson")]
        public static IActionResult SwaggerJson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger.json")] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation($"Path to function is {context.FunctionDirectory}");
            var path = System.IO.Path.Combine(context.FunctionDirectory, "../api/swagger.json");
            log.LogInformation($"Path to swagger is {path}");
            var stream = File.OpenRead(path);
            return (ActionResult)new FileStreamResult(stream, "application/json");
        }
    }
}
