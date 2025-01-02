using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface ITaxRateCalculator
    {
        Task<CalculateResult> CalculateAsync(decimal income);
    }
}
