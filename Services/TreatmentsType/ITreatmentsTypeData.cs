using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TreatmentsType
{
    public interface ITreatmentsTypeData
    {

        Task<ActionResult<IEnumerable<Treatmentstype>>> GetTreatmentstypes();

        Task<ActionResult<Treatmentstype>> GetTreatmentstype(int id);

        Task<IActionResult> PutTreatmentstype(int id, Treatmentstype treatmentstype);

        Task<ActionResult<Treatmentstype>> PostTreatmentstype(Treatmentstype treatmentstype);

        Task<IActionResult> DeleteTreatmentstype(int id);

        bool TreatmentstypeExists(int id);
    }
}

