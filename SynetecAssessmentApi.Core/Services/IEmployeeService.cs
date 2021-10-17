using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Core.Services
{
    /// <summary>
    /// IEmployeeService provides the contract for implementing a employee service.
    /// This is a replacement for the Bonus Service as employee service should be responsible for employee related functionlaities
    /// This abstracts the employee service and used with depedency injection.
    /// </summary>
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();

        Task<EmployeeDto> GetEmployeeByIdAsync(int id);

        Task<BonusPoolCalculatorResultDto> CalculateEmployeeBonusAsync(int bonusPoolAmount, int selectedEmployeeId);
    }
}