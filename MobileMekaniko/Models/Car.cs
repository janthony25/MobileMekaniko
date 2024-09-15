using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MobileMekaniko.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [DisplayName("Rego #")]
        public required string CarRego { get; set; }

        [DisplayName("Year")]
        public int? CarYear { get; set; }

        [DisplayName("Payment Status")]
        public bool? CarPaymentStatus { get; set; }

        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [DisplayName("Last Edited")]
        public DateTime? DateEdited { get; set; }

        // FK To Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // M-to-M Car-Model
        public List<CarModel> CarModel { get; set; }

        // M-to-M Car-Make
        public List<CarMake> CarMake { get; set; }
    }
}
