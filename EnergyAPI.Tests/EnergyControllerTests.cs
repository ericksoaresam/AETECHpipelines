using EnergyAPI.Controllers;
using EnergyAPI.Data;
using Microsoft.Extensions.Configuration;
using Xunit;
using System;
using System.IO;
//
public class EnergyControllerTests
{
    // M�todo para carregar a configura��o do appsettings.json
    private IConfiguration GetTestConfiguration()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json") // Certifique-se de que este arquivo est� no projeto de testes.
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

    // Gera Email e CPF �nicos
    var email = $"teste_{Guid.NewGuid()}@example.com";
    var cpf = Guid.NewGuid().ToString("N").Substring(0, 11); // Gera um CPF fict�cio de 11 d�gitos

    // Act
    var result = controller.InsertUsuario("Teste", email, "senha123", cpf);

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
        var result = controller.InsertDispositivo(27, "Dispositivo Teste", "Tipo Teste", "Local Teste");

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
    }
}
