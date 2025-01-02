using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class ProgressiveCalculator : IProgressiveCalculator, ITaxRateCalculator
    {
        private readonly Dictionary<Func<decimal, bool>, decimal> TaxRates;

        public ProgressiveCalculator()
        {
            TaxRates = new Dictionary<Func<decimal, bool>, decimal>
            {
                { x => x > 0 && x <= 8350, 0.1M },  
                { x => x > 8350 && x <= 33950, 0.15M },
                { x => x > 33950 && x <= 82250, 0.25M },
                { x => x > 82250 && x <= 171550, 0.28M },
                { x => x > 171550 && x <= 372950, 0.33M },
                { x => x > 372950, 0.35M } 
            };
        }

        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            return new CalculateResult()
            {
                Calculator = CalculatorType.Progressive,
                Tax = income * TaxRates.First(sw => sw.Key(income)).Value
            };
        }
    }
}