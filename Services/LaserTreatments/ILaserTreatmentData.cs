using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LaserTreatments
{
    public interface ILaserTreatmentData
    {
        Task<List<Lasertreatment>> GetLasertreatments();
        Task<Lasertreatment?> GetLasertreatmentById(int id);
        Task<bool> UpdateLasertreatment(int id, Lasertreatment lasertreatment);
        Task<bool> CreateLasertreatments(Lasertreatment lasertreatment);
        Task<bool> DeleteLasertreatmentById(int id);
        bool LasertreatmentExists(int id);

    }
}
