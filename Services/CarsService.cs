using CarsMongoDbDemo.ViewModels.Car;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CarsMongoDbDemo.Services;

public class CarsService
{
    private readonly IMongoCollection<Car> _carsCollection;

    // Setup connection method using CarStoreDatabaseSettings class
    public CarsService(IOptions<CarStoreDatabaseSettings> carStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            carStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            carStoreDatabaseSettings.Value.DatabaseName);

        _carsCollection = mongoDatabase.GetCollection<Car>(
            carStoreDatabaseSettings.Value.CarsCollectionName);
    }

    // Get Cars
    public async Task<List<CarResultViewModel>> GetCarsAsync()
    {
        var cars = await _carsCollection.Find(_ => true).ToListAsync();
        var mappedCars = cars.Select(x => new CarResultViewModel
        {
            Id = x.Id,
            VehicleBrand = x.VehicleBrand,
            Name = x.Name,
            Price = x.Price
        }).ToList();

        return mappedCars;
    }

    // Get Car By Id
    public async Task<CarResultViewModel?> GetCarById(string id)
    {
        var car = await _carsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        if (car == null) return null;

        var mappedCar = new CarResultViewModel
        {
            Id = car.Id,
            VehicleBrand = car.VehicleBrand,
            Name = car.Name,
            Price = car.Price
        };

        return mappedCar;
    }

    // Add new Car
    public async Task AddCar(CarInputViewModel newCar)
    {
        var mappedCar = new Car
        {
            VehicleBrand = newCar.VehicleBrand,
            Name = newCar.Name,
            Price = newCar.Price
        };

        await _carsCollection.InsertOneAsync(mappedCar);
    }

    // Update Car
    public async Task<CarResultViewModel> UpdateCar(CarInputViewModel updatedCarInput)
    {
        var car = await _carsCollection.Find(x => x.Id == updatedCarInput.Id).FirstOrDefaultAsync();

        if (car == null) return null;

        car.Name = updatedCarInput.Name;
        car.VehicleBrand = updatedCarInput.VehicleBrand;
        car.Price = updatedCarInput.Price;

        await _carsCollection.ReplaceOneAsync(x => x.Id == updatedCarInput.Id, car);

        var mappedCar = new CarResultViewModel
        {
            Name = updatedCarInput.Name,
            VehicleBrand = updatedCarInput.VehicleBrand,
            Price = updatedCarInput.Price
        };

        return mappedCar;
    }

    // Remove Car
    public async Task<CarResultViewModel> RemoveCar(string id)
    {
        // await _carsCollection.DeleteOneAsync(x => x.Id == id);
        var deletedCar = await _carsCollection.FindOneAndDeleteAsync(x => x.Id == id);

        if (deletedCar == null) return null;

        var mappedCar = new CarResultViewModel
        {
            Id = deletedCar.Id,
            Name = deletedCar.Name,
            VehicleBrand = deletedCar.VehicleBrand,
            Price = deletedCar.Price
        };

        return mappedCar;
    }
}