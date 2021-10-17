using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SynetecAssessmentApi.Core.Exceptions;
using SynetecAssessmentApi.Core.Services;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    /// <summary>
    /// Employees api controller.
    /// This is a replacement for previous Bonus api Controller as employee api should be responsible for employee related api endpoints.
    /// The api action uri follows the recommended approach. See controller action for more details.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private const string PROBLEMDETAILS_EMPLOYEENOTFOUND = "Employee not found";
        
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesController"/> class.
        /// </summary>
        /// <param name="employeeService">The employee service.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="System.ArgumentNullException">employeeService</exception>
        /// <exception cref="System.ArgumentNullException">logger</exception>
        public EmployeesController(
            IEmployeeService employeeService,
            ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets all the employees.
        /// The Uri for getting all employees is 'api/employees'
        /// </summary>
        /// <returns>The action result.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _employeeService.GetEmployeesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Calculates the bonus for an employee.
        /// The Uri for getting the employee bonus is 'api/employees/{id}/bonus'.
        /// </summary>
        /// <param name="id">The employee id.</param>
        /// <param name="request">The bonus pool object.</param>
        /// <returns>The action result.</returns>
        [HttpPost("{id}/bonus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Bonus(int id, [FromBody] CalculateBonusDto request)
        {
            // if request is null return bad request
            if (request == null) return BadRequest();

            // if total amount is less thatn 0 return bad request
            if (request.TotalBonusPoolAmount < 0) return BadRequest();

            try
            {
                return Ok(await _employeeService.CalculateEmployeeBonusAsync(
                    request.TotalBonusPoolAmount,
                    id));
            }
            catch (EmployeeNotFoundException ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name}");
                return NotFound(new ProblemDetails
                {
                    Title = PROBLEMDETAILS_EMPLOYEENOTFOUND,
                    Detail = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{MethodBase.GetCurrentMethod()?.Name}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}