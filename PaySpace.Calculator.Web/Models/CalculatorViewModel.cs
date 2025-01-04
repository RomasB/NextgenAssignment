using Microsoft.AspNetCore.Mvc.Rendering;

namespace PaySpace.Calculator.Web.Models
{
    public sealed class CalculatorViewModel
    {
        public required SelectList PostalCodes { get; set; }

        public required string PostalCode { get; set; }

        public required decimal Income { get; set; }
    }
}