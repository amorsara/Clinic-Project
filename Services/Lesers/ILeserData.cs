using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Lesers
{
    public interface ILeserData
    {

        Task<List<Leserarea>> GetAllLeserareas();

        Task<Leserarea?> GetLeserareaById(int id);

        Task<bool> CreateLeserarea(Leserarea leserarea);

        bool LeserareaExists(int id);
     
    }
}
