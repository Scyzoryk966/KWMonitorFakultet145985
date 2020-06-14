using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoronaWirusMonitor3.Models;
using KoronaWirusMonitor3.Repository;
using Microsoft.EntityFrameworkCore;

namespace KWMonitor.Services
{
    public class DistrictServices : IDistrictServices
    {
        private readonly KWMContext _context;

        public DistrictServices(KWMContext context)
        {
            _context = context;
        }
        public async Task<List<District>> GetAll()
        {
            return await _context.Districts.Include(c => c.Region).ToListAsync();
        }

        public District GetById(int id)
        {
            return _context.Districts.Include(r => r.Region).FirstOrDefault(r => r.Id == id);
        }

        public async Task<bool> Update(District district)
        {
            _context.Entry(district).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictExists(district.Id))
                    return false;
                throw;
            }
            return true;
        }

        private bool DistrictExists(int id)
        {
            return _context.Districts.Any(e => e.Id == id);
        }
    }
}
