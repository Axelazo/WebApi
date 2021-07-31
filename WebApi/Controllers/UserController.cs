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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            Response response = new Response();

            var lista = _userRepository.ListUsers();
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

            var data = _userRepository.ViewUser(id);
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
        public IActionResult Post(UserModel user)
        {
            Response response = new Response();

            var data = _userRepository.InsertUser(user);
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
        public IActionResult Modify(UserModel user)
        {
            Response response = new Response();

            var data = _userRepository.ModifyUser(user);
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

            var data = _userRepository.DeleteUser(id);
            if (data)
            {
                response.status = "Success";
                response.message = "Datos borrados correctamente";
                response.data = data;
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public IActionResult Login(UserModel user)
        {
            Response response = new Response();

            var data = _userRepository.LoginUser(user.Username, user.Password);
            if (data == 0)
            {
                response.status = "Fallo!";
                response.message = "La contrasena ingresada no corresponde";
                response.data = data;
                return BadRequest(response);
            } else if(data == 1)
            {
                response.status = "Success";
                response.message = "Logueado";
                response.data = data;
                return Ok(response);
            }

            response.status = "Fallo";
            response.message = "Usuario no existe";
            return NotFound(response);
        }
    }
}
