using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace MobileMekaniko.Models.Dto
{
    public class CustomerListSummaryDto
    {
        public int CustomerId { get; set; }
        public required string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerNumber { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateEdited { get; set; }
    }
}
