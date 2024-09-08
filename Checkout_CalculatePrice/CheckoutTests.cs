using BrightHR_Checkout_CalculatePrice.Models;
using Xunit;

namespace Checkout_CalculatePrice
{
    public class CheckoutTests
    {
        private Checkout _checkout;

        public CheckoutTests()
        {
            var pricingRules = new Dictionary<string, PriceRule>
            {
                { "A", new PriceRule(50) }, 
            };
            _checkout = new Checkout(pricingRules);
        }

        [Fact]
        public void Scan_NoItems_ReturnsTotalZero()
        {
            // Act
            var totalPrice = _checkout.GetTotalPrice();

            // Assert
            Assert.Equal(0, totalPrice); // No items scanned
        }

        [Fact]
        public void Scan_SingleItem_ReturnsThePrice()
        {
            // Arrange
            _checkout.Scan("A");

            // Act
            var totalPrice = _checkout.GetTotalPrice();

            // Assert
            Assert.Equal(50, totalPrice);
        }

        [Fact]
        public void Scan_TwoItemsWithoutSpecialPrice_ReturnsCorrectTotal()
        {
            // Arrange
            _checkout.Scan("A");
            _checkout.Scan("A");

            // Act
            var totalPrice = _checkout.GetTotalPrice();

            // Assert
            Assert.Equal(100, totalPrice); 
        }

        [Fact]
        public void Scan_ThreeItemsWithSpecialPrice_ReturnsSpecialPrice()
        {
            // Arrange
            var pricingRules = new Dictionary<string, PriceRule>
            {
                { "A", new PriceRule(50, 3, 130) }, // Special price: 3 for 130
            };
            _checkout = new Checkout(pricingRules);

            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");

            // Act
            var totalPrice = _checkout.GetTotalPrice();

            // Assert
            Assert.Equal(130, totalPrice); 
        }

        [Fact]
        public void Scan_FourItemsWithSpecialPrice_ReturnsCorrectTotal()
        {
            // Arrange
            var pricingRules = new Dictionary<string, PriceRule>
            {
                { "A", new PriceRule(50, 3, 130) }, // Special price: 3 for 130
            };
            _checkout = new Checkout(pricingRules);

            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");

            // Act
            var totalPrice = _checkout.GetTotalPrice();

            // Assert
            Assert.Equal(180, totalPrice); // 3 for 130 + 1 for 50
        }

        [Fact]
        public void Scan_SingleItemMultipleTimesWithSpecialPrice_ReturnsCorrectTotal()
        {
            // Arrange
            var pricingRules = new Dictionary<string, PriceRule>
            {
                { "A", new PriceRule(50, 3, 130) }, // Special price: 3 for 130
            };
            _checkout = new Checkout(pricingRules);

            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");

            // Act
            var totalPrice = _checkout.GetTotalPrice();

            // Assert
            Assert.Equal(360, totalPrice); // 6 for 260 + 2 for 100
        }

        [Fact]
        public void Scan_DifferentItems_ReturnsTotal()
        {
            // Arrange
            var pricingRules = new Dictionary<string, PriceRule>
            {
                { "A", new PriceRule(50) }, 
                { "B", new PriceRule(30) }, 
                { "C", new PriceRule(20) },       
            };
            _checkout = new Checkout(pricingRules);

            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("C"); 

            // Act
            var totalPrice = _checkout.GetTotalPrice();

            // Assert
            Assert.Equal(100, totalPrice); // 50 (A) + 30 (B) + 20 (C)
        }

        [Fact]
        public void Scan_DifferentItemsWithAndWithoutSpecialPrice_ReturnsCorrectTotal()
        {
            // Arrange
            var pricingRules = new Dictionary<string, PriceRule>
            {
                { "A", new PriceRule(50, 3, 130) }, // Special price: 3 for 130
                { "B", new PriceRule(30, 2, 45) },  // Special price: 2 for 45
                { "C", new PriceRule(20) },         // No special price for C
            };
            _checkout = new Checkout(pricingRules);

            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");  // Special price for A (3 for 130)
            _checkout.Scan("B");
            _checkout.Scan("B");  // Special price for B (2 for 45)
            _checkout.Scan("C");  // No special price for C

            // Act
            var totalPrice = _checkout.GetTotalPrice();

            // Assert
            Assert.Equal(195, totalPrice); // 130 (A) + 45 (B) + 20 (C)
        }

    }
}
