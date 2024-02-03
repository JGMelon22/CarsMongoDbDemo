using CarsMongoDbDemo.Controllers.Extensions;
using CarsMongoDbDemo.Services;
using CarsMongoDbDemo.ViewModels.Car;
using FluentValidation;

namespace CarsMongoDbDemo.Controllers;

public class CarsController : Controller
{
    private readonly IValidator<AddCarViewModel> _addCarViewModel;

    // DI
    private readonly CarsService _carsService;
    private readonly IValidator<UpdateCarViewModel> _updateCarViewModel;

    public CarsController(CarsService carsService, IValidator<AddCarViewModel> addCarViewModel,
        IValidator<UpdateCarViewModel> updateCarViewModel)
    {
        _carsService = carsService;
        _addCarViewModel = addCarViewModel;
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
        if (id == null || id.Length < 24)
            return NotFound();

        var car = await _carsService.GetCarById(id);

        return await Task.Run(() => View(car));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return await Task.Run(View);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddCarViewModel newCar)
    {
        var result = await _addCarViewModel.ValidateAsync(newCar);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(nameof(Create));
        }

        await _carsService.AddCar(newCar);

        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }

    // Update Car
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var car = await _carsService.GetCarById(id);
        if (id == null || id.Length < 24 || id.Length > 24)
            return NotFound();

        return await Task.Run(() => View(car));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateCarViewModel updateCarViewModel)
    {
        var result = await _updateCarViewModel.ValidateAsync(updateCarViewModel);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(nameof(Edit));
        }

        await _carsService.UpdateCar(updateCarViewModel);

        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }

    // Call view to remove
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var car = await _carsService.GetCarById(id);
        if (id == null || id.Length < 24 || id.Length > 24)
            return NotFound();

        return await Task.Run(() => View(car));
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var carToRemove = await _carsService.RemoveCar(id);
        if (carToRemove == null)
            return NotFound();

        await _carsService.RemoveCar(id);

        return await Task.Run(() => RedirectToAction(nameof(Index)));
    }
}