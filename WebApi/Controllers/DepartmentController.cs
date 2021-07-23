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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            Response response = new Response();

            var lista = _departmentRepository.listDepartments();
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

            var data = _departmentRepository.viewDepartment(id);
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
        public IActionResult Post(DepartmentModel department)
        {
            Response response = new Response();

            var data = _departmentRepository.insertDepartment(department);
            if (data)
            {
                response.status = "Success";
                response.message = "Datos ingresados correctamente";
                response.data = data;
                return Ok(response);
            }

            return NotFound();
        }


        [HttpPut]
        public IActionResult Modify(DepartmentModel department)
        {
            Response response = new Response();

            var data = _departmentRepository.modifyDepartment(department);
            if (data != null)
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

            var data = _departmentRepository.deleteDepartment(id);
            if (data)
            {
                response.status = "Success";
                response.message = "Datos borrados correctamente";
                response.data = data;
                return Ok(response);
            }

            return NotFound();
        }
    }
}
