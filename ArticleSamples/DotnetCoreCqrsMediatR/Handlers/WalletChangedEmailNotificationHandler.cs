using DotnetCoreCqrsMediatR.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Handlers
{
    public class WalletChangedEmailNotificationHandler : INotificationHandler<WalletChangedNotification>
    {
        public WalletChangedEmailNotificationHandler()
        {
        }

        public async Task Handle(WalletChangedNotification notification, CancellationToken cancellationToken)
        {
            //send email to user about wallet

            await Task.CompletedTask;
        }
    }
}
