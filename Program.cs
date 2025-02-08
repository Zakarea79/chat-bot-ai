using API;
using Newtonsoft.Json;

TelegramBotApi telegramBotApi = new TelegramBotApi();
telegramBotApi.Token = "7487389693:AAH5hfhVBNh0EWC4Hkb6ipcfagzHKbjWbPQ";
AiApi ai_api = new AiApi();

Console.WriteLine("Start Bot ... ");

long offset = 0;
while (true)
{
    dynamic t_data = telegramBotApi.getUpdate(offset);
    t_data = JsonConvert.DeserializeObject<dynamic>(t_data);
    foreach (var item in t_data["result"])
    {
        try
        {
            if ((string)item["message"]["text"] != "")
            {
                Console.WriteLine(item["message"]["chat"]["id"]);
                // _ = Task.Run(() =>
                // {
                Console.WriteLine(item["message"]["text"]);
                var from_ai = ai_api.ai_response(item["message"]["text"]);
                telegramBotApi.sendMessage(from_ai, item["message"]["chat"]["id"]);
                // });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        offset = item["update_id"] + 1;
    }
}