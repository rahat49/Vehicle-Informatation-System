using System.ComponentModel.DataAnnotations;
using VIMS.CustomValidation;

namespace VIMS.DataModel
{
    public class Renewapply
    {
        [Key]
        public int id { get; set; }
        public int Uid { get; set; }
        [Required]
        public string Vehnum   { get; set; }
        [Required]
        public string Vcategory { get; set; }
        [Required]
        public string Brandname { get; set; }
        [Required]
        public string MadeYear{ get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string ChassisNo { get; set; }
        [Required]
        public string EngineNo { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public string OwnerAddress { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [LessDate]
        public Nullable<System.DateTime> Registrationdate { get; set; }


        [Required]
        public int TaxTNumber { get; set; }
        [Required]

        
        public string TTExpirydate { get; set; }

        [Required]
        public string Status { get; set; }
        [Required]
        public string CardOwnerName { get; set; }
        [Required]
        public int CardNumber { get; set; }
        [Required]
        public string ExpiryDate { get; set; }
        [Required]
        public int CVV { get; set; }

        public int Fees { get; set; }
    }
}
