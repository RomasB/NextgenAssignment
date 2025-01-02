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

            var response = await httpClient.GetAsync($"{calculatorSettings.Value.ApiUrl}api/posta1code"); 
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Cannot fetch postal codes, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<List<PostalCode>>() ?? [];
        }

        public async Task<List<CalculatorHistory>> GetHistoryAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            // https://localhost:7119/api/Calculator/calculate-tax

            throw new NotImplementedException();
        }
    }
}