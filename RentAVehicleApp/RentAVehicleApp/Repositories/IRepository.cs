using Microsoft.EntityFrameworkCore;
using RentAVehicleApp.Entities;

namespace RentAVehicleApp.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
    //IEnumerable<T> GetAll();
    //T GetById(int id);
    //void Add(T item);
    //void Remove(T item);
    //void Save();
}