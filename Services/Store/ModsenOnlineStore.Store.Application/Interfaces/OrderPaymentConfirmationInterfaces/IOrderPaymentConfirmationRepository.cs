using ModsenOnlineStore.Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Store.Application.Interfaces.OrderPaymentConfirmationInterfaces
{
    public interface IOrderPaymentConfirmationRepository
    {
        Task<OrderPaymentConfirmation?> GetOrderPaymentConfirmationAsync(int orderId, string code);

        Task AddOrderPaymentConfirmationAsync(OrderPaymentConfirmation OrderPaymentConfirmation);

        Task RemoveOrderPaymentConfirmationByIdAsync(int id);
    }
}
