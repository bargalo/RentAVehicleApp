using System.Xml.Linq;

namespace RentAVehicleApp.Entities
{
    public class Manager : Rentier
    { 
        public override string ToString() => $"Manager Id: {Id}, Name: {Name}";
    }
}
