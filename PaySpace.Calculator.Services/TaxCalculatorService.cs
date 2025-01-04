using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services
{
    public class TaxCalculatorService(IPostalCodeService postalCodeService, ITaxCalculator taxRateCalculator, IHistoryService historyService) : ITaxCalculatorService
    {
        public async Task<CalculateResult> CalculateTaxAsync(string postalCode, decimal income)
        {
            var calculationType = await postalCodeService.CalculatorTypeAsync(postalCode);
            if (calculationType is null)
            {
                throw new ApplicationException($"Postal code '{postalCode}' is not supported");
            }

            var result = await taxRateCalculator.CalculateAsync(calculationType.Value, income);

            await historyService.AddAsync(new CalculatorHistory
            {
                Tax = result.Tax,
                Calculator = result.Calculator,
                PostalCode = postalCode ?? "Unknown",
                Income = income,
                Timestamp = DateTime.UtcNow
            });

            return result;
        }
    }
}
