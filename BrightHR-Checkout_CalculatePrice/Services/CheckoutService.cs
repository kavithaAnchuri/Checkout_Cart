using BrightHR_Checkout_CalculatePrice.Models;

namespace BrightHR_Checkout_CalculatePrice.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly Checkout _checkout;

        public CheckoutService()
        {
            var pricingRules = new Dictionary<string, PriceRule>
            {
                { "A", new PriceRule(50, 3, 130) },  // Special price: 3 for 130
                { "B", new PriceRule(30, 2, 45) },   // Special price: 2 for 45
                { "C", new PriceRule(20) },          // No special price for C
                { "D", new PriceRule(15) }           // No special price for D
            };

            _checkout = new Checkout(pricingRules);
        }

        public void Scan(string item)
        {
            _checkout.Scan(item);
        }

        public int GetTotalPrice()
        {
            return _checkout.GetTotalPrice();
        }

        public void SetScannedItems(Dictionary<string, int> items)
        {
            _checkout.SetScannedItems(items);
        }

        public Dictionary<string, int> GetScannedItems()
        {
            return _checkout.GetScannedItems(); 
        }
    }
}
