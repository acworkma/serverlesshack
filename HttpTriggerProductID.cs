using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace My.ProductID
{
    public static class HttpTriggerProductID
    {
        [FunctionName("HttpTriggerProductID")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string ProductID = req.Query["ProductID"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            ProductID = ProductID ?? data?.ProductID;

            string responseMessage = string.IsNullOrEmpty(ProductID)
                ? "This HTTP triggered function executed successfully. Pass a ProductID in the query string or in the request body for a personalized response."
                : $"The product name for {ProductID} is Starfuit Explosion";

            return new OkObjectResult(responseMessage);
        }
    }
}
