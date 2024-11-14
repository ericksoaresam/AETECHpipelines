using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

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

        // Adicione os outros métodos para as outras stored procedures aqui...
    }
}
