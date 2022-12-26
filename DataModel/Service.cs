using System;
using System.ComponentModel.DataAnnotations;

namespace VIMS.DataModel
{
	public class Service
	{
		[Key]
		public int  sid { get; set; }
		[Required]
		public string CompanyName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Contact { get; set; }

    }
}

