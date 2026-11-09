using MediatR;
using RentalManagementSystem.Application.ServiceContracts;
using RentalManagementSystem.Domain.Common;

namespace RentalManagementSystem.Application.Features.Admin.query.GetPagedRentalCompany
{
    public class GetPagedRentalCompanyQueryHandler(IAdminService adminService) : IRequestHandler<GetPagedRentalCompanyQuery, PagedResult<GetPagedRentalCompanyQueryResponse>>
    {
        public async Task<PagedResult<GetPagedRentalCompanyQueryResponse>> Handle(GetPagedRentalCompanyQuery request, CancellationToken cancellationToken)
        {
            return await adminService.GetPagedRentalCompany(request);
        }
    }
}
