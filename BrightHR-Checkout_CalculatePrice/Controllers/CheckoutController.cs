using BrightHR_Checkout_CalculatePrice.Helpers;
using BrightHR_Checkout_CalculatePrice.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BrightHR_Checkout_CalculatePrice.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;
        private const string SessionKeyItems = "ScannedItems";

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        public IActionResult Index()
        {
            // Retrieve scanned items from session
            var scannedItems = GetScannedItemsFromSession();


            ViewBag.TotalPrice = _checkoutService.GetTotalPrice();
            ViewBag.Items = scannedItems;

            return View();
        }

        // POST: Checkout/ScanItem
        [HttpPost]
        public IActionResult ScanItem(string item)
        {
            var scannedItems = GetScannedItemsFromSession();

            if (!string.IsNullOrEmpty(item))
            {
                ProcessScannedItem(scannedItems, item);
            }

            // Return the updated result
            return Json(CreateResult());
        }

        // Helper method to retrieve scanned items from session
        private Dictionary<string, int> GetScannedItemsFromSession()
        {
            return HttpContext.Session.Get<Dictionary<string, int>>(SessionKeyItems) ?? new Dictionary<string, int>();
        }

        // Process the new scanned item and update session
        private void ProcessScannedItem(Dictionary<string, int> scannedItems, string item)
        {
            // Update scanned items in the checkout service
            _checkoutService.SetScannedItems(scannedItems);

            // Scan the new item and update scanned items
            _checkoutService.Scan(item);

            HttpContext.Session.Set(SessionKeyItems, _checkoutService.GetScannedItems());
        }

        // Helper method to create the JSON result object
        private object CreateResult()
        {
            return new
            {
                TotalPrice = _checkoutService.GetTotalPrice(),
                Items = _checkoutService.GetScannedItems()
            };
        }

    }
}
