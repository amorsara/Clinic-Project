﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
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

        public PaymentsController(ClinicDBContext context, IPymentsData pymentsData)
        {
            _context = context;
            _iPymentsData = pymentsData;
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
        public async Task<IActionResult> UpdatePayment(int id, Payment payment)
        {
            if (id != payment.Idpayment)
            {
                return NoContent();
            }

            var res = await _iPymentsData.UpdatePayment(id, payment);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }


        [HttpPost]
        [Route("/api/payments/createpayment")]
        public async Task<ActionResult<PaymentsDto>> CreatePayment(PaymentsDto payment)
        {
            var result = await _iPymentsData.CreatePayment(payment);
            if (result)
            {
                return CreatedAtAction("CreatePayment", new { id = payment.Id }, payment);
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
