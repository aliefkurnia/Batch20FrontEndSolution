using System.Collections;

namespace Realta.Frontend.Pages.Booking;

public class RoomDesc
{
    public string? RoomName { get; set; }
    public double RoomPrice { get; set; }
    public double RoomMaxPrice { get; set; }
    public int RoomCapacity { get; set; }
    public string? RoomPicLink { get; set; }
        
}

public class HotelDesc
{
        
    public string? HotelName { get; set; }
    public string? HotelLocation { get; set; }
    public double RatingStar { get; set; }
    public string? MemberStatus { get; set; }
    public string? Description { get; set; }
    public string? RatingDescription { get; set; }
    public int TotalRating { get; set; }
}
    
public class Coupon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public bool IsActive { get; set; }
    public double DiscountPercent { get; set; }
}

public class CartItem
{
    public RoomDesc? Room { get; set; }
    public int Quantity { get; set;}

    public double Subtotal
    {
        get { return (Room.RoomPrice * Quantity); } 
    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public class Cart
    {
        public static readonly Cart Instance = new Cart();
        
        private Cart() {}
        
        private List<CartItem> items = new List<CartItem>();

        public void AddItem(RoomDesc desc, int qty = 1)
        {
            // Cek apakah kamar sudah ditambahkan sebelumnya
            /*if (items.Any(item => item.Room.RoomName == desc.RoomName))
            {
                Console.WriteLine($"Kamar {desc.RoomName} sudah ditambahkan sebelumnya.");
                return;
            }*/

            var existingItem = items.FirstOrDefault(item => item.Room.RoomName == desc.RoomName);

            if (existingItem != null)
            {
                existingItem.Quantity += qty;
            }
            else
            {
                items.Add(new CartItem
                {
                    Room = desc,
                    Quantity = qty,
                });
            }
            }


        public IEnumerable<CartItem> getItems
        {
            get { return items; }
        }
        
        public void UpdateQuantity(string roomName, int quantity)
        {
            var itemToUpdate = items.FirstOrDefault(item => item.Room.RoomName == roomName);

            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity;
            }
        }
        
        public void RemoveItem(string roomName)
        {
            var itemToRemove = items.FirstOrDefault(item => item.Room.RoomName == roomName);    

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
        }
        
        // Clear all items from cart
        public void Clear()
        {
            items.Clear();
        }
        public int TotalItems
        {
            get { return items.Sum(item => item.Quantity); }
        }

        // Get total price of items in cart (excluding tax)
        public double TotalPriceExcludingTax
        {
            get { return items.Sum(item => item.Room.RoomPrice * item.Quantity); }
        }
        public double TaxAmount
        {
            get { return TotalPriceExcludingTax * 0.1d; }
        }

        // Get total price of items in cart (including tax)
        public double TotalPrice
        {
            get { return TotalPriceExcludingTax + TaxAmount; }
        }
        
    }
    