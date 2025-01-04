namespace PaySpace.Calculator.Web.Services.Models
{
    public sealed record class CalculateRequest
    {
        public required string PostalCode { get; set; }

        public required decimal Income { get; set; }
    }
}