using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CRM.Web.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient, IHttpContextAccessor contextAccessor, IConfiguration config)
    {
        _httpClient = httpClient;

        var token = contextAccessor.HttpContext?.Session.GetString("JWT");
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        _httpClient.BaseAddress = new Uri(config["ApiBaseUrl"]!);
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        var res = await _httpClient.GetAsync(url);
        if (!res.IsSuccessStatusCode) return default;
        var json = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<bool> PostAsync<T>(string url, T data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var res = await _httpClient.PostAsync(url, content);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> PutAsync<T>(string url, T data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var res = await _httpClient.PutAsync(url, content);
        return res.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(string url)
    {
        var res = await _httpClient.DeleteAsync(url);
        return res.IsSuccessStatusCode;
    }
}
