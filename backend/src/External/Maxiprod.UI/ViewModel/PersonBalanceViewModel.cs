using Maxiprod.Application.DTO;

namespace Maxiprod.UI.ViewModel
{
    /// <summary>
    /// ViewModel to display the balance of people, including the overall total.
    /// </summary>
    public class PersonBalanceViewModel
    {
        /// <summary>
        /// List of people with their respective balances (income, expense, and net balance).
        /// </summary>
        public IEnumerable<BalanceDtoPerson> People { get; set; } = default!;

        /// <summary>
        /// Overall total statistics of all people.
        /// </summary>
        public BalanceDto TotalStatistic { get; set; } = default!;

        /// <summary>
        /// Converts a <see cref="BalanceListDto"/> into a <see cref="PersonBalanceViewModel"/>.
        /// </summary>
        /// <param name="dto">The DTO containing people balances and overall total.</param>
        /// <returns>A <see cref="PersonBalanceViewModel"/> instance populated with data from the DTO.</returns>
        public static PersonBalanceViewModel FromDto(BalanceListDto dto)
            => new PersonBalanceViewModel()
            {
                People = dto.People,
                TotalStatistic = dto.TotalStatistic
            };
    }
}
