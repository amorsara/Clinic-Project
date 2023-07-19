using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Waitings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Waitings
{
    public class WaitingsData : IWaitingsData
    {
        private readonly ClinicDBContext _context;

        public WaitingsData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateWaiting(Waiting waiting)
        {
            var isExsists = WaitingExists(waiting.Idwaiting);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(waiting);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<Waiting?> GetWaitingById(int id)
        {
            var waiting = await _context.Waitings.FindAsync(id);
            return waiting;
        }

        public async Task<List<Waiting>> GetAllWaitings()
        {
             return await _context.Waitings.ToListAsync();
        }

        public bool WaitingExists(int id)
        {
            var waiting = _context.Waitings.Where(w => w.Idwaiting == id).FirstOrDefault();
            if (waiting == null)
            {
                return false;
            }
            return true;
        }
    }
}
