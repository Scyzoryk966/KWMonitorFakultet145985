using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoronaWirusMonitor3.Models;
using KoronaWirusMonitor3.Repository;
using KWMonitor.DTO;
using KWMonitor.Services;
using KWMonitor.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KWMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly KWMContext _context;
        private readonly ICityServices _cityServices;

        public CityController(KWMContext context, ICityServices cityServices)
        {
            _context = context;
            _cityServices = cityServices;
        }

        // GET: api/City
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCitys()
        {
            return await _cityServices.GetAll();
        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public ActionResult<City> GetCity(int id)
        {
            var validator = new IdValidator();
            var result = validator.Validate(id);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var city = _cityServices.GetById(id);
            if (city == null) return NotFound();
            return city;
        }

        // PUT: api/City/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id) return BadRequest();
            var validator = new CityValidator();
            var resultValid = validator.Validate(city);
            if (!resultValid.IsValid)
            {
                return BadRequest(resultValid.Errors);
            }
            var result = await _cityServices.Update(city);
            if(result)
            {
                return Ok();
            }
            return NoContent();
        }

        // POST: api/City
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(CityDto city)
        {
            var newCity = _context.Cities.Add(
                new City
                {
                    Name = city.Name,
                    DistrictId = city.DistrictId
                });
            await _context.SaveChangesAsync();
            var response = _context.Cities.Include(r => r.District).FirstOrDefault(r => r.Id == newCity.Entity.Id);
            return CreatedAtAction("GetCity", new {id = response.Id}, response);
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null) return NotFound();
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return city;
        }

        
    }
}
