﻿using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.TreatmentsType;
using Services.WorkHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Employees
{
    public class EmployeesData : IEmployeesData
    {

        private readonly ClinicDBContext _context;
        private readonly IWorkHoursData _iWorkHoursData;
        private readonly ITreatmentsTypeData _iTreatmentsTypeData;

        public EmployeesData(ClinicDBContext context, IWorkHoursData workHoursData, ITreatmentsTypeData treatmentsTypeData)
        {
            _context = context;
            _iWorkHoursData = workHoursData;
            _iTreatmentsTypeData = treatmentsTypeData;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            employee.Isshow = true;
            var isExsists = EmployeeExists(employee.Idemployee);
            if (isExsists)
            {
                return false;
            }
            await _context.AddAsync(employee);
            var isOk = await _context.SaveChangesAsync() >= 0;
            if (isOk)
            {
                return true;
            }
            return false;
        }

        public bool EmployeeExists(int id)
        {
            var employee = _context.Employees.Where(e => e.Idemployee == id).FirstOrDefault();
            if (employee == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.Where(e => e.Isshow == true).ToListAsync();
        }

        public async Task<List<Employee>> GetAllEmployeesForRoom(List<string>? treatmentsType)
        {
            var employees = await GetAllEmployees();
            var list = new List<Employee>();
            foreach (var employee in employees)
            {
                if(employee == null || employee.Treatmentstype == null || employee.Isshow == false)
                {
                    continue;
                }
                var flag = treatmentsType?.Where(t => employee.Treatmentstype.Contains(t));
                if(flag?.Count() > 0)
                {
                    list.Add(employee);
                }
            }
            return list;
        }

        public async Task<string?> GetColorById(int id)
        {
            var employee = await _context.Employees.Where(e => e.Idemployee == id).FirstOrDefaultAsync();
            return employee?.Color;
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee;
        }

        public async Task<List<EmployeeFieldsDto>> GetAllEmployeesFields()
        {
            var employees = await GetAllEmployees();
            var listEmployeeFields = new List<EmployeeFieldsDto>();
            foreach(var employee in employees)
            {
                var employeeFieldesDto = new EmployeeFieldsDto();
                employeeFieldesDto.Id = employee.Idemployee;
                employeeFieldesDto.nameWorker = employee.Name;
                employeeFieldesDto.colorWorker = employee.Color;
                if(employee == null || employee.Isshow == false)
                {
                    continue;
                }
                employeeFieldesDto.fields = employee.Treatmentstype?.Split(",").ToList();
                listEmployeeFields.Add(employeeFieldesDto);
            }
            return listEmployeeFields;
        }

        public async Task<List<EmployeeDto>> GetEmployeesForSchedule(List<Employee> employees, bool regular, int idRoom)
        {
            var list = new List<EmployeeDto>();
            int i = 0;
            foreach(var e in employees)
            {
                var emp = new EmployeeDto();
                emp.Id = i++;
                emp.IdWorker = e.Idemployee;
                emp.nameWorker = e.Name;
                emp.colorWorker = e.Color;
                emp.treatments = e.Treatmentstype?.Split(",").ToList();
                emp.weeklyHouers = await _iWorkHoursData.GetWorkHourByEmployee(idRoom, e.Idemployee, regular);
                list.Add(emp);
            }
            return list;
        }

        public async Task<int?> GetEmployeIdByName(string? name)
        {
            var employee = await _context.Employees.Where(e => e.Name == name).FirstOrDefaultAsync();
            return employee?.Idemployee;
        }

        public async Task<List<string>> GetAllNameEmployees()
        {
            var employees = await GetAllEmployees();
            var list = new List<string>();
            foreach (var emp in employees)
            {
                if (emp == null || emp.Name == null || emp.Isshow == false)
                {
                    continue;
                }
                list.Add(emp.Name);
            }
            return list;
        }

        public async Task<bool> UpdateEmployee(int id, Employee employee)
        {
            employee.Isshow = employee.Isshow == false ? false : true;
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        public async Task<Employee?> GetEmployeeByName(string? name)
        {
            var employees = await GetAllEmployees();
            foreach (var employee in employees)
            {
                if (employee != null && employee.Name == name && employee.Isshow == true)
                {
                    return employee;
                }
            }
            return null;
        }

        public async Task<bool> ChangeEmployees(List<List<RoomEmployeeDto>> employees)
        {
            foreach(var employee in employees)
            {
                var size = employee.Count();
                var newEmployee = new Employee();
                newEmployee.Name = employee[size - 4].c;
                newEmployee.Color = employee[size - 5].c;
                newEmployee.Password1 = employee[size - 3].c;
                newEmployee.Password2 = employee[size - 2].c;
                int id;
                if (employee[size - 1].c != null && int.TryParse(employee[size - 1].c, out id))
                {
                }
                else
                {
                    id = 0;
                }
                var list = new List<string>();
                var t = await _iTreatmentsTypeData.GetlistTreatmentstypes();
                var types = String.Join(",", t);
                foreach(var item in employee)
                {
                    if(item == null || item?.name == null)
                    {
                        continue;
                    }
                    if(item.name != "Color" && item.name != "Worker Name" && item.name != "id" && item.name != "password1" && item.name != "password2" && !types.Contains(item.name)) // check if add a new treatment type
                    {
                        var treatmentstype = new Treatmentstype();
                        treatmentstype.Nametreatment = item.name;
                        var flage = await _iTreatmentsTypeData.CreateTreatmentstype(treatmentstype);
                    }
                    if(item.c == "true")
                    {
                        list.Add(item.name);
                    }
                    newEmployee.Treatmentstype = String.Join(",", list);
                    var res = await GetEmployeeById(id);
                    if(res != null) // emplotee is exists - go to update...
                    {
                        if(newEmployee.Name != res.Name || newEmployee.Treatmentstype != res.Treatmentstype || newEmployee.Password1 != res.Password1 || newEmployee.Password2 != res.Password2 || newEmployee.Color != res.Color) // check if need to update
                        {
                            res.Name = newEmployee.Name;
                            res.Password1 = newEmployee.Password1;
                            res.Password2 = newEmployee.Password2;
                           // res.Color = newEmployee.Color;
                            res.Treatmentstype = newEmployee.Treatmentstype;
                            var result = await UpdateEmployee(res.Idemployee, res);
                        }
                    }
                    else // emplotee isnt exists - go to create
                    {
                        var s = await CreateEmployee(newEmployee);
                    }
                }
            }
            return true;
        }

        public async Task<List<List<RoomEmployeeDto>>> GetAllEmployeesWithTypes()
        {
            var employees = await GetAllEmployees();
            var types = await _iTreatmentsTypeData.GetlistTreatmentstypes();
            var listEmployees = new List<List<RoomEmployeeDto>>();
            foreach(var employee in employees)
            {
                if(employee == null || employee.Treatmentstype == null || employee.Isshow == false)
                {
                    continue;
                }
                var list = new List<RoomEmployeeDto>();
                foreach(var item in types)
                {
                    if(item == null)
                    {
                        continue ;
                    }
                    var employeeDto = new RoomEmployeeDto();
                    employeeDto.name = item;
                    if (employee.Treatmentstype.Contains(item))
                    {
                        employeeDto.c = "true";
                    }
                    else
                    {
                        employeeDto.c = "false";
                    }
                    list.Add(employeeDto);
                }
                var emp = new RoomEmployeeDto();
                emp.c = employee.Color;
                emp.name = "Color";
                var emp1 = new RoomEmployeeDto();
                emp1.c = employee.Name;
                emp1.name = "Worker Name";
                var empPass1 = new RoomEmployeeDto();
                empPass1.name = "password1";
                empPass1.c = employee.Password1;
                var empPass2 = new RoomEmployeeDto();
                empPass2.name = "password2";
                empPass2.c = employee.Password2;
                var idEmp = new RoomEmployeeDto();
                idEmp.name = "id";
                idEmp.c = "" + employee.Idemployee;
                list.Add(emp);
                list.Add(emp1);
                list.Add(empPass1);
                list.Add(empPass2);
                list.Add(idEmp);
                listEmployees.Add(list);
            }
            return listEmployees;
        }

        public async Task<List<EmployeeDetails>> GetEmployeeDetails()
        {
            var employees = await GetAllEmployees();
            var list = new List<EmployeeDetails>();
            foreach(var employee in employees)
            {
                if(employee.Isshow == false)
                {
                    continue;
                }
                var emp = new EmployeeDetails();
                emp.Id = employee.Idemployee;
                emp.Name = employee.Name;
                emp.Color = employee.Color;
                list.Add(emp);
            }
            return list;
        }

        public async Task<bool> DeleteEmployeeById(int id)
        {
            var emp = await GetEmployeeById(id);
            if(emp == null)
            {
                return false;
            }
            emp.Isshow = false;
            var isOk = await UpdateEmployee(emp.Idemployee, emp);
            return isOk;
        }

        public async Task<int?> GetIdForAdmin()
        {
            var admin = await _context.Employees.Where(e => e.Name == "Admin" && e.Isshow == true).FirstOrDefaultAsync();
            return admin?.Idemployee;
        }

        public async Task<List<string>?> GetAllTreatmentsForEmployee(int id)
        {
            var employee = await GetEmployeeById(id);
            return employee?.Treatmentstype?.Split(",").ToList();
        }

        public async Task<bool> UpdateLastMessageIdForEmployee(int id, int idMessage)
        {
            var employee = await GetEmployeeById(id);
            if(employee == null)
            {
                return false;
            }
            employee.Lastmessageread = idMessage;
            var isOk = await UpdateEmployee(id, employee);
            return isOk;
        }

        public async Task<int?> GetLastIdMessageById(int id)
        {
            var employee = await GetEmployeeById(id);
            return employee?.Lastmessageread;
        }
    }
}


