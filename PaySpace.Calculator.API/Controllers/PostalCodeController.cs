using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Services.Abstractions;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class PostalCodeController(IPostalCodeService postalCodeService, IMapper mapper, ILogger<PostalCodeController> logger) : ControllerBase
    {
        [HttpGet("postal-codes")]
        public async Task<ActionResult<List<PostalCodeDto>>> History()
        {
            var history = await postalCodeService.GetPostalCodesAsync();

            return this.Ok(mapper.Map<List<PostalCodeDto>>(history));
        }
    }
}