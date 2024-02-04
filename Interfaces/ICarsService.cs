using CarsMongoDbDemo.ViewModels.Car;

namespace CarsMongoDbDemo.Interfaces;

public interface ICarsService
{
    Task<ServiceResponse<List<CarResultViewModel>>> GetCarsAsync();
    Task<ServiceResponse<CarResultViewModel?>> GetCarById(string id);
    Task<ServiceResponse<CarResultViewModel>> AddCar(CarInputViewModel newCar);
    Task<ServiceResponse<CarResultViewModel>> UpdateCar(string id, CarInputViewModel updatedCarInput);
    Task<ServiceResponse<bool>> RemoveCar(string id);
}