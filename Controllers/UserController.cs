using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Repositories;
using Shop.Services;

namespace Shop.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
                    [FromServices] UserRepository repository,
                    [FromBody]User model)
        {
            var user = repository.Get(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("anonymous")]
        public string Anonymous()
        {
            return "Qualquer um pode ver";
        }

        [HttpGet]
        [Authorize]
        [Route("authenticated")]
        public string Authenticated()
        {
            return "Qualquer usuário autenticado";
        }

        [HttpGet]
        [Authorize(Roles = "employee")]
        [Route("employee")]
        public string Employee()
        {
            return "Somente funcionários";
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        [Route("manager")]
        public string Manager()
        {
            return "Somente gerentes";
        }
    }
}