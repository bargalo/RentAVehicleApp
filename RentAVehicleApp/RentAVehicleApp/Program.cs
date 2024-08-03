using RentAVehicleApp.Data;
using RentAVehicleApp.Entities;
using RentAVehicleApp.Entities.Extensions;
using RentAVehicleApp.Repositories;
using RentAVehicleApp.Repositories.Extensions;
using RentAVehicleApp.UserInterface;
using System.Reflection;
using System.Security.AccessControl;

var auditFile = new AuditFile("plikAudytowy.txt");

var rentierRepository = new ListRepository<Rentier>();
var userInterface = new UserInterface(rentierRepository);

LoadDataFromJson(rentierRepository);
rentierRepository.ItemAdded += (sender, e) => RentierAdded(sender, e, auditFile);
rentierRepository.ItemRemoved += (sender, e) => RentierRemoved(sender, e, auditFile);

userInterface.MainMenu();

static void LoadDataFromJson(ListRepository<Rentier> rentierRepository)
{
    var dataFromJson = rentierRepository.LoadFromJson<Rentier>();
    if (dataFromJson != null)
    {
        rentierRepository.AddFromJson(dataFromJson);
    }
}

static void RentierRemoved(object? sender, Rentier e, AuditFile auditFile)
{
    auditFile.AddInfo_HasBeenRemoved(e);
    Console.WriteLine($"Rentier {e.Name} {e.Surname} with Id:{e.Id} from {sender.GetType().Name} has been removed!\n");
}

static void RentierAdded(object? sender, Rentier e, AuditFile auditFile)
{
    auditFile.AddInfo_HasBeenAdded(e);
    Console.WriteLine($"Rentier {e.Name} {e.Surname} with Id:{e.Id} from {sender.GetType().Name} has been added!\n");
}

//AddRentiers(rentierRepository);

//static void AddRentiers(IRepository<Rentier> rentierRepository)
//{
//    var rentiers = new[]
//    {
//        new Rentier {Name = "Bartek"},
//        new Rentier {Name = "Piotr"},
//        new Rentier {Name = "Zuzia"}
//    };

//    rentierRepository.AddBatch(rentiers);
//}







