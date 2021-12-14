﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;

namespace Lab16._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Запись информации в базу о товарах.");
            Console.WriteLine("Введите количество товаров, которое вы хотите записать.");
            int productCount = Convert.ToInt32(Console.ReadLine());
            Product[] productArray = new Product[productCount];
            for (int i = 0; i < productCount; i++)
            {
                Console.WriteLine("Введите код {0} товара.", i + 1);
                int code = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите имя {0} товара.", i + 1);
                string name = Console.ReadLine();
                Console.WriteLine("Введите цену {0} товара.", i + 1);
                double price = Convert.ToDouble(Console.ReadLine());
                Product prod = new Product()
                {
                    ProductCode = code,
                    ProductName = name,
                    ProductPrice = price
                };
                productArray[i] = prod;
            }
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string report = JsonSerializer.Serialize(productArray, options);
            Console.WriteLine(report);
            string pathToDir = "Example";
            if (!Directory.Exists(pathToDir))
            {
                Directory.CreateDirectory(pathToDir);
            }
            string fileName = "Products.json";
            string pathToFile = pathToDir + "/" + fileName;
            if (!File.Exists(pathToFile))
            {
                File.Create(pathToFile);
            }
            using (StreamWriter sw = new StreamWriter(pathToFile, false))
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
