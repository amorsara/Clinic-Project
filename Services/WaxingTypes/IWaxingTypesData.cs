using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WaxingTypes
{
    public interface IWaxingTypesData
    {
        Task<List<Waxingtype>> GetAllWaxingtypes();

        Task<Waxingtype?> GetWaxingtypeById(int id);

        Task<bool> UpdateWaxingtype(int id, Waxingtype waxingtype);

        Task<bool> CreateWaxingtype(Waxingtype waxingtype);

        Task<bool> DeleteWaxingtype(int id);

        bool WaxingtypeExists(int id);

    }
}
