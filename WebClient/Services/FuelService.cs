using RentACarBlazorServer.Models;
using System.Net.Http.Json;
using WebClient.Models;

namespace WebClient.Services;

public class FuelService
{
    private readonly HttpClient _httpClient;
    public FuelService(HttpClient httpClient)
    => (_httpClient) = (httpClient);

    public async Task<ListResponse<Fuel>> GetAll()
    {
        var result = await _httpClient.GetFromJsonAsync<ListResponse<Fuel>>("Fuels/getall");

        return result;
    }
}