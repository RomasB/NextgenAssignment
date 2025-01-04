using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.AppSettings;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services
{
    public class CalculatorHttpService(IOptions<CalculatorSettings> calculatorSettings) : ICalculatorHttpService
    {
        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{calculatorSettings.Value.ApiUrl}/api/postalcode/postal-codes"); 
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot fetch postal codes, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<List<PostalCode>>() ?? [];
        }

        public async Task<List<CalculatorHistory>> GetHistoryAsync()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{calculatorSettings.Value.ApiUrl}/api/history/history");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot fetch calculation history, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<List<CalculatorHistory>>() ?? [];
        }

        public async Task<CalculateResult?> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync($"{calculatorSettings.Value.ApiUrl}/api/calculator/calculate-tax", calculationRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot fetch calculatios result, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<CalculateResult>() ?? null;
        }
    }
}