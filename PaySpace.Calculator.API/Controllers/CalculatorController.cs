using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class CalculatorController(TaxCalculator taxRateCalculator, IHistoryService historyService, IMapper mapper, ILogger<CalculatorController> logger) : ControllerBase
    {
        [HttpPost("calculate-tax")]
        public async Task<ActionResult<CalculateResult>> Calculate(CalculateRequest request)
        {
            try
            {
                var result = await taxRateCalculator.CalculateAsync(request.PostalCode, request.Income);

                await historyService.AddAsync(new CalculatorHistory
                {
                    Tax = result.Tax,
                    Calculator = result.Calculator,
                    PostalCode = request.PostalCode ?? "Unknown",
                    Income = request.Income
                });

                return this.Ok(mapper.Map<CalculateResultDto>(result));
            }
            catch (CalculatorException e)
            {
                logger.LogError(e, e.Message);

                return this.BadRequest(e.Message);
            }
        }


    }
}