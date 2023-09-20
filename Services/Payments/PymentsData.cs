using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.Contacts;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Payments
{
    public class PymentsData : IPymentsData
    {
        private readonly ClinicDBContext _context;
        private readonly IContactsData _iContactsData;

        public PymentsData(ClinicDBContext context, IContactsData contactsData)
        {
            _context = context;
            _iContactsData = contactsData;
        }

        public async Task<bool> CreatePayment(Payment payment)
        {
            var isExsists = PaymentExists(payment.Idpayment);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(payment);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeletePayment(int id)
        {
            if (_context.Payments == null)
            {
                return false;
            }
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return false;
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<AccountsDto>> GetAllPayments()
        {
            var payments = await GetPayments();
            var list = new List<AccountsDto>();
            foreach(var payment in payments)
            {
                if(payment == null)
                {
                    continue;
                }
                var accountsDto = new AccountsDto();
                accountsDto.id = payment.Idpayment;
                accountsDto.datePayment = payment.Datepayment;
                accountsDto.date = payment.Date;
                accountsDto.tretment = payment.Treatment?.Split(",").ToList();
                accountsDto.laser = payment.Laser?.Split(",").ToList();
                accountsDto.type = payment.Type;
                accountsDto.Payed = payment.Pay;
                accountsDto.Debt = payment.Owes;
                accountsDto.credit = payment.Credit;
                accountsDto.employee = payment.Employee;
                accountsDto.remark = payment.Remark;
                accountsDto.Advanced = payment.Advanced;
                accountsDto.electrolysis = payment.Electrolysis;
                accountsDto.waxing = payment.Waxing?.Split(",").ToList();
                accountsDto.allCredit = await _iContactsData.GetAllCredit(payment.Idcontact);
                var contact = await _iContactsData.GetContactById(payment.Idcontact);
                if(contact != null)
                {
                    accountsDto.phone = contact.Phonenumber1;
                    accountsDto.fullName =  contact.Firstname + " " + contact.Lastname;
                }
                list.Add(accountsDto);
            }
            return list;
        }


    public async Task<Payment?> GetPaymentById(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            return payment;
        }

        public async Task<List<Payment>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        public bool PaymentExists(int id)
        {
            var payment = _context.Payments.Where(p => p.Idpayment == id).FirstOrDefault();
            if (payment == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdatePayment(int id, AccountsDto accountsDto)
        {
            var payment = await GetPaymentById(id);
            if(payment == null)
            {
                return false;
            }

            payment.Datepayment = accountsDto.datePayment;
            payment.Date = accountsDto.date;
            payment.Employee = accountsDto.employee;
            payment.Pay = accountsDto.Payed;
            payment.Type = accountsDto.type;
            payment.Owes = accountsDto.Debt;
            payment.Credit = accountsDto.credit;
            payment.Treatment = accountsDto.tretment?.Count != null ? String.Join(",", accountsDto.tretment) : null;
            payment.Laser = accountsDto.laser?.Count != null ? String.Join(",", accountsDto.laser) : null;


            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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
    }
}
