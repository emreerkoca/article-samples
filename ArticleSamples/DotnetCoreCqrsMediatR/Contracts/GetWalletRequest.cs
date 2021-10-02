using DotnetCoreCqrsMediatR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreCqrsMediatR.Contracts
{
    public class GetWalletRequest
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public OperationType? OperationType { get; set; }
        public int? MoneyType { get; set; }
    }
}
