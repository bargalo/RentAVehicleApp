using RentAVehicleApp.Repositories;
using System.Text.Json;
using System.IO;
using RentAVehicleApp.Entities;

namespace RentAVehicleApp.Data;

public class EntitiesInFile
{
    public EntitiesInFile(string jsonFileName)
    {
        JsonFileName = jsonFileName;
    }

    public string JsonFileName { get; private set; }

    public void SaveToJson<T>(T entity)
    {
        string jsonData = JsonSerializer.Serialize(entity);
        File.AppendAllText(JsonFileName, jsonData);
    }

    public T? LoadFromJson<T>() where T : class, IEntity
    {
        T? deserializedEntity = default;
        if (File.Exists(JsonFileName))
        {
            string jsonData = File.ReadAllText(JsonFileName);
            deserializedEntity = JsonSerializer.Deserialize<T>(jsonData);
        }
        return deserializedEntity;
    }
}
