namespace API;

public class TelegramBotApi
{
    private const string URL = "https://api.telegram.org/bot";
    private string? _token;
    public string Token
    {
        get
        {
            return $"{URL}{_token}/";
        }
        set
        {
            _token = value;
        }
    }
    public string getUpdate(long offset)
    {
        string? json = null;
        try
        {
            using (HttpClient Client = new HttpClient())
            {
                var Response = Client.GetStringAsync($"{Token}getUpdates?offset={offset}").Result;
                return Response;
            }
        }
        catch
        {
            json = getUpdate(offset);
        }
        return json;
    }
    public string UpdateMessage(dynamic chat_id, dynamic msg_id, string message)
    {
        try
        {
            using (HttpClient Client = new HttpClient())
            {
                return Client.GetStringAsync($"{Token}editMessageText?chat_id={chat_id}&message_id={msg_id}&text={message}").Result;
            }
        }
        catch (Exception)
        {
            return "---false---";
        }
    }
    public string sendMessage(dynamic chat_id, string message)
    {
        try
        {
            using (HttpClient Client = new HttpClient())
            {
                return Client.GetStringAsync($"{Token}sendMessage?chat_id={chat_id}&text={message}").Result;
            }
        }
        catch (Exception)
        {
            return "---false---";
        }
    }
}