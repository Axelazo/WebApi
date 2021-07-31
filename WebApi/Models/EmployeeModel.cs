using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Dni { get; set; }

        public int Age { get; set; }

        public string BirthDate { get; set; }

        public char Gender { get; set; }

        public string Address { get; set; }

        public int Position { get; set; }

        public int Department { get; set; }

    }
}
