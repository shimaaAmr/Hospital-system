using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_System.DbContexts;
using Hospital_System.Models;

namespace Hospital_System.Controllers
{
	public class MedicalSpecialtiesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public MedicalSpecialtiesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: MedicalSpecialties
		public async Task<IActionResult> Index()
		{
			return View(await _context.MedicalSpecialties.ToListAsync());
		}

		// GET: MedicalSpecialties/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var medicalSpecialty = await _context.MedicalSpecialties
				.FirstOrDefaultAsync(m => m.Id == id);
			if (medicalSpecialty == null)
			{
				return NotFound();
			}

			return View(medicalSpecialty);
		}

		// GET: MedicalSpecialties/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: MedicalSpecialties/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name")] MedicalSpecialty medicalSpecialty)
		{
			if (ModelState.IsValid)
			{
				_context.Add(medicalSpecialty);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(medicalSpecialty);
		}

		// GET: MedicalSpecialties/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var medicalSpecialty = await _context.MedicalSpecialties.FindAsync(id);
			if (medicalSpecialty == null)
			{
				return NotFound();
			}
			return View(medicalSpecialty);
		}

		// POST: MedicalSpecialties/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MedicalSpecialty medicalSpecialty)
		{
			if (id != medicalSpecialty.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(medicalSpecialty);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!MedicalSpecialtyExists(medicalSpecialty.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(medicalSpecialty);
		}

		// GET: MedicalSpecialties/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var medicalSpecialty = await _context.MedicalSpecialties
				.FirstOrDefaultAsync(m => m.Id == id);
			if (medicalSpecialty == null)
			{
				return NotFound();
			}

			return View(medicalSpecialty);
		}

		// POST: MedicalSpecialties/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var medicalSpecialty = await _context.MedicalSpecialties.FindAsync(id);
			if (medicalSpecialty != null)
			{
				_context.MedicalSpecialties.Remove(medicalSpecialty);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool MedicalSpecialtyExists(int id)
		{
			return _context.MedicalSpecialties.Any(e => e.Id == id);
		}
	}
}
