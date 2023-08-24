using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Inquiries
{
    public class InquiriesData : IInquiriesData
    {
        private readonly ClinicDBContext _context;
        private readonly IEmployeesData _iEmployeesData;

        public InquiriesData(ClinicDBContext context, IEmployeesData employeesData)
        {
            _context = context;
            _iEmployeesData = employeesData;
        }

        public async Task<bool> CreateInquiry(Inquiry inquiry)
        {
            var isExsists = InquiryExists(inquiry.Idinquirie);
            if (isExsists)
            {
                return false;
            }
           
            await _context.AddAsync(inquiry);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteInquiry(int id)
        {
            if (_context.Inquiries == null)
            {
                return false;
            }
            var inquiry = await _context.Inquiries.FindAsync(id);
            if (inquiry == null)
            {
                return false;
            }

            _context.Inquiries.Remove(inquiry);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<InquiryDto>?> GetAllInquiries(int id)
        {
            var idAdmin = await _iEmployeesData.GetIdForAdmin();
            if(idAdmin == null || idAdmin != id)
            {
                return null;
            }
            var inquiries = await GetInquiries();
            var list = new List<InquiryDto>();
            foreach (var inquiry in inquiries)
            {
                if (inquiry == null)
                {
                    continue;
                }

                var newInq = new InquiryDto();
                newInq.id = inquiry.Idinquirie;
                newInq.doInquirie = inquiry.Doinquirie;
                newInq.time = inquiry.Time;
                newInq.date = inquiry.Date;
                newInq.fullname = inquiry.Fullname;
                newInq.phon = inquiry.Phon;
                newInq.sum = inquiry.Sum;
                newInq.remark = inquiry.Remark;
                newInq.response = inquiry.Response;
                newInq.status = inquiry.Status;

                var employee = await _iEmployeesData.GetEmployeeById(inquiry.Idemployee);
                newInq.employee = new EmployeeDetails();
                newInq.employee.Color = employee?.Color;
                newInq.employee.Name = employee?.Name;
                newInq.employee.Id = employee?.Idemployee != null ? employee.Idemployee : 0;
                list.Add(newInq);

            }
            return list;
        }

        public async Task<List<InquiryDto>> GetAllInquiriesById(int id)
        {
            var inquiries = await _context.Inquiries.Where(i => i.Idemployee == id).ToListAsync();
            var list = new List<InquiryDto>();
            foreach(var inquiry in inquiries)
            {
                if(inquiry == null)
                {
                    continue;
                }

                var newInq = new InquiryDto();
                newInq.id = inquiry.Idinquirie;
                newInq.doInquirie = inquiry.Doinquirie;
                newInq.time = inquiry.Time;
                newInq.date = inquiry.Date;
                newInq.fullname = inquiry.Fullname;
                newInq.phon = inquiry.Phon;
                newInq.sum = inquiry.Sum;
                newInq.remark = inquiry.Remark;
                newInq.response = inquiry.Response;
                newInq.status = inquiry.Status;

                var employee = await _iEmployeesData.GetEmployeeById(id);
                newInq.employee = new EmployeeDetails();
                newInq.employee.Color = employee?.Color;
                newInq.employee.Name = employee?.Name;
                newInq.employee.Id = employee?.Idemployee != null ? employee.Idemployee : 0;
                list.Add(newInq);

            }
            return list;
        }

        public async Task<List<Inquiry>> GetInquiries()
        {
            return await _context.Inquiries.ToListAsync();
        }

        public async Task<Inquiry?> GetInquiryById(int id)
        {
            var inquiry = await _context.Inquiries.FindAsync(id);
            return inquiry;
        }

        public bool InquiryExists(int id)
        {
            var inquiry = _context.Inquiries.Where(c => c.Idinquirie == id).FirstOrDefault();
            if (inquiry == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateInquiry(int id, Inquiry inquiry)
        {
            _context.Entry(inquiry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InquiryExists(id))
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
