using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Azure.Functions.Worker;

public class UpdateCounterFunction
{
    private readonly ILogger<UpdateCounterFunction> _logger;

    public UpdateCounterFunction(ILogger<UpdateCounterFunction> logger)
    {
        _logger = logger;
    }

    // Cosmos DB binding - retrieves a document with a fixed ID
    [Function("GetReusmeCounter")]
    public async Task<HttpResponseMessage> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        [CosmosDBInput(
            databaseName: "%CosmosDBDatabaseName%",
            containerName: "%CosmosDBContainerName%",
            Connection = "AzureResumeConnectionString",
            Id = "1",  
            PartitionKey = "1")] Counter counter)
    {
        _logger.LogInformation("HTTP trigger function started.");

        if (counter == null)
        {
            _logger.LogWarning("No counter document found with ID = 1.");
            return new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("Counter document with ID = 1 not found.")
            };
        }

        // Update the Count property (increment by 1 in this case)
        counter.Count += 1;

        // Cosmos client to update the document
        var client = new CosmosClient(Environment.GetEnvironmentVariable("AzureResumeConnectionString"));
        var container = client.GetContainer(Environment.GetEnvironmentVariable("CosmosDBDatabaseName"), Environment.GetEnvironmentVariable("CosmosDBContainerName"));
        
        try
        {
            // Use ReplaceItemAsync to replace the updated counter document back into Cosmos DB
            var response = await container.ReplaceItemAsync(counter, counter.Id, new PartitionKey(counter.Id));

            _logger.LogInformation($"Counter updated. ID: {counter.Id}, New Count: {counter.Count}");
        }
        catch (CosmosException ex)
        {
            _logger.LogError($"Failed to update counter document. Error: {ex.Message}");
            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent($"Failed to update counter document: {ex.Message}")
            };
        }

        // Return the updated counter document as the response (in JSON format)
        var responseJson = JsonConvert.SerializeObject(counter);
        var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(responseJson, System.Text.Encoding.UTF8, "application/json")
        };

        return httpResponse;
    }
}
