using API;
using Newtonsoft.Json;

TelegramBotApi telegramBotApi = new TelegramBotApi();
telegramBotApi.Token = "7487389693:AAH5hfhVBNh0EWC4Hkb6ipcfagzHKbjWbPQ";
AiApi ai_api = new AiApi();

Console.WriteLine("Start Bot ... ");

dynamic offset = 0;
while (true)
{
    dynamic t_data = telegramBotApi.getUpdate(offset);
    t_data = JsonConvert.DeserializeObject<dynamic>(t_data);
    foreach (var item in t_data["result"])
    {
        if (item["message"]["text"] != "" && item["message"]["text"] != null)
        {
            _ = Task.Run(() =>
            {
                var from_ai = ai_api.using_ai(item["message"]["text"]);
                telegramBotApi.sendMessage(from_ai, item["message"]["chat"]["id"]);
                Console.WriteLine(item["message"]["chat"]["id"]);
            });
            offset += item["update_id"] + 1;
        }
        else
        {
            offset += item["update_id"] + 1;
        }
    }
}