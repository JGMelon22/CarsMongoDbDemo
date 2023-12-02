using Bogus;
using MongoDB.Driver;

namespace CarsMongoDbDemo.Configuration.Seeding;

public class InitialSeeding
{
    public static void Seed(IConfiguration configuration)
    {
        var car = new Faker<Car>()
            .RuleFor(c => c.Name, f => f.Vehicle.Manufacturer())
            .RuleFor(c => c.Name, f => f.Vehicle.Model())
            .RuleFor(c => c.Price, f => f.Random.Number(2000, 300000));
        
        configuration.ToBsonDocument()
    }
}