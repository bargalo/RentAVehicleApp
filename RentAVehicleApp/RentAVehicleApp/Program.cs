using RentAVehicleApp.Data;
using RentAVehicleApp.Entities;
using RentAVehicleApp.Repositories;


var rentierRepository = new SqlRepository<Rentier>(new RentAVehicleAppDbContext());
AddRentiers(rentierRepository);
AddManagers(rentierRepository);
WriteAllToConsole(rentierRepository);

static void AddRentiers(IRepository<Rentier> rentierRepository)
{
    rentierRepository.Add(new Rentier { Name = "Bartek" });
    rentierRepository.Add(new Rentier { Name = "Piotr" });
    rentierRepository.Add(new Rentier { Name = "Zuzia" });
    rentierRepository.Save();
}

static void AddManagers(IWriteRepository<Manager> managerRepository)
{
    managerRepository.Add(new Manager { Name = "Przemek" });
    managerRepository.Add(new Manager { Name = "Tomek" });
    managerRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}






