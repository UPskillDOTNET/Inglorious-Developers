using CentralAPI.Data;
using CentralAPI.Models;
using CentralAPI.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentralAPI.Repositories.Repository
{
    public class SubletRepository : BaseRepository<Sublet>, ISubletRepository
    {
        public SubletRepository(CentralAPIContext CentralAPIContext) : base(CentralAPIContext)
        {
        }

        public async Task<IEnumerable<Sublet>> GetSublets()
        {
            return await GetAll().ToListAsync();
        }
        public async Task<IEnumerable<Sublet>> GetSubletsByMainUserId(string id)
        {
            return await GetAll().Where(x => x.mainUserID == id).ToListAsync();
        }
        public async Task<IEnumerable<Sublet>> GetSubletsBySubUserId(string id)
        {
            return await GetAll().Where(x => x.subUserID == id).ToListAsync();
        }
        public async Task<IEnumerable<Sublet>> GetActiveSublets()
        {
            return await GetAll().Where(x => x.startTime <= DateTime.Now && x.endTime > DateTime.Now).ToListAsync();
        }
        public async Task<IEnumerable<Sublet>> GetSubletsbyDate(DateTime startTime, DateTime endTime)
        {
            return await GetAll().Where(r => (r.startTime >= startTime && r.endTime <= endTime) || (r.startTime <= endTime && r.endTime >= startTime)).ToListAsync();
        }

        public async Task<bool> subletAny(Sublet sublet)
        {
            return await GetAll().Where(r => (r.startTime >= sublet.startTime && r.endTime <= sublet.endTime) || (r.startTime <= sublet.endTime && r.endTime >= sublet.startTime)&& r.reservationID == sublet.reservationID).AnyAsync();
        }

        public async Task<Sublet> CreateSublet(Sublet sublet)
        {
            sublet = await AddAsync(sublet);
            return sublet;
        }
        public async Task<Sublet> CancelSublet(Sublet sublet)
        {
            sublet = await UpdateAsync(sublet);
            return sublet;
        }

        public async Task<Sublet> GetSublet(string id)
        {
            return await Find(id);
        }
    }
}
