using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PaySpace.Calculator.Web.Models;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Controllers
{
    public class CalculatorController(ICalculatorHttpService calculatorHttpService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var vm = await GetCalculatorViewModelAsync();

            return View(vm);
        }

        public async Task<IActionResult> History()
        {
            return View(new CalculatorHistoryViewModel
            {
                CalculatorHistory = await calculatorHttpService.GetHistoryAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> Index(CalculateRequestViewModel request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await calculatorHttpService.CalculateTaxAsync(new CalculateRequest
                    {
                        PostalCode = request.PostalCode,
                        Income = request.Income
                    });

                    return RedirectToAction(nameof(History));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            var vm = await GetCalculatorViewModelAsync(request);

            return View(vm);
        }

        private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel? request = null)
        {
            var postalCodes = await calculatorHttpService.GetPostalCodesAsync();

            return new CalculatorViewModel
            {
                PostalCodes = new SelectList (postalCodes.Select(x => new SelectListItem { Value = x.Code, Text = x.Code }), nameof(SelectListItem.Value), nameof(SelectListItem.Text)),
                Income = request is not null ? request.Income : 0,
                PostalCode = request?.PostalCode ?? string.Empty
            };
        }
    }
}