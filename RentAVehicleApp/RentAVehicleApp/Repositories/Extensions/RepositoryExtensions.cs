using RentAVehicleApp.Data;
using RentAVehicleApp.Entities;

namespace RentAVehicleApp.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static void AddBatch<T>(this IRepository<T> repository, T[] items, EntitiesInFile jsonFile)
      where T : class, IEntity
    {
        foreach (var item in items)
        {
            repository.Add(item);
        }

        repository.Save();
        jsonFile.SaveToJson(items);
    }
}
