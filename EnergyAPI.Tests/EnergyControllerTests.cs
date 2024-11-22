using EnergyAPI.Controllers;
using EnergyAPI.Data;
using Microsoft.Extensions.Configuration;
using Xunit;
using System;
using System.IO;
//
public class EnergyControllerTests
{
    private IConfiguration GetTestConfiguration()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json") 
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
    var cpf = Guid.NewGuid().ToString("N").Substring(0, 11); 

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
