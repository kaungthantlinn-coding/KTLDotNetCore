using KTLDotNetCore.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace KTLDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _db.TblBlogs.AsNoTracking().Where(TblBlogs => TblBlogs.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            var lst = _db.TblBlogs.AsNoTracking().FirstOrDefault(TblBlogs => TblBlogs.BlogId == id);
            if (lst == null)
            {
                return NotFound();
            }
            return Ok(lst);
        }


        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok(blog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var lst = _db.TblBlogs.FirstOrDefault(TblBlogs => TblBlogs.BlogId == id);
            if (lst == null)
            {
                return NotFound();
            }

            lst.BlogTitle = blog.BlogTitle;
            lst.BlogAuthor = blog.BlogAuthor;
            lst.BlogContent = blog.BlogContent;



            _db.Entry(lst).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(lst);
        }


        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var lst = _db.TblBlogs.FirstOrDefault(TblBlogs => TblBlogs.BlogId == id);
            if (lst == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                lst.BlogTitle = blog.BlogTitle;

            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                lst.BlogAuthor = blog.BlogAuthor;
            }


            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                lst.BlogContent = blog.BlogContent;
            }




            _db.Entry(lst).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(lst);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var lst = _db.TblBlogs.FirstOrDefault(TblBlogs => TblBlogs.BlogId == id);
            if (lst == null)
            {
                return NotFound();
            }
            lst.DeleteFlag = true;
            //_db.Entry(lst).State = EntityState.Deleted;
            _db.SaveChanges();

            return Ok(lst);
        }


    }
}
