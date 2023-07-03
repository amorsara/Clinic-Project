using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Contacts;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IContactsData _iContactsData;

        public ContactsController(ClinicDBContext context, IContactsData contactsData)
        {
            _context = context;
            _iContactsData = contactsData;
        }

        [HttpGet]
        [Route("/api/contacts/getallcontacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            var contacts = await _iContactsData.GetAllContact();
            if (contacts == null)
            {
                 return NotFound();
            }
            return contacts;
        }

        [HttpGet]
        [Route("/api/contacts/getcontactbyid/{id}")]
        public async Task<ActionResult<Contact>> GetContactById(int id)
        {
            var contact = await _iContactsData.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }         
            return contact;
        }
      
        [HttpPut]
        [Route("/api/contacts/updatecontact/{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            if (id != contact.Idcontact)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_iContactsData.ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Route("/api/contacts/createcontact")]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            var result = await _iContactsData.CreateContact(contact);
            if (result)
            {
                return CreatedAtAction("CreateContact", new { id = contact.Idcontact }, contact);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/contacts/deletecontact/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
