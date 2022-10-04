using Microsoft.EntityFrameworkCore;
using Test1.Models;

namespace Test1.Data
{
    public class ContactsAPI_DbContext : DbContext
    {
        public ContactsAPI_DbContext(DbContextOptions options) : base(options)
        {
        }

        //acts for tables
        public DbSet<Contact> Contacts { get; set; }
    }
}
