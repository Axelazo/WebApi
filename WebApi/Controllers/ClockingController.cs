using Microsoft.AspNetCore.Http;
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
    public class ClockingController : Controller
    {
        private readonly IClockingRepository _clockingRepository;

        public ClockingController(IClockingRepository clockingRepository)
        {
            this._clockingRepository = clockingRepository;
        }

        [Route("list/{employee_id}")]
        [HttpGet]
        public IActionResult Get(int employee_id)
        {
            Response response = new Response();

            var lista = _clockingRepository.ListEmployeeClockings(employee_id);
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
        [HttpGet("{employee_id}/{id}")]
        public IActionResult Get(int employee_id, int id)
        {
            Response response = new Response();

            var data = _clockingRepository.ViewClocking(id);
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
        public IActionResult Post(DateTime created_at, int input)
        {
            Response response = new Response();

            var data = _clockingRepository.InsertClocking(created_at, input);
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
        public IActionResult Modify(int id)
        {
            Response response = new Response();

            var data = _clockingRepository.ModifyClocking(id);
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

            var data = _clockingRepository.DeleteClocking(id);
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
