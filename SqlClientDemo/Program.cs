using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SqlClientDemo
{
    class Program
    {
        static int count = 10000;

        static void Main(string[] args)
        {
            FirstTest();
            SecondTest();
            ThirdTest();
        }

        private static void FirstTest()
        {
            CleanProductTable();

            var sw = new Stopwatch();
            sw.Start();

            using (var con = new SqlConnection())
            {
                con.ConnectionString = @"Server=.\sqlexpress;Database=MyTestDatabase;Trusted_Connection=True;";
                con.Open();

                try
                {
                    for (int i = 1; i < count; i++)
                    {
                        using (
                            var command = new SqlCommand("INSERT INTO Product(Name, Description) VALUES(@Name, @Description)",
                                con))
                        {
                            command.Parameters.Add("Name", "Name" + i);
                            command.Parameters.Add("Description", "Description" + i);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }

        private static void SecondTest()
        {
            CleanProductTable();

            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < count; i++)
            {
                using (var con = new SqlConnection())
                {
                    con.ConnectionString = @"Server=.\sqlexpress;Database=MyTestDatabase;Trusted_Connection=True;";
                    con.Open();

                    try
                    {
                        using (
                            var command =
                                new SqlCommand("INSERT INTO Product(Name, Description) VALUES(@Name, @Description)",
                                    con))
                        {
                            command.Parameters.Add("Name", "Name" + i);
                            command.Parameters.Add("Description", "Description" + i);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }

        private static void ThirdTest()
        {
            CleanProductTable();

            var sw = new Stopwatch();
            sw.Start();

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
        }

        private static void Test(object data)
        {
            var threadName = data.ToString();

            for (int i = 0; i < count; i++)
            {
                using (var con = new SqlConnection())
                {
                    con.ConnectionString = @"Server=.\sqlexpress;Database=MyTestDatabase;Trusted_Connection=True;";
                    con.Open();

                    try
                    {
                        using (
                            var command =
                                new SqlCommand("INSERT INTO Product(Name, Description, Thread) VALUES(@Name, @Description, @Thread)",
                                    con))
                        {
                            command.Parameters.Add("Name", "Name" + i);
                            command.Parameters.Add("Description", "Description" + i);
                            command.Parameters.Add("Thread", threadName);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        private static void CleanProductTable()
        {
            using (var con = new SqlConnection())
            {
                con.ConnectionString = @"Server=.\sqlexpress;Database=MyTestDatabase;Trusted_Connection=True;";
                con.Open();

                try
                {
                    using (var command = new SqlCommand("truncate table Product", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Thread { get; set; }
    }
}
