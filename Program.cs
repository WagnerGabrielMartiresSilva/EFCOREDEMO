using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCOREDEMO // Note: actual namespace depends on the project name.
{
     class Program
    {
        static void Main(string[] args)
        {
           using (var db = new LivroContext())
            
           {

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                
                db.Livros?.Add(new Livro { Titulo = "Domain-Driven Design: Tackling Complexity in the Heart of Software", Autor = "Eric Evans", AnoPublicacao = 2003 });
                db.Livros?.Add(new Livro { Titulo = "Agile Principles, Patterns, and Practices in C#", Autor = "Robert C. Martin", AnoPublicacao = 2006 });
               
               
                db.SaveChanges();
             
                Console.WriteLine("---------RESULTADO----------------");
                db.Livros?.ForEachAsync(x => Console.WriteLine($"Titulo:{x.Titulo} | Autor:{x.Autor}"));

           }
        }
    }


    
    
    public class LivroContext : DbContext
    {
         public DbSet<Livro>? Livros { get; set; }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
              optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCore.Demo;Trusted_Connection=True;");
                                            
         }
       
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>()
                .ToTable("Livro");

            modelBuilder.Entity<Livro>()
                .HasKey(p => p.LivroId);

            modelBuilder.Entity<Livro>()
                .Property(p => p.Titulo)
                .HasColumnType("varchar(50)");

        }

    }
    public class Livro
    {
        

                public int LivroId {get; set;}

                public string? Titulo {get; set;}

                public string? Autor {get; set;}

                public int AnoPublicacao {get; set;}
                
    }
}
