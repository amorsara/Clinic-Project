using Repository.GeneratedModels;
using Services.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Home
{
    public class HomeData : IHomeData
    {

        private readonly IEmployeesData _iEmployeesData;

        public HomeData(IEmployeesData employeesData)
        {
            _iEmployeesData = employeesData;
        }

        public async Task<int> LoginUser(string? name, string? password)
        {
            if(name == null || password == null)
            {
                return 0;
            }
            var emp = await _iEmployeesData.GetEmployeeByName(name);
            if(emp == null) 
            { 
                return 0; 
            }
            if(password == emp.Password1)
            {
                if(name == "Admin")
                {
                    return 1;
                }
                return 3;
            }
            else
            {
                if (password == emp.Password2)
                {
                    if (name == "Admin")
                    {
                        return 2;
                    }
                    return 4;
                }
            }
            return 0;
        }
    }
}
