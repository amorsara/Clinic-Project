using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TempCloseEmployees
{
    public interface ITempCloseEmployeesData
    {
        Task<List<Tempcloseemployee>> GetAllTempcloseemployees();

        Task<List<Tempcloseemployee>> GetAllTempcloseemployeesById(int id);

        Task<Tempcloseemployee?> GetTempcloseemployeeById(int id);

        Task<bool> UpdateTempcloseemployee(int id, Tempcloseemployee tempcloseemployee);

        Task<bool> CreateTempcloseemployee(Tempcloseemployee tempcloseemployee);

        Task<bool> DeleteTempcloseemployee(int id);

        bool TempcloseemployeeExists(int id);
      
    }
}
