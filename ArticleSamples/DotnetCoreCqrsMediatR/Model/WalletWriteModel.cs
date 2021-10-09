using System;

namespace DotnetCoreCqrsMediatR.Model
{
    public class WalletWriteModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public long UserId { get; set; }
        public long OperationAmount { get; set; }
        public long CurrentAmount { get; set; }
        public OperationType OperationType { get; set; }
        public int MoneyType { get; set; }
        public DateTime CreateDate { get; set; }
        public string PublishedEvents { get; set; }
    }


}
