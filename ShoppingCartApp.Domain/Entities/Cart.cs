using ShoppingCartApp.Domain.Entities.Item;
using ShoppingCartApp.Domain.Entities.Promotion;
using ShoppingCartApp.Domain.Enums;
using ShoppingCartApp.Domain.ValueObjects;

namespace ShoppingCartApp.Domain.Entities;

public class Cart
{
    private readonly List<BaseItem> _items;
    private const int MaxUniqueItems = 10;
    private const int MaxTotalQuantity = 30;
    private const double MaxTotalAmount = 500_000;

    public Cart()
    {
        _items = new List<BaseItem>();
    }

    public List<BaseItem> Items => _items.Where(item => !(item is VasItem)).ToList(); // exclude vasItems
    public double TotalPrice => _items.Sum(item => item.Price * item.Quantity);
    public Discount Discount { get; private set; } = new(0, DiscountType.Fixed);
    public int PromotionId { get; private set; } = 0;
    public double TotalAmount => Discount.ApplyDiscount(TotalPrice);


    public bool AddItem(BaseItem item)
    {
        if (!item.Validate())
        {
            Console.WriteLine("Item Validation is failed! itemId: " + item.Id);
            return false;
        }

        if (_items.Count >= MaxUniqueItems)
        {
            Console.WriteLine("Add Item is failed! Violation of max 10 different item : " + item.Id);
            return false; // max 10 different item
        }

        if (_items.Sum(i => i.Quantity) + item.Quantity > MaxTotalQuantity)
        {
            Console.WriteLine("Add Item is failed! Violation of  max item quantity must be under 30 : " + item.Id);
            return false; // max item quantity must be under 30
        }


        if (TotalAmount + item.Price * item.Quantity > MaxTotalAmount)
        {
            Console.WriteLine("Add Item is failed! Violation of max total price : " + item.Id);
            return false; // max total price is exceeded
        }


        _items.Add(item);
        Console.WriteLine("Add Item is successful! itemId: " + item.Id);
        return true;
    }

    public bool RemoveItem(int itemId)
    {
        // TODO: check vasItems!!
        var itemToRemove = _items.FirstOrDefault(i => i.Id == itemId);
        if (itemToRemove != null)
        {
            _items.Remove(itemToRemove);
            Console.WriteLine("Remove Item is successful! itemId: " + itemToRemove.Id);
            return true;
        }

        Console.WriteLine("Remove Item is failed! itemId: " + itemToRemove?.Id);
        return false;
    }

    public void ApplyPromotion(Discount discount, int promotionId)
    {
        Discount = discount;
        PromotionId = promotionId;
    }

    public void ResetCart()
    {
        _items.Clear();
        Discount = new Discount(0, DiscountType.Fixed);
        Console.WriteLine("Reset cart is successful!");
    }
}