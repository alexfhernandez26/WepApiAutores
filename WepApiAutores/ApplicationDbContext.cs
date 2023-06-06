﻿using Microsoft.EntityFrameworkCore;
using WepApiAutores.Entidades;

namespace WepApiAutores
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        
        public DbSet<Autor> Autors { get; set; }

    }
}
