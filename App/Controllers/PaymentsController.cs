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
using Services.Payments;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IPymentsData _iPymentsData;
        private readonly IContactsData _iContactsData;

        public PaymentsController(ClinicDBContext context, IPymentsData pymentsData, IContactsData contactsData)
        {
            _context = context;
            _iPymentsData = pymentsData;
            _iContactsData = contactsData;
        }

       
        [HttpGet]
        [Route("/api/payments/getpayments")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var payments = await _iPymentsData.GetPayments();
            if (payments == null)
            {
                return NotFound();
            }
            return payments;
        }

        [HttpGet]
        [Route("/api/payments/updateallcredit/{id}/{allCredit}")]
        public async Task<IActionResult> UpdateAllCredit(int id, int allCredit)
        {
            var payments = await _iContactsData.UpdateAllCredit(id, allCredit);
            if (payments == true)
            {
                return Ok(true);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("/api/payments/getallpayments")]
        public async Task<ActionResult<IEnumerable<AccountsDto>>> GetAllPayments()
        {
            var payments = await _iPymentsData.GetAllPayments();
            if (payments == null)
            {
                return NotFound();
            }
            return payments;
        }

        [HttpGet]
        [Route("/api/payments/getpaymentsbyid/{id}")]
        public async Task<ActionResult<Payment>> GetPaymentById(int id)
        {
            var payment = await _iPymentsData.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return payment;
        }

        [HttpPut]
        [Route("/api/payments/updatepayment/{id}")]
        public async Task<IActionResult> UpdatePayment(int id, AccountsDto accountsDto)
        {
            if (id != accountsDto.id)
            {
                return NoContent();
            }

            var res = await _iPymentsData.UpdatePayment(id, accountsDto);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPost]
        [Route("/api/payments/createpayment")]
        public async Task<ActionResult<PaymentsDto>> CreatePayment(PaymentsDto paymentDto)
        {
            var payment = new Payment();
            payment.Idcontact = paymentDto.idContact;
            payment.Datepayment = paymentDto.datePayment;
            payment.Date = paymentDto.date;
            payment.Credit = paymentDto.credit;
            payment.R = paymentDto.r;
            payment.Owes = paymentDto.owes;
            payment.Pay = paymentDto.pay;
            payment.Employee = paymentDto.employee;
            payment.Treatment = paymentDto.treatment?.Count != null ? String.Join(",",paymentDto.treatment) : null;
            payment.Laser = paymentDto.laser?.Count != null ? String.Join(",", paymentDto.laser) : null;
            payment.Type = paymentDto.type;
            payment.Remark = paymentDto.remark;
            payment.Waxing = paymentDto.waxing?.Count != null ? String.Join(",", paymentDto.waxing) : null; ;
            payment.Electrolysis = paymentDto.electrolysis;
            payment.Advanced = paymentDto.AdvancedElectrolysis;

            var okAllCredit = await _iContactsData.UpdateAllCredit(paymentDto.idContact, paymentDto.allCredit);
            if(okAllCredit == false)
            {
                return BadRequest();
            }

            var result = await _iPymentsData.CreatePayment(payment);
            if (result)
            {
                return CreatedAtAction("CreatePayment", new { id = payment.Idpayment }, payment);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("/api/payments/deletepayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = await _iPymentsData.DeletePayment(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok(res);
        }


    }
}
