using API.Context;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UsuarioController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticador([FromBody] Usuario usuarioObj)
        {
            if (usuarioObj == null)
                return BadRequest();
            var usuario = await _appDbContext.Usuario.FirstOrDefaultAsync(x => x.Nome == usuarioObj.Nome && x.Senha == usuarioObj.Senha);
            if (usuario == null)
                return NotFound(new { Message = "Usuario nao encontrado." });
            return Ok(new { Message = $"Sucesso no login. Usuario: {usuarioObj.Nome} Senha: {usuarioObj.Senha}" });
        }

        [HttpPost("registro")]
        public async Task<IActionResult> RegistroUsuario([FromBody] Usuario usuarioObj)
        {
            if (usuarioObj == null)
                return BadRequest();
            await _appDbContext.Usuario.AddAsync(usuarioObj);
            await _appDbContext.SaveChangesAsync();
            return Ok(new { Message = "Usuario registrado com sucesso." });
        }

        [HttpGet("cpf")]
        public async Task<IActionResult> BuscarPorCPF(string cpf)
        {
            var usuario = await _appDbContext.Usuario.FirstOrDefaultAsync(x => x.CPF == cpf);
            if (usuario != null)
            {
                var response = new
                {
                    usuario.Id,
                    usuario.CPF
                };
                return Ok(response);
            }
            return NotFound();
        }
    }
}
