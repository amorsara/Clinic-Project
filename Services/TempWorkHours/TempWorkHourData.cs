using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TempWorkHours
{
    public class TempWorkHourData : ITempWorkHourData
    {
        private readonly ClinicDBContext _context;

        public TempWorkHourData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTempworkhour(Tempworkhour tempworkhour)
        {
            var isExsists = TempworkhourExists(tempworkhour.Idtempworkhour);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(tempworkhour);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTempworkhour(int id)
        {
            if (_context.Tempworkhours == null)
            {
                return false;
            }
            var tempworkhour = await GetTempworkhourById(id);
            if(tempworkhour == null)
            {
                return false;
            }
            var ok = await _context.Tempworkhours.FindAsync(tempworkhour.Idtempworkhour);
            if (ok == null)
            {
                return false;
            }

            _context.Tempworkhours.Remove(tempworkhour);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Tempworkhour?> GetTempworkhourById(int id)
        {
            var tempworkhour = await _context.Tempworkhours.FindAsync(id);
            return tempworkhour;
        }

        public async Task<List<Tempworkhour>> GetAllTempworkhours()
        {
            return await _context.Tempworkhours.ToListAsync();
        }

        public async Task<List<Tempworkhour>> GetAllTempworkhoursForId(int id)
        {
            return await _context.Tempworkhours.Where(t => t.Idemployee == id).ToListAsync();
        }

        public bool TempworkhourExists(int id)
        {
            var tempworkhour = _context.Tempworkhours.Where(t => t.Idtempworkhour == id).FirstOrDefault();
            if (tempworkhour == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateTempworkhour(int id, Tempworkhour tempworkhour)
        {
            _context.Entry(tempworkhour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempworkhourExists(id))
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

        public async Task<bool> UpdateStatusTempworkhour(int id)
        {
            var tempworkhour = await GetTempworkhourById(id);
            if(tempworkhour == null)
            {
                return false;
            }
            tempworkhour.Status = true;
            var isOk = await UpdateTempworkhour(id, tempworkhour);
            return isOk;
        }
    }
}
