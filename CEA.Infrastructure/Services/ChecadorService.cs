using CEA.Application.DTOs.Checador;
using CEA.Application.Services;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CEA.Infrastructure.Services
{
    public class ChecadorService : IChecadorService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ChecadorService> _logger;
        private const string ApiPath = "ISAPI/AccessControl/AcsEvent?format=json";

        public ChecadorService(IHttpClientFactory httpClientFactory, ILogger<ChecadorService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<AcsEventResponse> GetEventosAsync(string deviceKey, AcsEventRequestDto requestBody)
        {
            var httpClient = _httpClientFactory.CreateClient(deviceKey);

            // --- AQUÍ ESTÁ EL CAMBIO ---

            // 2. Serializa el objeto a un string JSON
            // (Usamos WriteIndented = true para que se vea bonito en tu consola)
            var jsonBody = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions { WriteIndented = true });

            // 3. Loguea el string exacto que se va a enviar
            //_logger.LogInformation("--- Preparando POST para {DeviceKey} ---", deviceKey);
            //_logger.LogInformation("URL Completa: {BaseUrl}{ApiPath}", httpClient.BaseAddress, ApiPath);
            //_logger.LogInformation("BODY Enviado:\n{JsonBody}", jsonBody);

            // 4. Crea el contenido manualmente
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // --- FIN DEL CAMBIO ---

            try
            {
                // 5. Usa PostAsync (en lugar de PostAsJsonAsync)
                var response = await httpClient.PostAsync(ApiPath, content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Respuesta recibida de {DeviceKey}: {StatusCode}", deviceKey, response.StatusCode);
                    return await response.Content.ReadFromJsonAsync<AcsEventResponse>();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error en API checador ({DeviceKey}): {StatusCode}. Respuesta: {ErrorContent}",
                        deviceKey, response.StatusCode, errorContent);
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                // Este es el error que estabas viendo. Ahora tendrás el JSON del body
                // justo antes de que este error ocurra.
                _logger.LogError(ex, "Excepción al conectar con API del checador ({DeviceKey})", deviceKey);
                return null;
            }
        }
    }
}