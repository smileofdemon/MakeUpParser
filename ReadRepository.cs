using HtmlAgilityPack;
using System.Collections.Generic;

namespace MakeUpParser
{
    public class ReadRepository
    {
        private const string PERFUMERY_CATEGORY_URL = "https://makeup.com.ua/categorys/3/";
        private const string ITEM_URL = "https://makeup.com.ua/product/";
        private const string NEXT_PAGE_URL = "#offset=";
        private const int NEXT_PAGE_STEP_URL = 36;
        private int _currentStep = 0;

        public List<IItem> GetItems(int count, bool isAddСharacteristics, bool isAddDescription)
        {
            List<HtmlNodeCollection> itemCollections = new();
            List<IItem> items = new();

            for (;count > 0;)
            {
                var doc = GetNextPage();
                var itemsCatalog = doc.DocumentNode.SelectSingleNode("//div[@class=\"catalog-products\"]");
                var itemCollection = itemsCatalog.SelectNodes("//li[@data-id]");

                foreach (var item in itemCollection)
                {
                    if(count > 0)
                    {
                        var id = System.Int32.Parse(item.GetAttributeValue("data-id", Item.NOT_AVALIABLE_PRICE_VALUE.ToString()));
                        var info = new Info();

                        var url = ITEM_URL + id + "/";
                        var web = new HtmlWeb();
                        var itemDoc = web.Load(url);

                        var price = itemDoc.DocumentNode.SelectSingleNode("//span[@itemprop=\"price\"]")?.InnerText ?? Item.NOT_AVALIABLE_PRICE_VALUE.ToString();
                        var name = itemDoc.DocumentNode.SelectSingleNode("//div[@class=\"product-item__name\"]")?.InnerText;

                        if (isAddСharacteristics)
                        {
                            var characteristic = itemDoc.DocumentNode.SelectSingleNode("//div[@class=\"product-item__content\"]").SelectSingleNode("//div[@class=\"product-item__text\"]").InnerText;
                            info.Characteristic = characteristic;
                        }

                        if(isAddDescription)
                        {
                            var description = itemDoc.DocumentNode.SelectSingleNode("//div[@itemprop=\"description\"]").InnerText;
                            info.Description = description;
                        }

                        items.Add(new Item(id, System.Decimal.Parse(price), name, info));
                        count--;
                    }
                }
            }

            return items;
        }

        public void GetItemInformation()
        {
            var web = new HtmlWeb();
            var doc = web.Load(PERFUMERY_CATEGORY_URL);
            var itemContent = doc.DocumentNode.SelectSingleNode("//div[@class=\"product-item__content\"]");

            var node = itemContent.SelectSingleNode("//span[@itemprop=\"price\"]");
            var price = node?.InnerText;

            node = doc.DocumentNode.SelectSingleNode("//div[@class=\"product-item__name\"]");
            var name = node?.InnerText;

            node = doc.DocumentNode.SelectSingleNode("//div[@class=\"product-item__text\"]");
            var text = node?.InnerText;
        }

        private HtmlDocument GetNextPage()
        {
            var web = new HtmlWeb();
            var url = PERFUMERY_CATEGORY_URL + (_currentStep == 0 ? "" : (NEXT_PAGE_URL + _currentStep));
            _currentStep += NEXT_PAGE_STEP_URL;

            return web.Load(url);
        }
    }
}
