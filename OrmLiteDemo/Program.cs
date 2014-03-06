using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace OrmLiteDemo
{
    class Program
    {
        static int count = 10000;

        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            var dbFactory = new OrmLiteConnectionFactory(@"Server=.\sqlexpress;Database=MyTestDatabase;Trusted_Connection=True;", SqlServerDialect.Provider);

            sw.Start();
            Console.WriteLine("One using");
            using (var db = dbFactory.OpenDbConnection())
            {
                db.DropAndCreateTable<Product>();
                db.DropAndCreateTable<Product1>();
                db.DropAndCreateTable<Product2>();
                db.DropAndCreateTable<Product3>();
                db.DropAndCreateTable<Product4>();
                db.DropAndCreateTable<Product5>();
                db.DropAndCreateTable<Product6>();
                db.DropAndCreateTable<Product7>();
                db.DropAndCreateTable<Product8>();
                db.DropAndCreateTable<Product9>();
                db.DropAndCreateTable<Product10>();
                db.DropAndCreateTable<Product11>();
                db.DropAndCreateTable<Product12>();
            }

            using (var db = dbFactory.OpenDbConnection())
            {
                for (int i = 1; i < count; i++)
                {
                    db.Insert(new Product() { Id = i, Name = "Name" + i, Description = "Description" + i });
                }
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);


            sw.Reset();
            sw.Start();
            Console.WriteLine("Multiple using");
            using (var db = dbFactory.OpenDbConnection())
            {
                db.DropAndCreateTable<Product>();
            }

            for (int i = 1; i < count; i++)
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    db.Insert(new Product() { Id = i, Name = "Name" + i, Description = "Description" + i });
                }
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);


            sw.Reset();
            sw.Start();
            using (var db = dbFactory.OpenDbConnection())
            {
                db.DropAndCreateTable<Product>();
            }

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 20; i++)
            {
                var thread = new Thread(Test) { Name = "Thread: " + i };
                threads.Add(thread);
            }
            int counter = 1;
            foreach (var thread in threads)
            {
                thread.Start(counter++);
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            sw.Reset();
            sw.Start();
            using (var db = dbFactory.OpenDbConnection())
            {
                db.DropAndCreateTable<Product>();
            }

            threads = new List<Thread>();
            for (int i = 0; i < 20; i++)
            {
                var thread = new Thread(Test2) { Name = "Thread: " + i };
                threads.Add(thread);
            }
            counter = 1;
            foreach (var thread in threads)
            {
                thread.Start(counter++);
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }

        public static void Test(object data)
        {
            var threadName = data.ToString();

            var dbFactory = new OrmLiteConnectionFactory(@"Server=.\sqlexpress;Database=MyTestDatabase;Trusted_Connection=True;", SqlServerDialect.Provider);
            using (var db = dbFactory.OpenDbConnection())
            {
                for (int i = 1; i < count; i++)
                {
                    db.Insert(new Product() { Name = "Name" + i, Description = "Description" + i, Thread = threadName });
                }
            }
        }

        public static void Test2(object data)
        {
            var threadName = data.ToString();

            var dbFactory = new OrmLiteConnectionFactory(@"Server=.\sqlexpress;Database=MyTestDatabase;Trusted_Connection=True;", SqlServerDialect.Provider);
            for (int i = 1; i < count; i++)
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    db.Insert(new Product() { Name = "Name" + i, Description = "Description" + i, Thread = threadName });
                }
            }
        }
    }


    public class Product
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }

    public class Product1
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }

    public class Product2
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }

    public class Product3
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }

    public class Product4
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }


    public class Product5
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }


    public class Product6
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }


    public class Product7
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }


    public class Product8
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }



    public class Product9
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }



    public class Product10
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }



    public class Product11
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }


    public class Product12
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }
}
