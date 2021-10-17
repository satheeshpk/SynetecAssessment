using SynetecAssessmentApi.Domain;
using System.Collections;
using System.Collections.Generic;

namespace SynetecAssessmentApi.Tests
{
    public class EmployeesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {   
            yield return new object[] { TestData.employees[0], 50000, 4581 };
            yield return new object[] { TestData.employees[1], 60000, 8247 };
            yield return new object[] { TestData.employees[2], 70000, 10156 };
            yield return new object[] { TestData.employees[3], 50000, 4200 };
            yield return new object[] { TestData.employees[4], 40000, 2749 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}