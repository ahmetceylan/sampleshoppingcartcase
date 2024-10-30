using ShoppingCartApp.Domain.Enums;
using ShoppingCartApp.Domain.Interfaces;
using ShoppingCartApp.Domain.ValueObjects;

namespace ShoppingCartApp.Domain.Entities.Promotion;

public class TotalPricePromotion : IPromotion
{
    private const int PromotionId = 1232;
    private readonly double _totalPricePromoLevel1 = 500;
    private readonly double _totalPricePromoLevel2 = 5000;
    private readonly double _totalPricePromoLevel3 = 10000;
    private readonly double _totalPricePromoLevel4 = 50000;

    public Discount GetDiscount(Cart cart)
    {
        var totalPrice = cart.TotalPrice;
        if (totalPrice >= _totalPricePromoLevel1 && totalPrice < _totalPricePromoLevel2)
            return new Discount(250, DiscountType.Fixed);
        if (totalPrice >= _totalPricePromoLevel2 && totalPrice < _totalPricePromoLevel3)
            return new Discount(500, DiscountType.Fixed);
        if (totalPrice >= _totalPricePromoLevel3 && totalPrice < _totalPricePromoLevel4)
            return new Discount(1000, DiscountType.Fixed);
        if (totalPrice >= _totalPricePromoLevel4)
            return new Discount(2000, DiscountType.Fixed);
        return new Discount(0, DiscountType.Fixed);
    }

    public int GetPromotionId()
    {
        return PromotionId;
    }
}