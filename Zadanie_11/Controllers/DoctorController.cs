using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zadanie_11.Models;

namespace Zadanie_11.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly MedicDbContext _context;

        public DoctorController(MedicDbContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllDoctors() {

            return Ok(_context.Doctors.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            return Ok(_context.Doctors.Where(e => e.Id == id).Single());
        }

        [HttpPost("{id}")]
        public IActionResult UpdateDoctor(Doctor doctor, int id)
        {
            var d = new Doctor
            {
                Id = id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
            _context.Attach(d);
            _context.Entry(d).Property("FirstName").IsModified = true;
            _context.Entry(d).Property("LasttName").IsModified = true;
            _context.Entry(d).Property("Email").IsModified = true;
            _context.SaveChanges();

            return Ok(d);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {

            var d = _context.Doctors.Where(e => e.Id == id).First();
            _context.Doctors.Remove(d);
            _context.SaveChanges();
            return Ok("Doctor "+ id +" usunięty");

        }
    }
}