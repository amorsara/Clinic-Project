using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IContactsData
    {
        Task<List<Contact>> GetAllContacts();
        Task<List<Contact>> GetSemContacts();
        Task<List<Contact>> GetActiveContacts();
        Task<List<WaitTreatmentsDto>> GetAllWaitDates();
        Task<List<DateOnly>> GetAllFutureDates();
        Task<List<ContactDateDto>> GetContactsWithDates();
        Task <Contact?> GetContactById(int id);
        Task<bool> UpdateRemark(int id, string? remark, string type);
        Task<bool> CreateContact(Contact contact);
        Task<string?> GetRemark(int id, string type);
        bool ContactExists(int id);
        Task<bool> UpdateContact(int id, Contact contact);
        Task<ActionResult<Contact?>> UpdateTreatementNameForContact(int id, char? type);
    }
}
