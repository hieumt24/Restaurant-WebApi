﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes;

public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
    public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Remove all dishes for restaurant with id : {RestaurantId}", request.RestaurantId);
        
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if(restaurant == null) 
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        
        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidException();
        
        // await dishesRepository.Delete(restaurant.Dishes);
        var deletedCount = await dishesRepository.DeleteForRestaurantAsync(request.RestaurantId);
        
        logger.LogInformation("Successfully deleted {DeletedCount} dishes for restaurant {RestaurantId}", deletedCount, request.RestaurantId);
    }
}