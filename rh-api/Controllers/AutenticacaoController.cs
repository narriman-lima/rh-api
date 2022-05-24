using Microsoft.AspNetCore.Mvc;
using rh_api.Models;
using rh_api.Repositories;
using rh_api.Service;

namespace rh_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UsuarioModel dto)
        {
            var usuario = FuncionarioRepository.ObterPorUsuarioESenha(dto.Usuario, dto.Senha);

            if (usuario == null) return NotFound();

            var token = TokenService.GerarToken(usuario);
            return Ok(new
            {
                token
            });
        }
    }
}
