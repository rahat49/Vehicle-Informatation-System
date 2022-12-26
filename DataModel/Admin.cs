using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VIMS.DataModel
{
    public class Admin
    {
        [Key]
        public int id { get; set; }
   
        public string Email { get; set; }
    
        public string Password { get; set; }
    }
   
}
