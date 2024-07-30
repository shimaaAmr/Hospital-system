using Hospital_System.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hospital_System.Models
{
	public class MedicalSpecialty
	{
		public int Id { get; set; }
		public string Name { get; set; }



		//Navigation property
		[ValidateNever]
		public List<Doctor> Doctors
		{
			get; set;
		}
	}
}
