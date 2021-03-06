using System;
using CinemaWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace CinemaWebApp
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // ��������� ������ �������� ���� �������������
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // ��������� ������ �������� �������
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // ��������� ���������� ���������� �� ���������
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // ����������� ����������� ������������� �������� �����������. ��� ��������� ���� �������� ������������ � ������ ���������� ������������ ������� � ��������� ����������� �����
            // ����� ����� ������� ����������� ��������� � ���������� ���.
            manager.RegisterTwoFactorProvider("���, ���������� �� ��������",
                new PhoneNumberTokenProvider<ApplicationUser>
                {
                    MessageFormat = "��� ��� ������������: {0}"
                });
            manager.RegisterTwoFactorProvider("��� �� ���������", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "��� ������������",
                BodyFormat = "��� ��� ������������: {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}