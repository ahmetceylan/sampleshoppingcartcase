using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Domain.Entities.Promotion;
using ShoppingCartApp.Domain.Enums;
using ShoppingCartApp.Domain.Interfaces;
using ShoppingCartApp.Domain.ValueObjects;

namespace ShoppingCartApp.Application.Services;

public class PromotionService
{
    private readonly List<IPromotion> _promotions;

    public PromotionService()
    {
        _promotions = new List<IPromotion>
        {
            new SameSellerPromotion(),
            new CategoryPromotion(),
            new TotalPricePromotion()
        };
    }

    public void ApplyBestPromotion(Cart cart)
    {
        var bestDiscount = new Discount(0, DiscountType.Fixed);
        double bestDiscountAmount = 0;
        var appliedPromotionId = 0;

        foreach (var promotion in _promotions)
        {
            var discount = promotion.GetDiscount(cart);
            var discountAmount = discount.GetDiscountAmount(cart.TotalPrice);
            if (discountAmount > bestDiscountAmount)
            {
                bestDiscount = discount;
                bestDiscountAmount = discountAmount;
                appliedPromotionId = promotion.GetPromotionId();
            }
        }

        if (appliedPromotionId != 0) cart.ApplyPromotion(bestDiscount, appliedPromotionId);
    }
}