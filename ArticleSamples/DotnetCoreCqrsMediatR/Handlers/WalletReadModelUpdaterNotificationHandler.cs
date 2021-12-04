using DotnetCoreCqrsMediatR.Data;
using DotnetCoreCqrsMediatR.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Handlers
{
    public class WalletReadModelUpdaterNotificationHandler : INotificationHandler<WalletChangedNotification>
    {
        private readonly ISampleDataStore _sampleDataStore;

        public WalletReadModelUpdaterNotificationHandler(ISampleDataStore sampleDataStore)
        {
            _sampleDataStore = sampleDataStore;
        }

        public async Task Handle(WalletChangedNotification notification, CancellationToken cancellationToken)
        {
            await _sampleDataStore.UpdateWalletReadModel(notification.walletWriteModel);
            await Task.CompletedTask;
        }
    }
}
