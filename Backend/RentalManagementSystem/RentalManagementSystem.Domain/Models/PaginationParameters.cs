using System;
using System.Collections.Generic;
using System.Text;

namespace RentalManagementSystem.Domain.Models
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; }

        public string? SortDirection { get; set; }

        public string? SearchBy { get; set; }
    }
}
