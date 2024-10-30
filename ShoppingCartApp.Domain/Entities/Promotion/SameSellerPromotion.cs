using ShoppingCartApp.Domain.Entities.Item;
using ShoppingCartApp.Domain.Enums;
using ShoppingCartApp.Domain.Interfaces;
using ShoppingCartApp.Domain.ValueObjects;

namespace ShoppingCartApp.Domain.Entities.Promotion;

public class SameSellerPromotion : IPromotion
{
    private const int PromotionId = 9909;
    private const double DiscountPercentage = 10; // %10 discount

    public Discount GetDiscount(Cart cart)
    {
        var distinctSellers = cart.Items
            .Where(item => !(item is VasItem)) // exclude vasItems
            .Select(item => item.SellerId)
            .Distinct();

        if (cart.Items.Count > 1 && distinctSellers.Count() == 1)
            return new Discount(DiscountPercentage, DiscountType.Percentage); // %10 discount

        return new Discount(0, DiscountType.Fixed);
    }

    public int GetPromotionId()
    {
        return PromotionId;
    }
}