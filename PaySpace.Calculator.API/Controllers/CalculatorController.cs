using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class CalculatorController(ITaxCalculatorService taxCalculatorService, IMapper mapper, ILogger<CalculatorController> logger) : ControllerBase
    {
        [HttpPost("calculate-tax")]
        public async Task<ActionResult<CalculateResult>> Calculate(CalculateRequest request)
        {
            try
            {
                var result = await taxCalculatorService.CalculateTaxAsync(request.PostalCode, request.Income);

                return Ok(mapper.Map<CalculateResultDto>(result));
            }
            catch (CalculatorException e)
            {
                logger.LogError(e, e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}