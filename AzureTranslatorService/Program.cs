using System.Text;
using Newtonsoft.Json;

namespace AzureTranslatorService;

internal static class Program
{
    private const string Key = "<your-translator-key>";
    private const string Endpoint = "https://api.cognitive.microsofttranslator.com";

    // location, also known as region.
    // required if you're using a multi-service or regional (not global) resource. It can be found in the Azure portal on the Keys and Endpoint page.
     private const string location = "centralindia";

    private static async Task Main()
    {
        // Input and output languages are defined as parameters.
        const string route = "/translate?api-version=3.0&from=en&to=hi";
        const string textToTranslate = "I would really like to drive your car around the block a few times!";
        var body = new object[] {new {Text = textToTranslate}};
        var requestBody = JsonConvert.SerializeObject(body);

        using var client = new HttpClient();
        using var request = new HttpRequestMessage();
        // Build the request.
        request.Method = HttpMethod.Post;
        request.RequestUri = new Uri(Endpoint + route);
        request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
        request.Headers.Add("Ocp-Apim-Subscription-Key", Key);
        // location required if you're using a multi-service or regional (not global) resource.
        request.Headers.Add("Ocp-Apim-Subscription-Region", location);

        // Send the request and get response.
        var response = await client.SendAsync(request).ConfigureAwait(false);
        // Read response as a string.
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine(result);
    }
}