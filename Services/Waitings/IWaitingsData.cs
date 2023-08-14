using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Waitings
{
    public interface IWaitingsData
    {
        Task<List<Waiting>> GetAllWaitings();

        Task<Waiting?> GetWaitingById(int id);

        Task<bool> CreateWaiting(Waiting waiting);

        Task<bool> DeleteWaiting(Waiting waiting);

        Task<bool> DeleteAllWaitingWithPastDate();

        bool WaitingExists(int id);

    }
}
