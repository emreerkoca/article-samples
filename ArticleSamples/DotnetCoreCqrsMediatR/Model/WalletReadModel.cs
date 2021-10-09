using System;

namespace DotnetCoreCqrsMediatR.Model
{
    public class WalletReadModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public long UserId { get; set; }
        public long CurrentAmount { get; set; }
        public int MoneyType { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PublishedEvents { get; set; }
    }
}
