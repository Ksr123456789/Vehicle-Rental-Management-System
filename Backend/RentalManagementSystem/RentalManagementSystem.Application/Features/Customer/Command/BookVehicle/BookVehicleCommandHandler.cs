using MediatR;
using RentalManagementSystem.Application.ServiceContracts;

namespace RentalManagementSystem.Application.Features.Customer.Command.BookVehicle
{
    public class BookVehicleCommandHandler(ICustomerService customerService) : IRequestHandler<BookVehicleCommand, BookVehicleCommandResponse>
    {
        public async Task<BookVehicleCommandResponse> Handle(BookVehicleCommand request, CancellationToken cancellationToken)
        {
            return await customerService.BookVehicle(request);
        }
    }
}
