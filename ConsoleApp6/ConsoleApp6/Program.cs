using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp6
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Your file/Database Name;");
        }
    }
    public class Product
    {

        public int Id { get; set; }

        [MaxLength]
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }


        public string Name { get; set; }


    }


    class Program
    {
        static void Main(string[] args)
        {
            //AddProducts();
            //GetAllProducts(6);
            //UpdateProducts();
            DeleteProducts(7);
        }
        static void AddProducts()
        {
            using (var db = new ShopContext())
            {
                var products = new List<Product>() {
                    new Product {Name="Iphone 3", Price=3000 },
                    new Product { Name = "Iphone 8", Price = 15000 },
                    new Product { Name = "Iphone X", Price = 20000 }


                    };


                foreach (var item in products)
                {
                    db.Products.Add(item);

                }

                db.SaveChanges();
                Console.Write("OK");


            }
        }
        static void GetAllProducts(int id)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.Where(p => p.Id == id).FirstOrDefault();

                Console.WriteLine($"name:{products.Name} price: {products.Price}");

            }

        }

        static void UpdateProducts()
        {

            using (var p = new ShopContext())
            {
                var productsupdate = p.Products.Where(i => i.Id == 1).FirstOrDefault();
                if (productsupdate!=null)
                {
                    productsupdate.Price = 2500;
                    p.Products.Update(productsupdate);
                    p.SaveChanges();

                    //productsupdate.Price += (productsupdate.Price * 22) / 100;
                    //p.SaveChanges();
                    Console.WriteLine("OK upd");
                }
            }

        }

        static void DeleteProducts(int id) 
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.FirstOrDefault(p => p.Id == id);

                if (products!=null)
                {
                    context.Products.Remove(products);
                    context.SaveChanges();
                    Console.WriteLine("Kaldırdık");
                }

                

            }
        }


    }
}