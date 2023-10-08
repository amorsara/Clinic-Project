using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.TempWorkHours
{
    public interface ITempWorkHourData
    {
        Task<List<TempWorkHourDto>> GetAllTempworkhoursForWeek(DateOnly date);

        Task<List<TempWorkHourDto>> GetAllTempworkhoursForId(int id);

        Task<Tempworkhour?> GetTempworkhourById(int id);

        Task<bool> UpdateTempworkhourWrapper(int id, TempWorkHourDto tempWorkHourDto);

        Task<bool> UpdateTempworkhour(int id, Tempworkhour tempWorkHour);

        Task<bool> UpdateStatusTempworkhour(int id);

        Task<bool> CreateTempworkhour(TempWorkHourDto tempWorkHourDto);

        Task<bool> DeleteTempworkhour(int id);

        bool TempworkhourExists(int id);
    }
}
