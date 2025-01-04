using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Extensions
{
    public static class DecimalExtentions
    {
        public static decimal ToTax(this decimal number, CalculatorSetting setting)
        {
            return setting.RateType == RateType.Percentage ? number * setting.Rate / 100 : setting.Rate;
        }
    }
}
