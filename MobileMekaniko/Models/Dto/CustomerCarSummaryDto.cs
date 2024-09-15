using System.ComponentModel;

namespace MobileMekaniko.Models.Dto
{
    public class CustomerCarSummaryDto
    {
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Email Address")]
        public string? CustomerEmail { get; set; }

        [DisplayName("Contact #")]
        public string? CustomerNumber { get; set; }

        [DisplayName("ID")]
        public int CarId { get; set; }

        [DisplayName("Rego #")]
        public string CarRego { get; set; }

        [DisplayName("Car Make")]
        public string MakeName { get; set; }

        [DisplayName("Car Model")]
        public string ModelName { get; set; }

        [DisplayName("Year")]
        public int? CarYear { get; set; }
    }
}
