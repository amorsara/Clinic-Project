using Repository.GeneratedModels;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Messages
{
    public interface IMessageData
    {

        Task<List<Message>> GetMessages();

        Task<List<MessageDto>?> GetAllMessagesById(int id);

        Task<bool> HaveNewMessageById(int id);

        Task<string?> GetAllIdTo(List<EmployeeDetails> to);

        Task<Message?> GetMessageById(int id);

        Task<bool> UpdateMessage(int id, MessageDto messageDto);

        Task<bool> CreateMessage(MessageDto messageDto);

        Task<bool> DeleteMessage(int id);

        bool MessageExists(int id);
    }
}
