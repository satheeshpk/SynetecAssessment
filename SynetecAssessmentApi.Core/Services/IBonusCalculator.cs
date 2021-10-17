namespace SynetecAssessmentApi.Core
{
    /// <summary>
    /// IBonusCalculator provides the contract for implementing bonus calculator
    /// This abstracts the calculator functionlity and used with depedency injection.
    /// </summary>
    public interface IBonusCalculator
    {
        /// <summary>
        /// Calculates the bonus.
        /// </summary>
        /// <param name="employeeSalary">The employee salary.</param>
        /// <param name="totalSalary">The total salary.</param>
        /// <param name="bonusPoolAmount">The bonus pool amount.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.DivideByZeroException"></exception>
        int CalculateBonus(int employeeSalary, int totalSalary, int bonusPoolAmount);
    }
}