using RentalManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Domain.Models
{
    public class RentalCompanyFilterParameters : PaginationParameters
    {
        public CompanyType? CompanyType { get; set; }

        public CompanyStatus? CompanyStatus { get; set; }
    }
}
