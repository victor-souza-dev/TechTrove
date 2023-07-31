using AutoMapper;
using Back.Models.Entities;
using Back.Models.Input;
using Back.Models.View;
using Back.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(UserInputLogin data) {
            var user = _mapper.Map<User>(data);
            try
            {
                string result = _service.Login(user);
                return Ok(result);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
                {
                    string token = authorizationHeader.Substring("Bearer ".Length);

                    var payloadData = _service.Me(token);

                    return Ok(payloadData);
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> result = _service.GetAll();
            var viewModel = _mapper.Map<List<UserView>>(result);

            return Ok(viewModel);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                User result = _service.GetById(id);
                var viewModel = _mapper.Map<UserView>(result);
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post(UserInputCreate data)
        {
            var user = _mapper.Map<User>(data);

            try
            {
                _service.Created(user);
                return Ok("Usuário criado com sucesso!");
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UserInputUpdate user)
        {
            bool result = _service.Update(id, user);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            bool result = _service.Delete(id);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}