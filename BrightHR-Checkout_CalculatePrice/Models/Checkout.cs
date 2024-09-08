namespace BrightHR_Checkout_CalculatePrice.Models
{
    public class Checkout
    {
        private readonly Dictionary<string, PriceRule> _pricingRules;
        private readonly Dictionary<string, int> _scannedItems;

        public Checkout(Dictionary<string, PriceRule> pricingRules)
        {
            _pricingRules = pricingRules;
            _scannedItems = new Dictionary<string, int>();
        }

        public void Scan(string item)
        {
            if (_scannedItems.ContainsKey(item))
            {
                _scannedItems[item]++;
            }
            else
            {
                _scannedItems[item] = 1;
            }
        }

        public int GetTotalPrice()
        {
            int total = 0;
            foreach (var item in _scannedItems)
            {
                var sku = item.Key;
                var count = item.Value;

                if (_pricingRules.ContainsKey(sku))
                {
                    var priceRule = _pricingRules[sku];
                    total += priceRule.CalculateTotalPrice(count);
                }
            }
            return total;
        }

        // set all scanned items
        public void SetScannedItems(Dictionary<string, int> items)
        {
            _scannedItems.Clear(); // Clear current items
            foreach (var item in items)
            {
                _scannedItems[item.Key] = item.Value;
            }
        }

        public Dictionary<string, int> GetScannedItems()
        {
            return new Dictionary<string, int>(_scannedItems);
        }
    }
}
