using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Appointments;
using Services.DTO;
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
        private readonly IAppointmentsData _iAppointmentsData;

        public ContactsData(ClinicDBContext context, IAppointmentsData appointmentsData)
        {
            _context = context;
            _iAppointmentsData = appointmentsData;
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
            contact.Laser = false;
            contact.Waxing = false;
            contact.Electrolysis = false;
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

        public async Task<List<Contact>> GetAllContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        //public async Task<List<Contact>> GetSemContacts()
        //{
        //    return await _context.Contacts.Where(c => c.Sem == true && c.Isshow == true).ToListAsync();            
        //}

        //public async Task<List<Contact>> GetActiveContacts()
        //{
        //    return await _context.Contacts.Where(c => c.Isactive == true && c.Isshow == true).ToListAsync();
        //}

        public async Task<List<ContactDateDto>> GetContactsWithDates()
        {
            var contacts = await GetAllContacts();
            var listContacts = new List<ContactDateDto>();
            foreach (var contact in contacts)
            {
                if(contact.Isshow == false)
                {
                    continue;
                }
                var contactDates = new ContactDateDto();
                var listDates = await _iAppointmentsData.GetDatesOfAppointments(contact.Idcontact);
                contactDates.Idcontact = contact.Idcontact;
                contactDates.Treatment[0] = (bool)(contact.Laser != null ? contact.Laser : false);
                contactDates.Treatment[1] = (bool)(contact.Waxing != null ? contact.Waxing : false); ;
                contactDates.Treatment[2] = (bool)(contact.Electrolysis != null ? contact.Electrolysis : false);
                contactDates.Firstname = contact.Firstname;
                contactDates.Lastname = contact.Lastname;
                contactDates.Phonenumber1 = contact.Phonenumber1;
                contactDates.Phonenumber2 = contact.Phonenumber2;
                contactDates.Phonenumber3 = contact.Phonenumber3;
                contactDates.Sem = contact.Sem.ToString();
                contactDates.Email = contact.Email;
                contactDates.Remark = contact.Remark;
                contactDates.Isactive = contact.Isactive;
                contactDates.ListDates = listDates.ToList();
                contactDates.allCredit = contact.Credit;
                contactDates.isshow = contact.Isshow;
                listContacts.Add(contactDates);
            }
            return listContacts;
        }

        //public async Task<List<WaitTreatmentsDto>> GetAllWaitDates()
        //{
        //    var appointments = await _iAppointmentsData.GetAllWaitDates();
        //    var list = new List<WaitTreatmentsDto>();
        //    int cnt = 0;
        //    foreach (var appointment in appointments)
        //    {
        //        var contact = await GetContactById(appointment.Idcontact);
        //        if (contact == null || appointment.Date == null)
        //        {
        //            continue;
        //        }
        //        var waitTreatment = new WaitTreatmentsDto() ;
        //        waitTreatment.Id = cnt++;
        //        waitTreatment.FullName = contact.Firstname + " " + contact.Lastname;
        //        waitTreatment.Phonenumber1 = contact.Phonenumber1;
        //        waitTreatment.Phonenumber2 = contact.Phonenumber2;
        //        waitTreatment.Type = appointment.Treatmentname;
        //        waitTreatment.Date = (DateOnly)appointment.Date;
        //        waitTreatment.Remark = contact.Remark;
        //        list.Add(waitTreatment);
        //    }
        //    return list;
        //}


        public async Task<bool> UpdateContact(int id, Contact contact)
        {
            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<ActionResult<Contact?>> UpdateTreatementNameForContact(int id, string? type)
        {
            Console.WriteLine(type);
            var c = await GetContactById(id);
            if (c == null)
            {
                return c;
            }
            if (type == "Laser" || type == "laser")
            {
                c.Laser = true;
            }
            else
            {
                if (type == "Electrolysis" || type == "electrolysis")
                {
                    c.Electrolysis = true;
                }
                else
                    //(type == 'W' || type == 'w') and other...
                {
                    c.Waxing = true;
                }
            }
           

            await UpdateContact(id, c);
            return c;

        }

        public async Task<bool> UpdateRemark(int id, string? remark, string type)
        {
            var contact = await GetContactById(id);
            if(contact == null)
            {
                return false;
            }
            if(type == "laser")
            {
                contact.Remarklaser = remark;
            }
            else
            {
                contact.Remarkelecr = remark;
            }
   
            var ok = await UpdateContact(id, contact);
            return ok;
        }

        public async Task<string?> GetRemark(int id, string type)
        {
            var contact = await GetContactById(id);
            if(type == "laser")
            {
                return contact?.Remarklaser;
            }
            return contact?.Remarkelecr;
        }

        public async Task<bool> UpdateAllCredit(int id, int allCredit)
        {
            var contact = await GetContactById(id);
            if(contact == null)
            {
                return false;
            }
            
            contact.Credit = allCredit;
            var isOk = await UpdateContact(id, contact);
            return isOk;
        }

        public async Task<bool> UpdateRemarkLaser(LaserDetailsDto laserDetailsDto)
        {
            var contact = await GetContactById(laserDetailsDto.idTreated);
            if(contact == null)
            {
                return false;
            }

            contact.Remarklaser = laserDetailsDto.remarkLaser + "," + laserDetailsDto.hair + "," + laserDetailsDto.skin;

            var isOk = await UpdateContact(contact.Idcontact,contact);
            return isOk;
        }

        //public async Task<int?> GetAllCredit(int id)
        //{
        //    var contact = await GetContactById(id);
        //    return contact?.Credit;
        //}
    }
}


