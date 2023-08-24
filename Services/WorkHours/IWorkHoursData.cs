using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WorkHours
{
    public interface IWorkHoursData
    {
        Task<List<Workhour>> GetAllWorkHours();
        Task<Workhour?> GetWorkHourById(int id);
        Task<bool> CreateWorkHour(Workhour workHour);
        Task <char?> GetShiftEmployee(int id, TimeOnly? time);
        bool WorkHourExists(int id);
        Task<List<EmployeeShiftDto>> GetWorkHourByEmployee(int idRoom,int id, bool regular);
        Task<List<Workhour>> GetShiftByDay(int idRoom, int id, int day);
        Task<bool> DeleteShift(int id, int day, TimeOnly time);
        Task<bool> DeleteWorkhour(int id);
        Task<bool> UpdateWorkhour(int id, Workhour workhour);
        Task<bool> Check(Workhour workhour);
    }
}
