using CarsMongoDbDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsMongoDbDemo.Controllers;

public class CarsController : Controller
{
    // DI
    private readonly CarsService _carsService;

    public CarsController(CarsService carsService)
    {
        _carsService = carsService;
    }

    // Call cars index view with all results!
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cars = await _carsService.GetCarsAsync();
        return cars != null
            ? await Task.Run(() => View(cars))
            : NoContent();
    }
}