using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TempWorkHours
{
    public interface ITempWorkHourData
    {
        Task<List<Tempworkhour>> GetAllTempworkhours();
        Task<List<Tempworkhour>> GetAllTempworkhoursForId(int id);
        Task<Tempworkhour?> GetTempworkhourById(int id);
        Task<bool> UpdateTempworkhour(int id, Tempworkhour tempworkhour);
        Task<bool> UpdateStatusTempworkhour(int id);
        Task<bool> CreateTempworkhour(Tempworkhour tempworkhour);
        Task<bool> DeleteTempworkhour(int id);
        bool TempworkhourExists(int id);
    }
}
