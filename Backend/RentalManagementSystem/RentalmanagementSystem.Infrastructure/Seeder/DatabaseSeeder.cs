using Microsoft.EntityFrameworkCore;
using RentalManagementSystem.Domain.Entities;
using RentalManagementSystem.Domain.Enums;
using RentalmanagementSystem.Infrastructure.DbContext;

namespace RentalmanagementSystem.Infrastructure.Seeder
{
    public static class DatabaseSeeder
    {


        public static void Seed(AppDbContext context)
        {
            if (!context.VehicleCategories.Any())
            {
                context.VehicleCategories.AddRange(
                    new VehicleCategory { CategoryName = "Economy" },
                    new VehicleCategory { CategoryName = "Sedan" },
                    new VehicleCategory { CategoryName = "SUV" },
                    new VehicleCategory { CategoryName = "Luxury" },
                    new VehicleCategory { CategoryName = "Van/Bus" }
                );

                context.SaveChanges();
            }

            if (!context.RentalCompanies.Any())
            {
                context.RentalCompanies.AddRange(
                    new RentalCompany
                    {
                        CompanyName = "company1",
                        CompanyType = CompanyType.Budget,
                        OwnerName = "sam",
                        PhoneNumber = "9876543210",
                        HeadquartersLocation = "New Delhi",
                        LicenseNumber = "NDWE56UI",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company2",
                        CompanyType = CompanyType.Luxury,
                        OwnerName = "jhon",
                        PhoneNumber = "9878543210",
                        HeadquartersLocation = "Noida",
                        LicenseNumber = "NO456FS",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company3",
                        CompanyType = CompanyType.Premium,
                        OwnerName = "aman",
                        PhoneNumber = "7645432160",
                        HeadquartersLocation = "Pune",
                        LicenseNumber = "PU78VBF",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company4",
                        CompanyType = CompanyType.Economy,
                        OwnerName = "akash",
                        PhoneNumber = "8876543210",
                        HeadquartersLocation = "Gurugram",
                        LicenseNumber = "GU787XD",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company5",
                        CompanyType = CompanyType.Premium,
                        OwnerName = "ashu",
                        PhoneNumber = "6876543210",
                        HeadquartersLocation = "Mohali",
                        LicenseNumber = "MO43XD",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company6",
                        CompanyType = CompanyType.Budget,
                        OwnerName = "ashish",
                        PhoneNumber = "9996543210",
                        HeadquartersLocation = "Gurugram",
                        LicenseNumber = "GU547XD",
                        Status = CompanyStatus.InActive,
                        IsDeleted = false
                    }
                );

                context.SaveChanges();
            }

            if (!context.Vehicles.Any())
            {
                VehicleCategory economy = context.VehicleCategories
                    .First(x => x.CategoryName == "Economy");

                VehicleCategory sedan = context.VehicleCategories
                    .First(x => x.CategoryName == "Sedan");

                VehicleCategory suv = context.VehicleCategories
                    .First(x => x.CategoryName == "SUV");

                VehicleCategory luxury = context.VehicleCategories
                    .First(x => x.CategoryName == "Luxury");

                RentalCompany company1 = context.RentalCompanies
                    .First(x => x.CompanyName == "company1");

                RentalCompany company2 = context.RentalCompanies
                    .First(x => x.CompanyName == "company2");

                RentalCompany company3 = context.RentalCompanies
                    .First(x => x.CompanyName == "company3");

                RentalCompany company4 = context.RentalCompanies
                    .First(x => x.CompanyName == "company4");

                context.Vehicles.AddRange(
                    new Vehicle
                    {
                        MakeAndModel = "Volkswagen Passat",
                        LicensePlate = "UK15EW23",
                        RegistrationNumber = "AZ324DS",
                        YearofManufacture = 2011,
                        SeatingCapacity = 4,
                        DailyRentalRate = 1200,
                        VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                        Mileage = 34,
                        IsDeleted = false,
                        RentalCompanyId = company1.Id,
                        VehicleCategoryId = economy.Id
                    },
                    new Vehicle
                    {
                        MakeAndModel = "Honda Civic",
                        LicensePlate = "UK07SDF",
                        RegistrationNumber = "GHRTXD45S",
                        YearofManufacture = 2001,
                        SeatingCapacity = 4,
                        DailyRentalRate = 1500,
                        VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                        Mileage = 44,
                        IsDeleted = false,
                        RentalCompanyId = company2.Id,
                        VehicleCategoryId = sedan.Id
                    },
                    new Vehicle
                    {
                        MakeAndModel = "Toyota Camry",
                        LicensePlate = "CH0145",
                        RegistrationNumber = "JK456XCD",
                        YearofManufacture = 2002,
                        SeatingCapacity = 6,
                        DailyRentalRate = 500,
                        VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                        Mileage = 36,
                        IsDeleted = false,
                        RentalCompanyId = company3.Id,
                        VehicleCategoryId = suv.Id
                    },
                    new Vehicle
                    {
                        MakeAndModel = "Toyota Camry",
                        LicensePlate = "CH02342F",
                        RegistrationNumber = "LUH45C",
                        YearofManufacture = 1999,
                        SeatingCapacity = 4,
                        DailyRentalRate = 1000,
                        VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                        Mileage = 23,
                        IsDeleted = false,
                        RentalCompanyId = company4.Id,
                        VehicleCategoryId = luxury.Id
                    }
                );

                context.SaveChanges();
            }

            if (!context.ExtraServices.Any())
            {
                context.ExtraServices.AddRange(
                    new ExtraService
                    {
                        Name = "GPS",
                        Description = "Navigation System",
                        Price = 500
                    },
                    new ExtraService
                    {
                        Name = "Child Seat",
                        Description = "Child safety seat",
                        Price = 500
                    },
                    new ExtraService
                    {
                        Name = "Etc",
                        Description = "Other Services",
                        Price = 500
                    }
                );

                context.SaveChanges();
            }
        }


        public static async Task SeedAsync(
            AppDbContext context
            )
        {
            if (!await context.VehicleCategories.AnyAsync())
            {
                await context.VehicleCategories.AddRangeAsync(
                    new VehicleCategory { CategoryName = "Economy" },
                    new VehicleCategory { CategoryName = "Sedan" },
                    new VehicleCategory { CategoryName = "SUV" },
                    new VehicleCategory { CategoryName = "Luxury" },
                    new VehicleCategory { CategoryName = "Van/Bus" }
                    
                );

                await context.SaveChangesAsync();
            }

            if (!await context.RentalCompanies.AnyAsync())
            {
                await context.RentalCompanies.AddRangeAsync(
                    new RentalCompany
                    {
                        CompanyName = "company1",
                        CompanyType = CompanyType.Budget,
                        OwnerName = "sam",
                        PhoneNumber = "9876543210",
                        HeadquartersLocation = "New Delhi",
                        LicenseNumber = "NDWE56UI",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company2",
                        CompanyType = CompanyType.Luxury,
                        OwnerName = "jhon",
                        PhoneNumber = "9878543210",
                        HeadquartersLocation = "Noida",
                        LicenseNumber = "NO456FS",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company3",
                        CompanyType = CompanyType.Premium,
                        OwnerName = "aman",
                        PhoneNumber = "7645432160",
                        HeadquartersLocation = "Pune",
                        LicenseNumber = "PU78VBF",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company4",
                        CompanyType = CompanyType.Economy,
                        OwnerName = "akash",
                        PhoneNumber = "8876543210",
                        HeadquartersLocation = "Gurugram",
                        LicenseNumber = "GU787XD",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company5",
                        CompanyType = CompanyType.Premium,
                        OwnerName = "ashu",
                        PhoneNumber = "6876543210",
                        HeadquartersLocation = "Mohali",
                        LicenseNumber = "MO43XD",
                        Status = CompanyStatus.Active,
                        IsDeleted = false
                    },
                    new RentalCompany
                    {
                        CompanyName = "company6",
                        CompanyType = CompanyType.Budget,
                        OwnerName = "ashish",
                        PhoneNumber = "9996543210",
                        HeadquartersLocation = "Gurugram",
                        LicenseNumber = "GU547XD",
                        Status = CompanyStatus.InActive,
                        IsDeleted = false
                    }
                );

                await context.SaveChangesAsync();
            }

            if (!await context.Vehicles.AnyAsync())
            {
                VehicleCategory economy = await context.VehicleCategories
                    .FirstAsync(x => x.CategoryName == "Economy");

                VehicleCategory sedan = await context.VehicleCategories
                    .FirstAsync(x => x.CategoryName == "Sedan");

                VehicleCategory suv = await context.VehicleCategories
                    .FirstAsync(x => x.CategoryName == "SUV");

                VehicleCategory luxury = await context.VehicleCategories
                    .FirstAsync(x => x.CategoryName == "Luxury");

                RentalCompany company1 = await context.RentalCompanies
                    .FirstAsync(x => x.CompanyName == "company1");

                RentalCompany company2 = await context.RentalCompanies
                    .FirstAsync(x => x.CompanyName == "company2");

                RentalCompany company3 = await context.RentalCompanies
                    .FirstAsync(x => x.CompanyName == "company3");

                RentalCompany company4 = await context.RentalCompanies
                    .FirstAsync(x => x.CompanyName == "company4");

                await context.Vehicles.AddRangeAsync(
                    new Vehicle
                    {
                        MakeAndModel = "Volkswagen Passat",
                        LicensePlate = "UK15EW23",
                        RegistrationNumber = "AZ324DS",
                        YearofManufacture = 2011,
                        SeatingCapacity = 4,
                        DailyRentalRate = 1200,
                        VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                        Mileage = 34,
                        IsDeleted = false,
                        RentalCompanyId = company1.Id,
                        VehicleCategoryId = economy.Id
                    },
                    new Vehicle
                    {
                        MakeAndModel = "Honda Civic",
                        LicensePlate = "UK07SDF",
                        RegistrationNumber = "GHRTXD45S",
                        YearofManufacture = 2001,
                        SeatingCapacity = 4,
                        DailyRentalRate = 1500,
                        VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                        Mileage = 44,
                        IsDeleted = false,
                        RentalCompanyId = company2.Id,
                        VehicleCategoryId = sedan.Id
                    },
                    new Vehicle
                    {
                        MakeAndModel = "Toyota Camry",
                        LicensePlate = "CH0145",
                        RegistrationNumber = "JK456XCD",
                        YearofManufacture = 2002,
                        SeatingCapacity = 6,
                        DailyRentalRate = 500,
                        VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                        Mileage = 36,
                        IsDeleted = false,
                        RentalCompanyId = company3.Id,
                        VehicleCategoryId = suv.Id
                    },
                    new Vehicle
                    {
                        MakeAndModel = "Toyota Camry",
                        LicensePlate = "CH02342F",
                        RegistrationNumber = "LUH45C",
                        YearofManufacture = 1999,
                        SeatingCapacity = 4,
                        DailyRentalRate = 1000,
                        VehicleAvailabilityStatus = VehicleAvailabilityStatus.Available,
                        Mileage = 23,
                        IsDeleted = false,
                        RentalCompanyId = company4.Id,
                        VehicleCategoryId = luxury.Id
                    }
                );

                await context.SaveChangesAsync();
            }

            if (!await context.ExtraServices.AnyAsync())
            {
                await context.ExtraServices.AddRangeAsync(
                    new ExtraService
                    {
                        Name = "GPS",
                        Description = "Navigation System",
                        Price = 500
                    },
                    new ExtraService
                    {
                        Name = "Child Seat",
                        Description = "Child safety seat",
                        Price = 500
                    },
                    new ExtraService
                    {
                        Name = "Etc",
                        Description = "Other Services",
                        Price = 500
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
