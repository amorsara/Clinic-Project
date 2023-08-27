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

        public ContactsController(ClinicDBContext context, IContactsData contactsData)
        {
            _context = context;
            _iContactsData = contactsData;
        }

        [HttpGet]
        [Route("/api/contacts/getallcontacts")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            var contacts = await _iContactsData.GetAllContacts();
            if (contacts == null)
            {
                 return NotFound();
            }
            return contacts;
        }

        [HttpGet]
        [Route("/api/contacts/getallcontactswithdates")]
        public async Task<ActionResult<IEnumerable<ContactDateDto>>> GetAllContactsWithDates()
        {
            var contacts = await _iContactsData.GetContactsWithDates();
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


        [HttpPost]
        [Route("/api/contacts/updatecontactwrapper")]
        public async Task<IActionResult> UpdateContactWrapper(ContactDto contact)
        {
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
            return c;
        }

        [HttpPut]
        [Route("/api/contacts/updatecontact/{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            if (id != contact.Idcontact)
            {
                return NoContent();
            }
            var res = await _iContactsData.UpdateContact(id, contact);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
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

        [HttpPost]
        [Route("/api/contacts/createcontactwarpper")]
        public async Task<ActionResult<Contact>> CreateContactWarpper(ContactDto contactDetails)
        {
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

            return contact;       
        }


        [HttpDelete]
        [Route("/api/contacts/deletecontact/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _iContactsData.GetContactById(id);
            if(contact == null)
            {
                return BadRequest();
            }
            contact.Isshow = false;
            var isOk = await UpdateContact(contact.Idcontact, contact);
            return Ok(isOk);
        }



        [HttpDelete]
        [Route("/api/contacts/deletecontactbyid/{id}")]
        public async Task<IActionResult> DeleteContactById(int id)
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








//[HttpGet]
//[Route("/api/contacts/getsemcontacts")]
//public async Task<ActionResult<IEnumerable<Contact>>> GetSemContacts()
//{
//    var result = await _iContactsData.GetSemContacts();
//    if (result == null)
//    {
//        return NotFound();
//    }
//    return result;
//}

//[HttpGet]
//[Route("/api/contacts/getactivecontacts")]
//public async Task<ActionResult<IEnumerable<Contact>>> GetActiveContacts()
//{
//    var result = await _iContactsData.GetActiveContacts();
//    if (result == null)
//    {
//        return NotFound();
//    }
//    return result;
//}

//[HttpGet]
//[Route("/api/contacts/getallwaitdates")]
//public async Task<ActionResult<IEnumerable<WaitTreatmentsDto>>> GetAllWaitDates()
//{
//    var result = await _iContactsData.GetAllWaitDates();
//    if (result == null)
//    {
//        return NotFound();
//    }
//    return result;
//}