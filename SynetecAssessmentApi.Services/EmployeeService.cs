using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Core;
using SynetecAssessmentApi.Core.Exceptions;
using SynetecAssessmentApi.Core.Services;
using SynetecAssessmentApi.Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    /// <summary>
    /// Employee Service is responsible for employee related data.
    /// Implements the <see cref="SynetecAssessmentApi.Core.Services.IEmployeeService" />
    /// </summary>
    /// <seealso cref="SynetecAssessmentApi.Core.Services.IEmployeeService" />
    public class EmployeeService : IEmployeeService
    {
        private readonly IBonusCalculator _bonusCalculator;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="bonusCalculator">The bonus calculator.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="System.ArgumentNullException">bonusCalculator</exception>
        /// <exception cref="System.ArgumentNullException">dbContext</exception>
        /// <exception cref="System.ArgumentNullException">mapper</exception>
        public EmployeeService(
            IBonusCalculator bonusCalculator,
            AppDbContext dbContext,
            IMapper mapper)
        {
            _bonusCalculator = bonusCalculator ?? throw new ArgumentNullException(nameof(bonusCalculator));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all the employees as an asynchronous operation.
        /// </summary>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var employees = await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        /// <summary>
        /// Get employee by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task&lt;EmployeeDto&gt; representing the asynchronous operation.</returns>
        /// <exception cref="SynetecAssessmentApi.Core.Exceptions.EmployeeNotFoundException"></exception>
        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == id);
            if (employee == null)
            {
                throw new EmployeeNotFoundException(id);
            }

            return _mapper.Map<EmployeeDto>(employee);
        }

        /// <summary>
        /// Calculate the employee bonus based on the bonus pool amount as an asynchronous operation.
        /// </summary>
        /// <param name="bonusPoolAmount">The bonus pool amount.</param>
        /// <param name="selectedEmployeeId">The selected employee identifier.</param>
        /// <returns>A Task&lt;BonusPoolCalculatorResultDto&gt; representing the asynchronous operation.</returns>
        /// <exception cref="SynetecAssessmentApi.Core.Exceptions.EmployeeNotFoundException"></exception>
        public async Task<BonusPoolCalculatorResultDto> CalculateEmployeeBonusAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            var employee = await GetEmployeeByIdAsync(selectedEmployeeId);
            if (employee == null)
            {
                throw new EmployeeNotFoundException(selectedEmployeeId);
            }

            //get the total salary budget for the company
            var totalSalary = await _dbContext.Employees.SumAsync(item => item.Salary);

            return new BonusPoolCalculatorResultDto
            {
                Employee = _mapper.Map<EmployeeDto>(employee),
                Amount = _bonusCalculator.CalculateBonus(employee.Salary, totalSalary, bonusPoolAmount)
            };
        }
    }
}