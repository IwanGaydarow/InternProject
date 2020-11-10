namespace HCMS.Data
{
    using System;
    
    using Microsoft.AspNetCore.Identity;
    
    using HCMS.GlobalConstants;

    public static class IdentityOptionsProvider
    {
        public static void GetIdentityOptions(IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.Password.RequireDigit = true;
            options.Password.RequiredLength = GlobalConstant.MinPasswordLenght;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = false;

            options.User.RequireUniqueEmail = true;

            options.Lockout.MaxFailedAccessAttempts = 6;
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
        }
    }
}
