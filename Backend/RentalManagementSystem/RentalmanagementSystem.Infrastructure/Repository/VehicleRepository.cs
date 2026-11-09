using Microsoft.EntityFrameworkCore;
using RentalmanagementSystem.Infrastructure.DbContext;
using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using RentalManagementSystem.Domain.Models;
using RentalManagementSystem.Domain.RepositoryContracts;

namespace RentalmanagementSystem.Infrastructure.Repository
{
    public class VehicleRepository(AppDbContext context) : IVehicleRepository
    {

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await context.Vehicles.AddAsync(vehicle);
            await context.SaveChangesAsync();
        }

        public async Task<List<VehicleCategory>> GetAllVehicleCategoryAsync()
        {
            return await context.VehicleCategories.ToListAsync();
        }
        
        public async Task<bool> LicensePlateExistAsync(string LicensePlate)
        {
            return await context.Vehicles.AnyAsync(x => x.LicensePlate == LicensePlate);
        }
        
        public async Task<bool> RegistrationNumberExistAsync(string RegistrationNumber)
        {
            return await context.Vehicles.AnyAsync(x => x.RegistrationNumber == RegistrationNumber);
        }

        public async Task<bool> UniqueLicensePlateOnUpdate(int VehicleId, string LicensePlate)
        {
            return await context.Vehicles.AnyAsync(x => x.Id != VehicleId && x.LicensePlate == LicensePlate);
        }
       
        public async Task<bool> UniqueRegistrationNumberOnUpdate(int VehicleId, string RegistrationNumber)
        {
            return await context.Vehicles.AnyAsync(x => x.Id != VehicleId && x.RegistrationNumber == RegistrationNumber);
        }
       
        public async Task<PagedResult<Vehicle>> GetPagedVehicleAsync(VehicleFilterParameters query)
        {

            var vehicle = context.Vehicles.Include(x => x.VehicleCategory).Include(x => x.RentalCompany).Where(x => !x.IsDeleted);

            if (query.VehicleAvailabilityStatus.HasValue)
            {
                vehicle = vehicle.Where(x => x.VehicleAvailabilityStatus == query.VehicleAvailabilityStatus);
            }

            if (query.VehicleCategoryId.HasValue)
            {
                vehicle = vehicle.Where(x => x.VehicleCategory.Id == query.VehicleCategoryId);
            }


            if (!string.IsNullOrWhiteSpace(query.SearchBy))
            {
                var search = query.SearchBy.Trim();

                vehicle = vehicle.Where(x => x.MakeAndModel.Contains(search) ||
                                                         x.LicensePlate.Contains(search) ||
                                                         x.RegistrationNumber.Contains(search) ||
                                                         x.YearofManufacture.ToString().Contains(search) ||
                                                         x.SeatingCapacity.ToString().Contains(search) ||
                                                         x.VehicleAvailabilityStatus.ToString().Contains(search) ||
                                                         x.RentalCompany.CompanyName.Contains(search));

            }

            vehicle = query.SortBy?.ToLower() switch
            {
                "id" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.Id) :
                vehicle.OrderBy(x => x.Id),
                "makeandmodel" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.MakeAndModel) :
                vehicle.OrderBy(x => x.MakeAndModel),
                "licenseplate" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.LicensePlate.ToString()) :
                vehicle.OrderBy(x => x.LicensePlate.ToString()),
                "registrationnumber" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.RegistrationNumber) :
                vehicle.OrderBy(x => x.RegistrationNumber),
                "yearofmanufacture" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.YearofManufacture) :
                vehicle.OrderBy(x => x.YearofManufacture),
                "seatingcapacity" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.SeatingCapacity) :
                vehicle.OrderBy(x => x.SeatingCapacity),
                "dailyrentalrate" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.DailyRentalRate) :
                vehicle.OrderBy(x => x.DailyRentalRate),
                "vehicleavailabilitystatus" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.VehicleAvailabilityStatus.ToString()) :
                vehicle.OrderBy(x => x.VehicleAvailabilityStatus.ToString()),
                "mileage" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.Mileage) :
                vehicle.OrderBy(x => x.Mileage),
                "companyname" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.RentalCompany.CompanyName) :
                vehicle.OrderBy(x => x.RentalCompany.CompanyName),
                _ => vehicle.OrderByDescending(x => x.Id)
            };


            var totalCount = await vehicle.CountAsync();

            var items = await vehicle
                              .Skip((query.PageNumber - 1) * query.PageSize)
                              .Take(query.PageSize)
                              .ToListAsync();

            return new PagedResult<Vehicle>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
            };

        }
        
        public async Task<PagedResult<Vehicle>> GetAllAvailableVehicle(AvailableVehicleFilterParameters query)
        {
            var vehicle = context.Vehicles.Include(x => x.RentalCompany).Include(x => x.VehicleCategory).Where(x => !x.IsDeleted && x.VehicleAvailabilityStatus == VehicleAvailabilityStatus.Available && x.RentalCompany.Status != CompanyStatus.InActive);

            

            if (query.VehicleCategoryId.HasValue)
            {
                vehicle = vehicle.Where(x => x.VehicleCategoryId == query.VehicleCategoryId);
            }

            if (query.RentalCompanyId.HasValue)
            {
                vehicle = vehicle.Where(x => x.RentalCompanyId == query.RentalCompanyId);
            }

            if (query.SeatingCapacity.HasValue)
            {
                vehicle = vehicle.Where(x => x.SeatingCapacity == query.SeatingCapacity);
            }

            if(query.FromDailyRentalRate.HasValue && query.ToDailyRentalRate.HasValue)
            {
                vehicle = vehicle.Where(x => x.DailyRentalRate >= query.FromDailyRentalRate.Value && x.DailyRentalRate <= query.ToDailyRentalRate);
            }

            else if (query.FromDailyRentalRate.HasValue)
            {
                vehicle = vehicle.Where(x => x.DailyRentalRate >= query.FromDailyRentalRate.Value);
            }

            else if (query.ToDailyRentalRate.HasValue)
            {
                vehicle = vehicle.Where(x =>  x.DailyRentalRate <= query.ToDailyRentalRate);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchBy))
            {
                var search = query.SearchBy.Trim();

                vehicle = vehicle.Where(x => x.MakeAndModel.Contains(search) ||
                                                         x.LicensePlate.Contains(search) ||
                                                         x.RegistrationNumber.Contains(search) ||
                                                         x.YearofManufacture.ToString().Contains(search) ||
                                                         x.SeatingCapacity.ToString().Contains(search) || 
                                                         x.RentalCompany.CompanyName.Contains(search) || 
                                                         x.Mileage.ToString().Contains(search)
                                                         );

            }

            vehicle = query.SortBy?.ToLower() switch
            {
                "id" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.Id) :
                vehicle.OrderBy(x => x.Id),
                "makeandmodel" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.MakeAndModel) :
                vehicle.OrderBy(x => x.MakeAndModel),
                "licenseplate" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.LicensePlate.ToString()) :
                vehicle.OrderBy(x => x.LicensePlate.ToString()),
                "registrationnumber" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.RegistrationNumber) :
                vehicle.OrderBy(x => x.RegistrationNumber),
                "yearofmanufacture" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.YearofManufacture) :
                vehicle.OrderBy(x => x.YearofManufacture),
                "seatingcapacity" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.SeatingCapacity) :
                vehicle.OrderBy(x => x.SeatingCapacity),
                "dailyrentalrate" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.DailyRentalRate) :
                vehicle.OrderBy(x => x.DailyRentalRate),
                "companyname" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.RentalCompany.CompanyName) :
                vehicle.OrderBy(x => x.RentalCompany.CompanyName),
                "categoryname" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.VehicleCategory.CategoryName) :
                vehicle.OrderBy(x => x.VehicleCategory.CategoryName),
                "mileage" => query.SortDirection == "desc" ? vehicle.OrderByDescending(x => x.Mileage) :
                vehicle.OrderBy(x => x.Mileage),
                _ => vehicle.OrderByDescending(x => x.Id)
            };


            var totalCount = await vehicle.CountAsync();

            var items = await vehicle
                              .Skip((query.PageNumber - 1) * query.PageSize)
                              .Take(query.PageSize)
                              .ToListAsync();

            return new PagedResult<Vehicle>
            {
                Items = items,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
            };
        }
        public async Task<Vehicle?> GetVehicleById(int VehicleId)
        {
            return await context.Vehicles.FirstOrDefaultAsync(x => x.Id == VehicleId);
        }
        public async Task DeleteVehicleAsync(int VehicleId, Guid user)
        {
            var vehicle = await context.Vehicles.FirstOrDefaultAsync(x => x.Id == VehicleId);
            if(vehicle != null)
            {
                vehicle.IsDeleted = true;
                vehicle.DeletedDate = DateTime.Now;
                vehicle.DeletedBy = user;

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            context.Vehicles.Update(vehicle);
            await context.SaveChangesAsync();
        }
        
    }
}
