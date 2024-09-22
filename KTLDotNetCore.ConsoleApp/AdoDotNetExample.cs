using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTLDotNetCore.ConsoleApp
{
    
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTesting;User ID=sa;Password=sasa@123;";
       
        //Multiple Record
        public void Read()
        {
          

            Console.WriteLine("Hello, World!");
            //Console.ReadLine();
            Console.ReadKey();

            //markdown= md

            //C#  => Database

            //ADO.NET => Database
            //Dapper => Database  ----  ORM
            //EF Core => Database
            //C# => Sql query =>
            //nuget


            //Ctrl+ .


            // Connection string to connect to the database

          

            // Create a new SqlConnection with the connection string
            SqlConnection connection = new SqlConnection(_connectionString);


            Console.WriteLine("Connection opening...");
            connection.Open();  // Open the database connection
            Console.WriteLine("Connection opened.");

            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog]
";


            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                Console.WriteLine(reader["DeleteFlag"]);
            }

            Console.WriteLine("Connection closing...");
            connection.Close();
            Console.WriteLine("Connection closed.");

            //DataSet
            //DataTable
            //DataRow
            //DataColumn


            //foreach(DataRow dr in dt.Rows)
            //{
            //    Console.WriteLine(dr["BlogId"]);
            //    Console.WriteLine(dr["BlogTitle"]);
            //    Console.WriteLine(dr["BlogAuthor"]);
            //    Console.WriteLine(dr["BlogContent"]);
            //    Console.WriteLine(dr["DeleteFlag"]);
            //}


            Console.ReadKey();
        }

       


    }
}
