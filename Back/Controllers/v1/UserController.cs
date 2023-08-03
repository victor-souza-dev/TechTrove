using AutoMapper;
using Back.Models.Entities;
using Back.Models.Input;
using Back.Models.View;
using Back.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IMemoryCache _memoryCache;
        public const string ME_KEY = "Me";

        public UserController(IUserService service, IMapper mapper, IMemoryCache memoryCache)
        {
            _service = service;
            _mapper = mapper;
            _memoryCache = memoryCache;

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
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                if(_memoryCache.TryGetValue(ME_KEY, out Dictionary<string, string> me))
                {
                    return Ok(me);
                }

                var memoryCacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                    SlidingExpiration = TimeSpan.FromSeconds(1200)
                };

                string token = authorizationHeader.Substring("Bearer ".Length);

                var payloadData = _service.Me(token);

                _memoryCache.Set(ME_KEY, payloadData, memoryCacheEntryOptions);

                return Ok(payloadData);
            }

            return Unauthorized();
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