using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Entities;

namespace RentalManagementSystem.Application.Features.Customer.Query.GetAllExtraServices
{
    public class GetAllExtraServicesQueryHandler(ICustomerService customerService) : IRequestHandler<GetAllExtraServicesQuery, List<ExtraService>>
    {
        public async Task<List<ExtraService>> Handle(GetAllExtraServicesQuery request, CancellationToken cancellationToken)
        {
            return await customerService.GetAllExtraServices(request);
        }
    }
}
