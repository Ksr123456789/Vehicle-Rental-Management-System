using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;

namespace RentalmanagementSystem.Infrastructure.DbContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser , IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){  }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<RentalCompany> RentalCompanies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<ExtraService> ExtraServices { get; set; }
        public DbSet<BookingExtraService> BookingExtraServices { get; set; }
        



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            /*builder.Entity<RentalCompany>().HasData(

                new RentalCompany { Id = 1, CompanyName = "company1", CompanyType = CompanyType.Budget, OwnerName = "sam", PhoneNumber = "9876543210", HeadquartersLocation = "New Delhi", LicenseNumber = "NDWE56UI", Status = CompanyStatus.Active, IsDeleted = false },
                new RentalCompany { Id = 2, CompanyName = "company2", CompanyType = CompanyType.Luxury, OwnerName = "jhon", PhoneNumber = "9878543210", HeadquartersLocation = "Noida", LicenseNumber = "NO456FS", Status = CompanyStatus.Active, IsDeleted = false },
                new RentalCompany { Id = 3, CompanyName = "company3", CompanyType = CompanyType.Premium, OwnerName = "aman", PhoneNumber = "7645432160", HeadquartersLocation = "Pune", LicenseNumber = "PU78VBF", Status = CompanyStatus.Active, IsDeleted = false },
                new RentalCompany { Id = 4, CompanyName = "company4", CompanyType = CompanyType.Economy, OwnerName = "akash", PhoneNumber = "8876543210", HeadquartersLocation = "Gurugram", LicenseNumber = "GU787XD", Status = CompanyStatus.Active, IsDeleted = false },
                new RentalCompany { Id = 5, CompanyName = "company5", CompanyType = CompanyType.Premium, OwnerName = "ashu", PhoneNumber = "6876543210", HeadquartersLocation = "Mohali", LicenseNumber = "MO43XD", Status = CompanyStatus.Active, IsDeleted = false },
                new RentalCompany { Id = 6, CompanyName = "company6", CompanyType = CompanyType.Budget, OwnerName = "ashish", PhoneNumber = "9996543210", HeadquartersLocation = "Gurugram", LicenseNumber = "GU547XD", Status = CompanyStatus.InActive, IsDeleted = false }

            );

            builder.Entity<Vehicle>().HasData(

                new Vehicle
                {
                    Id = 1,
                    MakeAndModel = "Volkswagen Passat",
                    LicensePlate = "UK15EW23",
                    RegistrationNumber = "AZ324DS",
                    YearofManufacture = 2011,
                    SeatingCapacity = 4,
                    DailyRentalRate = 1200,
                    VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                    Mileage = 34,
                    IsDeleted = false,
                    RentalCompanyId = 1,
                    VehicleCategoryId = 1
                },
                 new Vehicle
                 {
                     Id = 2,
                     MakeAndModel = "Honda Civic",
                     LicensePlate = "UK07SDF",
                     RegistrationNumber = "GHRTXD45S",
                     YearofManufacture = 2001,
                     SeatingCapacity = 4,
                     DailyRentalRate = 1500,
                     VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                     Mileage = 44,
                     IsDeleted = false,
                     RentalCompanyId = 2,
                     VehicleCategoryId = 2
                 },
                  new Vehicle
                  {
                      Id = 3,
                      MakeAndModel = "Toyota Camry",
                      LicensePlate = "CH0145",
                      RegistrationNumber = "JK456XCD",
                      YearofManufacture = 2002,
                      SeatingCapacity = 6,
                      DailyRentalRate = 500,
                      VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                      Mileage = 36,
                      IsDeleted = false,
                      RentalCompanyId = 3,
                      VehicleCategoryId = 3
                  },
                   new Vehicle
                   {
                       Id = 4,
                       MakeAndModel = "Toyota Camry",
                       LicensePlate = "CH02342F",
                       RegistrationNumber = "LUH45C",
                       YearofManufacture = 1999,
                       SeatingCapacity = 4,
                       DailyRentalRate = 1000,
                       VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                       Mileage = 23,
                       IsDeleted = false,
                       RentalCompanyId = 4,
                       VehicleCategoryId = 4
                   }

                );


            builder.Entity<VehicleCategory>().HasData(

                new VehicleCategory { Id = 1, CategoryName = "Economy" },
                new VehicleCategory { Id = 2, CategoryName = "Sedan" },
                new VehicleCategory { Id = 3, CategoryName = "SUV" },
                new VehicleCategory { Id = 4, CategoryName = "Luxury" },
                new VehicleCategory { Id = 5, CategoryName = "Van/Bus" }

            );

            builder.Entity<ExtraService>().HasData(
                new ExtraService { Id = 1, Name = "GPS", Description = "Navigation System", Price = 500 },
                new ExtraService { Id = 2, Name = "Child Seat", Description = "Child safety seat", Price = 500 },
                new ExtraService { Id = 3, Name = "Etc", Description = "Other Services", Price = 500 }
            );*/

        }
     



    }
}


