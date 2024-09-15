using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MobileMekaniko.Models
{
    public class Make
    {
        [Key]
        public int MakeId { get; set; }

        [DisplayName("Car Make")]
        public required string MakeName { get; set; }

        // M-to-M Car-Make
        public List<CarMake> CarMake { get; set; } 
    }
}
