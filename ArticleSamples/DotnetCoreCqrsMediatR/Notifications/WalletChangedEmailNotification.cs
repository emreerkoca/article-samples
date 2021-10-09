using DotnetCoreCqrsMediatR.Model;
using MediatR;

namespace DotnetCoreCqrsMediatR.Notifications
{
    public record WalletChangedEmailNotification(WalletWriteModel walletWriteModel)  : INotification;
}
