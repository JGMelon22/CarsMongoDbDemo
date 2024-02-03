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
    public async Task<ServiceResponse<List<CarResultViewModel>>> GetCarsAsync()
    {
        var serviceResponse = new ServiceResponse<List<CarResultViewModel>>();

        try
        {
            var cars = await _carsCollection.Find(_ => true).ToListAsync();

            if (cars is null)
                throw new Exception("Cars list is empty!");

            var carsMapped = new List<CarResultViewModel>();

            foreach (var car in cars)
            {
                var carResult = new CarResultViewModel
                {
                    Id = car.Id,
                    VehicleBrand = car.VehicleBrand,
                    Name = car.Name,
                    Price = car.Price
                };

                carsMapped.Add(carResult);
            }

            serviceResponse.Data = carsMapped;
        }
        catch (Exception ex)
        {
            serviceResponse.Message = ex.Message;
            serviceResponse.Success = false;
        }

        return serviceResponse;
    }

    // Get Car By Id
    public async Task<ServiceResponse<CarResultViewModel?>> GetCarById(string id)
    {
        var serviceResponse = new ServiceResponse<CarResultViewModel>();

        try
        {
            var car = await _carsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (car is null)
                throw new Exception($"Car with id {id} not found!");

            var mappedCar = new CarResultViewModel
            {
                Id = car.Id,
                VehicleBrand = car.VehicleBrand,
                Name = car.Name,
                Price = car.Price
            };

            serviceResponse.Data = mappedCar;
        }
        catch (Exception ex)
        {
            serviceResponse.Message = ex.Message;
            serviceResponse.Success = false;
        }

        return serviceResponse!;
    }

    // Add new Car
    public async Task<ServiceResponse<CarResultViewModel>> AddCar(CarInputViewModel newCar)
    {
        var serviceResponse = new ServiceResponse<CarResultViewModel>();

        try
        {
            var car = new Car
            {
                VehicleBrand = newCar.VehicleBrand,
                Name = newCar.Name,
                Price = newCar.Price
            };

            await _carsCollection.InsertOneAsync(car);

            var mappedCar = new CarResultViewModel
            {
                Id = car.Id,
                VehicleBrand = car.VehicleBrand,
                Name = car.Name,
                Price = car.Price
            };

            serviceResponse.Data = mappedCar;
        }

        catch (Exception ex)
        {
            serviceResponse.Message = ex.Message;
            serviceResponse.Success = false;
        }

        return serviceResponse;
    }

    // Update Car
    public async Task<ServiceResponse<CarResultViewModel>> UpdateCar(string id, CarInputViewModel updatedCarInput)
    {
        var serviceResponse = new ServiceResponse<CarResultViewModel>();

        try
        {
            var car = await _carsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (car is null)
                throw new Exception($"Car with id {id} not found!");

            car.Name = updatedCarInput.Name;
            car.VehicleBrand = updatedCarInput.VehicleBrand;
            car.Price = updatedCarInput.Price;

            await _carsCollection.ReplaceOneAsync(x => x.Id == id, car);

            var mappedCar = new CarResultViewModel
            {
                Id = car.Id,
                VehicleBrand = car.VehicleBrand,
                Name = car.Name,
                Price = car.Price
            };

            serviceResponse.Data = mappedCar;
        }
        catch (Exception ex)
        {
            serviceResponse.Message = ex.Message;
            serviceResponse.Success = false;
        }

        return serviceResponse;
    }

    // Remove Car
    public async Task<ServiceResponse<bool>> RemoveCar(string id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        try
        {
            var car = await _carsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (car is null)
                throw new Exception($"Car with id {id} not found!");

            await _carsCollection.DeleteOneAsync(x => x.Id == car.Id);
        }
        catch (Exception ex)
        {
            serviceResponse.Message = ex.Message;
            serviceResponse.Success = false;
        }

        return serviceResponse;
    }
}