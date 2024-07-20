namespace RentAVehicleApp.Entities
{
    public class Rentier : EntityBase
    {
        public string? Name { get; set; }

        public override string ToString() => $"Rentier Id: {Id}, Name: {Name}";
        
    }
}
