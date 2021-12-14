using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Lab16._2
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Получение информации из базы данных о товарах.");
            string path = "Example/Products.json";
            string line = "";
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл не найден!");
            }
            else
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    line = sr.ReadToEnd();
                }
            }
            line = line.Remove(0, 3);
            line = line.Remove(line.Length - 5, 5);
            string[] separatingStrings = { "}," };
            string[] stringArray = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < stringArray.Length - 1; i++)
            {
                stringArray[i] = stringArray[i] + "}";
            }
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            Product[] prodBase = new Product[stringArray.Length];
            int num = 0;
            foreach (string item in stringArray)
            {
                Product prod = JsonSerializer.Deserialize<Product>(item, options);
                prodBase[num] = prod;
                num++;
            }
            double productPriceMax = 0;
            string productPriceMaxName = "";
            foreach (Product item in prodBase)
            {
                Console.WriteLine("В базе имеется товар с кодом: {0}, называнием: {1} и стоимостью {2}.", item.ProductCode, item.ProductName, item.ProductPrice);
                if (item.ProductPrice > productPriceMax)
                {
                    productPriceMax = item.ProductPrice;
                    productPriceMaxName = item.ProductName;
                }
            }
            Console.WriteLine();
            Console.Write("Самый дорогой товар в базе - это {0}, его стоимость {1}.", productPriceMaxName, productPriceMax);
            Console.ReadKey();
        }
        class Product
        {
            public int ProductCode { get; set; }
            public string ProductName { get; set; }
            public double ProductPrice { get; set; }
        }
    }
}
