using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Transactions;

namespace BestBuyBestPractices
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion

            IDbConnection conn = new MySqlConnection(connString);

            //var repo = new DapperDepartmentRepository(conn);
            //var departments = repo.GetAllDepartments();

            //Print(departments);


            //Console.WriteLine("Do you want to add a department? Yes or No");
            //string userResponse = Console.ReadLine();

            //if (userResponse.ToLower() == "yes")
            //{
            //    Console.WriteLine("What is the name of the new Department?");
            //    userResponse = Console.ReadLine();

            //    repo.InsertDepartment(userResponse);
            //    Print(repo.GetAllDepartments());
            //}

            var repo = new DapperProductRepository(conn);
            var products = repo.GetAllProducts();

            Console.WriteLine("Do you want to add a product? Yes or No");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("Enter the name of the product...");
                string prodName = Console.ReadLine();
                Console.WriteLine("Enter the price of the product...");
                double prodPrice = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter the categoryID of the product...");
                int prodCategoryID = int.Parse(Console.ReadLine());

                repo.InsertProducts(prodName, prodPrice, prodCategoryID);
                Print(repo.GetAllProducts());
            }


        }
        private static void Print(IEnumerable<Product> products)
        {
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price}");
            }
        }
    }
}
