using RentACarBlazorServer.Models;
using System.Net.Http.Json;
using MudBlazor;

namespace WebClient.Services;

public class CarService
{
    private readonly System.Net.Http.HttpClient _httpClient;
    private readonly ISnackbar _snackbar;
    public CarService(HttpClient httpClient, ISnackbar snackbar)
        => (_httpClient, _snackbar) = (httpClient, snackbar);

    public async Task<Response> AddCarWithImages(MultipartFormDataContent content)
    {
        var result = await _httpClient.PostAsync("Cars/addwithimages", content);
        if (!result.IsSuccessStatusCode)
        {
            _snackbar.Add("A problem occured", Severity.Error);

        }
        var response = await result.Content.ReadFromJsonAsync<Response>();
        return response;
    }

    public async Task<ListResponse<CarDetailDto>> GetCarDetails()
    {
        var result = await _httpClient.GetFromJsonAsync<ListResponse<CarDetailDto>>("Cars/getall");

        return result;
    }

    public async Task<ListResponse<CarDetailDto>> GetCarDetailsByBrandId(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<ListResponse<CarDetailDto>>
        ($"Cars/getcardetailsbybrandid?brandId={id}");

        return result;
    }

    public async Task<ListResponse<CarDetailDto>> GetCarDetailsByColourId(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<ListResponse<CarDetailDto>>
        ($"Cars/getcardetailsbycolourid?colourId={id}");

        return result;
    }

    public async Task<ListResponse<CarDetailDto>> GetCarDetailsByBrandIdAndColourId(int brandId, int colourId)
    {
        var result = await _httpClient.GetFromJsonAsync<ListResponse<CarDetailDto>>
        ($"Cars/getcardetailsbybrandidandcolourid?brandId={brandId}&colourId={colourId}");

        return result;
    }

    public async Task<ListResponse<CarDetailDto>> GetCarDetailByCarId(int id)
    {
        var result = await _httpClient.GetFromJsonAsync<ListResponse<CarDetailDto>>
        ($"Cars/getcardetailbycarid?carId={id}");

        return result;
    }
}