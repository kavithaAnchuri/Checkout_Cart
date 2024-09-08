namespace BrightHR_Checkout_CalculatePrice.Services
{
    public interface ICheckoutService
    {
        void Scan(string item);

        int GetTotalPrice();

        void SetScannedItems(Dictionary<string, int> items);

        Dictionary<string, int> GetScannedItems();
    }
}
