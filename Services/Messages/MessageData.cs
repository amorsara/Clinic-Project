using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Employees;
using Services.FuncRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messages
{
    public class MessageData : IMessageData
    {
        private readonly ClinicDBContext _context;
        private readonly IEmployeeRef _iEmployeeRef;

        public MessageData(ClinicDBContext context, IEmployeeRef employeeRef)
        {
            _context = context;
            _iEmployeeRef = employeeRef;
        }

        public async Task<bool> CreateMessage(MessageDto messageDto)
        {
            var message = new Message();
            message.Idfrom = messageDto.from?.Id != null ? messageDto.from.Id : 0;
            message.Idto = await GetAllIdTo(messageDto.to);
            message.Question = messageDto.question;
            message.Answer = messageDto.answer;

            var isExsists = MessageExists(message.Idmessage);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(message);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteMessage(int id)
        {
            if (_context.Messages == null)
            {
                return false;
            }

            var message = await GetMessageById(id);
            if(message == null)
            {
                return false;
            }

            var ok = await _context.Messages.FindAsync(message.Idmessage);
            if (ok == null)
            {
                return false;
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<string?> GetAllIdTo(List<EmployeeDetails>? to)
        {
            var ids = to?.Select(t => t.Id).ToList();
            var stringId = ids?.Count > 0 ? String.Join(",", ids) : null;
            return Task.FromResult(stringId);
        }

        public async Task<List<MessageDto>> GetAllMessagesById(int id)
        {
            var messages = await GetMessages();
            var list = new List<MessageDto>();
            foreach(var message in messages)
            {
                var ids = "" + id;
                if (message.Idfrom == id || message.Idto?.Contains(ids) == true) {
                    var mess = new MessageDto();
                    mess.id = message.Idmessage;
                    mess.question = message.Question;
                    mess.answer = message.Answer;
                    
                    var emp = await _iEmployeeRef.GetEmployeeById(message.Idfrom);
                    if(emp != null)
                    {
                        var from = new EmployeeDetails();
                        from.Name = emp.Name;
                        from.Id = emp.Idemployee;
                        from.Color = emp.Color;
                        mess.from = from;
                    }
                    var listId = message.Idto?.Split(",").ToList();
                    if(listId != null)
                    {
                        var listTo = new List<EmployeeDetails>();
                        foreach (var item in listId)
                        {
                            var e = await _iEmployeeRef.GetEmployeeById(int.Parse(item));
                            if (e != null)
                            {
                                var to = new EmployeeDetails();
                                to.Name = e.Name;
                                to.Id = e.Idemployee;
                                to.Color = e.Color;
                                listTo.Add(to);
                            }
                        }
                        mess.to = listTo;
                    }
                    list.Add(mess);
                }
            }
            return list;
        }

        public async Task<Message?> GetMessageById(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            return message;
        }

        public async Task<List<Message>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        public bool MessageExists(int id)
        {
            var room = _context.Messages.Where(m => m.Idmessage == id).FirstOrDefault();
            if (room == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateMessage(int id, MessageDto messageDto)
        {
            var message = await GetMessageById(id);
            if(message == null)
            {
                return false;
            }
            message.Idfrom = messageDto.from?.Id != null ? messageDto.from.Id : 0;
            message.Idto = await GetAllIdTo(messageDto.to);
            message.Question = messageDto.question;
            message.Answer = messageDto.answer;


            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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
