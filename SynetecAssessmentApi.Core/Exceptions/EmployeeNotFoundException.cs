using System;

namespace SynetecAssessmentApi.Core.Exceptions
{
    /// <summary>
    /// Employee Not Found Exception.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EmployeeNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeNotFoundException"/> class.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        public EmployeeNotFoundException(int employeeId)
            : base($"Employee with id '{employeeId}' cannot be found")
        {
        }
    }
}