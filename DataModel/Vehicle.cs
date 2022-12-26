using System;
using System.ComponentModel.DataAnnotations;

namespace VIMS.DataModel
{
	public class Vehicle
	{
		[Key]
		public int vid { get; set; }
		[Required]
		public string Vehnum { get; set; }
	}
}

