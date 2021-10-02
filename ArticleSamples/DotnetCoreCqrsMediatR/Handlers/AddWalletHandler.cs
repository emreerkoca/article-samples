﻿using DotnetCoreCqrsMediatR.Commands;
using DotnetCoreCqrsMediatR.Data;
using DotnetCoreCqrsMediatR.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Handlers
{
    public class AddWalletHandler : IRequestHandler<AddWalletCommand, Wallet>
    {
        private readonly ISampleDataStore _sampleDataStore;
        public AddWalletHandler(ISampleDataStore sampleDataStore)
        {
            _sampleDataStore = sampleDataStore;
        }

        public async Task<Wallet> Handle(AddWalletCommand request, CancellationToken cancellationToken)
        {
            return await _sampleDataStore.AddWallet(request.wallet);
        }
    }
}
