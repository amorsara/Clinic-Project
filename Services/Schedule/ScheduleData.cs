using Repository.GeneratedModels;
using Services.Appointments;
using Services.Contacts;
using Services.DTO;
using Services.Employees;
using Services.Rooms;
using Services.WorkHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Schedule
{
    public class ScheduleData : IScheduleData
    {
        private readonly IAppointmentsData _iAppintmentsData;
        private readonly IEmployeesData _iEmployeesData;
        private readonly IRoomsData _iRoomsData;
        private readonly IContactsData _iContactsData;
        private readonly IWorkHoursData _iWorkHoursData;

        public ScheduleData(IAppointmentsData appointmentsData, IEmployeesData employeesData, IRoomsData roomsData, IContactsData contactsData, IWorkHoursData workHoursData)
        {
            _iAppintmentsData = appointmentsData;
            _iEmployeesData = employeesData;
            _iRoomsData = roomsData;
            _iContactsData = contactsData;
            _iWorkHoursData = workHoursData;
        }

        public async Task<List<ScheduleDto>> GetAllDates()
        {
            var list = new List<ScheduleDto>();
            var appointments = await _iAppintmentsData.GetAllAppointments();
            int cnt = 0;
            foreach (var appointment in appointments)
            {
                var scheduleDto = new ScheduleDto();
                var contact = await _iContactsData.GetContactById(appointment.Idcontact);
                scheduleDto.Id = cnt++;
                scheduleDto.StartHouer = appointment.Timestart;
                scheduleDto.EndHouer = appointment.Timeend;
                scheduleDto.Date = appointment.Date;
                scheduleDto.Type = appointment.Treatmentname;
                scheduleDto.IsRemined = appointment.Isremaind;
                scheduleDto.Cancel = appointment.Cancle;
                scheduleDto.IdEmployee = appointment.Idemployee;
                scheduleDto.ColorEmployee = await _iEmployeesData.GetColorById(appointment.Idemployee);
                scheduleDto.NameRoom = await _iRoomsData.GetNameRoom(appointment.Idroom);
                scheduleDto.Shift = (int)await _iWorkHoursData.GetShiftEmployee(appointment.Idemployee, appointment.Timestart);
                scheduleDto.FirstName = contact?.Firstname;
                scheduleDto.LastName = contact?.Lastname;
                scheduleDto.Remark = contact?.Remark;
                scheduleDto.Phonenumber1 = contact?.Phonenumber1;
                scheduleDto.Phonenumber2 = contact?.Phonenumber2;
                scheduleDto.Phonenumber3 = contact?.Phonenumber3;
                list.Add(scheduleDto);
            }
            return list;
        }

        public async Task<List<RoomScheduleDto>> GetAllSchedules()
        {
            var rooms = await _iRoomsData.GetAllRooms();
            var list = new List<RoomScheduleDto>();
            foreach(var room in rooms)
            {
                if(room == null)
                {
                    continue;
                }
                var roomScheduleDto = new RoomScheduleDto();
                roomScheduleDto.NameRoom = room.Nameroom;
                var employees = await _iRoomsData.GetAllEmployeesForRoom(room.Idroom);
                roomScheduleDto.Employees = await _iEmployeesData.GetEmployeesForSchedule(employees);
                list.Add(roomScheduleDto);
            }
            return list;
        }

    }
}

