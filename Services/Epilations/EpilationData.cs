using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Epilations
{
    public class EpilationData : IEpilationData
    {
        private readonly ClinicDBContext _context;

        public EpilationData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEpilationarea(Epilationarea epilationarea)
        {
            var isExsists = EpilationareaExists(epilationarea.Idepilationarea);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(epilationarea);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public bool EpilationareaExists(int id)
        {
            var epilationarea = _context.Epilationareas.Where(e => e.Idepilationarea == id).FirstOrDefault();
            if (epilationarea == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Epilationarea>> GetAllEpilationareas()
        {
            return await _context.Epilationareas.ToListAsync();
        }

        public async Task<Epilationarea?> GetEpilationareaById(int id)
        {
            var epilationarea = await _context.Epilationareas.FindAsync(id);
            return epilationarea;
        }
    }
}
