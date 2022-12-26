using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VIMS.DataModel
{
    public class TrafficPolice
    { 
            [Key]
            public int Tid { get; set; }
            [Required]
            public string Name{ get; set; }
            [Required]
            public string Phone { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public int Policeid { get; set; }
            [Required]
            public string Designation { get; set; }
            [Required]
            public string Range { get; set; }
            [Required]
            public string Password { get; set; }


        }

    }

