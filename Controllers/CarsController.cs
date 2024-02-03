using CarsMongoDbDemo.Controllers.Extensions;
using CarsMongoDbDemo.Services;
using CarsMongoDbDemo.ViewModels.Car;
using FluentValidation;

namespace CarsMongoDbDemo.Controllers;

public class CarsController : Controller
{
    private readonly IValidator<CarInputViewModel> _CarInputViewModel;

    // DI
    private readonly CarsService _carsService;
    private readonly IValidator<CarInputViewModel> _updateCarViewModel;

    public CarsController(CarsService carsService, IValidator<CarInputViewModel> CarInputViewModel,
        IValidator<CarInputViewModel> updateCarViewModel)
    {
        _carsService = carsService;
        _CarInputViewModel = CarInputViewModel;
        _updateCarViewModel = updateCarViewModel;
    }

    // Call cars index view with all results!
    [HttpGet]
    public async Task<IActionResult> Index(string sortOrder)
    {
        var sortingService = new SortingService(_carsService); // Sort service
        ViewBag.BrandSortParam = string.IsNullOrEmpty(sortOrder) ? "brand_desc" : "";

        var cars = await sortingService.SortCars(sortOrder);
        return cars != null
            ? await Task.Run(() => View(cars))
            : NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var car = await _carsService.GetCarById(id);
        return car.Data != null
            ? View(car.Data)
            : NotFound();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CarInputViewModel newCar)
    {
        var result = await _CarInputViewModel.ValidateAsync(newCar);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(nameof(Create));
        }

        await _carsService.AddCar(newCar);
        return RedirectToAction(nameof(Index));
    }

    // Update Car
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var car = await _carsService.GetCarById(id);
        return car.Data != null
            ? View(car.Data)
            : NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, CarInputViewModel carInputViewModel)
    {
        var result = await _updateCarViewModel.ValidateAsync(carInputViewModel);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(nameof(Edit));
        }

        await _carsService.UpdateCar(id, carInputViewModel);

        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }

    // Call view to remove
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var car = await _carsService.GetCarById(id);

        return car.Data != null
            ? View(car.Data)
            : NotFound();
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var car = await _carsService.RemoveCar(id);

        return car.Success != false
            ? RedirectToAction(nameof(Index))
            : BadRequest();
    }
}