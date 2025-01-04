using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Extensions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    internal sealed class FlatValueCalculator(ICalculatorSettingsService calculatorSettingsService) : IFlatValueCalculator, ITaxRateCalculator
    {
        private readonly CalculatorType calculatorType = CalculatorType.FlatValue;

        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            var settings = await calculatorSettingsService.GetSettingsAsync(calculatorType);

            var setting = settings.FirstOrDefault(x => income >= x.From && (x.To is null || income < x.To));
            if (setting is null)
            {
                throw new InvalidOperationException($"No tax setting found for income {income}.");
            }

            return new CalculateResult()
            {
                Calculator = calculatorType,
                Tax = income.ToTax(setting)
            };
        }
    }
}