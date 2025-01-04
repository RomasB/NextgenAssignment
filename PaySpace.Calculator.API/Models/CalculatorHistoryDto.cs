namespace PaySpace.Calculator.API.Models
{
    public sealed class CalculatorHistoryDto
    {
        public required string PostalCode { get; set; }

        public required DateTime Timestamp { get; set; }

        public required decimal Income { get; set; }

        public required decimal Tax { get; set; }

        public required string Calculator { get; set; }
    }
}