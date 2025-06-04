/* using Azure;
using Azure.Identity;
using Azure.AI.OpenAI; // Ensure this is the correct namespace for OpenAI
using Azure.AI.Agents.Persistent; // Ensure this is the correct namespace for Agents

public class OpenAIAgentServiceV2
{
    // This method runs a conversation with an AI agent using Azure AI Agents SDK.
    // It retrieves messages from the agent and displays them in the console.

    public async Task RunAgentConversation()
    {
        var endpoint = new Uri("https://bh-in-openai-aireimagin-resource.services.ai.azure.com/api/projects/bh-in-openai-aireimagin-project");
        
        // Create an instance of the OpenAIClient
        var client = new OpenAIClient(endpoint, new DefaultAzureCredential());

        // Create an instance of the PersistentAgentsClient
        PersistentAgentsClient agentsClient = new PersistentAgentsClient(endpoint.ToString(), new DefaultAzureCredential());

        // Retrieve the agent
        PersistentAgent agent = agentsClient.Administration.GetAgent("asst_TOYNhU4klwFGcozqPTWZMyrB");

        // Retrieve the thread
        PersistentAgentThread thread = agentsClient.Threads.GetThread("thread_n8lXg0Wmif8vzMKGeIijGKKI");

        // Create a message
        PersistentThreadMessage messageResponse = agentsClient.Messages.CreateMessage(
            thread.Id,
            MessageRole.User,
            "Get Confidence score for User ID: U_10003993, App ID: App_2dd824db"
        );

        // Create a run
        ThreadRun run = agentsClient.Runs.CreateRun(
            thread.Id,
            agent.Id
        );

        // Poll until the run reaches a terminal status
        do
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            run = agentsClient.Runs.GetRun(thread.Id, run.Id);
        }
        while (run.Status == RunStatus.Queued || run.Status == RunStatus.InProgress);

        if (run.Status != RunStatus.Completed)
        {
            throw new InvalidOperationException($"Run failed or was canceled: {run.LastError?.Message}");
        }

        // Retrieve messages
        Pageable<PersistentThreadMessage> messages = agentsClient.Messages.GetMessages(
            thread.Id, order: ListSortOrder.Ascending
        );

        // Display messages
        foreach (PersistentThreadMessage threadMessage in messages)
        {
            Console.Write($"{threadMessage.CreatedAt:yyyy-MM-dd HH:mm:ss} - {threadMessage.Role,10}: ");
            foreach (MessageContent contentItem in threadMessage.ContentItems)
            {
                if (contentItem is MessageTextContent textItem)
                {
                    Console.Write(textItem.Text);
                }
                else if (contentItem is MessageImageFileContent imageFileItem)
                {
                    Console.Write($"<image from ID: {imageFileItem.FileId}");
                }
                Console.WriteLine();
            }
        }
    }
}


 */