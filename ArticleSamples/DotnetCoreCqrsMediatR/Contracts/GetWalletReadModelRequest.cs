namespace DotnetCoreCqrsMediatR.Contracts
{
    public class GetWalletReadModelRequest
    {
        public long? UserId { get; set; }
        public int? MoneyType { get; set; }
    }
}
