﻿using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LaserMedicalTypes
{
    public interface ILaserMedicalTypesData
    {
        Task<List<Lasermedicaltype>> GetAllLasermedicaltypes();

        Task<Lasermedicaltype?> GetLasermedicaltypeById(int id);

        Task<bool> UpdateLasermedicaltype(int id, Lasermedicaltype lasermedicaltype);

        Task<bool> CreateLasermedicaltype(Lasermedicaltype lasermedicaltype);

        Task<bool> DeleteLasermedicaltype(int id);

        bool LasermedicaltypeExists(int id);

        Task<List<MedicalListDto>> GetListLasermedicaltype();

    }
}
