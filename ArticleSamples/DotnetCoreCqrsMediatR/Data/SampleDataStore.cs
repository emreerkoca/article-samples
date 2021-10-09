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
        private static List<WalletWriteModel> _walletWriteModels;
        private static List<WalletReadModel> _walletReadModels;

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

            _walletWriteModels = new List<WalletWriteModel>()
            {
                new WalletWriteModel { Id = Guid.NewGuid(), UserId =1, OperationType = OperationType.Buy, OperationAmount = 1000, CurrentAmount = 1000, MoneyType = 3, CreateDate = DateTime.UtcNow },
                new WalletWriteModel { Id = Guid.NewGuid(), UserId =1, OperationType = OperationType.Buy, OperationAmount = 100, CurrentAmount = 100, MoneyType = 2, CreateDate = DateTime.UtcNow },
                new WalletWriteModel { Id = Guid.NewGuid(), UserId =1, OperationType = OperationType.Sell, OperationAmount = 200, CurrentAmount = 800, MoneyType = 3, CreateDate = DateTime.UtcNow },
                new WalletWriteModel { Id = Guid.NewGuid(), UserId =2, OperationType = OperationType.Buy, OperationAmount = 6000, CurrentAmount = 6000, MoneyType = 3, CreateDate = DateTime.UtcNow },
            };

            _walletReadModels = new List<WalletReadModel>()
            {
                new WalletReadModel { Id = Guid.NewGuid(), UserId = 1, CurrentAmount = 800, MoneyType = 3, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow },
                new WalletReadModel { Id = Guid.NewGuid(), UserId = 1, CurrentAmount = 100, MoneyType = 2, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow  },
                new WalletReadModel { Id = Guid.NewGuid(), UserId = 2, CurrentAmount = 6000, MoneyType = 3, CreateDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow }
            };
        }

        public async Task<WalletWriteModel> AddWallet(WalletWriteModel walletWriteModel)
        {
            _walletWriteModels.Add(walletWriteModel);

            return await Task.FromResult(walletWriteModel);
        }

        public async Task<List<WalletReadModel>> GetWalletReadModels(GetWalletReadModelRequest getWalletReadModelRequest)
        {
            var walletReadModelsQueryable = _walletReadModels.AsQueryable();

            if (getWalletReadModelRequest.UserId.HasValue)
            {
                walletReadModelsQueryable = walletReadModelsQueryable.Where(m => m.UserId == getWalletReadModelRequest.UserId);
            }

            if (getWalletReadModelRequest.MoneyType.HasValue)
            {
                walletReadModelsQueryable = walletReadModelsQueryable.Where(m => m.MoneyType == getWalletReadModelRequest.MoneyType);
            }

            return await Task.FromResult(walletReadModelsQueryable.ToList());
        }

        public async Task SetPublishedEvent(WalletWriteModel walletWriteModel, string eventName)
        {
            _walletWriteModels.Single(m => m.Id == walletWriteModel.Id).PublishedEvents = $"{walletWriteModel.PublishedEvents} eventName: {eventName}";

            await Task.CompletedTask;   
        }

        public async Task UpdateWalletReadModel(WalletWriteModel walletWriteModel)
        {
            WalletReadModel walletReadModel = _walletReadModels.FirstOrDefault(m => m.UserId == walletWriteModel.UserId && m.MoneyType == walletWriteModel.MoneyType);

            if (walletReadModel == null)
            {
                _walletReadModels.Add(new WalletReadModel
                {
                    UserId = walletWriteModel.UserId,
                    MoneyType = walletWriteModel.MoneyType,
                    CurrentAmount = walletWriteModel.CurrentAmount,
                    CreateDate = DateTime.UtcNow,
                    UpdateDate = DateTime.UtcNow
                });
            }
            else
            {
                walletReadModel.CurrentAmount = walletWriteModel.CurrentAmount;
                walletReadModel.UpdateDate = DateTime.UtcNow;
            }

            await Task.CompletedTask;
        }
    }
}
