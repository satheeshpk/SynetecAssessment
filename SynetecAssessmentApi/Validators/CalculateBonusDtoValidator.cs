using FluentValidation;
using SynetecAssessmentApi.Core.Services;

namespace SynetecAssessmentApi.Validators
{
    /// <summary>
    /// Validator class for Calculate bonus dto object passed to the api
    /// This validator will run before the action is executed and return the calling clients with 400 bad request error with details of the failure
    /// </summary>
    public class CalculateBonusDtoValidator : AbstractValidator<CalculateBonusDto>
    {
        /// <summary>Initializes a new instance of the <see cref="T:SynetecAssessmentApi.Validators.CalculateBonusDtoValidator" /> class.</summary>
        public CalculateBonusDtoValidator()
        {
            RuleFor(x => x.TotalBonusPoolAmount)
                .Must(x => x > 0)
                .WithMessage("Total bonus pool amount is not valid");
        }
    }
}