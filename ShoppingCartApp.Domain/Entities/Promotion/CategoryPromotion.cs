using ShoppingCartApp.Domain.Enums;
using ShoppingCartApp.Domain.Interfaces;
using ShoppingCartApp.Domain.ValueObjects;

namespace ShoppingCartApp.Domain.Entities.Promotion;

public class CategoryPromotion : IPromotion
{
    private const int PromotionId = 5676;
    private const double Discount = 0.05; // %5 discount
    private const int PromoCategoryId = 3003;

    public Discount GetDiscount(Cart cart)
    {
        var discountAmount = cart.Items
            .Where(item => item.CategoryId == PromoCategoryId)
            .Sum(item => item.Price * item.Quantity * Discount);

        return new Discount(discountAmount, DiscountType.Fixed);
    }

    public int GetPromotionId()
    {
        return PromotionId;
    }
}