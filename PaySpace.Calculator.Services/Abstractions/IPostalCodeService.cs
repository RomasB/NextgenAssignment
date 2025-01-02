using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface IPostalCodeService
    {
        Task<CalculatorType?> CalculatorTypeAsync(string code);

        //Task<List<PostalCode>> GetPostalCodesAsync();
    }
}