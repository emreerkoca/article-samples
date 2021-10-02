using DotnetCoreCqrsMediatR.Contracts;
using DotnetCoreCqrsMediatR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Data
{
    public class SampleDataStore : ISampleDataStore
    {
        private static List<User> _users;
        private static List<MoneyType> _moneyTypes;
        private static List<Wallet> _wallets;

        public SampleDataStore()
        {
            _users = new List<User>()
            {
                new User { Id = 1, UserName = "Sample User 1" },
                new User {Id = 2, UserName = "Sample User 2" }
            };

            _moneyTypes = new List<MoneyType>()
            {
                new MoneyType { Id = 1, MoneyTypeName = "Sample Money 1" },
                new MoneyType { Id = 2, MoneyTypeName = "Sample Money 2" },
                new MoneyType { Id = 3, MoneyTypeName = "Sample Money 3" },
            };

            _wallets = new List<Wallet>()
            {
                new Wallet { Id = 1, UserId =1, OperationType = OperationType.Buy, OperationAmount = 1000, CurrentAmount = 1000, MoneyType = 3, OperationTime = DateTime.UtcNow },
                new Wallet { Id = 2, UserId =1, OperationType = OperationType.Buy, OperationAmount = 100, CurrentAmount = 100, MoneyType = 2, OperationTime = DateTime.UtcNow },
                new Wallet { Id = 3, UserId =1, OperationType = OperationType.Sell, OperationAmount = 200, CurrentAmount = 800, MoneyType = 3, OperationTime = DateTime.UtcNow },
                new Wallet { Id = 4, UserId =2, OperationType = OperationType.Buy, OperationAmount = 6000, CurrentAmount = 6000, MoneyType = 3, OperationTime = DateTime.UtcNow },
            };
        }

        public async Task<Wallet> AddWallet(Wallet wallet)
        {
            _wallets.Add(wallet);

            return await Task.FromResult(wallet);
        }

        public async Task<List<Wallet>> GetWallets(GetWalletRequest getWalletRequest)
        {
            var walletsQueryable = _wallets.AsQueryable();

            if (getWalletRequest.Id.HasValue)
            {
                walletsQueryable = walletsQueryable.Where(m => m.Id == getWalletRequest.Id);
            }

            if (getWalletRequest.UserId.HasValue)
            {
                walletsQueryable = walletsQueryable.Where(m => m.UserId == getWalletRequest.UserId);
            }

            if (getWalletRequest.MoneyType.HasValue)
            {
                walletsQueryable = walletsQueryable.Where(m => m.MoneyType == getWalletRequest.MoneyType);
            }

            if (getWalletRequest.OperationType.HasValue)
            {
                walletsQueryable = walletsQueryable.Where(m => m.OperationType == getWalletRequest.OperationType);
            }

            return await Task.FromResult(walletsQueryable.ToList());
        }

        public async Task EventPublished(Wallet wallet, string eventName)
        {
            _wallets.Single(m => m.Id == wallet.Id).PublishedEvents = $"{wallet.PublishedEvents} eventName: {eventName}";

            await Task.CompletedTask;   
        }
    }
}
