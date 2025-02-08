using Newtonsoft.Json;

namespace JueAiBot
{
    public class Api
    {
        public string get_prompt_from_user(string user_prompt)
        {
            var data = new { model = "", prompt = user_prompt };
            var json = JsonConvert.SerializeObject(data);
            return json;
        }
        public static async Task<string> sendData(string Url, Dictionary<string, string> data)
    {
        return await Task<string>.Run(() =>
        {
            try
            {
                using (HttpClient Client = new HttpClient())
                {
                    FormUrlEncodedContent Content = new FormUrlEncodedContent(data);
                    HttpResponseMessage Response = Client.PostAsync(Url, Content).Result;
                    return Response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (System.Exception)
            {
                return "false";
            }
        });
    }
        public string get_answers_from_model(string requaset)
        {

        }
    }
}

namespace TelegramBotApi
{
}