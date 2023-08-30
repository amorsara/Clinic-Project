using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TempCloseEmployees
{
    public class TempCloseEmployeesData : ITempCloseEmployeesData
    {
        private readonly ClinicDBContext _context;

        public TempCloseEmployeesData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTempcloseemployee(Tempcloseemployee tempcloseemployee)
        {
            if (tempcloseemployee == null)
            {
                return false;
            }

            var isExsists = TempcloseemployeeExists(tempcloseemployee.Idtempcloseemployee);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(tempcloseemployee);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTempcloseemployee(int id)
        {
            if (_context.Closerooms == null)
            {
                return false;
            }

            var tempcloseemployee = await GetTempcloseemployeeById(id);
            if (tempcloseemployee == null)
            {
                return false;
            }

            _context.Tempcloseemployees.Remove(tempcloseemployee);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Tempcloseemployee>> GetAllTempcloseemployees()
        {
            return await _context.Tempcloseemployees.ToListAsync();
        }

        public async Task<List<Tempcloseemployee>> GetAllTempcloseemployeesById(int id)
        {
            return await _context.Tempcloseemployees.Where(t => t.Idemployee == id).ToListAsync();
        }

        public async Task<Tempcloseemployee?> GetTempcloseemployeeById(int id)
        {
            var tempcloseemployee = await _context.Tempcloseemployees.FindAsync(id);
            return tempcloseemployee;
        }

        public bool TempcloseemployeeExists(int id)
        {
            var tempcloseemployee = _context.Tempcloseemployees.Where(t => t.Idtempcloseemployee == id).FirstOrDefault();
            if (tempcloseemployee == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateTempcloseemployee(int id, Tempcloseemployee tempcloseemployee)
        {

            if (tempcloseemployee == null)
            {
                return false;
            }

            _context.Entry(tempcloseemployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempcloseemployeeExists(id))
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
