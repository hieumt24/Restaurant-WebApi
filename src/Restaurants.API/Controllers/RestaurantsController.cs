using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dtos;
using Restaurants.Application.Restaurants;

namespace Restaurants.API.Controllers;
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var restaurants = await restaurantsService.GetAllRestaurants();
        return Ok(restaurants);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await restaurantsService.GetRestaurantById(id);
        if (restaurant is null)
            return NotFound();
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
    {
        var id = await restaurantsService.Create(createRestaurantDto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
}