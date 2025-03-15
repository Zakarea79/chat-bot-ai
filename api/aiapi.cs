using Newtonsoft.Json;
using System.Text;

namespace API;

public class AiApi
{
    private static async Task<string> sendData(string Url, string data)
    {
        return await Task<string>.Run(() =>
        {
            try
            {
                using (HttpClient Client = new HttpClient())
                {
                    var Content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage Response = Client.PostAsync(Url, Content).Result;
                    return Response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception)
            {
                return "false";
            }
        });
    }
    private static async IAsyncEnumerable<string> sendDataAsync(string apiUrl, string jsonData)
    {
        using (HttpClient client = new HttpClient())
        {
            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };
            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = await reader.ReadLineAsync();
                        if (!string.IsNullOrEmpty(line))
                        {
                            yield return line;
                        }
                    }
                }
            }
        }
    }
    public async IAsyncEnumerable<string> ai_response_async(string promt_user)
    {
        var data = new
        {
            model = "phi3:mini",
            prompt = promt_user,
            stream = true
        };
        string output = JsonConvert.SerializeObject(data);
        await foreach (var chunk in sendDataAsync("http://localhost:11434/api/generate", output))
        {
            var data_from_ai = JsonConvert.DeserializeObject<dynamic>(chunk);

            if (data_from_ai != null)
            {
                if (data_from_ai["done"] == true)
                {
                    continue;
                }
                yield return data_from_ai["response"];
            }
            else
                yield return "--false--";
        }
    }
    public string ai_response(string promt_user)
    {
        var data = new
        {
            model = "phi3:mini",
            prompt = promt_user,
            stream = false
        };
        string output = JsonConvert.SerializeObject(data);
        string res = sendData("http://localhost:11434/api/generate", output).Result;
        var data_from_ai = JsonConvert.DeserializeObject<dynamic>(res);
        if (data_from_ai != null)
            res = data_from_ai["response"];
        else
            res = "--false--";
        return res;
    }
}