using DietMAnagemenApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DietMAnagemenApp.Controllers
{
    public class DietplanController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public DietplanController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("DietApi");
            var dietPlans = await client.GetFromJsonAsync<List<DietPlan>>("DietPlan");
            return View(dietPlans);
        }
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("DietApi");
                var dietPlan = await client.GetFromJsonAsync<DietPlan>($"DietPlan/{id}");
                var dietClient = await client.GetFromJsonAsync<Client>($"Client/{dietPlan.ClientId}");
                ViewBag.Client = dietClient;
                return View(dietPlan);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}

