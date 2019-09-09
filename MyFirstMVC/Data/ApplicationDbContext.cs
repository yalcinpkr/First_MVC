﻿using MyFirstMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyFirstMVC.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Project>().HasOptional(p => p.Category).WithMany(c => c.Projects).HasForeignKey(p => p.CategoryId).WillCascadeOnDelete(false);
        }
    }
}