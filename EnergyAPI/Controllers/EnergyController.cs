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

        [HttpPost("insertUsuario")]
        public IActionResult InsertUsuario(string nome, string email, string senha, string cpf)
        {
            _dbContext.InsertUsuario(nome, email, senha, cpf);
            return Ok("Usuário inserido com sucesso!");
        }

        [HttpPost("insertDispositivo")]
        public IActionResult InsertDispositivo(int idUsuario, string nomeDispositivo, string tipo, string localizacao)
        {
            _dbContext.InsertDispositivo(idUsuario, nomeDispositivo, tipo, localizacao);
            return Ok("Dispositivo inserido com sucesso!");
        }

        [HttpPost("insertConsumo")]
        public IActionResult InsertConsumo(int idDispositivo, DateTime dataHora, decimal consumoKwh)
        {
            _dbContext.InsertConsumo(idDispositivo, dataHora, consumoKwh);
            return Ok("Consumo inserido com sucesso!");
        }

        [HttpPost("insertCustoEnergia")]
        public IActionResult InsertCustoEnergia(DateTime dataInicio, DateTime dataFim, decimal custoPorKwh)
        {
            _dbContext.InsertCustoEnergia(dataInicio, dataFim, custoPorKwh);
            return Ok("Custo de energia inserido com sucesso!");
        }

        [HttpPost("insertRelatorio")]
        public IActionResult InsertRelatorio(int idUsuario, int idCusto, DateTime periodoInicio, DateTime periodoFim, decimal consumoTotalKwh, decimal custoTotal)
        {
            _dbContext.InsertRelatorio(idUsuario, idCusto, periodoInicio, periodoFim, consumoTotalKwh, custoTotal);
            return Ok("Relatório inserido com sucesso!");
        }

        
    }
}
