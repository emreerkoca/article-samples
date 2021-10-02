using DotnetCoreCqrsMediatR.Model;
using MediatR;

namespace DotnetCoreCqrsMediatR.Notifications
{
    public record WalletChangedEmailNotification(Wallet wallet)  : INotification;
}
