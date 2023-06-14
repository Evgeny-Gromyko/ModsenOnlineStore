using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModsenOnlineStore.EmailAuthentication.Application.Interfaces;

namespace ModsenOnlineStore.EmailAuthentication.Infrastructure.Services
{
    public class VerificationCodeGeneratior : IVerificationCodeGeneratior
    {
        public string generateCode()
        {
            Random random = new Random();
            string code = "";
            string ch;

            for (int i = 0; i < 6; i++)
            {
                ch = random.Next(10).ToString();
                code += ch;
            }

            return code;
        }
    }
}
