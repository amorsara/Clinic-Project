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

        Task<List<ContactDateDto>> GetContactsWithDates();

        Task <Contact?> GetContactById(int id);

        Task<bool> UpdateRemark(int id, string? remark, string type);

        Task<bool> UpdateMedicalList(int id, Dictionary<string, string> medicalList, string type);

        Task<bool> UpdateRemarkLaser(LaserDetailsDto laserDetailsDto);

        Task<bool> UpdateAllCredit(int id, int allCredit);

        Task<bool> CreateContact(Contact contact);

        Task<string?> GetRemark(int id, string type);

        Task<string?> GetMedicalList(int id, string type);

        Task<Dictionary<string, string>?> GetMedicalListById(int id, string type);

        Task<int?> GetAllCredit(int id);

        bool ContactExists(int id);

        Task<bool> UpdateContact(int id, Contact contact);

        Task<ActionResult<Contact?>> UpdateTreatementNameForContact(int id, string? type);
    }
}


//Task<List<Contact>> GetSemContacts();
//Task<List<Contact>> GetActiveContacts();
//Task<List<WaitTreatmentsDto>> GetAllWaitDates();