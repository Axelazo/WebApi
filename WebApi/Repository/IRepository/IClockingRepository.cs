using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    public interface IClockingRepository
    {

        bool CreateClocking(int employeeId);

        bool InsertClocking(InsertClockingModel insertClocking);

        ClockingModel ViewClocking(int id);

        List<ClockingModel> ListEmployeeClockings(int employee_id);

        bool DeleteClocking(int id);

        public bool ModifyClocking(int id);
    }
}
