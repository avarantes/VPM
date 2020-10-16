using Microsoft.EntityFrameworkCore;
using System;
using VPM.Models;

namespace VPM.Data
{
    public class VPMDBContext : DbContext
    {
        public VPMDBContext(DbContextOptions<VPMDBContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Task> Task { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "Sample Costumer A", Address = "Sample Address A", VatNumber = "500500500", ZipCode = "4470" },
                new Customer { CustomerId = 2, Name = "Sample Costumer B", Address = "Sample Address B", VatNumber = "500500501", ZipCode = "4471" },
                new Customer { CustomerId = 3, Name = "Sample Costumer C", Address = "Sample Address C", VatNumber = "500500502", ZipCode = "4472" },
                new Customer { CustomerId = 4, Name = "Sample Costumer D", Address = "Sample Address D", VatNumber = "500500503", ZipCode = "4473" },
                new Customer { CustomerId = 5, Name = "Sample Costumer E", Address = "Sample Address E", VatNumber = "500500504", ZipCode = "4474" }
                );

            modelBuilder.Entity<Project>().HasData(
               new Project { ProjectId = 1, CreateDate = DateTime.Now.AddDays(-2), CustomerId = 1, DeliveryDate = DateTime.Now.AddDays(10), Description = "Sample Project Description A1", Title = "Sample project A1" },
               new Project { ProjectId = 2, CreateDate = DateTime.Now.AddDays(-2), CustomerId = 4, DeliveryDate = DateTime.Now.AddDays(50), Description = "Sample Project Description D2", Title = "Sample project D2" },
               new Project { ProjectId = 3, CreateDate = DateTime.Now.AddDays(-10), CustomerId = 2, DeliveryDate = DateTime.Now.AddDays(24), Description = "Sample Project Description B3", Title = "Sample project B3" }
               );

            modelBuilder.Entity<Task>().HasData(
                new Task {TaskId = 1, BillableTime = DateTime.Now.AddMinutes(-50),CostPerHour = Convert.ToDecimal(4.03),CreateDate = DateTime.Now.AddDays(-2),Description= "Task Project 1",ProjectId=1,Title= "Task Project 1",TaskCost= Convert.ToDecimal(95.71) }
                );
        }
    }
}