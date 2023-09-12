using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LaserMedicalTypes
{
    public class LaserMedicalTypesData : ILaserMedicalTypesData
    {
        private readonly ClinicDBContext _context;

        public LaserMedicalTypesData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateLasermedicaltype(Lasermedicaltype lasermedicaltype)
        {
            var isExsists = LasermedicaltypeExists(lasermedicaltype.Idlasermedicaltype);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(lasermedicaltype);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLasermedicaltype(int id)
        {
            if (_context.Lasermedicaltypes == null)
            {
                return false;
            }
            var lasermedicaltype = await _context.Lasermedicaltypes.FindAsync(id);
            if (lasermedicaltype == null)
            {
                return false;
            }

            _context.Lasermedicaltypes.Remove(lasermedicaltype);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Lasermedicaltype?> GetLasermedicaltypeById(int id)
        {
            var lasermedicaltype = await _context.Lasermedicaltypes.FindAsync(id);
            return lasermedicaltype;
        }

        public async Task<List<Lasermedicaltype>> GetAllLasermedicaltypes()
        {
            return await _context.Lasermedicaltypes.ToListAsync();
        }

        public bool LasermedicaltypeExists(int id)
        {
            var lasermedicaltype = _context.Lasermedicaltypes.Where(l => l.Idlasermedicaltype == id).FirstOrDefault();
            if (lasermedicaltype == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateLasermedicaltype(int id, Lasermedicaltype lasermedicaltype)
        {
            _context.Entry(lasermedicaltype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LasermedicaltypeExists(id))
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
