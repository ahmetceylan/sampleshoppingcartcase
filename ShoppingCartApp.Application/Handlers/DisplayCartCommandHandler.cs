using ShoppingCartApp.Application.Commands;
using ShoppingCartApp.Application.DTOs;
using ShoppingCartApp.Domain.Entities;
using ShoppingCartApp.Domain.Entities.Item;

namespace ShoppingCartApp.Application.Handlers;

public class DisplayCartCommandHandler(Cart cart)
{
    public DisplayCartResultDto Handle(DisplayCartCommand command)
    {
        var result = new DisplayCartResultDto
        {
            Items = new List<ItemDto>()
        };

        foreach (var item in cart.Items)
            if (item is DefaultItem defaultItem)
            {
                // prepare the defaultItem DTO
                var defaultItemDto = new DefaultItemDto
                {
                    ItemId = defaultItem.Id,
                    CategoryId = defaultItem.CategoryId,
                    SellerId = defaultItem.SellerId,
                    Price = defaultItem.Price,
                    Quantity = defaultItem.Quantity,
                    VasItems = defaultItem.GetVasItems().Select(vasItem => new VasItemDto
                    {
                        VasItemId = vasItem.VasItemId,
                        VasCategoryId = vasItem.CategoryId,
                        VasSellerId = vasItem.SellerId,
                        Price = vasItem.Price,
                        Quantity = vasItem.Quantity
                    }).ToList()
                };

                result.Items.Add(defaultItemDto);
            }
            else
            {
                // other item types like DigitalItem
                result.Items.Add(new ItemDto
                {
                    ItemId = item.Id,
                    CategoryId = item.CategoryId,
                    SellerId = item.SellerId,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

        // other fields in result obj
        result.TotalAmount = cart.TotalAmount;
        result.TotalDiscount = cart.Discount.GetDiscountAmount(cart.TotalPrice);
        result.AppliedPromotionId = cart.PromotionId;

        return result;
    }
}