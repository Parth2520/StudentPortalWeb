using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Data;
using StudentPortalWeb.Models;
using StudentPortalWeb.Models.Entities;

namespace StudentPortalWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDBContext dBContext;

        public StudentsController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentsViewModel viewmodel)
        {

            var students = new Student
            {
                Name = viewmodel.Name,
                Email = viewmodel.Email,
                Phone = viewmodel.Phone,
                Subscribed = viewmodel.Subscribed,
            };
            await dBContext.Students.AddAsync(students);
            dBContext.SaveChanges();
            return RedirectToAction("List", "students");
        }

        [HttpGet]

        public async Task<IActionResult> list()
        {
            var students = await dBContext.Students.ToListAsync();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var student = await dBContext.Students.FindAsync(Id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dBContext.Students.FindAsync(viewModel.Id);

            if (student != null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "students");
        }



        //[HttpPost]

        //public async Task<IActionResult> Delete(Student viewModel)
        //{
        //    var student = await dBContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

        //    if (student != null)
        //    {
        //        dBContext.Students.Remove(viewModel);

        //        await dBContext.SaveChangesAsync();
        //    }
        //    return RedirectToAction("List", "students");
        //}


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await dBContext.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await dBContext.Students.FindAsync(id);
            dBContext.Students.Remove(student);
            await dBContext.SaveChangesAsync();
            return RedirectToAction("List", "students");
        }
    }

}

