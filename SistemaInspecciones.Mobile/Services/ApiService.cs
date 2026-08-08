using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace SistemaInspecciones.Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenService _tokenService;

        // IMPORTANTE: cambia esta URL según tu entorno de pruebas (ver nota abajo)
        public const string BaseUrl = "https://192.168.1.17:7178/";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiService(TokenService tokenService)
        {
            _tokenService = tokenService;

#if DEBUG
            var handler = new HttpsClientHandlerService().GetPlatformMessageHandler();
            _httpClient = handler is not null ? new HttpClient(handler) : new HttpClient();
#else
            _httpClient = new HttpClient();
#endif
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        private async Task PrepararHeadersAsync()
        {
            var token = await _tokenService.ObtenerTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(token)
                ? null
                : new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            await PrepararHeadersAsync();
            var response = await _httpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>(JsonOptions);
        }

        public async Task<(bool Success, T? Data, string? Error)> PostAsync<T>(string endpoint, object body)
        {
            await PrepararHeadersAsync();
            var response = await _httpClient.PostAsJsonAsync(endpoint, body);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, default, error);
            }

            var data = await response.Content.ReadFromJsonAsync<T>(JsonOptions);
            return (true, data, null);
        }

        public async Task<bool> PutAsync(string endpoint, object body)
        {
            await PrepararHeadersAsync();
            var response = await _httpClient.PutAsJsonAsync(endpoint, body);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PatchAsync(string endpoint, object body)
        {
            await PrepararHeadersAsync();
            var content = JsonContent.Create(body);
            var response = await _httpClient.PatchAsync(endpoint, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            await PrepararHeadersAsync();
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        public HttpClient RawClient => _httpClient;
    }
}