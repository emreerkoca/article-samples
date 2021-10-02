using DotnetCoreCqrsMediatR.Contracts;
using DotnetCoreCqrsMediatR.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Data
{
    public interface ISampleDataStore
    {
        Task<Wallet> AddWallet(Wallet wallet);
        Task<List<Wallet>> GetWallets(GetWalletRequest getWalletRequest);
        Task EventPublished(Wallet wallet, string eventName);
    }
}
