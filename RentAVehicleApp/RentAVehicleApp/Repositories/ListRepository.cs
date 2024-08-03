using Microsoft.EntityFrameworkCore;
using RentAVehicleApp.Entities;
using System.Text.Json;

namespace RentAVehicleApp.Repositories
{
    public class ListRepository<T> : IRepository<T> 
        where T : class, IEntity, new()     
    {       
        private readonly List<T> _items = new();
        private int lastUsedId = 1;
        private readonly string jsonFileName = "dataInJson.json";

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T? GetById(int id)
        {
            T? item = _items.SingleOrDefault(item => item.Id == id);
            return item;
        }   
            
        public void Add(T item)
        {
            if (_items.Count == 0)
            {
                item.Id = lastUsedId;
                lastUsedId++;
            }
            else if (_items.Count > 0)
            {
                lastUsedId = _items[_items.Count - 1].Id;
                item.Id = ++lastUsedId;
            }
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
            ItemRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            var items = GetAll();
            File.Delete(jsonFileName);
            string jsonData = JsonSerializer.Serialize(items);
            File.AppendAllText(jsonFileName, jsonData);
        }

        public T[]? LoadFromJson<TKey>() where TKey : class, IEntity
        {
            T[]? deserializedEntity = null;
            if (File.Exists(jsonFileName))
            {
                string jsonData = File.ReadAllText(jsonFileName);
                deserializedEntity = JsonSerializer.Deserialize<T[]>(jsonData);
            }
            return deserializedEntity;
        }

        public void AddFromJson(T[] entities)
        {
            foreach (var entity in entities)
            {
                _items.Add(entity);
            }
        }
    }
}
