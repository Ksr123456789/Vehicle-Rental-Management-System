using RentalManagementSystem.Domain.Common;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using RentalManagementSystem.Domain.Models;
using System.Globalization;

namespace RentalManagementSystem.Domain.RepositoryContracts
{
    public interface IVehicleRepository
    {
        Task AddVehicleAsync(Vehicle vehicle);

        Task<List<VehicleCategory>> GetAllVehicleCategoryAsync();
        Task<bool> LicensePlateExistAsync(string LicensePlate);
        Task<bool> RegistrationNumberExistAsync(string RegistrationNumber);

        Task<bool> UniqueLicensePlateOnUpdate(int VehicleId, string LicensePlate);
        Task<bool> UniqueRegistrationNumberOnUpdate(int VehicleId, string RegistrationNumber);
        Task<PagedResult<Vehicle>>GetPagedVehicleAsync(VehicleFilterParameters query);
        Task<PagedResult<Vehicle>> GetAllAvailableVehicle(AvailableVehicleFilterParameters query);
        Task<Vehicle?> GetVehicleById(int VehicleId);
        Task DeleteVehicleAsync(int VehicleId, Guid user);
        Task UpdateVehicleAsync(Vehicle vehicle);

    }
}


