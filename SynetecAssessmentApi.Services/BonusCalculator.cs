using SynetecAssessmentApi.Core;
using System;

namespace SynetecAssessmentApi.Services
{
    /// <summary>
    /// Bonus Calculator responsible for calculating the bonus.
    /// This functionlity is extracted out from old BonusPoolService. This allows us to unit test the bonus calculation
    /// Implements the <see cref="SynetecAssessmentApi.Core.IBonusCalculator" />
    /// </summary>
    /// <seealso cref="SynetecAssessmentApi.Core.IBonusCalculator" />
    public class BonusCalculator : IBonusCalculator
    {
        /// <summary>
        /// Calculates the bonus.
        /// </summary>
        /// <param name="employeeSalary">The employee salary.</param>
        /// <param name="totalSalary">The total salary.</param>
        /// <param name="bonusPoolAmount">The bonus pool amount.</param>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.DivideByZeroException"></exception>
        public int CalculateBonus(int employeeSalary, int totalSalary, int bonusPoolAmount)
        {
            if (totalSalary == 0)
            {
                throw new DivideByZeroException();
            }

            // calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employeeSalary / (decimal)totalSalary;
            return (int)(bonusPercentage * bonusPoolAmount);
        }
    }
}