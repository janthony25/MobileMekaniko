using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MobileMekaniko.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [DisplayName("Customer Name")]
        public required string CustomerName { get; set; }

        [DisplayName("Email Address")]
        public string? CustomerEmail { get; set; }

        [DisplayName("Contact #")]
        public string? CustomerNumber { get; set; }

        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [DisplayName("Last Edited")]
        public DateTime? DateEdited { get; set; }

        // 1-to-M Customer-Car
        public ICollection<Car> Car { get; set; }   
    }
}
