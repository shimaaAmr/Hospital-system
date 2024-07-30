using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.Models
{
	/*
	 -General Surger
	-Forensic Patholog
	-Family Medicine
	-Family Medicine
	-Emergency Medicine
	-Dermatology
	-Dermatology
	 
	 
	 */
	public class Doctor
	{
		public int Id { get; set; }

		[DisplayName("Full Name")]
		[Required(ErrorMessage = "You have to provide Full Name")]
		[MinLength(4, ErrorMessage = "Not allowed less than 4 char")]
		[MaxLength(50, ErrorMessage = "Not allowed more than 30 char")]
		public string FullName { get; set; }
		[DisplayName("National Id")]
		[Required(ErrorMessage = "You have to provide National ID")]
		[MinLength(14, ErrorMessage = "Not allowed less than 14 number")]
		[MaxLength(14, ErrorMessage = "Not allowed more than 14 number")]

		public string NationalId { get; set; }
		[ValidateNever]
		[DisplayName("Occupation")]
		[Required(ErrorMessage = "You have to provide Medical Spcialty")]
		public string Position { get; set; }


		[DisplayName("Hiring Date")]
		[Required(ErrorMessage = "You have to provide Hiring Date")]
		[DataType(DataType.Date)]
		public DateTime HiringDate { get; set; }

		[DisplayName("Attendence Time")]
		[Required(ErrorMessage = "You have to provide Attendence Time ")]
		[DataType(DataType.Time)]
		public DateTime AttendenceTime { get; set; }

		[DisplayName("Phone Number")]
		[Required(ErrorMessage = "You have to provide phone Number")]
		[RegularExpression("^01\\d{9}$", ErrorMessage = "Invalid Phone Number")]
		public string PhoneNumber { get; set; }

		[DisplayName("Email Address")]
		[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Invalid e-mail")]
		public string EmailAddress { get; set; }

		[NotMapped]
		[DisplayName("Confirm Email Address")]
		[Compare("EmailAddress", ErrorMessage = "Not match Email")]
		public string ConfirmEmail { get; set; }

		[DisplayName("Password")]
		[Required(ErrorMessage = "You have to provide Password")]
		[DataType(DataType.Password)]
		[MinLength(4, ErrorMessage = "Secret Code can not be less than 4 char")]
		public string Password { get; set; }

		[NotMapped]
		[DisplayName("Confirm Password")]
		[Compare("Password", ErrorMessage = "Not match password")]
		public string ConfirmPassword { get; set; }
		[DisplayName("Is Active")]
		public bool IsActive { get; set; }
		[ValidateNever]
		public string? Bio { get; set; }


		//foreign key
		[DisplayName("Medical Spcialty")]
		[Range(1, double.MaxValue, ErrorMessage = "Choose a valid Spacialty")]
		public int MedicalSpecialtyId { get; set; }

		//Navigation virtual
		[ValidateNever]
		public MedicalSpecialty MedicalSpecialty { get; set; }
		[ValidateNever]
		public string ImagePath { get; set; }

		[NotMapped]
		[DisplayName("Image")]
		[ValidateNever]
		public IFormFile ImageFIle { get; set; }
	}
}
