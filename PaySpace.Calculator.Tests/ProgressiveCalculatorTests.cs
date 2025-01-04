using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Calculators;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class ProgressiveCalculatorTests
    {
        private readonly CalculatorType calculatorType = CalculatorType.Progressive;

        private Mock<ICalculatorSettingsService> mockCalculatorSettingsService = new();

        private ProgressiveCalculator progressiveCalculator;

        [SetUp]
        public void Setup()
        {
            mockCalculatorSettingsService
                .Setup(x => x.GetSettingsAsync(calculatorType))
                .Returns(Task.FromResult(new List<CalculatorSetting>()
                    {
                        new() { Id = 1, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 10, From = 0, To = 8350 },
                        new() { Id = 2, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 15, From = 8350, To = 33950 },
                        new() { Id = 3, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 25, From = 33950, To = 82250 },
                        new() { Id = 4, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 28, From = 82250, To = 171550 },
                        new() { Id = 5, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 33, From = 171550, To = 372950 },
                        new() { Id = 6, Calculator = CalculatorType.Progressive, RateType = RateType.Percentage, Rate = 35, From = 372950, To = null }
                    })
                );

            progressiveCalculator = new ProgressiveCalculator(mockCalculatorSettingsService.Object);
        }

        [TestCase(0, 0)]
        [TestCase(50, 5)]
        [TestCase(8350, 835)]
        [TestCase(8351, 1252.65)]
        [TestCase(33951, 8487.75)]
        [TestCase(82251, 23030.28)]
        [TestCase(171550, 48034)]
        [TestCase(171555, 56613.15)]
        [TestCase(999999, 349999.65)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            var tax = await progressiveCalculator.CalculateAsync(income);
            Assert.That(tax.Tax, Is.EqualTo(expectedTax));
            Assert.That(tax.Calculator, Is.EqualTo(calculatorType));
        }

        [TestCase(-1)]
        [TestCase(-99999)]
        public void Calculate_Should_Throw_Exception(decimal income)
        {
            Assert.ThrowsAsync<InvalidOperationException>(() => progressiveCalculator.CalculateAsync(income));
        }
    }
}