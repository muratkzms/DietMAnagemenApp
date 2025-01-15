using System.Diagnostics;
using DietMAnagemenApp.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace DietMAnagemenApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IToastNotification _toastNotification;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory, IToastNotification toastNotification)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("DietApi");
            var clients = await client.GetFromJsonAsync<List<Client>>("Client");
            var clientsCount = await client.GetFromJsonAsync<int>("Client/GetClientsCount");
            var dietplansCount = await client.GetFromJsonAsync<int>("DietPlan/GetDietplansCount");
            var mealsCount = await client.GetFromJsonAsync<int>("Meal/GetMealsCount");
            return View(new DashboardViewModel
            {
                Clients = clients.ToList(),
                ClientsCount = clientsCount,
                DietplansCount=dietplansCount,
                MealsCount=mealsCount
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
