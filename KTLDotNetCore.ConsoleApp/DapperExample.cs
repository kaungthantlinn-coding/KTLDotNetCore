﻿using Dapper;
using KTLDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KTLDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTesting;User ID=sa;Password=sasa@123;";
        private object db;

        public void Read()
        {
            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    string query = "SELECT * FROM Tbl_Blog Where DeleteFlag = 0;";
            //    var lst = db.Query(query).ToList();

            //    foreach (var item in lst)
            //    {
            //        Console.WriteLine(item.BlogId);
            //        Console.WriteLine(item.BlogTitle);
            //        Console.WriteLine(item.BlogAuthor);
            //        Console.WriteLine(item.BlogContent);
            //    }   
            //}

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tbl_Blog Where DeleteFlag = 0;";
                var lst = db.Query<BlogDataModel>(query).ToList();

                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }


        }

        public void Create(string title, string author, string content)
        {
            string queryInsert = $@"INSERT INTO [dbo].[Tbl_Blog]
         ([BlogTitle]
         ,[BlogAuthor]
         ,[BlogContent]
         ,[DeleteFlag])
   VALUES
         (@BlogTitle
         ,@BlogAuthor
         ,@BlogContent
         ,0)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(queryInsert, new
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                Console.WriteLine(result == 1 ? "Record Inserted" : "Record Not Inserted");
            }
        }

        public void Update(int id, string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
  SET [BlogTitle] = @BlogTitle
     ,[BlogAuthor] = @BlogAuthor
     ,[BlogContent] =@BlogContent
     ,[DeleteFlag] =0
WHERE BlogId = @BlogId;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new
                {
                    BlogId = id,
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });

                Console.WriteLine(result == 1 ? "Record Updated" : "Record Not Updated");
            }
        }

        public void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog] Where BlogId =@BlogId;";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new
                {
                    BlogId = id
                });

                Console.WriteLine(result == 1 ? "Record Deleted" : "Record Not Deleted");
            }
        }

        public void Edit(int id)
        {
          using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * From Tbl_Blog Where DeleteFlag = 0 and BlogId = @BlogId;";

                var lst = db.Query<BlogDataModel>(query, new BlogDataModel
                {
                    BlogId = id
                }).FirstOrDefault();

                if (lst is null)
                {
                  Console.WriteLine("Record Not Found");
                    return;
                }

                Console.WriteLine(lst.BlogId);
                Console.WriteLine(lst.BlogTitle);
                Console.WriteLine(lst.BlogAuthor);
                Console.WriteLine(lst.BlogContent);
                }
            }
        }
    }


