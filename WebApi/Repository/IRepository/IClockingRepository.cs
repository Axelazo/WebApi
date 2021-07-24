using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    public interface IClockingRepository
    {

        bool CreateClocking(ClockingModel clocking);

        bool InsertClocking(DateTime dateTime, int input);

        ClockingModel ViewClocking(int id);

        List<ClockingModel> ListEmployeeClockings(int employee_id);

        bool DeleteClocking(int id);

        public bool ModifyClocking(int id);
    }
}
