using Microsoft.EntityFrameworkCore;
using phone_book.Models;

namespace phone_book.Context
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PhoneBook;User Id=sa;Password=S3cureP@ssw0rd2024#;TrustServerCertificate=true");
            }
        }
    }
}