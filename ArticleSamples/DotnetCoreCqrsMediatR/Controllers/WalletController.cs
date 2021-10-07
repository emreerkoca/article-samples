using DotnetCoreCqrsMediatR.Commands;
using DotnetCoreCqrsMediatR.Contracts;
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

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] GetWalletRequest getWalletRequest)
        {
            var wallets = _mediator.Send(new GetWalletQuery(getWalletRequest));

            return Ok(wallets);
        }

        [HttpPost] 
        public async Task<IActionResult> Post([FromBody] Wallet wallet)
        {
            var addedWallet = _mediator.Send(new AddWalletCommand(wallet));

            await _mediator.Publish(new WalletChangedEmailNotification(wallet));

            return StatusCode((int)HttpStatusCode.Created, addedWallet);
        }
    }
}
