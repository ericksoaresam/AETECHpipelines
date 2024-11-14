using EnergyAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EnergyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyController : ControllerBase
    {
        private readonly OracleDbContext _dbContext;

        public EnergyController(OracleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("usuario")]
        public IActionResult InserirUsuario(string nome, string email, string senha, string cpf)
        {
            _dbContext.InsertUsuario(nome, email, senha, cpf);
            return Ok("Usuário inserido com sucesso!");
        }

        [HttpPost("dispositivo")]
        public IActionResult InserirDispositivo(int idUsuario, string nomeDispositivo, string tipo, string localizacao)
        {
            _dbContext.InsertDispositivo(idUsuario, nomeDispositivo, tipo, localizacao);
            return Ok("Dispositivo inserido com sucesso!");
        }

        // Adicione outros métodos para as outras procedures
    }
}
