using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Work_with_SQL_Table__HW_4_;

namespace Dapper_BDSQL.Controller
{
    class DapperLink
    {
        static string link;
        public SqlConnection connection { get; private set; }

        private DapperLink()     //Создание подключения к базе SQL
        {
            connection = new SqlConnection(link);
            connection.Open();      //Открытие соединения с базой SQL
        }

        private static DapperLink _instance;     //Внутренняя переменная класса, хранящая соединение с базой 
        public static DapperLink GetInstance(DBSettings settings)     //Метод создания подключения к базе SQL
        {
            if (_instance == null)
            {
                link = settings.ToString();
                _instance = new DapperLink();
            }
            return _instance;
        }

        public List<Product> ReadAndShow(bool show)  //Если параметр show при обращении к методу равен true то кроме чтения базы, данные также выведутся на экран
        {
            List<Product> Table = (List<Product>)connection.Query<Product>("SELECT * FROM [Product];");
                if (show) Table.ToList().ForEach(Console.WriteLine);
            return Table;
        }

    public void Add(Product Table)      //Метод записи данных в открытую базу SQL. Данные передаются в виде единичного объекта класса ObjectLog
    {
        if (Table != null)
        {
            int rows = connection.Execute($"INSERT INTO [dbo].[Product]([Name],[Category],[Price])VALUES (\'{Table.Name}\', \'{Table.Category}\', \'{Table.Price}\');");
            if (rows > 0) Console.WriteLine($"{rows} rows added!");
        }
    }

        public void Change(string name, Product newProduct)      //Метод записи данных в открытую базу SQL. Данные передаются в виде единичного объекта класса ObjectLog
        {
            if (ReadAndShow(false).Count > 1)
            {
                var sqlQuery = $"UPDATE Product SET Name = @Name, Category = @Category, Price = @Price WHERE Name = \'{name}\'";
                int rows = connection.Execute(sqlQuery, newProduct);
                if (rows > 0) Console.WriteLine($"Product with Name - {name} was changed!");
            }
        }

        public void Delete(int col)         //Удаление указанного в параметре col количества строк
        {
            if (ReadAndShow(false).Count > 1)
            {
                int rows = connection.Execute($"DELETE TOP ({col}) FROM [dbo].[Product]");
                if (rows > 0) Console.WriteLine($"{rows} rows deleted!");

            }
        }

        public void Delete(string nameCol, string value)        //Удаление строк с указанным в параметрах значением value для столбца с именем nameCol
        {
            if (ReadAndShow(false).Count > 1)
            {
                int rows = connection.Execute($"DELETE FROM [dbo].[Product] WHERE {nameCol}='{value}'");
                if (rows > 0) Console.WriteLine($"In the list of products column {nameCol}={value} was found and deleted {rows} rows");
            }
        }
    }
}
