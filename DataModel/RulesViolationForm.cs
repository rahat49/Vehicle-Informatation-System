
using System.ComponentModel.DataAnnotations;

namespace VIMS.DataModel
{
    public class RulesViolationForm
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string TPoliceName { get; set; }
        [Required]
        public string RulesType { get; set; }
        [Required]
        public string VehCategory { get; set; }
        [Required]
        public string Vehnum { get; set; }
        [Required]
        public string VehOwnerName { get; set; }
        [Required]
        public string Fees { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public  string Date { get; set; }

    }
}
