using LGTMClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using LGTMClient.ApiModels;
using RestSharp;

namespace LGTMClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RestClient _restClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _restClient = new RestClient("https://localhost:7000/api/");
        }

        public IActionResult Index()
        {
            var request = new RestRequest("ProductsApi");

            var response = _restClient.Get(request);
            var productData = JsonSerializer.Deserialize<ProductsApiModel[]>(response.Content);

            return View(productData.ToList());
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