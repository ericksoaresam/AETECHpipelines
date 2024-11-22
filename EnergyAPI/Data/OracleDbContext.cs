using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

//conectando banco
namespace EnergyAPI.Data
{
    public class OracleDbContext : IDisposable
    {
        private readonly OracleConnection _connection;

        public OracleDbContext(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("OracleDB");
            _connection = new OracleConnection(connectionString);
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public void InsertUsuario(string nome, string email, string senha, string cpf)
        {
            using var command = new OracleCommand("sp_insert_usuario", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("p_nome", OracleDbType.Varchar2).Value = nome;
            command.Parameters.Add("p_email", OracleDbType.Varchar2).Value = email;
            command.Parameters.Add("p_senha", OracleDbType.Varchar2).Value = senha;
            command.Parameters.Add("p_cpf", OracleDbType.Varchar2).Value = cpf;
            command.ExecuteNonQuery();
        }

        
        public void InsertDispositivo(int idUsuario, string nomeDispositivo, string tipo, string localizacao)
        {
            using var command = new OracleCommand("sp_insert_dispositivo", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("p_id_usuario", OracleDbType.Int32).Value = idUsuario;
            command.Parameters.Add("p_nome_dispositivo", OracleDbType.Varchar2).Value = nomeDispositivo;
            command.Parameters.Add("p_tipo", OracleDbType.Varchar2).Value = tipo;
            command.Parameters.Add("p_localizacao", OracleDbType.Varchar2).Value = localizacao;
            command.ExecuteNonQuery();
        }

       
        public void InsertConsumo(int idDispositivo, DateTime dataHora, decimal consumoKwh)
        {
            using var command = new OracleCommand("sp_insert_consumo", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("p_id_dispositivo", OracleDbType.Int32).Value = idDispositivo;
            command.Parameters.Add("p_data_hora", OracleDbType.TimeStamp).Value = dataHora;
            command.Parameters.Add("p_consumo_kwh", OracleDbType.Decimal).Value = consumoKwh;
            command.ExecuteNonQuery();
        }

       
        public void InsertCustoEnergia(DateTime dataInicio, DateTime dataFim, decimal custoPorKwh)
        {
            using var command = new OracleCommand("sp_insert_custo_energia", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("p_data_inicio", OracleDbType.Date).Value = dataInicio;
            command.Parameters.Add("p_data_fim", OracleDbType.Date).Value = dataFim;
            command.Parameters.Add("p_custo_por_kwh", OracleDbType.Decimal).Value = custoPorKwh;
            command.ExecuteNonQuery();
        }

       
        public void InsertRelatorio(int idUsuario, int idCusto, DateTime periodoInicio, DateTime periodoFim, decimal consumoTotalKwh, decimal custoTotal)
        {
            using var command = new OracleCommand("sp_insert_relatorio", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("p_id_usuario", OracleDbType.Int32).Value = idUsuario;
            command.Parameters.Add("p_id_custo", OracleDbType.Int32).Value = idCusto;
            command.Parameters.Add("p_periodo_inicio", OracleDbType.Date).Value = periodoInicio;
            command.Parameters.Add("p_periodo_fim", OracleDbType.Date).Value = periodoFim;
            command.Parameters.Add("p_consumo_total_kwh", OracleDbType.Decimal).Value = consumoTotalKwh;
            command.Parameters.Add("p_custo_total", OracleDbType.Decimal).Value = custoTotal;
            command.ExecuteNonQuery();
        }

        public void TestConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                Console.WriteLine("Conexão bem-sucedida!");
            }
            else
            {
                throw new Exception("Falha ao conectar ao banco de dados.");
            }
        }

        public OracleCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }
    }
}
