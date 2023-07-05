using ModsenOnlineStore.Common;

namespace ModsenOnlineStore.Login.Application.Interfaces
{
    public interface IUserMoneyService
    {
        Task<ResponseInfo> MakePaymentAsync(int id, decimal money);

        Task<ResponseInfo> AddMoneyAsync(int id, decimal money);
    }
}
