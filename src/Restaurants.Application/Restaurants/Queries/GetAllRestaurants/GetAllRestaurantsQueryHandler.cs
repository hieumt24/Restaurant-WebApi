using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(
    ILogger<GetAllRestaurantsQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
    public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all Restaurants");
        var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase, 
            request.PageSize, 
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        // var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);
        var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        
        var result = new PagedResult<RestaurantDto>(restaurantsDto, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}