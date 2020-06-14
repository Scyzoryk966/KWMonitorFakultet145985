using KoronaWirusMonitor3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KWMonitor.Services
{
    public interface ICityServices
    {
        Task<List<City>> GetAll();
        City GetById(int id);
        Task<bool> Update(City city);
    }
}