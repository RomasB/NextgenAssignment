using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface ITaxCalculator
    {
        Task<CalculateResult> CalculateAsync(CalculatorType calculatorType, decimal income);
    }
}
