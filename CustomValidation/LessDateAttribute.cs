using System;
using System.ComponentModel.DataAnnotations;

namespace VIMS.CustomValidation
{
	public class LessDateAttribute:ValidationAttribute
	{
		
		public LessDateAttribute() : base("{0} Date should less than current date")
		{

		}

        public override bool IsValid(object? value)
        {
			DateTime propValue = Convert.ToDateTime(value);
			if (propValue <= DateTime.Now)
				return true;
			else
				return false;

            
        }
    }
}

