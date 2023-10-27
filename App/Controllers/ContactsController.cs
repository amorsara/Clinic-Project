using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Contacts;
using Services.DTO;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IContactsData _iContactsData;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ClinicDBContext context, IContactsData contactsData, ILogger<ContactsController> logger)
        {
            _context = context;
            _iContactsData = contactsData;
            _logger = logger;
        }

        [HttpGet]
        [Route("/api/contacts/getallcontacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            _logger.LogInformation("Starting treatment: GetAllContacts");
            var contacts = await _iContactsData.GetAllContacts();
            if (contacts == null)
            {
                 return NotFound();
            }
            _logger.LogInformation("Finishing treatment: GetAllContacts");
            return contacts;
        }

        [HttpGet]
        [Route("/api/contacts/getallcontactswithdates")]
        public async Task<ActionResult<IEnumerable<ContactDateDto>>> GetAllContactsWithDates()
        {
            _logger.LogInformation("Starting treatment: GetAllContactsWithDates");
            var contacts = await _iContactsData.GetContactsWithDates();
            if (contacts == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Finishing treatment: GetAllContactsWithDates");
            return contacts;
        }

        [HttpGet]
        [Route("/api/contacts/getcontactbyid/{id}")]
        public async Task<ActionResult<Contact>> GetContactById(int id)
        {
            _logger.LogInformation("Starting treatment: GetContactById");
            var contact = await _iContactsData.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }         
            _logger.LogInformation("Finishing treatment: GetContactById");
            return contact;
        }

        [HttpGet]
        [Route("/api/contacts/getmedicallistbyid/{id}/{type}")]
        public async Task<ActionResult<List<MedicalListDto>>> GetMedicalListById(int id, string type)
        {
            _logger.LogInformation("Starting treatment: GetMedicalListById");
            var medical = await _iContactsData.GetMedicalListById(id, type);
            if (medical == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Finishing treatment: GetMedicalListById");
            return medical;
        }

        [HttpPost]
        [Route("/api/contacts/updatecontactwrapper")]
        public async Task<IActionResult> UpdateContactWrapper(ContactDto contact)
        {
            _logger.LogInformation("Starting treatment: UpdateContactWrapper");
            var newContact = await _iContactsData.GetContactById(contact.id);
            if(newContact == null)
            {
                return BadRequest();
            }
            newContact.Idcontact = contact.id;
            newContact.Remark = contact.Values[0]?.field1;
            newContact.Phonenumber1 = contact.Values[0]?.field2;
            newContact.Firstname = contact.Values[0]?.field3;
            newContact.Howcomeus = contact.Values[1]?.field1;
            newContact.Phonenumber2 = contact.Values[1]?.field2;
            newContact.Lastname = contact.Values[1]?.field3;
            newContact.Urlfile = contact.Values[2]?.field1;
            newContact.Phonenumber3 = contact.Values[2]?.field2;
            newContact.Email = contact.Values[2]?.field3;
            newContact.Sem = contact.Sem;
            newContact.Isactive = contact.Active;
            newContact.Credit = contact.credit == null ? null : contact.credit;
            newContact.Isshow = contact.IsShow == null ? true : contact.IsShow;
            var c = await UpdateContact(newContact.Idcontact, newContact);
            _logger.LogInformation("Finishing treatment: UpdateContactWrapper");
            return c;
        }

        [HttpPut]
        [Route("/api/contacts/updatecontact/{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            _logger.LogInformation("Starting treatment: UpdateContact");
            if (id != contact.Idcontact)
            {
                return NoContent();
            }
            var res = await _iContactsData.UpdateContact(id, contact);
            if (res == false)
            {
                return BadRequest();
            }
            _logger.LogInformation("Finishing treatment: UpdateContact");
            return Ok();
        }

        [HttpPost]
        [Route("/api/contacts/createcontact")]
        public async Task<ActionResult<Contact>> CreateContact(Contact contact)
        {
            _logger.LogInformation("Starting treatment: CreateContact");
            var result = await _iContactsData.CreateContact(contact);
            _logger.LogInformation("Finishing treatment: CreateContact");
            if (result)
            {
                return CreatedAtAction("CreateContact", new { id = contact.Idcontact }, contact);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/contacts/createcontactwarpper")]
        public async Task<ActionResult<Contact>> CreateContactWarpper(ContactDto contactDetails)
        {
            _logger.LogInformation("Starting treatment: CreateContactWarpper");
            Console.WriteLine(contactDetails);
            var newContact = new Contact();
            newContact.Credit = 0;
            newContact.Isshow = true;
            newContact.Laser = newContact.Waxing = newContact.Electrolysis = false;
            newContact.Remark = contactDetails.Values[0]?.field1;
            newContact.Firstname = contactDetails.Values[0]?.field3;
            newContact.Howcomeus = contactDetails.Values[1]?.field1;
            newContact.Lastname = contactDetails.Values[1]?.field3;
            newContact.Urlfile = contactDetails.Values[2]?.field1;
            newContact.Email = contactDetails.Values[2]?.field3;
            newContact.Sem = contactDetails.Sem;
            newContact.Isactive = contactDetails.Active;
            newContact.Credit = 0;
            if(contactDetails.Priority == "Phonenumber1")
            {
                newContact.Phonenumber1 = contactDetails.Values[0]?.field2;
                newContact.Phonenumber2 = contactDetails.Values[1]?.field2;
                newContact.Phonenumber3 = contactDetails.Values[2]?.field2;
            }
            if (contactDetails.Priority == "Phonenumber2")
            {
                newContact.Phonenumber1 = contactDetails.Values[1]?.field2;
                newContact.Phonenumber2 = contactDetails.Values[0]?.field2;
                newContact.Phonenumber3 = contactDetails.Values[2]?.field2;
            }
            if (contactDetails.Priority == "Phonenumber3")
            {
                newContact.Phonenumber1 = contactDetails.Values[2]?.field2;
                newContact.Phonenumber2 = contactDetails.Values[0]?.field2;
                newContact.Phonenumber3 = contactDetails.Values[1]?.field2;
            }

            var contact = await CreateContact(newContact);
            _logger.LogInformation("Finishing treatment: CreateContactWarpper");
            return contact;       
        }


        [HttpDelete]
        [Route("/api/contacts/deletecontact/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
             _logger.LogInformation("Starting treatment: DeleteContact");
            var contact = await _iContactsData.GetContactById(id);
            if(contact == null)
            {
                return BadRequest();
            }
            contact.Isshow = false;
            var isOk = await UpdateContact(contact.Idcontact, contact);
            _logger.LogInformation("Finishing treatment: DeleteContact");
            return Ok(isOk);
        }



        [HttpDelete]
        [Route("/api/contacts/deletecontactbyid/{id}")]
        public async Task<IActionResult> DeleteContactById(int id)
        {
            _logger.LogInformation("Starting treatment: DeleteContactById");
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
            _logger.LogInformation("Finishing treatment: DeleteContactById");
            return NoContent();
        }

    }
}