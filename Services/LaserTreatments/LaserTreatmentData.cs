using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LaserTreatments
{
    public class LaserTreatmentData : ILaserTreatmentData
    {

        private readonly ClinicDBContext _context;

        public LaserTreatmentData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLasertreatments(Lasertreatment lasertreatment)
        {
            var isExsists = LasertreatmentExists(lasertreatment.Idlasertreatment);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(lasertreatment);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLasertreatmentById(int id)
        {
            if (_context.Lasertreatments == null)
            {
                return false;
            }
            var lasertreatment = await _context.Lasertreatments.FindAsync(id);
            if (lasertreatment == null)
            {
                return false;
            }

            _context.Lasertreatments.Remove(lasertreatment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Lasertreatment?> GetLasertreatmentById(int id)
        {
            var lasertreatment = await _context.Lasertreatments.FindAsync(id);
            return lasertreatment;
        }

        public async Task<List<Lasertreatment>> GetLasertreatments()
        {
            return await _context.Lasertreatments.ToListAsync();
        }

        public bool LasertreatmentExists(int id)
        {
            var lasertreatment = _context.Lasertreatments.Where(l => l.Idlasertreatment == id).FirstOrDefault();
            if (lasertreatment == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateLasertreatment(int id, Lasertreatment lasertreatment)
        {
            _context.Entry(lasertreatment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LasertreatmentExists(id))
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
    }
}
