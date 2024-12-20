using OpenAI.Chat;

ChatClient client = new(
    model: "gpt-4o",
    apiKey: "123456");
ChatCompletion completion = client.CompleteChat("Say 'this is a test.'");

Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");