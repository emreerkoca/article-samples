using DotnetCoreCqrsMediatR.Data;
using DotnetCoreCqrsMediatR.Model;
using DotnetCoreCqrsMediatR.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Handlers
{
    public class GetWalletHandler : IRequestHandler<GetWalletQuery, List<WalletReadModel>>
    {
        private readonly ISampleDataStore _sampleDataStore;

        public GetWalletHandler(ISampleDataStore sampleDataStore)
        {
            _sampleDataStore = sampleDataStore;
        }

        public async Task<List<WalletReadModel>> Handle(GetWalletQuery request, CancellationToken cancellationToken)
        {
            return await _sampleDataStore.GetWalletReadModels(request.getWalletReadModelRequest);
        }
    }
}
