using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatValueCalculatorTests
    {
        private readonly CalculatorType calculatorType = CalculatorType.FlatValue;  
        private Mock<ICalculatorSettingsService> mockCalculatorSettingsService = new();
        private FlatValueCalculator flatValueCalculator;

        [SetUp]
        public void Setup()
        {
            mockCalculatorSettingsService
                .Setup(x => x.GetSettingsAsync(calculatorType))
                .Returns(Task.FromResult(new List<CalculatorSetting>()
                    {
                        new() { Id = 7, Calculator = calculatorType, RateType = RateType.Percentage, Rate = 5, From = 0, To = 200000 },
                        new() { Id = 8, Calculator = calculatorType, RateType = RateType.Amount, Rate = 10000, From = 200000, To = null }
                    }
                ));

            flatValueCalculator = new FlatValueCalculator(mockCalculatorSettingsService.Object);
        }

        [TestCase(0, 0)]
        [TestCase(199999, 9999.95)]
        [TestCase(100, 5)]
        [TestCase(200000, 10000)]
        [TestCase(6000000, 10000)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            var tax = await flatValueCalculator.CalculateAsync(income);
            Assert.That(tax.Tax, Is.EqualTo(expectedTax));
            Assert.That(tax.Calculator, Is.EqualTo(calculatorType));
        }

        [TestCase(-1)]
        [TestCase(-99999)]
        public void Calculate_Should_Throw_Exception(decimal income)
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => flatValueCalculator.CalculateAsync(income));
        }
    }
}