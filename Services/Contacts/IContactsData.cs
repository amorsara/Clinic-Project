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
        Task<List<ContactDateDto>> GetContactsWithDates();
        Task <Contact?> GetContactById(int id);
        Task<bool> CreateContact(Contact contact);
        bool ContactExists(int id);
    }
}
