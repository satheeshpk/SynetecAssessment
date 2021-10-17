#nullable disable

namespace SynetecAssessmentApi.Core.Services
{
    public class EmployeeDto
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public DepartmentDto Department { get; set; }
    }
}