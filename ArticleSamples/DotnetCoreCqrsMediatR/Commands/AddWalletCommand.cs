using DotnetCoreCqrsMediatR.Model;
using MediatR;

namespace DotnetCoreCqrsMediatR.Commands
{
    public record AddWalletCommand(Wallet wallet) : IRequest<Wallet>
    {
    }
}
