using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirementHandler(IRestaurantsRepository restaurantsRepository,
    IUserContext userContext) : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        var restaurants = await restaurantsRepository.GetAllAsync();
        
        var userRestaurantsCount = restaurants.Count(x => x.OwnerId == currentUser!.Id);

        if (userRestaurantsCount >= requirement.MinimumRestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}