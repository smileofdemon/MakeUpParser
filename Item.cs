using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUpParser
{
    public class Item : IItem
    {
        private int _id;
        private decimal _price;
        private string _name;
        private Info _info;

        public const decimal NOT_AVALIABLE_PRICE_VALUE = -1; //price was not found on site(can be that site don't have item)
        private const string LINK = "https://makeup.com.ua/product/"; //start path of link(just add id to the end)

        public Item(int id, decimal price, string name, Info info)
        {
            _id = id;
            _price = price;
            _name = name;
            _info = info;
        }

        public string GetName()
        {
            return _name;
        }

        public decimal GetPrice()
        {
            return _price;
        }

        public string GetLink()
        {
            return LINK + _id + "/";
        }

        public Info GetInfo()
        {
            return _info;
        }

        public bool TryGetPrice(out decimal price)
        {
            price = _price;
            return price != NOT_AVALIABLE_PRICE_VALUE;
        }

        private void SetPriceToNotAvailable()
        {
            _price = NOT_AVALIABLE_PRICE_VALUE;
        }
    }
}
