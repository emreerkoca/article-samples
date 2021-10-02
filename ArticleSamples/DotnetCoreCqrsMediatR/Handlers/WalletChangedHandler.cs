using DotnetCoreCqrsMediatR.Data;
using DotnetCoreCqrsMediatR.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Handlers
{
    public class WalletChangedHandler : INotificationHandler<WalletChangedEmailNotification>
    {
        private readonly ISampleDataStore _sampleDataStore;

        public WalletChangedHandler(ISampleDataStore sampleDataStore)
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
