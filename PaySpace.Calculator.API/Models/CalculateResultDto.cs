namespace PaySpace.Calculator.API.Models
{
    public sealed class CalculateResultDto
    {
        public required string Calculator { get; set; }

        public required decimal Tax { get; set; }
    }
}