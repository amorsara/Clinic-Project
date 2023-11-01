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
        private readonly IEmployeesData _iEmployeeData;
        private readonly IRoomsData _iRoomsData;
        private readonly IContactsData _iContactsData;
        private readonly IWorkHoursData _iWorkHoursData;

        public ScheduleData(IAppointmentsData appointmentsData, IEmployeesData employeeData, IRoomsData roomsData, IContactsData contactsData, IWorkHoursData workHoursData)
        {
            _iAppintmentsData = appointmentsData;
            _iEmployeeData = employeeData;
            _iRoomsData = roomsData;
            _iContactsData = contactsData;
            _iWorkHoursData = workHoursData;
        }

        public async Task<List<ScheduleDto>> GetAllDates(DateOnly? date = null)
        {
            var list = new List<ScheduleDto>();
            var appointments = await _iAppintmentsData.GetAllAppointmentsForWeek(date);
            foreach (var appointment in appointments)
            {
                var scheduleDto = new ScheduleDto
                {
                    Id = appointmentData.Idappointment,
                    IdContact = appointmentData.Idcontact,
                    startHouer = appointmentData.Timestart,
                    endTime = appointmentData.Timeend,
                    Date = appointmentData.Date,
                    type = appointmentData.Treatmentname,
                    isRemined = appointmentData.Isremaind,
                    cancel = appointmentData.Cancle,
                    idWorker = appointmentData.Idemployee,
                    colorWorker = appointmentData.Color,
                    nameRoom = appointmentData.RoomName,
                    shift = (char?)appointmentData.Shift,
                    firstName = appointmentData.Firstname,
                    lastName = appointmentData.Lastname,
                    note = appointmentData.Remark,
                    phone1 = appointmentData.Phonenumber1,
                    phonen2 = appointmentData.Phonenumber2,
                    phone3 = appointmentData.Phonenumber3,
                    detailsType = appointmentData.Duration != 0 ? appointmentData.Duration.ToString() : appointmentData.Area,
                    isPay = appointmentData.Ispay == true ? true : false
                };
                list.Add(scheduleDto);
            }
            return list;
        }

        public async Task<List<RoomScheduleDto>> GetAllSchedules(bool regular)
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
                roomScheduleDto.IdRoom = room.Idroom;
                roomScheduleDto.nameRoom = room.Nameroom;
                roomScheduleDto.listTreatments = await _iRoomsData.GetTreatmentsForRoom(room.Idroom);
                var employees = await _iRoomsData.GetAllEmployeesForRoom(room.Idroom);
                roomScheduleDto.Employees = await _iEmployeeData.GetEmployeesForSchedule(employees, regular, room.Idroom);
                list.Add(roomScheduleDto);
            }
            return list;
        }

    }
}


