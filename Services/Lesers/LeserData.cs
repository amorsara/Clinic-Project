using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lesers
{
    public class LeserData : ILeserData
    {
        private readonly ClinicDBContext _context;

        public LeserData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLeserarea(Leserarea leserarea)
        {
            var isExsists = LeserareaExists(leserarea.Idleserarea);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(leserarea);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Leserarea>> GetAllLeserareas()
        {
            return await _context.Leserareas.ToListAsync();
        }

        public async Task<Leserarea?> GetLeserareaById(int id)
        {
            var leserarea = await _context.Leserareas.FindAsync(id);
            return leserarea;
        }

        public bool LeserareaExists(int id)
        {
            var leserarea = _context.Leserareas.Where(l => l.Idleserarea == id).FirstOrDefault();
            if (leserarea == null)
            {
                return false;
            }
            return true;
        }
    }
}
