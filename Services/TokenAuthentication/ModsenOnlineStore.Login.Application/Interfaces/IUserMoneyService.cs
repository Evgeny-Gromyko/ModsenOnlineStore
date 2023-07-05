using ModsenOnlineStore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface IUserMoneyService
    {
        Task<ResponseInfo> MakePaymentAsync(int id, decimal money);

        Task<ResponseInfo> AddMoneyAsync(int id, decimal money);
    }
}
