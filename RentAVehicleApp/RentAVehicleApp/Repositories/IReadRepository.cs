using RentAVehicleApp.Entities;

namespace RentAVehicleApp.Repositories;

public interface IReadRepository<out T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    //IEnumerable<T> GetAllFromJson();
    T? GetById(int id);
}
