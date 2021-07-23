using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    interface IClockingRepository
    {

        bool createClocking(ClockingModel clocking);

        bool insertClocking(DateTime dateTime, int input);

        ClockingModel viewClocking(int id);

        List<ClockingModel> listEmployeeClockings(int employee_id);

        bool deleteClocking(int id);


    }
}
