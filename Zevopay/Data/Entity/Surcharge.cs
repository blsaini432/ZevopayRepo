namespace Zevopay.Data.Entity
{
    public class Surcharge
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public decimal RangeFrom { get; set; }
        public decimal RangeTo { get; set; }
        public decimal SurchargeAmount { get; set; }
        public bool IsFlat { get; set; }


    }
}
