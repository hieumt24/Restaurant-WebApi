using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(
    ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating restaurant with {RestaurantId} with {@UpdateRestaurant}", request.Id, request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
        if (restaurant is null) 
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException();
        mapper.Map(request, restaurant);
        // restaurant.Name = request.Name; 
        // restaurant.Description = request.Description;
        // request.HasDelivery = restaurant.HasDelivery;

        await restaurantsRepository.SaveChanges();
        return true;
    }
}