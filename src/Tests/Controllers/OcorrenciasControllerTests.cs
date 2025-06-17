using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Fase4Cap7WebserviceASPNET.Main;
using Fase4Cap7WebserviceASPNET.Main.ViewModels; // Se o namespace for Main.ViewModels — ajuste conforme necessário
using Fase4Cap7WebserviceASPNET;

namespace Fase4Cap7WebserviceASPNET.Tests.Controllers
{
    public class OcorrenciasControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OcorrenciasControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_InvalidModel_ReturnsBadRequest()
        {
            var model = new OcorrenciaViewModel
            {
                DataOcorrencia = DateTime.Now,
                Tipo = "Teste",
                TipoImpactoId = 1
            };

            var response = await _client.PostAsJsonAsync("/api/Ocorrencias", model);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_ValidStatusUpdate_ReturnsNoContent()
        {
            var idExistente = 12;
            var novoStatus = JsonContent.Create("RESOLVIDO");

            var response = await _client.PutAsync($"/api/Ocorrencias/{idExistente}/status", novoStatus);

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Ocorrencia_ReturnsNoContent_OrNotFound()
        {
            var id = 11;

            var response = await _client.DeleteAsync($"/api/Ocorrencias/{id}");

            Assert.Contains(response.StatusCode, new[]
            {
                System.Net.HttpStatusCode.NoContent,
                System.Net.HttpStatusCode.NotFound
            });
        }
    }
}
