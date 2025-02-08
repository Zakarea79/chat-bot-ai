using API;

TelegramBotApi telegramBotApi = new TelegramBotApi();
telegramBotApi.Token = "";

AiApi ai_api = new AiApi();

dynamic offset = 0;
while (true)
{
    dynamic t_data = telegramBotApi.getUpdate(offset);
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
    }
}