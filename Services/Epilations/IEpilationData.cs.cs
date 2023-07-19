using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Epilations
{
    public interface IEpilationData
    {
        Task<List<Epilationarea>> GetAllEpilationareas();

        Task<Epilationarea?> GetEpilationareaById(int id);

        Task<bool> CreateEpilationarea(Epilationarea epilationarea);

        bool EpilationareaExists(int id);
    }
}
