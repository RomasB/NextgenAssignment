﻿using MapsterMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [EnableCors("TaxCalculationAppPolicy")]
    public sealed class HistoryController(IHistoryService historyService, IMapper mapper, ILogger<HistoryController> logger) : ControllerBase
    {
        [HttpGet("history")]
        public async Task<ActionResult<List<CalculatorHistory>>> History()
        {
            try
            {
                var history = await historyService.GetHistoryAsync();

                return this.Ok(mapper.Map<List<CalculatorHistoryDto>>(history));

            }
            catch (HistoryException e)
            {
                logger.LogError(e, e.Message);

                return this.BadRequest(e.Message);
            }
        }
    }
}