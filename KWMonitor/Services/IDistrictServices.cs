using KoronaWirusMonitor3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KWMonitor.Services
{
    public interface IDistrictServices
    {
        Task<List<District>> GetAll();
        District GetById(int id);
        Task<bool> Update(District district);
    }
}