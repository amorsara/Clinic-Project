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

        public async Task<List<ScheduleDto>> GetAllDates()
        {
            var list = new List<ScheduleDto>();
            var appointments = await _iAppintmentsData.GetAllAppointments();
            foreach (var appointment in appointments)
            {
                var scheduleDto = new ScheduleDto();
                var contact = await _iContactsData.GetContactById(appointment.Idcontact);
                scheduleDto.Id = appointment.Idappointment;
                scheduleDto.IdContact = contact?.Idcontact;
                scheduleDto.startHouer = appointment.Timestart;
                scheduleDto.endTime = appointment.Timeend;
                scheduleDto.Date = appointment.Date;
                scheduleDto.type = appointment.Treatmentname;
                scheduleDto.isRemined = appointment.Isremaind;
                scheduleDto.cancel = appointment.Cancle;
                scheduleDto.idWorker = appointment.Idemployee;
                scheduleDto.colorWorker = await _iEmployeeData.GetColorById(appointment.Idemployee);
                scheduleDto.nameRoom = await _iRoomsData.GetNameRoom(appointment.Idroom);
                scheduleDto.shift = (char?)await _iWorkHoursData.GetShiftEmployee(appointment.Idemployee, appointment.Timestart);
                scheduleDto.firstName = contact?.Firstname;
                scheduleDto.lastName = contact?.Lastname;
                scheduleDto.note = appointment.Remark;
                scheduleDto.phone1 = contact?.Phonenumber1;
                scheduleDto.phonen2 = contact?.Phonenumber2;
                scheduleDto.phone3 = contact?.Phonenumber3;
                scheduleDto.detailsType = appointment.Duration != 0 ? appointment.Duration.ToString() : (appointment.Area != null ? appointment.Area : null);
                scheduleDto.isPay = appointment.Ispay == true ? true : false;
                list.Add(scheduleDto);
            }
            return list;
        }

        public async Task<List<ScheduleDto>> GetAllDatesForWeek(DateOnly date)
        {
            var list = new List<ScheduleDto>();
            var appointments = await _iAppintmentsData.GetAllAppointmentDataForWeek(date);
            foreach (var appointment in appointments)
            {
                var scheduleDto = new ScheduleDto
                {
                    Id = appointment.Idappointment,
                    IdContact = appointment.Idcontact,
                    startHouer = appointment.Timestart,
                    endTime = appointment.Timeend,
                    Date = appointment.Date,
                    type = appointment.Treatmentname,
                    isRemined = appointment.Isremaind,
                    cancel = appointment.Cancle,
                    idWorker = appointment.Idemployee,
                    colorWorker = appointment.Color,
                    nameRoom = appointment.RoomName,
                    shift = (char?)appointment.Shift,
                    firstName = appointment.Firstname,
                    lastName = appointment.Lastname,
                    note = appointment.Remark,
                    phone1 = appointment.Phonenumber1,
                    phonen2 = appointment.Phonenumber2,
                    phone3 = appointment.Phonenumber3,
                    detailsType = appointment.Duration != 0 ? appointment.Duration.ToString() : appointment.Area,
                    isPay = appointment.Ispay == true ? true : false,
                    discount = appointment.Discount
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


