using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using RentalManagementSystem.Domain.Models;
using System.Data;

namespace RentalManagementSystem.Domain.RepositoryContracts
{
    public interface IRentalCompanyRepository
    {

        Task AddRentalCompanyAsync(RentalCompany rentalCompany);
        Task<bool> CompanyNameExistAsync(string CompanyName);
        Task<bool> uniqueCompanyNameOnUpdate(int CompanyId, string CompanyName);
        Task<bool> LicenseNumberExistAsync(string LicenseNumber);
        Task<bool> UniqueLicenseOnUpdate(int CompanyId, string LicenseNumber);
        Task<PagedResult<RentalCompany>> GetPagedRentalCompanyAsync(RentalCompanyFilterParameters query);
        Task<List<RentalCompany>> GetAllRentalCompanyAsync();
        Task<RentalCompany?> RentalCompanyExistsAsync(int CompanyId);
        Task UpdateRentalCompanyAsync( RentalCompany rentalCompany);
        Task<RentalCompany?> GetRentalCompanyById(int CompanyId);
        Task DeleteRentalCompanyAsync(int CompanyId, Guid user);



    }
}


