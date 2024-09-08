namespace BrightHR_Checkout_CalculatePrice.Models
{
    public class PriceRule
    {
        public int UnitPrice { get; }
        public int SpecialQuantity { get; }
        public int SpecialPrice { get; }

        public PriceRule(int unitPrice, int specialQuantity = 0, int specialPrice = 0)
        {
            UnitPrice = unitPrice;
            SpecialQuantity = specialQuantity;
            SpecialPrice = specialPrice;
        }

        public int CalculateTotalPrice(int quantity)
        {
            if (SpecialQuantity > 0 && quantity >= SpecialQuantity)
            {
                int specialBundleCount = quantity / SpecialQuantity;
                int remainder = quantity % SpecialQuantity;
                return (specialBundleCount * SpecialPrice) + (remainder * UnitPrice);
            }
            return quantity * UnitPrice;
        }
    }
}
