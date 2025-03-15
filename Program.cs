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
                _ = Task.Run(async () =>
                {
                    dynamic? msg_id = null;
                    string? Message = null;
                    await foreach (var chunk in ai_api.ai_response_async((string)item["message"]["text"]))
                    {
                        Message += chunk.ToString();
                        if (msg_id != null)
                        {
                            telegramBotApi.UpdateMessage(item["message"]["chat"]["id"], msg_id, Message);
                            continue;
                        }
                        var res = telegramBotApi.sendMessage(item["message"]["chat"]["id"], Message);
                        msg_id = JsonConvert.DeserializeObject<dynamic>(res)["result"]["message_id"];
                        Console.WriteLine($"{msg_id}");
                    }
                    telegramBotApi.UpdateMessage(item["message"]["chat"]["id"], msg_id, Message + "\n-------end-------");

                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        offset = item["update_id"] + 1;
    }
}