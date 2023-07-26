using AutoMapper;
using Back.Models.Entities;
using Back.Models.Input;
using Back.Models.View;
using Back.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet] public IActionResult GetAll()
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
        [HttpPost]
        public IActionResult Post(UserInputCreate data)
        {
            var user = _mapper.Map<User>(data);

            bool result = _service.Created(user);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
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