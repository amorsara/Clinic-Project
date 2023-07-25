using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TreatmentsType
{
    public class TreatmentsTypeData : ITreatmentsTypeData
    {

        private readonly ClinicDBContext _context;

        public TreatmentsTypeData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetlistTreatmentstypes()
        {
            var tretments = await GetAllTreatmentstypes();
            var list = new List<string>();
            foreach (var item in tretments)
            {
                if (item != null && item.Nametreatment != null)
                {
                    list.Add(item.Nametreatment);
                }
            }
            return list;
        }

        public async Task<Treatmentstype?> GetTreatmentstypeById(int id)
        {
            var treatmentstype = await _context.Treatmentstypes.FindAsync(id);
            return treatmentstype;
        }

        public async Task<List<Treatmentstype>> GetAllTreatmentstypes()
        {
            return await _context.Treatmentstypes.ToListAsync();
        }

        public async Task<bool> CreateTreatmentstype(Treatmentstype treatmentstype)
        {
            var isExsists = TreatmentstypeExists(treatmentstype.Idtreatmenttype);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(treatmentstype);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdatetTreatmentstype(int id, Treatmentstype treatmentstype)
        {
            _context.Entry(treatmentstype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreatmentstypeExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public bool TreatmentstypeExists(int id)
        {
            var treatmentstypes = _context.Treatmentstypes.Where(t => t.Idtreatmenttype == id).FirstOrDefault();
            if (treatmentstypes == null)
            {
                return false;
            }
            return true;
        }
    }
}
