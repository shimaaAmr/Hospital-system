using Hospital_System.DbContexts;
using Hospital_System.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace Hospital_System.Controllers
{
	public class DoctorsController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public DoctorsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}
		//	Doctor doctor=new Doctor();
		public IActionResult GetIndexView(string? search)
		{
			ViewBag.Search = search;
			IQueryable<Doctor> querableDoc = _context.Doctors.AsQueryable();
			if (string.IsNullOrEmpty(search) == false)
			{
				querableDoc = querableDoc.Where(doc => doc.FullName.Contains(search) || doc.Position.Contains(search));
			}
			return View("Index", querableDoc.ToList());
		}
		public IActionResult GetDetailsView(int id)
		{
			Doctor doctor = _context.Doctors.FirstOrDefault(doc => doc.Id == id);
			if (doctor != null)
			{
				return View("Details", doctor);
			}
			else
			{
				return NotFound();
			}
		}
		[HttpGet]
		public IActionResult GetCreateView()
		{
			ViewBag.AllSpecialties = _context.MedicalSpecialties.ToList();
			return View("Create");
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult AddNew(Doctor doctor)
		{
			if (ModelState.IsValid == true)
			{
				if (doctor.ImageFIle == null)
				{
					doctor.ImagePath = "\\images\\No_Image.png";
				}
				else
				{
					//GUID => Globally Unique Identifier   9xw2m-o3i2n-sdf27-yt92m-ab99w.jpg

					Guid imageGuid = Guid.NewGuid();
					string imageExtention = Path.GetExtension(doctor.ImageFIle.FileName);
					doctor.ImagePath = "\\images\\" + imageGuid + imageExtention;
					string imageUpLoadPath = _webHostEnvironment.WebRootPath + doctor.ImagePath;
					FileStream imageStream = new FileStream(imageUpLoadPath, FileMode.Create);
					doctor.ImageFIle.CopyTo(imageStream);
					imageStream.Dispose();

				}
				_context.Doctors.Add(doctor);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				ViewBag.AllSpecialties = _context.MedicalSpecialties.ToList();
				return View("Create");
			}

		}


		public IActionResult GetDeleteView(int id)
		{
			Doctor doctor = _context.Doctors.FirstOrDefault(doc => doc.Id == id);
			if (doctor != null)
			{
				return View("Delete", doctor);
			}
			else
			{
				return NotFound();
			}
		}
		[ValidateAntiForgeryToken] //prevent data from Hakar
		[HttpPost]
		public IActionResult DeleteCurrent(int id)
		{
			Doctor doctor = _context.Doctors.Find(id);
			if (doctor != null)
			{
				if (doctor.ImagePath != "\\images\\No_Image.png")
				{
					System.IO.File.Delete(_webHostEnvironment.WebRootPath + doctor.ImagePath);
				}
				_context.Doctors.Remove(doctor);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				return NotFound();
			}
		}

		public IActionResult GetEditView(int id)
		{
			Doctor doctor = _context.Doctors.FirstOrDefault(doc => doc.Id == id);
			if (doctor != null)
			{
				ViewBag.AllSpecialties = _context.MedicalSpecialties.ToList();
				return View("Edit", doctor);
			}
			else
			{
				return NotFound();
			}
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult EditCurrent(Doctor doctor)
		{
			if (ModelState.IsValid == true)
			{
				if (doctor.ImageFIle != null)
				{
					if (doctor.ImagePath != "\\images\\No_Image.png")
					{
						System.IO.File.Delete(_webHostEnvironment.WebRootPath + doctor.ImagePath);
					}
					//GUID => Globally Unique Identifier   9xw2m-o3i2n-sdf27-yt92m-ab99w.jpg

					Guid imageGuid = Guid.NewGuid();
					string imageExtention = Path.GetExtension(doctor.ImageFIle.FileName);
					doctor.ImagePath = "\\images\\" + imageGuid + imageExtention;
					string imageUpLoadPath = _webHostEnvironment.WebRootPath + doctor.ImagePath;
					FileStream imageStream = new FileStream(imageUpLoadPath, FileMode.Create);
					doctor.ImageFIle.CopyTo(imageStream);
					imageStream.Dispose();

				}
				else// doctor.ImageFIle == null
				{

					doctor.ImagePath = "\\images\\No_Image.png";
				}
				_context.Doctors.Update(doctor);
				_context.SaveChanges();
				return RedirectToAction("GetIndexView");
			}
			else
			{
				ViewBag.AllSpecialties = _context.MedicalSpecialties.ToList();
				return View("Edit");
			}

		}
	}
}
