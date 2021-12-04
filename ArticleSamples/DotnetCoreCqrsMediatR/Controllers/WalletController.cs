using DotnetCoreCqrsMediatR.Commands;
using DotnetCoreCqrsMediatR.Contracts;
using DotnetCoreCqrsMediatR.Data;
using DotnetCoreCqrsMediatR.Model;
using DotnetCoreCqrsMediatR.Notifications;
using DotnetCoreCqrsMediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISampleDataStore _sampleDataStore;

        public WalletController(IMediator mediator, ISampleDataStore sampleDataStore)
        {
            _mediator = mediator;
            _sampleDataStore = sampleDataStore;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GetWalletReadModelRequest getWalletReadModelRequest)
        {
            var wallets = _mediator.Send(new GetWalletQuery(getWalletReadModelRequest));

            return Ok(wallets);
        }

        [HttpPost] 
        public async Task<IActionResult> Post([FromBody] WalletWriteModel walletWriteModel)
        {
            var addedWallet = _mediator.Send(new AddWalletCommand(walletWriteModel));

            await _mediator.Publish(new WalletChangedNotification(walletWriteModel));
            await _sampleDataStore.SetPublishedEvent(walletWriteModel, nameof(WalletChangedNotification));

            return StatusCode((int)HttpStatusCode.Created, addedWallet);
        }
    }
}
