using ShoppingCartApp.Domain.Enums;

namespace ShoppingCartApp.Domain.ValueObjects;

public class Discount
{
    private double Amount { get; }
    private DiscountType Type { get; } // Could be "percentage", "fixed"

    public Discount(double amount, DiscountType type)
    {
        if (amount < 0)
            throw new ArgumentException("Discount amount cannot be negative");

        Amount = amount;
        Type = type;
    }

    public double GetDiscountAmount(double totalPrice)
    {
        if (Type == DiscountType.Percentage)
            return totalPrice * Amount / 100;

        return Amount;
    }

    public double ApplyDiscount(double price)
    {
        if (Type == DiscountType.Percentage)
            return price - price * Amount / 100;

        return price - Amount;
    }
}