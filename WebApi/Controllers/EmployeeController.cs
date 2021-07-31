using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            Response response = new Response();

            var lista = _employeeRepository.ListEmployees();
            if (lista.Count > 0)
            {
                response.status = "Success";
                response.message = "Datos extraidos correctamente";
                response.data = lista;
                return Ok(response);
            }

            return NotFound();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Response response = new Response();

            var data = _employeeRepository.ViewEmployee(id);
            if (data != null)
            {
                response.status = "Success";
                response.message = "Datos extraidos correctamente";
                response.data = data;
                return Ok(response);
            }

            return BadRequest();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(EmployeeModel employee)
        {
            Response response = new Response();

            var data = _employeeRepository.InsertEmployee(employee);
            if (data)
            {
                response.status = "Success";
                response.message = "Datos ingresados correctamente";
                response.data = data;
                return Ok(response);
            }

            return BadRequest();
        }


        [HttpPut]
        public IActionResult Modify(EmployeeModel employee)
        {
            Response response = new Response();

            var data = _employeeRepository.ModifyEmployee(employee);
            if (data)
            {
                response.status = "Success";
                response.message = "Datos modificados correctamente";
                response.data = data;
                return Ok(response);
            }

            return BadRequest();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response response = new Response();

            var data = _employeeRepository.DeleteEmployee(id);
            if (data)
            {
                response.status = "Success";
                response.message = "Datos borrados correctamente";
                response.data = data;
                return Ok(response);
            }

            return BadRequest();
        }

    }
}
