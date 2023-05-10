public class LoginClient
{
    public HttpClient Client { get; }
    
    public LoginClient(HttpClient httpClient)
    {
        Client = httpClient;
    }
}