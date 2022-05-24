using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using rh_api.DTO;
using rh_api.Enums;
using rh_api.Models;
using rh_api.Repositories;

namespace rh_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        [HttpPost]
        [Route("cadastrar-novo-funcionario")]
        [Authorize(Roles = "Administrador")]
        public IActionResult CadastrarFuncionario([FromBody] FuncionarioDTO cadastrar)
        {
            FuncionarioRepository.AdicionarFuncionario(new FuncionarioModel
            {
                Nome = cadastrar.Nome,
                Senha = cadastrar.Senha,
                Salario = cadastrar.Salario,
                Permissao = (PermissoesEnum)cadastrar.Permissao,
            }); 
            return Created("", cadastrar);
        }

        [HttpDelete]
        [Route("excluir-funcionario/{id}")]
        [Authorize(Roles = "Administrador, Gerente")]
        public IActionResult ExcluirFuncionario([FromRoute] int id)
        {
            var excluir = FuncionarioRepository.ListarFuncionarios().Find(x => x.Id == id);
            if (excluir.Permissao == Enums.PermissoesEnum.Gerente)
            {
                FuncionarioRepository.DeletarFuncionario(excluir);
                return Ok();
            }
            if (excluir == null)
                return NotFound("Funcionário não encontrado");

            return BadRequest($"Sem permissão de {excluir.PermissaoNome} para excluir este funcionário");
        }

        [HttpDelete]
        [Route("excluir-gerente/{id}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult ExcluirGerente([FromRoute] int id)
        {
            var apagarGerente = FuncionarioRepository.ListarFuncionarios().Find(x => x.Id == id);
            if (apagarGerente.Permissao == Enums.PermissoesEnum.Gerente)
            {
                FuncionarioRepository.DeletarFuncionario(apagarGerente);
                return Ok();
            }
            if (apagarGerente == null)
                return NotFound("Funcionário não encontrado");
            return BadRequest($"Usuário possui permissão de {apagarGerente.PermissaoNome}, verifique seu usuário.");
        }

        [HttpPatch]
        [Route("alterar-salario")]
        [Authorize(Roles = "Gerente")]
        public IActionResult AlterarSalario([FromBody] FuncionarioModel modificar)
        {
            FuncionarioRepository.EditarFuncionario(modificar, modificar.Id);
            return Ok();
        }

        /*[HttpGet]
        [Route("listar")]
        [Authorize]
        public IActionResult Listar()
        {
           var listaFuncionarios = FuncionarioRepository.ListarFuncionarios();
           return Ok(listaFuncionarios);
        }*/

        [HttpGet]
        [Route("listar")]
        [Authorize]
        public IActionResult Listar()
            => User.IsInRole(PermissoesEnum.Funcionario.GetDisplayName())
            ? Ok(FuncionarioRepository.ListarFuncionarios().Select(x => new { x.Nome, x.PermissaoNome }))
            : Ok(FuncionarioRepository.ListarFuncionarios());
    }
}

