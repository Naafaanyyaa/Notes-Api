using System.Text.Json;

namespace WebProject.Helper
{
    public class HttpClientExtensions
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:7255");
            return Client;
        }
    }
}
