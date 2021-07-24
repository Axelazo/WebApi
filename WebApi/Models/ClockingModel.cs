using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ClockingModel
    {
        public int id { get; set; }
        public int employee_id{ get; set; }
        public DateTime clock_in { get; set; }
        public DateTime clock_out{ get; set; }
        public DateTime created_at { get; set; }
    }
}
