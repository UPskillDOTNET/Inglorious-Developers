using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentralAPI.DTO;
using CentralAPI.Models;

namespace CentralAPI.Repositories.IRepository
{
    public interface ISubletRepository : IBaseRepository<Sublet>
    {
        Task<IEnumerable<Sublet>> GetSublets();
        Task<IEnumerable<Sublet>> GetSubletsByMainUserId(string id);
        Task<IEnumerable<Sublet>> GetSubletsBySubUserId(string id);
        Task<IEnumerable<Sublet>> GetActiveSublets();
        Task<IEnumerable<Sublet>> GetSubletsbyDate(DateTime startDate, DateTime endDate);
        Task<Sublet> GetSublet(string id);
        Task<Sublet> CreateSublet(Sublet sublet);
        Task<Sublet> CancelSublet(Sublet sublet);
        Task<bool> subletAny(Sublet sublet);
    }
}
