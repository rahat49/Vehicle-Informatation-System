using System;
using System.ComponentModel.DataAnnotations;

namespace VIMS.DataModel
{
	public class Notice
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Heading { get; set; }
		[Required]
		public string Detail { get; set; }
		[Required]
		public string Date { get; set; }
	}
}

