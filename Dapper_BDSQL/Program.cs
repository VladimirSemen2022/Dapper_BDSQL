using System;
using System.Collections.Generic;
using System.Threading;
using Dapper_BDSQL.Controller;

namespace Dapper_BDSQL
{
    class Program
    {
        static void Main()
        {
            SQLController controller = new SQLController(@"D:\DBSetting.json");
            controller.Download();

            //Создание подключения типа Singleton используя Dapper через класс DapperLink используя созданную из файла строку подключения
            DapperLink newLink = DapperLink.GetInstance(controller.defaultsetting);

            //Чтение данных о перечне продуктов из БД после подключения к базе
            List <Product> products = new List<Product>();
            products = newLink.ReadAndShow(false);

            //Создание списка переменных
            Product newProduct = new Product();
            string choice, name;

            do
            {
                Console.Clear();
                Console.WriteLine("-----------WORK WITH THE PRODUCT LIST-----------");
                Console.WriteLine("Input the number of operation you want to do:");
                Console.WriteLine("0. Exit;");
                Console.WriteLine("1. Search and correct the product from the list;");
                Console.WriteLine("2. Add a new product in the list;");
                Console.WriteLine("3. Delete a product from the list;");
                Console.WriteLine("4. Show all products list");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        if (products.Count > 0)
                        {
                            Console.Write("Input name of product you want to search - ");
                            name = Console.ReadLine();
                            if (products.Exists(x => x.Name.ToLower() == name.ToLower()))
                            {
                                Console.WriteLine($"\n{products[products.FindIndex(x => x.Name.ToLower() == name.ToLower())].ToString()}\n");
                                Console.WriteLine("\nDo you want to correct this product? Press Y if Yes and any kay if No\n");
                                if (Console.ReadKey().Key == ConsoleKey.Y)
                                {
                                    Console.Write("\nInput new name of the product you want to change - ");
                                    newProduct.Name = Console.ReadLine();
                                    Console.Write("Input new category of the product  you want to change - ");
                                    newProduct.Category = Console.ReadLine();
                                    Console.Write("Input new price of the product  you want to change - ");
                                    newProduct.Price = Console.ReadLine();
                                    newLink.Change(name, newProduct);
                                    products = newLink.ReadAndShow(false);
                                }
                            }
                            else
                                Console.WriteLine("\nThe inputed product didn`t find!\n");
                        }
                        else
                            Console.WriteLine("\nThe list of products are empty yet!\n");
                        Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.Write("Input name of new product you want to add in the list - ");
                        newProduct.Name = Console.ReadLine();
                        Console.Write("Input category of the product - ");
                        newProduct.Category = Console.ReadLine();
                        Console.Write("Input price of the product - ");
                        newProduct.Price = Console.ReadLine();
                        newLink.Add(newProduct);
                        products = newLink.ReadAndShow(false);
                        Thread.Sleep(2000);
                        break;
                    case "3":
                        Console.Clear();
                        if (products.Count > 0)
                        {
                            Console.Write("Input name of product you want to delete from the list - ");
                            name = Console.ReadLine();
                            if (products.FindIndex(x => x.Name.ToLower() == name.ToLower()) != -1)
                            {
                                newLink.Delete("Name", name);
                                products = newLink.ReadAndShow(false);
                            }
                            else
                                Console.WriteLine("\nThe product didn`t find!");
                        }
                        else
                            Console.WriteLine("\nThe list of products are empty yet!\n");
                        Thread.Sleep(2000);
                        break;
                    case "4":
                        Console.Clear();
                        products=newLink.ReadAndShow(true);
                        Console.WriteLine("\nPress any key to continue");
                        Console.ReadKey();
                        break;
                }
            } while (choice != "0");
        }
    }
}
