﻿using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        return await dbContext.Restaurants.ToListAsync();
    }
}