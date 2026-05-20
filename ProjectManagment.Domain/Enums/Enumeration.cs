namespace ProjectManagment.Domain.Enums
{
    public class Enumeration
    {
        public enum ResponseCodeEnum
        {
            Success = 200,
            BadRequest = 400,
            UnAuthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            MethodNotAllowed = 405,
            Duplicate = 409,
            NextStepNameOneNullAndOneOnlyViolated = 460,
            NextStepReferToTheSameStep = 461,
            NamesAreSystemStepsNames = 462,
            NextStepNotReferToStepsNames = 463,
            CantApplyStepAndPreviousSteps = 470,
            ShouldEnterInitiatorViolation = 471,
            NotSuitableInitaitor = 472,
            ExternalAPINotAllowed = 473,
            InvalidCredentials = 480,
            InternalServerError = 500
        }

        public enum TaskStatusEnum
        {
            Pending = 1,
            Active = 2,
            Completed = 3
        }
    }
}