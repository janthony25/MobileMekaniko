using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MobileMekaniko.Models
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }

        [DisplayName("Car Model")]
        public required string ModelName { get; set; }

        // M-to-M Car-Model
        public List<CarModel> CarModel { get; set; }    
    }
}
