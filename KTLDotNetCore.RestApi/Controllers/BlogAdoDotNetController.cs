﻿using KTLDotNetCore.Database.Models;
using KTLDotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KTLDotNetCore.RestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTesting;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        [HttpGet]
        public IActionResult GetBlogs()
        {
           List <BlogViewModel> lst = new List<BlogViewModel>();
             
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
               
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["BlogId"]);
                    Console.WriteLine(reader["BlogTitle"]);
                    Console.WriteLine(reader["BlogAuthor"]);
                    Console.WriteLine(reader["BlogContent"]);
                lst.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                }); 
                
                }

               
                connection.Close();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
     ,         [BlogTitle]
     ,         [BlogAuthor]
     ,         [BlogContent]
     ,         [DeleteFlag]
     FROM [dbo].[Tbl_Blog] Where BlogId =@BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);

            }
            connection.Close();

            return Ok(id==0? "Not Found": "Found");
        }


        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog] (BlogTitle, BlogAuthor, BlogContent, DeleteFlag) 
                                     VALUES (@BlogTitle, @BlogAuthor, @BlogContent, @DeleteFlag)";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
            }


            return Ok(blog==null? "Not Found": "Found");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                             SET BlogTitle = @BlogTitle, 
                                 BlogAuthor = @BlogAuthor, 
                                 BlogContent = @BlogContent, 
                                 DeleteFlag = @DeleteFlag
                             WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            cmd.Parameters.AddWithValue("@DeleteFlag", blog.DeleteFlag);

            int result = cmd.ExecuteNonQuery();



            return Ok(result>0? "Updated": "Not Updated");
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {

            string conditions = "";
            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += "BlogTitle = @BlogTitle,";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += "BlogAuthor = @BlogAuthor,";
            }

            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += "BlogContent = @BlogContent,";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters");
            }

            conditions = conditions.Substring(0, conditions.Length - 1);
            SqlConnection connection = new SqlConnection(_connectionString);


            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
  SET {conditions} WHERE BlogId = @BlogId;";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            }

            if (!string.IsNullOrEmpty(blog.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            }

            if (!string.IsNullOrEmpty(blog.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            }


            int result = cmd.ExecuteNonQuery();

            return Ok(result > 0 ? "Updating Successful" : "Updating Failed");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@BlogId", id);

            int result = cmd.ExecuteNonQuery();




            return Ok(result > 0 ? "Deleted" : "Not Deleted");
        }
    }
}
