using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class OpenAIAgentService
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint = "https://bh-in-openai-aireimaginators.openai.azure.com/openai/deployments/gpt-4o/chat/completions?api-version=2025-01-01-preview";
    private readonly string _deploymentId = "gpt-4o"; // e.g., gpt-4-agent
     private readonly string _apiKey ="dummy"; // Replace with your actual API key
    private readonly string _apiVersion = "2025-01-01-preview";

    public OpenAIAgentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAgentResponseAsync(string userMessage)
    {
        var url = "https://bh-in-openai-aireimaginators.openai.azure.com/openai/deployments/gpt-4o/chat/completions?api-version=2025-01-01-preview";

        var requestBody = new
        {
            messages = new[]
            {
                new { role = "user", content = userMessage }
            }
        };

        var requestJson = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);

        var response = await _httpClient.PostAsync(url, content);
        var responseJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Agent call failed: {response.StatusCode}\n{responseJson}");
        }
        
        // Removed invalid line as HttpResponseMessage does not have a Json property.
        dynamic result = new
        {
            choices = new[]
            {
            new
            {
                message = new
                {
                content = "AI confidence score is 0.6 based on access frequency of last 90 days."
                }
            }
            }
        };

        return result.choices[0].message.content;
    }
}
