using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zadanie_11.Models;

namespace Zadanie_11.Controllers
{
    [Route("api/medicament")]
    [ApiController]
    public class MedicamentController : ControllerBase
    {
        private readonly MedicDbContext _context;
        public MedicamentController(MedicDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMedicament()
        {
            return Ok(_context.Medicaments.ToList());
        }

    }
}