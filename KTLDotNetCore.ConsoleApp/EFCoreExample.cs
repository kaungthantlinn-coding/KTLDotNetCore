using KTLDotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTLDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where(x => x.DeleteFlag == false).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
                
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result = db.SaveChanges();

            Console.WriteLine(result == 1 ? "Record Inserted" : "Record Not Inserted");
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();

            var lst = db.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
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

        public void Update(int id, string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();

            var lst = db.Blogs.AsNoTracking().Where(x => x.BlogId == id).FirstOrDefault();
            if (lst is null)
            {
                Console.WriteLine("Record Not Found");
                return;
            }
            if (string.IsNullOrEmpty(title))
            {
                lst.BlogTitle = title;
            }

            if (string.IsNullOrEmpty(author)) {
                lst.BlogAuthor = author;
            }

           if (string.IsNullOrEmpty(content)) {
                lst.BlogContent = content;
           }

            db.Entry(lst).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result==1 ? "Record Updated" : "Record Not Updated");
            
           
        }

        
    }
}
