using RentACarBlazorServer.Models;
using System.Net.Http.Json;

namespace WebClient.Services;

public class ColourService
{
    private readonly HttpClient _httpClient;
    public ColourService(HttpClient httpClient)
    => (_httpClient) = (httpClient);

    public async Task<ListResponse<Colour>> GetAll()
    {
        var result = await _httpClient.GetFromJsonAsync<ListResponse<Colour>>("Colours/getall");

        return result;
    }
}