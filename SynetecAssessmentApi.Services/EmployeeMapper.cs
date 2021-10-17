using AutoMapper;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Core.Services;

namespace SynetecAssessmentApi.Services
{
    /// <summary>
    /// Employee Mapper responsible for mapping employee objects.
    /// Implements the <see cref="AutoMapper.Profile" />
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class EmployeeMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeMapper"/> class.
        /// </summary>
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}