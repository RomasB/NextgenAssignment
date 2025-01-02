using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatRateCalculator : IFlatRateCalculator, ITaxRateCalculator
    {
        private const decimal TaxRate = 0.05M;

        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            return new CalculateResult()
            {
                Calculator = CalculatorType.FlatRate,
                Tax = income * TaxRate
            };
        }
    }
}