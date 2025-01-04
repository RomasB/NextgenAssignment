namespace PaySpace.Calculator.API.Models
{
    public sealed record class PostalCodeDto
    {
        public required string Code { get; set; }

        public required string Calculator { get; set; }
    }
}