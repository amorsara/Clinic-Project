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

        Task<List<string>> GetlistTreatmentstypes();

        Task<List<Treatmentstype>> GetAllTreatmentstypes();

        Task<Treatmentstype?> GetTreatmentstypeById(int id);

        Task<bool> UpdatetTreatmentstype(int id, Treatmentstype treatmentstype);

        Task<bool> CreateTreatmentstype(Treatmentstype treatmentstype);

        bool TreatmentstypeExists(int id);
    }
}

