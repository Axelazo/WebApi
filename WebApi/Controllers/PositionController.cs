using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;
using WebApi.Repository.IRepository;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionRepository _repoPosition;

        public PositionController(IPositionRepository repoPosition)
        {
            this._repoPosition = repoPosition;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            Response response = new Response();

            var lista = _repoPosition.listPositions();
            if(lista.Count > 0)
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

            var data = _repoPosition.viewPosition(id);
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
        public IActionResult Post(PositionModel position)
        {
            Response response = new Response();

            var data = _repoPosition.insertPosition(position);
            if(data)
            {
                response.status = "Success";
                response.message = "Datos ingresados correctamente";
                response.data = data;
                return Ok(response);
            }

            return NotFound();
        }


        [HttpPut]
        public IActionResult Modify(PositionModel position)
        {
            Response response = new Response();

            var data = _repoPosition.modifyPosition(position);
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

            var data = _repoPosition.deletePosition(id);
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
