using EnergyAPI.Controllers;
using EnergyAPI.Data;
using Microsoft.Extensions.Configuration;
using Xunit;
using System;
using System.IO;

public class EnergyControllerTests
{
    // Método para carregar a configuração do appsettings.json
    private IConfiguration GetTestConfiguration()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json") // Certifique-se de que este arquivo está no projeto de testes.
            .Build();

        return config;
    }

    [Fact]
    public void TestDatabaseConnection()
    {
        // Arrange
        var configuration = GetTestConfiguration();
        var dbContext = new OracleDbContext(configuration);

        // Act
        dbContext.TestConnection();

        // Assert
        Assert.NotNull(dbContext);
    }

    [Fact]
    public void InsertUsuario_AddsUsuarioToDbContext()
    {
        // Arrange
        var configuration = GetTestConfiguration();
        var dbContext = new OracleDbContext(configuration);
        var controller = new EnergyController(dbContext);

        // Act
        var result = controller.InsertUsuario("Teste", "teste@example.com", "senha123", "12345678900");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
    }

    [Fact]
    public void InsertDispositivo_AddsDispositivoToDbContext()
    {
        // Arrange
        var configuration = GetTestConfiguration();
        var dbContext = new OracleDbContext(configuration);
        var controller = new EnergyController(dbContext);

        // Act
        var result = controller.InsertDispositivo(1, "Dispositivo Teste", "Tipo Teste", "Local Teste");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
    }

    [Fact]
    public void InsertConsumo_AddsConsumoToDbContext()
    {
        // Arrange
        var configuration = GetTestConfiguration();
        var dbContext = new OracleDbContext(configuration);
        var controller = new EnergyController(dbContext);

        // Primeiro, insere um dispositivo válido
        controller.InsertDispositivo(1, "Dispositivo Teste", "Tipo Teste", "Local Teste");

        // Agora, insere o consumo
        var dataHora = DateTime.Now;
        var result = controller.InsertConsumo(1, dataHora, 123.45m);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
    }
}
