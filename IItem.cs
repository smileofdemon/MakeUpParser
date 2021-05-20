namespace MakeUpParser
{
    public interface IItem
    {
        public bool TryGetPrice(out decimal price);
        public string GetName();
        public decimal GetPrice();
        public string GetLink();
        public Info GetInfo();
    }
}
