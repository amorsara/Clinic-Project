using Microsoft.AspNetCore.Mvc;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public interface IContactsData
    {
        Task<List<Contact>> GetAllContact();
        Task <Contact?> GetContactById(int id);
        Task<bool> CreateContact(Contact contact);
        bool ContactExists(int id);
    }
}
