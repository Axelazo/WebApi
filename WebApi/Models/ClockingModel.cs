using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ClockingModel
    {
        public int Id { get; set; }
        public int EmployeeId{ get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut{ get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
