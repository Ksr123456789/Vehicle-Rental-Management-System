using MediatR;
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Enums;

namespace RentalManagementSystem.Application.Features.Admin.query.GetPagedRentalCompany
{
    public class GetPagedRentalCompanyQuery : IRequest<PagedResult<GetPagedRentalCompanyQueryResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public string? SearchBy { get; set; }
        public CompanyType? CompanyType { get; set; }
        public CompanyStatus? CompanyStatus { get; set; }

    }
}
