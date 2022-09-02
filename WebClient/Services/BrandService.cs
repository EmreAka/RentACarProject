using RentACarBlazorServer.Models;
using System.Net.Http.Json;

namespace WebClient.Services;

public class BrandService
{
    private readonly HttpClient _httpClient;
    public BrandService(HttpClient httpClient)
    => (_httpClient) = (httpClient);

    public async Task<ListResponse<Brand>> GetAll()
    {
        var result = await _httpClient.GetFromJsonAsync<ListResponse<Brand>>("Brands/getall");
        return result;
    }
}