using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddChatClient(new OllamaChatClient(new Uri("http://localhost:11434"), "llama3"));

var app = builder.Build();
var chatClient = app.Services.GetRequiredService<IChatClient>();

var chatHistory = new List<ChatMessage>();

while (true)
{
    try
    {
		Console.WriteLine("Ask anything:");
		var userPrompt = Console.ReadLine();
		chatHistory.Add(new ChatMessage(ChatRole.User, userPrompt));

		Console.WriteLine("Response:");
		var chatResponse = new StringBuilder();

		await foreach (var item in chatClient.GetStreamingResponseAsync(chatHistory))
		{
			Console.Write(item.Text);
			chatResponse.Append(item.Text);
		}

		chatHistory.Add(new ChatMessage(ChatRole.Assistant, chatResponse.ToString()));
		Console.WriteLine();
	}
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"{ex.Message}, please run the ollama docker container if not already running.");

	}
	catch (Exception ex)
	{
		Console.WriteLine(ex.Message);
	}
}