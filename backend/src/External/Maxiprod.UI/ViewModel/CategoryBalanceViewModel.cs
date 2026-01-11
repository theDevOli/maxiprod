using Maxiprod.Application.DTO;

namespace Maxiprod.UI.ViewModel
{
    /// <summary>
    /// ViewModel to display the balance of categories, including the overall total.
    /// </summary>
    public class CategoryBalanceViewModel
    {
        /// <summary>
        /// List of categories with their respective balances (income, expense, and net balance).
        /// </summary>
        public IEnumerable<BalanceDtoCategory> Categories { get; set; } = default!;

        /// <summary>
        /// Overall total statistics of all categories.
        /// </summary>
        public BalanceDto TotalStatistic { get; set; } = default!;

        /// <summary>
        /// Converts a <see cref="BalanceListDto"/> into a <see cref="CategoryBalanceViewModel"/>.
        /// </summary>
        /// <param name="dto">The DTO containing category balances and overall total.</param>
        /// <returns>A <see cref="CategoryBalanceViewModel"/> instance populated with data from the DTO.</returns>
        public static CategoryBalanceViewModel FromDto(BalanceListDto dto)
            => new CategoryBalanceViewModel()
            {
                Categories = dto.Categories,
                TotalStatistic = dto.TotalStatistic
            };
    }
}
