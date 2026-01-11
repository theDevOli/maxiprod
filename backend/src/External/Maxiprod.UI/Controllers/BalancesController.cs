using Maxiprod.Application.ServicesContracts.BalanceContracts;
using Maxiprod.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Maxiprod.UI.Controllers
{
    /// <summary>
    /// Controller for retrieving financial balances by categories and people.
    /// </summary>
    [Route("v1/api/[controller]")]
    [ApiController]
    public class BalancesController(IBalanceService balanceService) : ControllerBase
    {
        /// <summary>
        /// Retrieves the balances for all categories.
        /// </summary>
        /// <returns>
        /// A <see cref="CategoryBalanceViewModel"/> representing the balances of all categories.
        /// </returns>
        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategoriesBalanceAsync()
        {
            var balanceDto = await balanceService.GetBalanceAsync();

            return Ok(CategoryBalanceViewModel.FromDto(balanceDto));
        }

        /// <summary>
        /// Retrieves the balances for all people.
        /// </summary>
        /// <returns>
        /// A <see cref="PersonBalanceViewModel"/> representing the balances of all people.
        /// </returns>
        [HttpGet("People")]
        public async Task<IActionResult> GetPeopleBalanceAsync()
        {
            var balanceDto = await balanceService.GetBalanceAsync();

            return Ok(PersonBalanceViewModel.FromDto(balanceDto));
        }
    }
}
