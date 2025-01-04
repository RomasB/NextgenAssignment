namespace PaySpace.Calculator.API.Models
{
    public sealed class PostalCodeDto
    {
        public required string Code { get; set; }

        public required string Calculator { get; set; }
    }
}