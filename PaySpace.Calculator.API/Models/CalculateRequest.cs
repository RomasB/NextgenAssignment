namespace PaySpace.Calculator.API.Models
{
    public sealed record class CalculateRequest
    {
        public required string PostalCode { get; set; }

        public required decimal Income { get; set; }
    }
}