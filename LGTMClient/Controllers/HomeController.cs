using LGTMClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using LGTMClient.ApiModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: Products/Create
        public IActionResult Create()
        {
            var productCategories = new[]
            {
                new { ProductCategoryId = 1, Name = "Displays"},
                new { ProductCategoryId = 2, Name = "HDD"},
                new { ProductCategoryId = 3, Name = "CPU"},
                new { ProductCategoryId = 4, Name = "GPU"},
                new { ProductCategoryId = 5, Name = "RAM"},
                new { ProductCategoryId = 6, Name = "Keyboard"},
                new { ProductCategoryId = 7, Name = "Mouse"},
                new { ProductCategoryId = 8, Name = "Apparel"}
            };

            ViewData["ProductCategoryId"] = new SelectList(productCategories, "ProductCategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("productId,name,description,quantity,price, averageRatingValue, productCategoryId")] ProductsApiModel product)
        {
            var productCategories = new[]
            {
                new { ProductCategoryId = 1, Name = "Displays"},
                new { ProductCategoryId = 2, Name = "HDD"},
                new { ProductCategoryId = 3, Name = "CPU"},
                new { ProductCategoryId = 4, Name = "GPU"},
                new { ProductCategoryId = 5, Name = "RAM"},
                new { ProductCategoryId = 6, Name = "Keyboard"},
                new { ProductCategoryId = 7, Name = "Mouse"},
                new { ProductCategoryId = 8, Name = "Apparel"}
            };

            var validationResult = product.Validate();
            if(string.IsNullOrWhiteSpace(validationResult))
            {
                var request = new RestRequest("ProductsApi", Method.Post);
                request.AddParameter("application/json", JsonSerializer.Serialize(product),
                    ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                try
                {
                    await _restClient.ExecuteAsync(request);
                }
                catch (Exception error)
                {
                    ViewData["ProductCategoryId"] = new SelectList(productCategories, "ProductCategoryId", "Name", product.productCategoryId);
                    return View(product);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductCategoryId"] = new SelectList(productCategories, "ProductCategoryId", "Name", product.productCategoryId);
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new RestRequest("ProductsApi/" + id.Value);
            //request.AddQueryParameter("id", id.Value);

            await _restClient.ExecuteAsync(request);

            var response = _restClient.Get(request);
            var productData = JsonSerializer.Deserialize<ProductsApiModel>(response.Content);

            if (productData == null)
            {
                return NotFound();
            }

            var productCategories = new[]
            {
                new { ProductCategoryId = 1, Name = "Displays"},
                new { ProductCategoryId = 2, Name = "HDD"},
                new { ProductCategoryId = 3, Name = "CPU"},
                new { ProductCategoryId = 4, Name = "GPU"},
                new { ProductCategoryId = 5, Name = "RAM"},
                new { ProductCategoryId = 6, Name = "Keyboard"},
                new { ProductCategoryId = 7, Name = "Mouse"},
                new { ProductCategoryId = 8, Name = "Apparel"}
            };

            ViewData["ProductCategoryId"] = new SelectList(productCategories, "ProductCategoryId", "Name", productData.productCategoryId);
            return View(productData);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("productId,name,description,quantity,price, averageRatingValue, productCategoryId")] ProductsApiModel product)
        {
            var validationResult = product.Validate();
            if (string.IsNullOrWhiteSpace(validationResult))
            {
                var request = new RestRequest("ProductsApi/" + id, Method.Put);
                request.AddParameter("application/json", JsonSerializer.Serialize(product),
                    ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;

                try
                {
                    await _restClient.ExecuteAsync(request);
                }
                catch (Exception error)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            var productCategories = new[]
            {
                new { ProductCategoryId = 1, Name = "Displays"},
                new { ProductCategoryId = 2, Name = "HDD"},
                new { ProductCategoryId = 3, Name = "CPU"},
                new { ProductCategoryId = 4, Name = "GPU"},
                new { ProductCategoryId = 5, Name = "RAM"},
                new { ProductCategoryId = 6, Name = "Keyboard"},
                new { ProductCategoryId = 7, Name = "Mouse"},
                new { ProductCategoryId = 8, Name = "Apparel"}
            };

            ViewData["ProductCategoryId"] = new SelectList(productCategories, "ProductCategoryId", "Name", product.productCategoryId);
            return View(product);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new RestRequest("ProductsApi/" + id.Value);

            await _restClient.ExecuteAsync(request);

            var response = _restClient.Get(request);
            var productData = JsonSerializer.Deserialize<ProductsApiModel>(response.Content);

            if (productData == null)
            {
                return NotFound();
            }

            return View(productData);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = new RestRequest("ProductsApi/" + id.Value);

            await _restClient.ExecuteAsync(request);

            var response = _restClient.Get(request);
            var productData = JsonSerializer.Deserialize<ProductsApiModel>(response.Content);

            return View(productData);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = new RestRequest("ProductsApi/" + id, Method.Delete);

            await _restClient.ExecuteAsync(request);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductsStock()
        {
            var request = new RestRequest("ProductsApi/GetProductsStock");

            var response = _restClient.Get(request);
            var productData = JsonSerializer.Deserialize<GetProductsStockResponse>(response.Content);

            return View(productData.stocks);
        }

        public IActionResult ProductNameAndQuantity(int id)
        {
            var request = new RestRequest("ProductsApi/GetProductNameAndQuantity/" + id);

            var response = _restClient.Get(request);
            var productData = JsonSerializer.Deserialize<GetProductNameAndQuantityModel>(response.Content);

            return View(productData);
        }

        public IActionResult ProductNameAndQuantityForm()
        {
            return View();
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