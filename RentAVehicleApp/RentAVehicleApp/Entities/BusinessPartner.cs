namespace RentAVehicleApp.Entities
{
    public class BusinessPartner : EntityBase
    {
        public string? Name { get; set; }
        public override string ToString() => $"Business Partner Id: {Id}, Name: {Name}";
    }
}
