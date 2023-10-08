using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Inquiries
{
    public interface IInquiriesData
    {
        Task<List<Inquiry>> GetInquiries();

        Task<List<InquiryDto>> GetAllInquiriesById(int id);

        Task<List<InquiryDto>?> GetAllInquiries(int id);

        //Task<bool> HaveNewInquiriesById(int id);

        Task<Inquiry?> GetInquiryById(int id);

        Task<bool> UpdateInquiry(int id, Inquiry inquiry);

        Task<bool> CreateInquiry(Inquiry inquiry);

        Task<bool> DeleteInquiry(int id);

        bool InquiryExists(int id);

    }
}
