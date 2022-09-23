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
        static IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        static string connString = config.GetConnectionString("DefaultConnection");

        static IDbConnection conn = new MySqlConnection(connString);
        static void Main(string[] args)
        {
            //InsertDepartment();

            InsertProducts();
            
            UpdateProductName();

            DeleteProducts();

        }


        public static void InsertProducts()
        {
            var repo = new DapperProductRepository(conn);
            var products = repo.GetAllProducts();
            PrintProd(products);
            Console.WriteLine("                  ");

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
                PrintProd(repo.GetAllProducts());
                Console.WriteLine("\n");

            }

        }

        public static void UpdateProductName()
        {
            var repo = new DapperProductRepository(conn);
            Console.WriteLine("Do you want to update a product name? Yes or No");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {

                Console.WriteLine($"What is the productID of the product you would like to update?");
                var productID = int.Parse(Console.ReadLine());

                Console.WriteLine($"What is the new name you would like for the product with an id of {productID}?");
                var updatedName = Console.ReadLine();

                repo.UpdateProductName(productID, updatedName);
                PrintProd(repo.GetAllProducts());
                Console.WriteLine("\n");

            }

        }

        public static void DeleteProducts()
        {
            var repo = new DapperProductRepository(conn);
            Console.WriteLine("Do you want to delete a product? Yes or No");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {

                Console.WriteLine($"What is the productID of the product you would like to delete?");
                var productID = int.Parse(Console.ReadLine());

                repo.DeleteProducts(productID);
                PrintProd(repo.GetAllProducts());
                Console.WriteLine("\n");

            }

        }
        public static void InsertDepartment()
        {
            var repo = new DapperDepartmentRepository(conn);
            var departments = repo.GetAllDepartments();
            PrintDept(departments);
            Console.WriteLine("              ");

            Console.WriteLine("Do you want to add a department? Yes or No");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of the new Department?");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                PrintDept(repo.GetAllDepartments());
                Console.WriteLine("\n");
            }
        }



        private static void PrintDept(IEnumerable<Department> departments)
        {
            foreach (var item in departments)
            {
                Console.WriteLine($"{item.DepartmentID} {item.Name}");
            }
        }


        private static void PrintProd(IEnumerable<Product> products)
        {
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price}");
            }
        }
    }
}
