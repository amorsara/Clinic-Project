using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WaxingTypes
{
    public class WaxingTypesData : IWaxingTypesData
    {
        private readonly ClinicDBContext _context;

        public WaxingTypesData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateWaxingtype(Waxingtype waxingtype)
        {
            waxingtype.Ischecked = waxingtype.Ischecked != null ? waxingtype.Ischecked : false;
            var isExsists = WaxingtypeExists(waxingtype.Idwaxingtype);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(waxingtype);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteWaxingtype(int id)
        {
            if (_context.Waxingtypes == null)
            {
                return false;
            }
            var waxingtype = await _context.Waxingtypes.FindAsync(id);
            if (waxingtype == null)
            {
                return false;
            }

            _context.Waxingtypes.Remove(waxingtype);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Waxingtype?> GetWaxingtypeById(int id)
        {
            var waxingtype = await _context.Waxingtypes.FindAsync(id);
            return waxingtype;
        }

        public async Task<List<Waxingtype>> GetAllWaxingtypes()
        {
            return await _context.Waxingtypes.ToListAsync();
        }

        public async Task<bool> UpdateWaxingtype(int id, Waxingtype waxingtype)
        {
            _context.Entry(waxingtype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaxingtypeExists(id))
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

        public bool WaxingtypeExists(int id)
        {
            var waxingtype = _context.Waxingtypes.Where(w => w.Idwaxingtype == id).FirstOrDefault();
            if (waxingtype == null)
            {
                return false;
            }
            return true;
        }
    }
}

//
