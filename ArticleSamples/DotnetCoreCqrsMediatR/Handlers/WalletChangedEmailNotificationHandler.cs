using DotnetCoreCqrsMediatR.Data;
using DotnetCoreCqrsMediatR.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Handlers
{
    public class WalletChangedEmailNotificationHandler : INotificationHandler<WalletChangedEmailNotification>
    {
        private readonly ISampleDataStore _sampleDataStore;

        public WalletChangedEmailNotificationHandler(ISampleDataStore sampleDataStore)
        {
            _sampleDataStore = sampleDataStore;
        }

        public async Task Handle(WalletChangedEmailNotification notification, CancellationToken cancellationToken)
        {
            await _sampleDataStore.EventPublished(notification.wallet, nameof(WalletChangedEmailNotification));
            await Task.CompletedTask;
        }
    }
}
