using System.Text;
using ModsenOnlineStore.EmailAuthentication.Application.Interfaces;

namespace ModsenOnlineStore.EmailAuthentication.Infrastructure.Services
{
    public class VerificationCodeGeneratior : IVerificationCodeGeneratior
    {
        public string GenerateCode()
        {
            var random = new Random();
            var code = new StringBuilder();
            string codeSymbol;

            for (int i = 0; i < 6; i++)
            {
                codeSymbol = random.Next(10).ToString();
                code.Append(codeSymbol);
            }

            return code.ToString();
        }
    }
}
