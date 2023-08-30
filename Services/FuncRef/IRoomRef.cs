using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FuncRef
{
    public interface IRoomsRef
    {
        Task<List<Employee>> GetAllEmployeesForRoom(int id);

        Task<List<Room>> GetAllRooms();

        Task<string?> GetNameRoom(int id);

        Task<List<string>?> GetTreatmentsForRoom(int id);
    }
}
