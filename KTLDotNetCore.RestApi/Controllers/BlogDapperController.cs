using Dapper;
using KTLDotNetCore.Database.Models;
using KTLDotNetCore.RestApi.DataModels;
using KTLDotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace KTLDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTesting;User ID=sa;Password=sasa@123;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"
SELECT 
    [BlogId] AS Id,
    [BlogTitle] AS Title,
    [BlogAuthor] AS Author,
    [BlogContent] AS Content
FROM [dbo].[Tbl_Blog] 
WHERE DeleteFlag = 0";
                List<BlogViewModel> lst = db.Query<BlogViewModel>(query).ToList();


                return Ok(lst);
            }

        }

        [HttpGet("{ id}")]
        public IActionResult GetBlogs(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"
SELECT 
    [BlogId] AS Id,
    [BlogTitle] AS Title,
    [BlogAuthor] AS Author,
    [BlogContent] AS Content
FROM [dbo].[Tbl_Blog] 
WHERE DeleteFlag = 0";

                var lst = db.Query<BlogViewModel>(query, new BlogViewModel
                {
                    Id = id
                }).FirstOrDefault();

                if (lst is null)
                {
                    Console.WriteLine("Record Not Found");

                }

                Console.WriteLine(lst.Id);
                Console.WriteLine(lst.Title);
                Console.WriteLine(lst.Author);
                Console.WriteLine(lst.Content);

                return Ok(lst);
            }
        }
    


        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"INSERT INTO [dbo].[Tbl_Blog]
         ([BlogTitle]
         ,[BlogAuthor]
         ,[BlogContent]
         ,[DeleteFlag])
   VALUES
         (@BlogTitle
         ,@BlogAuthor
         ,@BlogContent
         ,0)";


                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = blog.BlogTitle,
                    BlogAuthor = blog.BlogAuthor,
                    BlogContent = blog.BlogContent

                });

                   return Ok(result > 0 ? "Added" : "Not Added");
                }

            }
            
        

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @Title
                    ,[BlogAuthor] = @Author
                    ,[BlogContent] = @Content
                    ,[DeleteFlag] = 0
                    WHERE BlogId= @Id";
                int result = db.Execute(query, new BlogViewModel
                {
                    Id = id,
                    Title = blog.Title,
                    Author = blog.Author,
                    Content = blog.Content,
                });
                return Ok(result == 1 ? "Updating Successful." : "Updating Fail");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                if (!string.IsNullOrEmpty(blog.Title))
                {
                    conditions += " [BlogTitle] = @Title, ";
                }
                if (!string.IsNullOrEmpty(blog.Author))
                {
                    conditions += " [BlogAuthor] = @Author, ";
                }
                if (!string.IsNullOrEmpty(blog.Content))
                {
                    conditions += " [BlogContent] = @Content, ";
                }
                if (conditions.Length == 0)
                {
                    BadRequest("Invalid Parameter");
                }
                conditions = conditions.Substring(0, conditions.Length - 1);
                string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET {conditions}
                    ,[DeleteFlag] = 0
                    WHERE BlogId= @Id";
int result = db.Execute(query, new BlogViewModel
{
    Id = id,
    Title = blog.Title,
    Author = blog.Author,
    Content = blog.Content,

});
                return Ok(result == 1 ? " Updating Successful." : "Updating Failed");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                 string query = $@"UPDATE [dbo].[Tbl_Blog] SET DeleteFlag = 1 WHERE BlogId = @BlogId";

                int result = db.Execute(query, new BlogViewModel
                {
                    Id = id
                });


                return Ok(result > 0 ? "Deleted" : "Not Deleted");
            }
            
        }
    }
}
