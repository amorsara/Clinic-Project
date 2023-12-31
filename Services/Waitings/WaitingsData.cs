﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Waitings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Waitings
{
    public class WaitingsData : IWaitingsData
    {
        private readonly ClinicDBContext _context;

        public WaitingsData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateWaiting(Waiting waiting)
        {
            var isExsists = WaitingExists(waiting.Idwaiting);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(waiting);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<Waiting?> GetWaitingById(int id)
        {
            var waiting = await _context.Waitings.FindAsync(id);
            return waiting;
        }

        public async Task<List<Waiting>> GetAllWaitings()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var waitings = await _context.Waitings.Where(w => w.Untildate >= today).ToListAsync();
            return waitings;
        }

        public bool WaitingExists(int id)
        {
            var waiting = _context.Waitings.Where(w => w.Idwaiting == id).FirstOrDefault();
            if (waiting == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteWaiting(Waiting waiting)
        {
            if (_context.Waitings == null)
            {
                return false;
            }
            var ok = await _context.Waitings.FindAsync(waiting.Idwaiting);
            if (ok == null)
            {
                return false;
            }

            _context.Waitings.Remove(waiting);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAllWaitingWithPastDate()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var waitings = await _context.Waitings.Where(w => w.Untildate < today).ToListAsync();
            var isOk = true;
            foreach(var waiting in waitings)
            {
                var okDel = await DeleteWaiting(waiting);
                if(okDel == false)
                {
                    isOk = false;
                }
            }
            return isOk;
        }
    }
}
