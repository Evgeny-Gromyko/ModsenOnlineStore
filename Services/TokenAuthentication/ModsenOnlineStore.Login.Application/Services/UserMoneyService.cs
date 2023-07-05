using AutoMapper;
using Microsoft.Extensions.Options;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Login.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenOnlineStore.Login.Application.Services
{
    public class UserMoneyService : IUserMoneyService
    {
        private readonly IUserRepository repository;

        public UserMoneyService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ResponseInfo> AddMoneyAsync(int id, decimal money)
        {
            var user = await repository.GetUserByIdAsync(id);

            if (user is null)
            {
                return new ResponseInfo(false, "user not found");
            }

            user.Money += money;

            await repository.EditUserAsync(user);

            return new ResponseInfo(true, $"money added successfully");
        }

        public async Task<ResponseInfo> MakePaymentAsync(int id, decimal money)
        {
            var user = await repository.GetUserByIdAsync(id);

            if (user is null)
            {
                return new ResponseInfo(false, "user not found");
            }

            if (user.Money < money)
            {
                return new ResponseInfo(false, "not enough money");
            }

            user.Money -= money;

            await repository.EditUserAsync(user);

            return new ResponseInfo(true, $"paid successfully");
        }
    }
}
