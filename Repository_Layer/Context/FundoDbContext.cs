using Microsoft.EntityFrameworkCore;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Context
{
    public class FundoDbContext:DbContext
    {
        public FundoDbContext(DbContextOptions options):base(options)
        {

        }
        public  DbSet<UserEntity>UserEntity { get; set; }

        public DbSet<NoteEntity>Note { get; set; }

        public DbSet<LableEntity> Lables { get; set; }

        public DbSet<CollabaratorEntity> Collabarators { get; set; }


    }
}
