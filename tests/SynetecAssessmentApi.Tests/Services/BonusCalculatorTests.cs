using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Services;
using System;
using System.Linq;
using Xunit;

namespace SynetecAssessmentApi.Tests.Services
{
    public class BonusCalculatorTests
    {
        private BonusCalculator _calculator;
        private int _totalSalary;

        public BonusCalculatorTests()
        {
            _calculator = new BonusCalculator();
            _totalSalary = TestData.employees.Sum(x => x.Salary);
        }

        [Fact]
        public void WhenZeroPassedForTotalSalary_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => _calculator.CalculateBonus(0, 0, 0));
        }

        [Theory]
        [ClassData(typeof(EmployeesTestData))]
        public void WhenCalculateBonus_ExpectExactBonus(Employee employee, int bonusPoolAmount, int exceptedBonus)
        {
            var bonus = _calculator.CalculateBonus(employee.Salary, _totalSalary, bonusPoolAmount);
            Assert.Equal(exceptedBonus, bonus);
        }
    }
}