using FirstLab.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebProject.Helper;

namespace WebProject.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        HttpClientExtensions _api = new HttpClientExtensions();

        public async Task<IActionResult> Index()
        {
            List<User>? users = new List<User>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/User/");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(result);
            }


            return View(users);
        }
    }
}
