using System;

namespace HCMS.GlobalConstants
{
    public static class GlobalConstant
    {
        public const string SystemEmail = "HCMS@gmail.com";

        public const string SystemName = "Human Capital Management System";

        public const string SystemShortName = "HCMS";

        public const string SystemAdministratorRole = "Administrator";

        public const string SystemManagerRole = "Manager";

        public const string SystemEmployeeRole = "Employee";

        public const string StringLenghtValidation = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ConfirmPasswordNotMatch = "The password and confirmation password do not match.";

        public const string NotValidEmail = "This is not a valid Email adress.";

        public const int MinPasswordLenght = 6;
    }
}
