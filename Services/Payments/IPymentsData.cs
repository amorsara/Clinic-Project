using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Payments
{
    public interface IPymentsData
    {

        Task<List<Payment>> GetPayments();
        Task<Payment?> GetPaymentById(int id);
        Task<bool> UpdatePayment(int id, Payment payment);
        Task<bool> CreatePayment(Payment payment);
        Task<bool> DeletePayment(int id);
        bool PaymentExists(int id);

    }
}
