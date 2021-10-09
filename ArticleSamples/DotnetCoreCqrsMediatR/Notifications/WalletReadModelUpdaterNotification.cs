using DotnetCoreCqrsMediatR.Model;
using MediatR;

namespace DotnetCoreCqrsMediatR.Notifications
{
    public record WalletReadModelUpdaterNotification(WalletWriteModel walletWriteModel) : INotification;
}
