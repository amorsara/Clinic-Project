﻿using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EpilationTreatments
{
    public class EpilationTreatmentData : IEpilationTreatmentData
    {

        private readonly ClinicDBContext _context;

        public EpilationTreatmentData(ClinicDBContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateEpilationtreatment(Epilationtreatment epilationtreatment)
        {
            var isExsists = EpilationtreatmentExists(epilationtreatment.Idepilationtreatment);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(epilationtreatment);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteEpilationtreatmentById(int id)
        {
            if (_context.Lasertreatments == null)
            {
                return false;
            }
            var epilationtreatment = await _context.Epilationtreatments.FindAsync(id);
            if (epilationtreatment == null)
            {
                return false;
            }

            _context.Epilationtreatments.Remove(epilationtreatment);
            await _context.SaveChangesAsync();

            return true;
        }

        public bool EpilationtreatmentExists(int id)
        {
            var epilationtreatment = _context.Epilationtreatments.Where(e => e.Idepilationtreatment == id).FirstOrDefault();
            if (epilationtreatment == null)
            {
                return false;
            }
            return true;
        }

        public async Task<EpilationCardDto> GetAllEpilationTreatment(int id)
        {
            var epilationCard = new EpilationCardDto();
            var list = new List<EpilationtreatmentDto>();
            var epilationTreatments = await GetEpilationtreatments();
            foreach (var epilationTreatment in epilationTreatments)
            {
                var epilationDto = new EpilationtreatmentDto();
                epilationDto.Idepilationtreatment = epilationTreatment.Idepilationtreatment;
                epilationDto.idClient = id;
                epilationDto.Date = epilationTreatment.Date;
                epilationDto.colorWorker = epilationTreatment.Coloremployee;
                epilationDto.Results = epilationTreatment.Results;
                epilationDto.Area = epilationTreatment.Area?.Split(",").ToList();
                epilationDto.Machine = epilationTreatment.Machine;
                epilationDto.Techniqe = epilationTreatment.Techniqe;
                epilationDto.Time = epilationTreatment.Time;
                if (epilationTreatment != null && epilationTreatment.Idcontact == id)
                {
                    list.Add(epilationDto);
                }
            }
            epilationCard.idClient = id;
            epilationCard.remarkElec = "";
            epilationCard.listTreatments = list;
            return epilationCard;
        }

        public async Task<Epilationtreatment?> GetEpilationtreatmentById(int id)
        {
            var epilationtreatment = await _context.Epilationtreatments.FindAsync(id);
            return epilationtreatment;
        }

        public async Task<List<Epilationtreatment>> GetEpilationtreatments()
        {
            return await _context.Epilationtreatments.ToListAsync();
        }

        public async Task<bool> UpdateEpilationtreatment(int id, Epilationtreatment epilationtreatment)
        {
            _context.Entry(epilationtreatment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpilationtreatmentExists(id))
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