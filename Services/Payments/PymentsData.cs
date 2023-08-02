using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
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

        public PymentsData(ClinicDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePayment(PaymentsDto paymentDto)
        {
            var isExsists = PaymentExists(paymentDto.Id);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(paymentDto);
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

        public async Task<bool> UpdatePayment(int id, Payment payment)
        {
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
