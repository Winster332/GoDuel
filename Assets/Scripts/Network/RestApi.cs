using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Network
{
  public class RestApi
  {
    private static RestApi _instance;
    private string _url;

    public RestApi()
    {
      _url = "http://localhost:5001";
    }

    public static RestApi Get() => _instance ?? (_instance = new RestApi());
    
    #region endpints
    public async Task<HttpResponseMessage> GetUser(Guid id)
    {
      return await GetData($"{_url}/api/authorize/user?id={id}");
    }
    
    public async Task<HttpResponseMessage> Logout(Guid id)
    {
      return await GetData($"{_url}/api/authorize/logout?id={id}");
    }
    
    public async Task<HttpResponseMessage> Login(string login, string password)
    {
      return await GetData($"{_url}/api/authorize/login?username={login}&pass={password}");
    }
    
    public async Task<HttpResponseMessage> Registration(string username, string password)
    {
      return await GetData($"{_url}/api/authorize/reg?username={username}&pass={password}");
    }
    #endregion
    
    #region get/post/put/delete
    public async Task<HttpResponseMessage> GetData(string url, string contentType = "application/json")
    {
      var request = UnityWebRequest.Get(url);
      request.Send().completed += operation =>
      {
        
      };
      return null;
    }

    public async Task<HttpResponseMessage> PatchData(string url, string stringContent)
    {
      var content = new StringContent(stringContent);

      content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

      var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
      {
        Content = content
      };

      HttpResponseMessage response = new HttpResponseMessage();

//      response = await _client.SendAsync(request);

      return response;
    }

    public async Task<HttpResponseMessage> PostData(string url, string data)
    {
      var httpContent = new StringContent(data);
      httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
//      var response = await _client.PostAsync(url, httpContent);

//      return response;
      return null;
    }
    #endregion
  }
}