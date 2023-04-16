using CarsMongoDbDemo.Models;
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
    public async Task<List<GetCarViewModel>> GetCarsAsync()
    {
        var cars = await _carsCollection.Find(_ => true).ToListAsync();
        var mappedCars = cars.Select(x => new GetCarViewModel()
        {
            Id = x.Id,
            VehicleBrand = x.VehicleBrand,
            Name = x.Name,
            Price = x.Price
        }).Take(15).ToList();

        return mappedCars;
    }
}