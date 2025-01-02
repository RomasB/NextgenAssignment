using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        private readonly Dictionary<CalculatorType, ITaxRateCalculator> TaxCalculators;
        private readonly IPostalCodeService postalCodeService;

        public TaxCalculator(IProgressiveCalculator progressiveCalculator, IFlatRateCalculator flatRateCalculator, IFlatValueCalculator flatValueCalculator, IPostalCodeService postalCodeService)
        {
            TaxCalculators = new Dictionary<CalculatorType, ITaxRateCalculator>
                {
                    { CalculatorType.Progressive, (ITaxRateCalculator)progressiveCalculator },
                    { CalculatorType.FlatRate, (ITaxRateCalculator)flatRateCalculator },
                    { CalculatorType.FlatValue, (ITaxRateCalculator)flatValueCalculator }
                    // add calculators here
                };

            this.postalCodeService = postalCodeService;
        }

        public async Task<CalculateResult> CalculateAsync(string code, decimal income)
        {
            var calculationType = await postalCodeService.CalculatorTypeAsync(code);
            if (!calculationType.HasValue)
            {
                return await TaxCalculators[calculationType.Value].CalculateAsync(income);
            }

            throw new ApplicationException("Postal code not found");
        }
    }
}