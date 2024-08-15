﻿using HotelReservationMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ReservationService> ReservationServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationService>()
                .HasKey(rs => new { rs.ReservationId, rs.ServiceId });

            // Diğer model yapılandırmaları...
        }


    }
}
