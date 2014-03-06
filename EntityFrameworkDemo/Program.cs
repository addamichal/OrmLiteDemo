using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace EntityFrameworkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            using (var db = new MyDbContext())
            {
                db.Database.ExecuteSqlCommand("truncate table Products");
                for (int i = 1; i < 10000; i++)
                {
                    db.Products.Add(new Product() { Name = "Name" + i, Description = "Description" + i });
                }
                db.SaveChanges();
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }

    public class MyDbContext : DbContext
    {
        public MyDbContext() : base(@"Server=.\sqlexpress;Database=MyTestDatabase;Trusted_Connection=True;")
        {
        }

        public DbSet<Product> Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
