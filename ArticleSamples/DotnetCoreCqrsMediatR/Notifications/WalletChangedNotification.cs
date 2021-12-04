using DotnetCoreCqrsMediatR.Model;
using MediatR;

namespace DotnetCoreCqrsMediatR.Notifications
{
    public record WalletChangedNotification(WalletWriteModel walletWriteModel)  : INotification;
}
