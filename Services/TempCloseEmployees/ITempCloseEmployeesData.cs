using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TempCloseEmployees
{
    public interface ITempCloseEmployeesData
    {
        Task<List<TempCloseEmployeeDto>> GetAllTempcloseemployees();

        Task<List<TempCloseEmployeeDto>> GetAllTempcloseemployeesById(int id);

        Task<Tempcloseemployee?> GetTempcloseemployeeById(int id);

        Task<bool> UpdateTempcloseemployee(int id, TempCloseEmployeeDto tempcloseemployeedto);

        Task<bool> CreateTempcloseemployee(TempCloseEmployeeDto tempcloseemployeedto);

        Task<bool> DeleteTempcloseemployee(int id);

        bool TempcloseemployeeExists(int id);
      
    }
}
