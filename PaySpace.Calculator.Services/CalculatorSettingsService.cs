﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.Services
{
    internal sealed class CalculatorSettingsService(CalculatorContext context, IMemoryCache memoryCache) : ICalculatorSettingsService
    {
        public Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType)
        {
            return memoryCache.GetOrCreateAsync($"CalculatorSettings:{calculatorType}", entry =>
            {
                return context.Set<CalculatorSetting>().AsNoTracking().Where(x => x.Calculator == calculatorType).ToListAsync();
            })!;
        }
    }
}