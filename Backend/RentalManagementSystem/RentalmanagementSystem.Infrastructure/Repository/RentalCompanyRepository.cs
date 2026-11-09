using Microsoft.EntityFrameworkCore;
using RentalmanagementSystem.Infrastructure.DbContext;
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using RentalManagementSystem.Domain.Models;
using RentalManagementSystem.Domain.RepositoryContracts;
using System.Data;

namespace RentalmanagementSystem.Infrastructure.Repository
{
    public class RentalCompanyRepository(AppDbContext context) : IRentalCompanyRepository
    {
        public async Task AddRentalCompanyAsync(RentalCompany rentalCompany)
        {
            await context.RentalCompanies.AddAsync(rentalCompany);
            await context.SaveChangesAsync();
        }
        public async Task<bool> CompanyNameExistAsync(string CompanyName)
        {
            return await context.RentalCompanies.AnyAsync(x => x.CompanyName == CompanyName && !x.IsDeleted);
        }
        public async Task<bool> uniqueCompanyNameOnUpdate(int CompanyId, string CompanyName)
        {
            return await context.RentalCompanies.AnyAsync(x=> x.Id != CompanyId && x.CompanyName == CompanyName && !x.IsDeleted);
        }
        public async Task<bool> LicenseNumberExistAsync(string LicenseNumber)
        {
            return await context.RentalCompanies.AnyAsync(x=>x.LicenseNumber == LicenseNumber);
        }
        public async Task<bool> UniqueLicenseOnUpdate(int CompanyId, string LicenseNumber)
        {
            return await context.RentalCompanies.AnyAsync(x => x.Id != CompanyId && x.LicenseNumber == LicenseNumber);
        }
        public async Task<PagedResult<RentalCompany>> GetPagedRentalCompanyAsync(RentalCompanyFilterParameters query)
        {

            var RentalCompany = context.RentalCompanies.Where(x => !x.IsDeleted);

            if (query.CompanyStatus.HasValue)
            {
                RentalCompany = RentalCompany.Where(x => x.Status == query.CompanyStatus);
            }

            if (query.CompanyType.HasValue)
            {
                RentalCompany = RentalCompany.Where(x =>x.CompanyType == query.CompanyType);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchBy))
            {
                var search = query.SearchBy.Trim();

                RentalCompany = RentalCompany.Where(x => x.CompanyName.Contains(search) ||
                                                         x.OwnerName.Contains(search) ||
                                                         x.PhoneNumber.Contains(search) ||
                                                         x.HeadquartersLocation.Contains(search) ||
                                                         x.CompanyType.ToString().Contains(search) ||
                                                         x.LicenseNumber.Contains(search) ||
                                                         x.Status.ToString().Contains(search) 
                                                         );

            }

            RentalCompany = query.SortBy?.ToLower() switch
            {
                "id" => query.SortDirection == "desc" ? RentalCompany.OrderByDescending(x => x.Id) :
                RentalCompany.OrderBy(x => x.Id),
                "companyname" => query.SortDirection == "desc" ? RentalCompany.OrderByDescending(x => x.CompanyName) :
                RentalCompany.OrderBy(x => x.CompanyName),
                "companytype" => query.SortDirection == "desc" ? RentalCompany.OrderByDescending(x => x.CompanyType.ToString()) :
                RentalCompany.OrderBy(x => x.CompanyType.ToString()),
                "ownername" => query.SortDirection == "desc" ? RentalCompany.OrderByDescending(x => x.OwnerName) :
                RentalCompany.OrderBy(x => x.OwnerName),
                "phonenumber" => query.SortDirection == "desc" ? RentalCompany.OrderByDescending(x => x.PhoneNumber) :
                RentalCompany.OrderBy(x => x.PhoneNumber),
                "headquarterslocation" => query.SortDirection == "desc" ? RentalCompany.OrderByDescending(x => x.HeadquartersLocation) :
                RentalCompany.OrderBy(x => x.HeadquartersLocation),
                "licensenumber" => query.SortDirection == "desc" ? RentalCompany.OrderByDescending(x => x.LicenseNumber) :
                RentalCompany.OrderBy(x => x.LicenseNumber),
                "companystatus" => query.SortDirection == "desc" ? RentalCompany.OrderByDescending(x => x.Status.ToString()) :
                RentalCompany.OrderBy(x => x.Status.ToString()),
                _ => RentalCompany.OrderByDescending(x => x.Id)
            };


            var totalCount = await RentalCompany.CountAsync();

            var items = await RentalCompany
                              .Skip((query.PageNumber - 1) * query.PageSize)
                              .Take(query.PageSize)
                              .ToListAsync();

            return new PagedResult<RentalCompany>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
            };


        }
        public async Task<List<RentalCompany>> GetAllRentalCompanyAsync()
        {

            return await context.RentalCompanies.Where(x => !x.IsDeleted && (x.Status == CompanyStatus.Active)).ToListAsync();
        }
        public async Task<RentalCompany?> RentalCompanyExistsAsync(int CompanyId)
        {
            return await context.RentalCompanies.FirstOrDefaultAsync(x => x.Id == CompanyId);
        }
        public async Task UpdateRentalCompanyAsync(  RentalCompany rentalCompany)
        {


                context.RentalCompanies.Update(rentalCompany);
                await context.SaveChangesAsync();
           
        }
        public async Task<RentalCompany?> GetRentalCompanyById(int CompanyId)
        {
            return await context.RentalCompanies.Include(x => x.Vehicles).FirstOrDefaultAsync(x => x.Id == CompanyId);
        }
        public async Task DeleteRentalCompanyAsync(int CompanyId, Guid user)
        {
            var RentalCompany = await context.RentalCompanies.FirstOrDefaultAsync(x => x.Id == CompanyId);

            if(RentalCompany != null)
            {
                RentalCompany.IsDeleted = true;
                RentalCompany.DeletedBy = user;
                RentalCompany.DeletedDate = DateTime.Now;
                await context.SaveChangesAsync();
            }
            
        }

    }
}

