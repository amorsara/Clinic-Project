using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Contacts;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.LaserTreatments
{
    public class LaserTreatmentData : ILaserTreatmentData
    {

        private readonly ClinicDBContext _context;
        private readonly IContactsData _iContactsData;

        public LaserTreatmentData(ClinicDBContext context, IContactsData iContactsData)
        {
            _context = context;
            _iContactsData = iContactsData;
        }

        public async Task<bool> CreateLasertreatments(Lasertreatment lasertreatment)
        {
            var isExsists = LasertreatmentExists(lasertreatment.Idlasertreatment);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(lasertreatment);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLasertreatmentById(int id)
        {
            if (_context.Lasertreatments == null)
            {
                return false;
            }
            var lasertreatment = await _context.Lasertreatments.FindAsync(id);
            if (lasertreatment == null)
            {
                return false;
            }

            _context.Lasertreatments.Remove(lasertreatment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<LaserCardDto> GetAllLaserTreatment(int id)
        {
            var laserCard = new LaserCardDto();
            var list = new List<LasertreatmentDto>();
            var laserTreatments = await GetLasertreatments();
            var remark = await _iContactsData.GetRemark(id, "laser");
            var listRemark = remark != null ? remark?.Split(",").ToList() : null;
            foreach(var lasertreatment in laserTreatments)
            {
                var laserDto = new LasertreatmentDto();
                laserDto.Idlasertreatment = lasertreatment.Idlasertreatment;
                laserDto.idClient = id;
                laserDto.Date = lasertreatment.Date;
                laserDto.Energy = lasertreatment.Energy?.Split(",").ToList();
                laserDto.colorWorker = lasertreatment.Coloremployee;
                laserDto.Results = lasertreatment.Results?.Split(",").ToList();
                laserDto.Area = lasertreatment.Area?.Split(",").ToList();
                laserDto.Ms = lasertreatment.Ms?.Split(",").ToList();
                laserDto.Spotsize = lasertreatment.Spotsize?.Split(",").ToList();
                if (lasertreatment != null && lasertreatment.Idcontact == id)
                {
                    list.Add(laserDto);
                }
            }
            laserCard.idClient = id;
            if(listRemark?.Count >= 1)
            {
                laserCard.remarkLaser = listRemark != null ? listRemark[0] : null;
            }
            var hair = new HairDto();
            if (listRemark?.Count >= 2)
            {
                hair.name = listRemark != null ? listRemark[1] : null;
            }
            if (listRemark?.Count >= 3)
            {
                hair.color = listRemark != null ? listRemark[2] : null;
            }
            if (listRemark?.Count >= 4)
            {
                laserCard.skin = listRemark != null ? listRemark[3] : null;
            }
            laserCard.hair = hair;
           
            laserCard.listTreatments = list;
            return laserCard;
        }

        public async Task<Lasertreatment?> GetLasertreatmentById(int id)
        {
            var lasertreatment = await _context.Lasertreatments.FindAsync(id);
            return lasertreatment;
        }

        public async Task<List<Lasertreatment>> GetLasertreatments()
        {
            return await _context.Lasertreatments.ToListAsync();
        }

        public bool LasertreatmentExists(int id)
        {
            var lasertreatment = _context.Lasertreatments.Where(l => l.Idlasertreatment == id).FirstOrDefault();
            if (lasertreatment == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateLasertreatment(int id, Lasertreatment lasertreatment)
        {
            _context.Entry(lasertreatment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LasertreatmentExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
    }
}
