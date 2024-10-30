using ShoppingCartApp.Application.Services;
using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Domain.Entities.Item;

namespace ShoppingCartApp.Application.Tests;

public class PromotionServiceTests
{
    private readonly PromotionService _promotionService;
    private readonly Cart _cart;

    public PromotionServiceTests()
    {
        _promotionService = new PromotionService();
        _cart = new Cart();
    }

    [Fact]
    public void ApplyBestPromotion_ShouldApplySameSellerPromotion_WhenAllItemsHaveSameSeller()
    {
        _cart.AddItem(new DefaultItem { Id = 1, Price = 100, Quantity = 1, SellerId = 2001 });
        _cart.AddItem(new DefaultItem { Id = 2, Price = 200, Quantity = 1, SellerId = 2001 });

        _promotionService.ApplyBestPromotion(_cart);
        var bestDiscount = _cart.Discount.GetDiscountAmount(_cart.TotalPrice);
        var appliedPromotionId = _cart.PromotionId;
        _cart.ResetCart();
        // Assert

        Assert.Equal(30, bestDiscount); // 10% of 300
        Assert.Equal(9909, appliedPromotionId); // SameSellerPromotion ID
    }

    [Fact]
    public void ApplyBestPromotion_ShouldApplyCategoryPromotion_WhenItemsHaveCategoryPromotion()
    {
        _cart.AddItem(new DefaultItem
            { Id = 1, Price = 100, Quantity = 1, CategoryId = 3003 }); // CategoryPromotion applicable

        _promotionService.ApplyBestPromotion(_cart);
        var bestDiscount = _cart.Discount.GetDiscountAmount(_cart.TotalPrice);
        var appliedPromotionId = _cart.PromotionId;
        _cart.ResetCart();
        // Assert
        Assert.Equal(5, bestDiscount); // 5% of 100
        Assert.Equal(5676, appliedPromotionId); // CategoryPromotion ID
    }

    [Fact]
    public void ApplyBestPromotion_ShouldApplyTotalPricePromotion_WhenCartExceedsPriceThreshold()
    {
        _cart.AddItem(new DefaultItem { Id = 1, Quantity = 1, Price = 6000 });

        _promotionService.ApplyBestPromotion(_cart);
        var bestDiscount = _cart.Discount.GetDiscountAmount(_cart.TotalPrice);
        var appliedPromotionId = _cart.PromotionId;
        _cart.ResetCart();
        // Assert
        Assert.Equal(500, bestDiscount); // 500 TL discount for price between 5000 and 10000
        Assert.Equal(1232, appliedPromotionId); // TotalPricePromotion ID
    }

    [Fact]
    public void ApplyBestPromotion_ShouldApplyMaximumDiscount_WhenMultiplePromotionsApplicable()
    {
        _cart.AddItem(new DefaultItem
        {
            Id = 1, Price = 6000, Quantity = 1, SellerId = 2001, CategoryId = 3003
        }); // Both SameSeller and TotalPrice promotions applicable

        _promotionService.ApplyBestPromotion(_cart);
        var bestDiscount = _cart.Discount.GetDiscountAmount(_cart.TotalPrice);
        var appliedPromotionId = _cart.PromotionId;
        _cart.ResetCart();
        // Assert
        Assert.Equal(500, bestDiscount); // TotalPricePromotion gives higher discount than SameSeller
        Assert.Equal(1232, appliedPromotionId); // TotalPricePromotion ID
    }

    [Fact]
    public void ApplyBestPromotion_ShouldReturnZeroDiscount_WhenNoPromotionIsApplicable()
    {
        _cart.AddItem(new DefaultItem { Id = 1, Quantity = 1, Price = 100, SellerId = 2003 });
        _cart.AddItem(new DefaultItem { Id = 2, Quantity = 1, Price = 200, SellerId = 2001 });

        _promotionService.ApplyBestPromotion(_cart);
        var bestDiscount = _cart.Discount.GetDiscountAmount(_cart.TotalPrice);
        var appliedPromotionId = _cart.PromotionId;
        _cart.ResetCart();
        Assert.Equal(0, bestDiscount);
        Assert.Equal(0, appliedPromotionId); // No promotion applied
    }
}