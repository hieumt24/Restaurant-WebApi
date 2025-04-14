using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Description { get; set; } = default !;
    public string  Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    
    public List<DishDto> Dishes { get; set; } = [];

    public static RestaurantDto? FromEntity(Restaurant? entity)
    {
        if (entity == null)
            return null;
        return new RestaurantDto()
        {
            Category = entity.Category,
            Description = entity.Description,
            Id = entity.Id,
            HasDelivery = entity.HasDelivery,
            Name = entity.Name,
            City = entity.Address?.City,
            Street = entity.Address?.Street,
            PostalCode = entity.Address?.PostalCode,
            Dishes = entity.Dishes.Select(DishDto.FromEntity).ToList()
        };
    }
}