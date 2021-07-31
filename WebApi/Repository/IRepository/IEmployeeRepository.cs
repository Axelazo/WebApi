using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        bool InsertEmployee(EmployeeModel employee);

        List<EmployeeModel> ListEmployees();

        EmployeeModel ViewEmployee(int id);

        bool ModifyEmployee(EmployeeModel employee);

        bool DeleteEmployee(int id);
    }
}
