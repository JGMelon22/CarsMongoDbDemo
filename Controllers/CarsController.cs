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

    // [HttpGet("{id:length(24)}")]
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        if (id == null || id.Length < 24)
            return NotFound();

        var car = await _carsService.GetCarById(id);

        return await Task.Run(() => View(car));
    }
}