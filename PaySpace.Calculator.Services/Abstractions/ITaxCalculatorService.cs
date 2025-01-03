using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface ITaxCalculatorService
    {
        Task<CalculateResult> CalculateTaxAsync(string postalCode, decimal income);
    }
}
