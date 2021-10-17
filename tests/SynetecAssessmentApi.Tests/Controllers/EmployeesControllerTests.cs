using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SynetecAssessmentApi.Controllers;
using SynetecAssessmentApi.Core.Exceptions;
using SynetecAssessmentApi.Core.Services;
using SynetecAssessmentApi.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace SynetecAssessmentApi.Tests
{
    public class EmployeesControllerTests
    {
        private EmployeesController _employeesController;

        public EmployeesControllerTests()
        {
            var calculator = new BonusCalculator();
            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock.Setup(x => x.GetEmployeesAsync())
                .Returns(() => Task.FromResult(TestData.employees.Select(x => TestData.ConvertToDto(x))));
            employeeServiceMock.Setup(x => x.CalculateEmployeeBonusAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int poolAmount, int id) => {
                    if (TestData.employees.Any(x => x.Id == id))
                    {
                        var employee = TestData.employees.First(x => x.Id == id);
                        var sum = TestData.employees.Sum(x => x.Salary);
                        var bonus = calculator.CalculateBonus(employee.Salary, sum, poolAmount);
                        return Task.FromResult(new BonusPoolCalculatorResultDto
                        {
                            Amount = bonus,
                            Employee = TestData.ConvertToDto(employee)
                        });
                    }
                    else
                    {
                        throw new EmployeeNotFoundException(id);
                    }
                });

            var logger = new Mock<ILogger<EmployeesController>>();

            _employeesController = new EmployeesController(employeeServiceMock.Object, logger.Object);
        }

        [Theory]
        [ClassData(typeof(EmployeeControllerTestData))]
        public async Task WhenValidEmployeeIdSpecified_ExpectValidResult(int employeeId, CalculateBonusDto calculateBonusDto, BonusPoolCalculatorResultDto expected)
        {
            var result = await _employeesController.Bonus(employeeId, calculateBonusDto);

            // assert is of type ok object result
            Assert.IsType<OkObjectResult>(result);
            var objectResult = (OkObjectResult)result;

            // assert status code is 200
            Assert.Equal(200, objectResult.StatusCode);

            // assert the result object is not null
            Assert.NotNull(objectResult.Value);

            Assert.IsType<BonusPoolCalculatorResultDto>(objectResult.Value);
            
            var obj = (BonusPoolCalculatorResultDto)objectResult.Value;

            // assert the bonus amount
            Assert.Equal(expected.Amount, obj.Amount);

            // assert the employee object is correct
            obj.Employee.Should().BeEquivalentTo(expected.Employee, options => options.ExcludingNestedObjects());
        }

        [Fact]
        public async Task WhenInvalidEmployeeIdSpecified_Excpect404NotFoundResult()
        {
            var result = await _employeesController.Bonus(50, new CalculateBonusDto { TotalBonusPoolAmount = 500000 });

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}