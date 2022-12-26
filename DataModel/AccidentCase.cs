using System;
using System.ComponentModel.DataAnnotations;

namespace VIMS.DataModel
{
	public class AccidentCase
	{
        [Key]
		public int id { get; set; }
        [Required]
		public string AccidentType { get; set; }
        [Required]
        public string AccidentVCate { get; set; }
        [Required]
        public double Damage { get; set; }
        [Required]
        public string Vehnum { get; set; }
        [Required]
        public string ADate { get; set; }
        [Required]
        public string Alocation { get; set; }
        [Required]
        public int Tpeople { get; set; }
        [Required]
        public int Injuredp { get; set; }
        [Required]
        public int Deathp { get; set; }
        
    }
}

