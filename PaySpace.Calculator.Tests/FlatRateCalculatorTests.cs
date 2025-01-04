using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatRateCalculatorTests
    {
        private readonly CalculatorType calculatorType = CalculatorType.FlatRate;

        private Mock<ICalculatorSettingsService> mockCalculatorSettingsService = new();

        private FlatRateCalculator flatRateCalculator;

        [SetUp]
        public void Setup()
        {
            mockCalculatorSettingsService
                .Setup(x => x.GetSettingsAsync(calculatorType))
                .Returns(Task.FromResult(new List<CalculatorSetting>()
                    {
                        new() { Id = 9, Calculator = CalculatorType.FlatRate, RateType = RateType.Percentage, Rate = 17.5M, From = 0, To = null }
                    })
                );

            flatRateCalculator = new FlatRateCalculator(mockCalculatorSettingsService.Object);
        }

        [TestCase(0, 0)]
        [TestCase(999999, 174999.825)]
        [TestCase(1000, 175)]
        [TestCase(5, 0.875)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            var tax = await flatRateCalculator.CalculateAsync(income);
            Assert.That(tax.Tax, Is.EqualTo(expectedTax));
            Assert.That(tax.Calculator, Is.EqualTo(calculatorType));
        }

        [TestCase(-1)]
        [TestCase(-99999)]
        public void Calculate_Should_Throw_Exception(decimal income)
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => flatRateCalculator.CalculateAsync(income));
        }
    }
}