using RentACarBlazorServer.Models;
using System.Net.Http.Json;
using WebClient.Models;

namespace WebClient.Services;

public class EngineService
{
    private readonly HttpClient _httpClient;
    public EngineService(HttpClient httpClient)
    => (_httpClient) = (httpClient);

    public async Task<ListResponse<Engine>> GetAll()
    {
        var result = await _httpClient.GetFromJsonAsync<ListResponse<Engine>>("Engines/getall");

        return result;
    }
}