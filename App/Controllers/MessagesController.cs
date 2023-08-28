using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Messages;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        private readonly IMessageData _iMessageData;

        public MessagesController(IMessageData messageData)
        {
            _iMessageData = messageData;
        }

        [HttpGet]
        [Route("/api/messages/getmessages")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            var messages = await _iMessageData.GetMessages();
            if(messages == null)
            {
                return BadRequest();
            }
            return messages;
        }

        [HttpGet]
        [Route("/api/messages/getallmessagesbyid/{id}")]
        public async Task<ActionResult<List<MessageDto>>> GetAllMessagesById(int id)
        {
            var messages = await _iMessageData.GetAllMessagesById(id);
            if (messages == null)
            {
                return NotFound();
            }
            return messages;
        }

        [HttpGet]
        [Route("/api/messages/getmessagebyid/{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
        {
            var message = await _iMessageData.GetMessageById(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        [HttpPut]
        [Route("/api/messages/updatemessage/{id}")]
        public async Task<IActionResult> UpdateMessage(int id, MessageDto messageDto)
        {
            if (id != messageDto.id)
            {
                return NoContent();
            }
            var res = await _iMessageData.UpdateMessage(id, messageDto);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/messages/createmessage")]
        public async Task<ActionResult<MessageDto>> CreateMessage(MessageDto messageDto)
        {
            var result = await _iMessageData.CreateMessage(messageDto);
            if (result)
            {
                return CreatedAtAction("CreateMessage", new { id = messageDto.id }, messageDto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/messages/deletemessage/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {

            var isOk = await _iMessageData.DeleteMessage(id);
            if (isOk == false)
            {
                return NotFound();
            }
            return Ok(isOk);
        }

    }
}
