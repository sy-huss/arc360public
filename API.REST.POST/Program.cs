using System.Text;
using System.Text.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        var uri = "https://httpbin.org/post";
        var dataObject = new { Name = "John Doe", Age = 30 };

        string json = JsonSerializer.Serialize(dataObject);
        await PostJsonAsync(uri, json);
    }

    public static async Task PostJsonAsync(string uri, string json)
    {
        using (var client = new HttpClient())
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            requestMessage.Headers.Add("X-Custom-Header", "YourValueHere");

            var response = await client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine($"Failed to POST data: {response.StatusCode}");
            }
        }
    }
}