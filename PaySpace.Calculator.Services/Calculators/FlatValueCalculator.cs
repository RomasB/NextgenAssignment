using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatValueCalculator : IFlatValueCalculator, ITaxRateCalculator
    {
        private const decimal TaxRate = 0.05M;
        private const int TaxAmmount = 10000;
        private const int FixedTaxFloor = 20000;

        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            return new CalculateResult()
            {
                Calculator = CalculatorType.FlatValue,
                Tax = (income < FixedTaxFloor) ? TaxAmmount : income * TaxRate
            };
        }
    }
}