namespace RentalManagementSystem.Domain.Entities
{
    public class VehicleCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Vehicle>Vehicles { get; set; } = new List<Vehicle>();
    }
}
