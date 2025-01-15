using DietMAnagemenApp.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Net.Http;

namespace DietMAnagemenApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IToastNotification _toastNotification;
        private static string ActionResultErrorMessage;
        private static string ActionResultSuccessMessage;

        public ClientController(IHttpClientFactory clientFactory, IToastNotification toastNotification)
        {
            _clientFactory = clientFactory;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("DietApi");
            var clients = await client.GetFromJsonAsync<List<Client>>("Client");

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
            return View(clients);
        }
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var client = _clientFactory.CreateClient("DietApi");
                var dietClient = await client.GetFromJsonAsync<Client>($"Client/{id}");
                return View(dietClient);

            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("DietApi");
            try
            {
                var response = await client.DeleteAsync($"Client/{id}");

                if (response.IsSuccessStatusCode)
                {
                    ActionResultSuccessMessage = $"{id} Id numaralı Danışan Silindi";
                    return RedirectToAction("Index");
                }
                else
                {
                    ActionResultErrorMessage = $"{id} Id numaralı Danışan Silinemedi";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ActionResultErrorMessage = $"{id} Id numaralı Danışan Silinemedi";
                return RedirectToAction("Index");
            }
        }

        // Update Action (GET)
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var client = _clientFactory.CreateClient("DietApi");
            var dietClient = await client.GetFromJsonAsync<Client>($"Client/{id}");
            if (client == null)
            {
                return NotFound();
            }
            return View(dietClient);
        }

        // Update Action (POST)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Client client)
        {
            var httpClient = _clientFactory.CreateClient("DietApi");
            if (!ModelState.IsValid)
            {
                return View(client);
            }

            var response = await httpClient.PutAsJsonAsync($"Client/{client.Id}", client);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Client updated successfully.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "An error occurred while updating the client.";
            return View(client);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new Client());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Client client)
        {
            if (!ModelState.IsValid)
            {
                return View(client);
            }

            var httpClient = _clientFactory.CreateClient("DietApi");
            var response = await httpClient.PostAsJsonAsync("Client", client);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "An error occurred while adding the client.");
            return View(client);
        }
    }
}
