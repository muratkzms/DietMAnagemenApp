using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using System.Net.Http;

namespace DietMAnagemenApp.Models
{
    public class MealController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IToastNotification _toastNotification;             
        private static string ActionResultErrorMessage;
        private static string ActionResultSuccessMessage;
        public MealController(IHttpClientFactory clientFactory, IToastNotification toastNotification)
        {
            _clientFactory = clientFactory;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("DietApi");
            var meals = await client.GetFromJsonAsync<List<Meal>>("Meal");

            if (!string.IsNullOrEmpty(ActionResultSuccessMessage))
            {
                _toastNotification.AddSuccessToastMessage(ActionResultSuccessMessage, new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
            }
            if (!string.IsNullOrEmpty(ActionResultErrorMessage))
            {
                _toastNotification.AddErrorToastMessage(ActionResultErrorMessage, new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
            }
            ActionResultErrorMessage = null;
            ActionResultSuccessMessage = null;
            return View(meals);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var client = _clientFactory.CreateClient("DietApi");

            var meal = await client.GetFromJsonAsync<Meal>($"Meal/{id}");
            var dietPlan = await client.GetFromJsonAsync<DietPlan>($"DietPlan/{meal.DietPlanId}");
            if (dietPlan==null)
            {
                return NotFound();
            }
            //var response = await client.GetAsync($"DietPlan/GetDietplansByClientId?clientId={meal.DietPlanId}");
            //if (!response.IsSuccessStatusCode)
            //{
            //    return NotFound();
            //}
            //var dietPlans = await response.Content.ReadFromJsonAsync<List<DietPlan>>();
            var mealclient = await client.GetFromJsonAsync<Client>($"Client/{dietPlan.ClientId}");
            ViewBag.Client = mealclient;
            ViewBag.DietPlan = dietPlan;
            return View(meal);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("DietApi");
            try
            {
                var response = await client.DeleteAsync($"meal/{id}");

                if (response.IsSuccessStatusCode)
                {
                    ActionResultSuccessMessage = $"{id} Id numaralı Öğün Silindi";
                    return RedirectToAction("Index");
                }
                else
                {
                    ActionResultErrorMessage = $"{id} Id numaralı Öğün Silinemedi";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ActionResultErrorMessage = $"{id} Id numaralı Öğün Silinemedi";
                return RedirectToAction("Index");
            }
        }
        // GET: Meal/Add
        public async Task<IActionResult> Add()
        {
            // Retrieve diet plans from API to populate dropdown
            var client = _clientFactory.CreateClient("DietApi");
            var dietPlans = await client.GetFromJsonAsync<List<DietPlan>>("DietPlan");
            if (dietPlans!=null)
            {
                ViewBag.DietPlanId = new SelectList(dietPlans, "Id", "Title");
            }
            else
            {
                ViewBag.DietPlanId = new SelectList(new List<DietPlan>(), "Id", "Title");
                ModelState.AddModelError(string.Empty, "Error retrieving diet plans from API.");
            }
            return View();
        }

        // POST: Meal/Add
        [HttpPost]
        public async Task<IActionResult> Add(Meal meal)
        {
            var client = _clientFactory.CreateClient("DietApi");
            try
            {
                var response = await client.PostAsJsonAsync("Meal", meal);
                response.EnsureSuccessStatusCode(); 
                return RedirectToAction("Index", "Home"); // Redirect to home or another page upon successful addition
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return View(meal); // Return view with model errors
            }
        }
    }
}
