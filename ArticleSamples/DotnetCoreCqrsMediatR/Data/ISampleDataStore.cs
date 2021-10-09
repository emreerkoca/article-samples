using DotnetCoreCqrsMediatR.Contracts;
using DotnetCoreCqrsMediatR.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Data
{
    public interface ISampleDataStore
    {
        Task<WalletWriteModel> AddWallet(WalletWriteModel walletWriteModel);
        Task<List<WalletReadModel>> GetWalletReadModels(GetWalletReadModelRequest getWalletReadModelRequest);
        Task SetPublishedEvent(WalletWriteModel walletWriteModel, string eventName);
        Task UpdateWalletReadModel(WalletWriteModel walletWriteModel);
    }
}
