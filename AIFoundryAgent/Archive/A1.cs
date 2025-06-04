/* using Azure;
using Azure.AI.OpenAI;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class A1
{
    public static async Task Main(string[] args)
    {
        string endpoint = "https://bh-in-openai-aireimaginators.openai.azure.com/";
        string key = "2b4f4918e0ab451983e4a261c64ff671";


        var azureClient = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));

        #pragma warning disable OPENAI001
        Azure.AI.OpenAI.AssistantClient assistantClient = azureClient.GetAssistantClient();

        AssistantCreationOptions assistantCreationOptions = new AssistantCreationOptions("gpt-35-turbo")
        {
            Name = "Assistant722",
            Instructions = "calculate the confidence score based on access_frequency_last_90_days for all applications. " +
                          "if access_frequency_last_90_days > 80 than confidence_score = 0.8 else if access_frequency_last_90_days > 50 and < 80 then confidence_score = 0.6 " +
                          "else if access_frequency_last_90_days > 10 and < 50 then confidence_score = 0.2 return confidence_score",
            Tools = { ToolDefinition.CreateCodeInterpreter() },
            ToolResources = new Dictionary<string, object>
            {
                { "file_search", new { vector_store_ids = new[] { "vs_Pj07jNURsFuxF1Rxxl8wbQhC" } } },
                { "code_interpreter", new { file_ids = new[] { "assistant-SdDWWvDvRCXkMeuZYUB16i", 
                                                                  "assistant-C8Rx6ivcSn4X7bXSNF4YKa",
                                                                  "assistant-9QLYSuHA3zbBhe4LTS84bG", 
                                                                  "assistant-52UsKvU8K1m4wGAGbPzjFC" } } }
            },
            Temperature = 1,
            TopP = 1
        };

        Assistant assistant = await assistantClient.CreateAssistantAsync(assistantCreationOptions);
    }
}
 */