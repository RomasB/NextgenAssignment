﻿using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    internal interface ITaxCalculator
    {
        Task<CalculateResult> CalculateAsync(string code, decimal income);
    }
}