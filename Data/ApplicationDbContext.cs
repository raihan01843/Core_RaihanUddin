using System;
using System.Collections.Generic;
using System.Text;
using CoreProject_Raihan.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreProject_Raihan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Auther> Authers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Trainee> Trainees { get; set; }

    }
}
