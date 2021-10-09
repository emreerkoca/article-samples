using DotnetCoreCqrsMediatR.Contracts;
using DotnetCoreCqrsMediatR.Model;
using MediatR;
using System.Collections.Generic;

namespace DotnetCoreCqrsMediatR.Queries
{
    public record GetWalletQuery(GetWalletReadModelRequest getWalletReadModelRequest) : IRequest<List<WalletReadModel>>
    {
    }
}
