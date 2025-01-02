using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class HistoryController(IHistoryService historyService, IMapper mapper, ILogger<HistoryController> logger) : ControllerBase
    {
        [HttpGet("history")]
        public async Task<ActionResult<List<CalculatorHistory>>> History()
        {
            var history = await historyService.GetHistoryAsync();

            return this.Ok(mapper.Map<List<CalculatorHistoryDto>>(history));
        }
    }
}