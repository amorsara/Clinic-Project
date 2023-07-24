using Microsoft.AspNetCore.Mvc;
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

        public Task<IActionResult> DeleteTreatmentstype(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<Treatmentstype>> GetTreatmentstype(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Treatmentstype>> GetAllTreatmentstypes()
        {
            return await _context.Treatmentstypes.ToListAsync();
        }

        public Task<ActionResult<Treatmentstype>> PostTreatmentstype(Treatmentstype treatmentstype)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> PutTreatmentstype(int id, Treatmentstype treatmentstype)
        {
            throw new NotImplementedException();
        }

        public bool TreatmentstypeExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
