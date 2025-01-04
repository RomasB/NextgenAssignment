namespace PaySpace.Calculator.Services.Exceptions
{
    public sealed class CalculatorException() : InvalidOperationException("Failed to calculate tax.");

    public sealed class HistoryException() : InvalidOperationException("Failed to fetch tax history.");

    public sealed class PostalCodesException() : InvalidOperationException("Failed to fetch postal code.");
}