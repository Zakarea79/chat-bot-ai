using System.Net;

namespace API;

public class BotApi
{
    private const string URL = "https://api.telegram.org/bot7568152532:AAG87-fHww5J78cYP4nmSxC3usoPfXqGE7Q/";
    public string getUpdate(long offset)
    {
        string? json = null;
        try
        {
            using (HttpClient Client = new HttpClient())
            {
                var Response = Client.GetStringAsync($"{URL}getUpdates?offset={offset}").Result;
                return Response;
            }
        }
        catch
        {
            json = getUpdate(offset);
        }
        return json;
    }
    public void sendMessage(dynamic chat_id, string message)
    {
        try
        {
            using (HttpClient Client = new HttpClient())
            {
                _ = Client.GetStringAsync($"{URL}sendMessage?chat_id={chat_id}&text={message}").Result;
            }
        }
        catch (Exception)
        {
            sendMessage(chat_id, message);
        }
    }
}