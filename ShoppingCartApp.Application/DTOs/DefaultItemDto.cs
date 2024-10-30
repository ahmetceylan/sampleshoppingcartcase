namespace ShoppingCartApp.Application.DTOs;

public class DefaultItemDto : ItemDto
{
    public List<VasItemDto> VasItems { get; set; }
}