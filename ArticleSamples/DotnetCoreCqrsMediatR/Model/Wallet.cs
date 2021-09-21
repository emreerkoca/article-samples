using System;

namespace DotnetCoreCqrsMediatR.Model
{
    public class Wallet
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long OperationAmount { get; set; }
        public long CurrentAmount { get; set; }
        public OperationType OperationType { get; set; }
        public int MoneyType { get; set; }
        public DateTime OperationTime { get; set; }
    }


}
