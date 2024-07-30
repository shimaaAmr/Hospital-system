using Hospital_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System.DbContexts
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<MedicalSpecialty> MedicalSpecialties { get; set; }
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{

		}
	}
}
