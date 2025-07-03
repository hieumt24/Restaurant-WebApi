using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(
    ILogger<GetRestaurantByIdQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get Restaurant {restaurantId}", request.Id);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        // var restaurantDto = RestaurantDto.FromEntity(restaurant);
        var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }
}