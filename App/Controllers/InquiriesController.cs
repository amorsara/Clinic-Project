using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Inquiries;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiriesController : ControllerBase
    {

        private readonly IInquiriesData _iInquiriesData;

        public InquiriesController(IInquiriesData inquiriesData)
        {
            _iInquiriesData = inquiriesData;
        }

        
        [HttpGet]
        [Route("/api/inquiries/getinquiries")]
        public async Task<ActionResult<IEnumerable<Inquiry>>> GetInquiries()
        {
            var inquiries = await _iInquiriesData.GetInquiries();
            if (inquiries == null)
            {
                return NotFound();
            }
            return inquiries;
        }

        [HttpGet]
        [Route("/api/inquiries/getinquirybyid/{id}")]
        public async Task<ActionResult<Inquiry>> GetInquiryById(int id)
        {
            var inquiry = await _iInquiriesData.GetInquiryById(id);
            if (inquiry == null)
            {
                return NotFound();
            }
            return inquiry;
        }

        [HttpGet]
        [Route("/api/inquiries/getallinquiriesbyid/{id}")]
        public async Task<ActionResult<IEnumerable<InquiryDto>>> GetAllInquiriesById(int id)
        {
            var inquiries = await _iInquiriesData.GetAllInquiriesById(id);
            if (inquiries == null)
            {
                return NotFound();
            }
            return inquiries;
        }

        [HttpGet]
        [Route("/api/inquiries/getallinquiries/{id}")]
        public async Task<ActionResult<IEnumerable<InquiryDto>>> GetAllInquiries(int id)
        {
            var inquiries = await _iInquiriesData.GetAllInquiries(id);
            if (inquiries == null)
            {
                return NotFound();
            }
            return inquiries;
        }

        [HttpGet]
        [Route("/api/inquiries/havenewinquiriesbyid/{id}")]
        public async Task<ActionResult> HaveNewInquiriesById(int id)
        {
            var res = await _iInquiriesData.HaveNewInquiriesById(id);
            return Ok(res);
        }


        [HttpPut]
        [Route("/api/inquiries/updateinquiry/{id}")]
        public async Task<IActionResult> UpdateInquiry(int id, Inquiry inquiry)
        {
            if (id != inquiry.Idinquirie)
            {
                return NoContent();
            }
            var res = await _iInquiriesData.UpdateInquiry(id, inquiry);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/inquiries/updateinquirywrapper")]
        public async Task<ActionResult<Inquiry>> UpdateInquiryWrapper(InquiryDto inquiryDto)
        {
            var inquiry = await _iInquiriesData.GetInquiryById(inquiryDto.id);
            if(inquiry == null)
            {
                return NoContent();
            }

            inquiry.Doinquirie = inquiryDto.doInquirie;
            inquiry.Sum = inquiryDto.sum;
            inquiry.Remark = inquiryDto.remark;
            inquiry.Response = inquiryDto.response;
            inquiry.Phon = inquiryDto.phon;
            inquiry.Fullname = inquiryDto.fullname;
            inquiry.Date = inquiryDto.date;
            inquiry.Time = inquiryDto.time;
            inquiry.Status = inquiryDto.status;
            inquiry.Idemployee = inquiryDto?.employee?.Id != null ? inquiryDto.employee.Id : 0;

            var res = await _iInquiriesData.UpdateInquiry(inquiry.Idinquirie, inquiry);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();

        }

        [HttpPost]
        [Route("/api/inquiries/createinquiry")]
        public async Task<ActionResult<Inquiry>> CreateInquiry(Inquiry inquiry)
        {
            var result = await _iInquiriesData.CreateInquiry(inquiry);
            if (result)
            {
                return CreatedAtAction("CreateInquiry", new { id = inquiry.Idinquirie }, inquiry);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/api/inquiries/createinquiryWrapper")]
        public async Task<ActionResult<Inquiry>> CreateInquiryWrapper(InquiryDto inquiryDto)
        {
            var inquiry = new Inquiry();
            inquiry.Doinquirie = inquiryDto.doInquirie;
            inquiry.Sum = inquiryDto.sum;
            inquiry.Remark = inquiryDto.remark;
            inquiry.Response = inquiryDto.response;
            inquiry.Phon = inquiryDto.phon;
            inquiry.Fullname = inquiryDto.fullname;
            inquiry.Date = inquiryDto.date;
            inquiry.Time = inquiryDto.time;
            inquiry.Status = inquiryDto.status;
            inquiry.Idemployee = inquiryDto?.employee?.Id != null ? inquiryDto.employee.Id : 0;

            var result = await CreateInquiry(inquiry);
            return result; 
        }


        [HttpDelete]
        [Route("/api/inquiries/deleteinquiry/{id}")]
        public async Task<IActionResult> DeleteInquiry(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = await _iInquiriesData.DeleteInquiry(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }

    }
}
