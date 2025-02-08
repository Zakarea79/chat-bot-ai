using Newtonsoft.Json;
using System.Text;

namespace AI;

public class AiApi
{
	public static async Task<string> sendData(string Url, string data)
	{
		return await Task<string>.Run(() =>
	    {
	    	 try
	    	 {
	    	 	 using (HttpClient Client = new HttpClient())
	             {
	                    //FormUrlEncodedContent Content = new FormUrlEncodedContent(data);
	                    var Content = new StringContent (data , Encoding.UTF8 , "application/json");
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
	    public string using_ai(string promt_user)
	    {
	    	var data = new {
	    		model = "phi3:mini" ,
	    		prompt = promt_user,
	    		stream = false
	    	};
	    	string output = JsonConvert.SerializeObject(data);
	    	string res = sendData("http://localhost:11434/api/generate" , output).Result;
	    	var data_from_ai = JsonConvert.DeserializeObject<dynamic>(res);
            if(data_from_ai != null)
                res = data_from_ai["response"];
            else
                res = "false";
	    	return res;
	    }
}