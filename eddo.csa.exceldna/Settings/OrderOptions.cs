namespace eddo.csa.exceldna.Settings
{
    public class OrderOptions
    {
        public string Customer { get; set; } = string.Empty;
        public int Number { get; set; } = -1;

        public AddressOptions Address { get; } = new AddressOptions();
    }
}
