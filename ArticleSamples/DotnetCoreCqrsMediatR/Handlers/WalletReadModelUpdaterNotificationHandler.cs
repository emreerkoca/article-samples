using DotnetCoreCqrsMediatR.Data;
using DotnetCoreCqrsMediatR.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Handlers
{
    public class WalletReadModelUpdaterNotificationHandler : INotificationHandler<WalletReadModelUpdaterNotification>
    {
        private readonly ISampleDataStore _sampleDataStore;

        public WalletReadModelUpdaterNotificationHandler(ISampleDataStore sampleDataStore)
        {
            _sampleDataStore = sampleDataStore;
        }

        public async Task Handle(WalletReadModelUpdaterNotification notification, CancellationToken cancellationToken)
        {
            await _sampleDataStore.UpdateWalletReadModel(notification.walletWriteModel);
            await Task.CompletedTask;
        }
    }
}
