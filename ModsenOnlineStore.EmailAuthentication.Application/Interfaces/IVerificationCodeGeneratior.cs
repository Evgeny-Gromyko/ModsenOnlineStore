using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.EmailAuthentication.Application.Interfaces
{
    public interface IVerificationCodeGeneratior
    {
        public string GenerateCode();
    }
}
