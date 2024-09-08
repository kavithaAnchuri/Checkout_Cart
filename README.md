# Checkout Application
This is an application implemented in ASP.NET MVC, a checkout system using a simple pricing model. It supports scanning items, applying special pricing rules, and calculating the total price dynamically. 

## Features

- **Scan Items**: Use the dropdown to select and scan items.
- **Special Pricing**: Applies offers when items are scanned.
- **Dynamic Pricing**: Updates total price and scanned items instantly.
- **Session Management**: Tracks items using session storage.
- **AJAX**: Updates cart and price without refreshing the page.

## Installation

1. **Clone the Repository** : git clone [<repository-url>](https://github.com/kavithaAnchuri/Checkout_Cart.git)

## Usage
Select an Item:  Choose and scan items from the dropdown.
View Cart: Scanned items and total price are shown in a table.
Update Total: Total price updates automatically with each scan.

## Code Structure 
Models handle scanning and pricing (Checkout.cs), controllers manage requests (CheckoutController.cs), and views display the interface (Checkout.cshtml).

**Testing**
Unit tests are implemented using xUnit.

![Screenshot (1)](https://github.com/user-attachments/assets/b21d8083-19c4-4438-910c-78ab6d88a466)

