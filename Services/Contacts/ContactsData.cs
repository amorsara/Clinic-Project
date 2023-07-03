using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contacts
{
    public class ContactsData : IContactsData
    {

        private readonly ClinicDBContext _context;

        public ContactsData(ClinicDBContext context)
        {
            _context = context;
        }

        public bool ContactExists(int id)
        {
            var contact = _context.Contacts.Where(c => c.Idcontact == id).FirstOrDefault();
            if(contact == null)
            {
                return false;
            }
            return true;         
        }

        public async Task<bool> CreateContact(Contact contact)
        {
            var isExsists = ContactExists(contact.Idcontact);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(contact);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<Contact?> GetContactById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            return contact;
        }

        public async Task<List<Contact>> GetAllContact()
        {
            return await _context.Contacts.ToListAsync();
        }

    }
}
