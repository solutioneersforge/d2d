namespace FunctionAppDoc2Data.DataContext
{
    public partial class SubscriptionType
    {
        public int SubscriptionTypeId { get; set; }
        public string SubscriptionName { get; set; }
        public int NumberOfDays { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
