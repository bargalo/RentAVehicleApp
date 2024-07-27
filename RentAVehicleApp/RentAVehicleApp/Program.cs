using RentAVehicleApp.Data;
using RentAVehicleApp.Entities;
using RentAVehicleApp.Entities.Extensions;
using RentAVehicleApp.Repositories;
using RentAVehicleApp.Repositories.Extensions;
using System.Reflection;
using System.Security.AccessControl;

var auditFile = new AuditFile("plikAudytowy.txt");
var jsonFile = new EntitiesInFile("dataInJson.json");

var rentierRepository = new SqlRepository<Rentier>(new RentAVehicleAppDbContext());

LoadDataFromJson(jsonFile, rentierRepository);

rentierRepository.ItemAdded += (sender, e) => RentierAdded(sender, e, auditFile);
rentierRepository.ItemRemoved += (sender, e) => RentierRemoved(sender, e, auditFile);

static void LoadDataFromJson(EntitiesInFile jsonFile, SqlRepository<Rentier> rentierRepository)
{
    var dataFromJson = jsonFile.LoadFromJson<Rentier>();
    if (dataFromJson != null)
    {
        rentierRepository.Add(dataFromJson);
    }
}

static void RentierRemoved(object? sender, Rentier e, AuditFile auditFile)
{
    auditFile.AddInfo_HasBeenRemoved(e);
    Console.WriteLine($"Rentier {e.Name} with Id:{e.Id} from {sender.GetType().Name} has been removed!");
}

static void RentierAdded(object? sender, Rentier e, AuditFile auditFile)
{
    auditFile.AddInfo_HasBeenAdded(e);
    Console.WriteLine($"Rentier {e.Name} with Id:{e.Id} from {sender.GetType().Name} has been added!");
}

AddRentiers(rentierRepository, jsonFile);
WriteAllToConsole(rentierRepository);

static void AddRentiers(IRepository<Rentier> rentierRepository, EntitiesInFile jsonFile)
{
    var rentiers = new[]
    {
        new Rentier {Name = "Bartek"},
        new Rentier {Name = "Piotr"},
        new Rentier {Name = "Zuzia"}
    };

    rentierRepository.AddBatch(rentiers, jsonFile);
}


static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
    //var copied = items[0].Copy();
    //Console.WriteLine(copied);
}





