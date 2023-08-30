using Repository.GeneratedModels;
using Services.Appointments;
using Services.Contacts;
using Services.DTO;
using Services.Employees;
using Services.FuncRef;
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
        private readonly IEmployeeRef _iEmployeeRef;
        private readonly IRoomsRef _iRoomRef;
        private readonly IContactsData _iContactsData;
        private readonly IWorkHourRef _iWorkHourRef;

        public ScheduleData(IAppointmentsData appointmentsData, IEmployeeRef employeeRef, IRoomsRef roomsRef, IContactsData contactsData, IWorkHourRef workHourRef)
        {
            _iAppintmentsData = appointmentsData;
            _iEmployeeRef = employeeRef;
            _iRoomRef = roomsRef;
            _iContactsData = contactsData;
            _iWorkHourRef = workHourRef;
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
                scheduleDto.colorWorker = await _iEmployeeRef.GetColorById(appointment.Idemployee);
                scheduleDto.nameRoom = await _iRoomRef.GetNameRoom(appointment.Idroom);
                scheduleDto.shift = (char?)await _iWorkHourRef.GetShiftEmployee(appointment.Idemployee, appointment.Timestart);
                scheduleDto.firstName = contact?.Firstname;
                scheduleDto.lastName = contact?.Lastname;
                scheduleDto.note = appointment.Remark;
                scheduleDto.phone1 = contact?.Phonenumber1;
                scheduleDto.phonen2 = contact?.Phonenumber2;
                scheduleDto.phone3 = contact?.Phonenumber3;
                scheduleDto.detailsType = appointment.Duration != 0 ? appointment.Duration.ToString() : (appointment.Area != null ? appointment.Area : null);
                list.Add(scheduleDto);
            }
            return list;
        }

        public async Task<List<RoomScheduleDto>> GetAllSchedules(bool regular)
        {
            var rooms = await _iRoomRef.GetAllRooms();
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
                roomScheduleDto.listTreatments = await _iRoomRef.GetTreatmentsForRoom(room.Idroom);
                var employees = await _iRoomRef.GetAllEmployeesForRoom(room.Idroom);
                roomScheduleDto.Employees = await _iEmployeeRef.GetEmployeesForSchedule(employees, regular, room.Idroom);
                list.Add(roomScheduleDto);
            }
            return list;
        }

        //public async Task<List<RoomScheduleDto>> GetAllSchedulesForWeek(DateOnly date)
        //{
        //    int day = (int)date.DayOfWeek + 1;
        //    var rooms = await _iRoomsData.GetAllRooms();
        //    var list = new List<RoomScheduleDto>();
        //    foreach (var room in rooms)
        //    {
        //        if (room == null)
        //        {
        //            continue;
        //        }
        //        var roomScheduleDto = new RoomScheduleDto();
        //        roomScheduleDto.IdRoom = room.Idroom;
        //        roomScheduleDto.nameRoom = room.Nameroom;
        //        roomScheduleDto.listTreatments = await _iRoomsData.GetTreatmentsForRoom(room.Idroom);
        //        var employees = await _iRoomsData.GetAllEmployeesForRoom(room.Idroom);
        //        roomScheduleDto.Employees = await _iEmployeesData.GetEmployeesForScheduleForWeek(employees, date, room.Idroom);
        //        list.Add(roomScheduleDto);
        //    }
        //    return list;
        //}
    }
}

// dd

