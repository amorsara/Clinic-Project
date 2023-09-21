using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EpilationMedicalTypes
{
    public class EpilationMedicalTypesData : IEpilationMedicalTypesData
    {
        private readonly ClinicDBContext _context;

        public EpilationMedicalTypesData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEpilationmedicaltype(Epilationmedicaltype epilationmedicaltype)
        {
            var isExsists = EpilationmedicaltypeExists(epilationmedicaltype.Idepilationmedicaltype);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(epilationmedicaltype);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteEpilationmedicaltype(int id)
        {
            if (_context.Epilationmedicaltypes == null)
            {
                return false;
            }
            var epilationmedicaltype = await _context.Epilationmedicaltypes.FindAsync(id);
            if (epilationmedicaltype == null)
            {
                return false;
            }

            _context.Epilationmedicaltypes.Remove(epilationmedicaltype);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool EpilationmedicaltypeExists(int id)
        {
            var epilationmedicaltype = _context.Epilationmedicaltypes.Where(e => e.Idepilationmedicaltype == id).FirstOrDefault();
            if (epilationmedicaltype == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Epilationmedicaltype?> GetEpilationmedicaltypeById(int id)
        {
            var epilationmedicaltype = await _context.Epilationmedicaltypes.FindAsync(id);
            return epilationmedicaltype;
        }

        public async Task<List<Epilationmedicaltype>> GetAllEpilationmedicaltypes()
        {
            return await _context.Epilationmedicaltypes.ToListAsync();
        }

        public async Task<bool> UpdateEpilationmedicaltype(int id, Epilationmedicaltype epilationmedicaltype)
        {
            _context.Entry(epilationmedicaltype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpilationmedicaltypeExists(id))
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

        public async Task<string> GetStringEpilationmedicaltype()
        {
            var medicalList = await GetAllEpilationmedicaltypes();
            var stringMedical = "";
            foreach (var medical in medicalList)
            {
                stringMedical += "," + medical.Nametype + "," + medical.Note;
            }
            return stringMedical;
        }
    }
}
