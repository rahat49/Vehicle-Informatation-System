using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace VIMS.DataModel
{
   
    public class User
    {
        [Key]
        public int Uid { get; set; }
        [Required]
        public string Name{ get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string NID { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Vehnum { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
