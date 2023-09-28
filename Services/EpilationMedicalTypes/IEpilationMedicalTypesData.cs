using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EpilationMedicalTypes
{
    public interface IEpilationMedicalTypesData
    {
        Task<List<Epilationmedicaltype>> GetAllEpilationmedicaltypes();

        Task<Epilationmedicaltype?> GetEpilationmedicaltypeById(int id);

        Task<bool> UpdateEpilationmedicaltype(int id, Epilationmedicaltype epilationmedicaltype);

        Task<bool> CreateEpilationmedicaltype(Epilationmedicaltype epilationmedicaltype);

        Task<bool> DeleteEpilationmedicaltype(int id);

        bool EpilationmedicaltypeExists(int id);

        Task<List<MedicalListDto>> GetListEpilationmedicaltype();

    }
}
