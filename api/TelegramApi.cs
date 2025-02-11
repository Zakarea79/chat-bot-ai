namespace API;

public class TelegramBotApi
{
    private const string URL = "https://api.telegram.org/bot";
    private string? _token;
    public string Token
    {
        get
        {
            return _token + "/";
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
                var Response = Client.GetStringAsync($"{URL + Token}getUpdates?offset={offset}").Result;
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
                _ = Client.GetStringAsync($"{URL}{Token}sendMessage?chat_id={chat_id}&text={message}").Result;
            }
        }
        catch (Exception)
        {
            sendMessage(chat_id, message);
        }
    }
}