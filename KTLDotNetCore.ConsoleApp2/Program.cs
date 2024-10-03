using KTLDotNetCore.Database.Models;

namespace KTLDotNetCore.ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext db = new AppDbContext();
            var lst =db.TblBlogs.ToList();
        }
    }
}
