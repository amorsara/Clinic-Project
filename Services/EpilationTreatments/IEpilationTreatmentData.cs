using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EpilationTreatments
{
    public interface IEpilationTreatmentData
    {
        Task<List<Epilationtreatment>> GetEpilationtreatments();
        Task<EpilationCardDto> GetAllEpilationTreatment(int id);
        Task<Epilationtreatment?> GetEpilationtreatmentById(int id);
        Task<bool> UpdateEpilationtreatment(int id, Epilationtreatment epilationtreatment);
        Task<bool> CreateEpilationtreatment(Epilationtreatment epilationtreatment);
        Task<bool> DeleteEpilationtreatmentById(int id);
        bool EpilationtreatmentExists(int id);
    }
}
