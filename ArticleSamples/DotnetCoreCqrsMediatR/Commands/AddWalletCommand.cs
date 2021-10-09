using DotnetCoreCqrsMediatR.Model;
using MediatR;

namespace DotnetCoreCqrsMediatR.Commands
{
    public record AddWalletCommand(WalletWriteModel walletWriteModel) : IRequest<WalletWriteModel>
    {
    }
}
