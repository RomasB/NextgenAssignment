namespace PaySpace.Calculator.API.Models
{
    public sealed record class CalculateRequest
    {
        public string? PostalCode { get; set; }

        public decimal Income { get; set; }
    }
}