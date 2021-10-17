using SynetecAssessmentApi.Core.Services;
using System.Collections;
using System.Collections.Generic;

namespace SynetecAssessmentApi.Tests
{
    public class EmployeeControllerTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] 
            { 
                1, 
                new CalculateBonusDto { TotalBonusPoolAmount = 50000 }, 
                new BonusPoolCalculatorResultDto { Amount = 4581, Employee = TestData.ConvertToDto(TestData.employees[0]) }
            };
            yield return new object[]
            {
                2,
                new CalculateBonusDto { TotalBonusPoolAmount = 60000 },
                new BonusPoolCalculatorResultDto { Amount = 8247, Employee = TestData.ConvertToDto(TestData.employees[1]) }
            };
            yield return new object[]
            {
                3,
                new CalculateBonusDto { TotalBonusPoolAmount = 70000 },
                new BonusPoolCalculatorResultDto { Amount = 10156, Employee = TestData.ConvertToDto(TestData.employees[2]) }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}