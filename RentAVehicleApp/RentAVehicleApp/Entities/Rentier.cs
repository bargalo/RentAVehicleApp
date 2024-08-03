namespace RentAVehicleApp.Entities
{
    public class Rentier : EntityBase
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public override string ToString() => $"Rentier Id: {Id}, Name: {Name}, Surname: {Surname}";
        
    }
}
