using MediatR;

namespace RentalManagementSystem.Application.Features.Customer.Command.BookVehicle
{
    public class BookVehicleCommand : IRequest<BookVehicleCommandResponse>
    {
        public DateTime PickUpDateTime { get; set; }
        public DateTime ReturnDateTime { get; set; }
        public string PickupLocation { get; set; }
        public string ReturnLocation { get; set; }
        public int VehicleId { get; set; }
        public Boolean Insurance { get; set; }
        public List<int> ExtraServiceIds { get; set; } = new();
    }
}
