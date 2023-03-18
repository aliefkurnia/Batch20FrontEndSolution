namespace Realta.Frontend.Pages.Resto
{
    public class CartItem
    {
        public Product? Product { get; set; }

        public int Quantity { get; set; }

        public decimal Subtotal
        {
            get { return Product.Price * Quantity; }
        }
    }

    public class Cart
    {
        // Singleton instance
        public static readonly Cart Instance = new Cart();

        // Private constructor to prevent direct instantiation
        private Cart() { }

        // List of cart items
        private List<CartItem> items = new List<CartItem>();

        // Add item to cart
        public void AddItem(Product product, int quantity = 1)
        {
            // Check if item already exists in cart
            var existingItem = items.FirstOrDefault(item => item.Product.VeproId == product.VeproId);

            if (existingItem != null)
            {
                // Item already exists, update the quantity
                existingItem.Quantity += quantity;
            }
            else
            {
                // Item doesn't exist yet, add it to the cart
                items.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        // Remove item from cart
        public void RemoveItem(int veproId)
        {
            var itemToRemove = items.FirstOrDefault(item => item.Product.VeproId == veproId);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }

        // Update item quantity in cart
        public void UpdateQuantity(int veproId, int quantity)
        {
            var itemToUpdate = items.FirstOrDefault(item => item.Product.VeproId == veproId);

            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity;
            }
        }

        // Clear all items from cart
        public void Clear()
        {
            items.Clear();
        }

        // Get all items in cart
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        // Get total number of items in cart
        public int TotalItems
        {
            get { return items.Sum(item => item.Quantity); }
        }

        // Get total price of items in cart (excluding tax)
        public decimal TotalPriceExcludingTax
        {
            get { return items.Sum(item => item.Product.Price * item.Quantity); }
        }

        // Get tax amount (10% of total price)
        public decimal TaxAmount
        {
            get { return TotalPriceExcludingTax * 0.1m; }
        }

        // Get total price of items in cart (including tax)
        public decimal TotalPrice
        {
            get { return TotalPriceExcludingTax + TaxAmount; }
        }
    }


    public class Product
    {
        public int VeproId { get; set; }
        public int StockId { get; set; }
        public string? VendorName { get; set; }
        public string? StockName { get; set; }
        public int Price { get; set; }
        public int EmpId { get; set; }
    }
}
