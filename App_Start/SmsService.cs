using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace CinemaWebApp
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // ���������� ����� ������ SMS, ����� ��������� ��������� ���������.
            return Task.FromResult(0);
        }
    }
}