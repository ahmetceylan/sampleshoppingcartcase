using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Domain.ValueObjects;

namespace ShoppingCartApp.Domain.Interfaces;

public interface IPromotion
{
    Discount GetDiscount(Cart cart);
    int GetPromotionId();
}