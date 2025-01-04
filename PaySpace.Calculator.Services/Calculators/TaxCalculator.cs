using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    public class TaxCalculator : ITaxCalculator
    {
        private readonly Dictionary<CalculatorType, ITaxRateCalculator> TaxCalculators;

        public TaxCalculator(IProgressiveCalculator progressiveCalculator, IFlatRateCalculator flatRateCalculator, IFlatValueCalculator flatValueCalculator)
        {
            TaxCalculators = new Dictionary<CalculatorType, ITaxRateCalculator>
            {
                { CalculatorType.Progressive, (ITaxRateCalculator)progressiveCalculator },
                { CalculatorType.FlatRate, (ITaxRateCalculator)flatRateCalculator },
                { CalculatorType.FlatValue, (ITaxRateCalculator)flatValueCalculator }
                // add calculator implementation here
            };
        }

        public async Task<CalculateResult> CalculateAsync(CalculatorType calculatorType, decimal income)
        {
            if (!TaxCalculators.ContainsKey(calculatorType))
            {
                throw new NotImplementedException($"Calculator type '{calculatorType}' is not implemented");
            }

            return await TaxCalculators[calculatorType].CalculateAsync(income);
        }
    }
}