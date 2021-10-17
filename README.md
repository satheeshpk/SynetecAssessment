# SynetecAssessment

This is the repo for completed synetec assessment. I have made a few changes to the project.

1. Added "SynetecAssessmentApi.Core" & "SynetecAssessmentApi.Services" projects
   1. SynetecAssessmentApi.Core project contains service contracts and models
   2. SynetecAssessmentApi.Services project contains the service implementations
2. I have abstracted out the Bonus Calculator from Employee Service (formerly BonusPoolService), so that I can unit test the bonus calculations seperately.
3. Used dependency injection to inject Employee Service into the controller and Bonus Calcuator to employee service.
4. Used AutoMapper for Domain to Model(Dto) mappings.
5. Used FluentValidation for api data validation to reduce the need to validate the model in the controller, keeping the controller code simple.
6. Renamed BonusPoolController to EmployeesController, as I think Employees controller should be responsible for getting all employees and calculating bonus for an employee.
7. Removed EmployeeId from "CalculateBonusDto" class, to make the api uri follow the recommended practice ('api/employees/{id}/bonus').
8. The bonus api endpoint, return 404 not found when the employee id is not found.
9. Unit test project "SynetecAssessmentApi.Tests" contains unit tests for the bonus calculator and employees controller.
